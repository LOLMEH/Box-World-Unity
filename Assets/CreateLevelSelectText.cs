using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static createLevel;

public class CreateLevelSelectText : MonoBehaviour
{
    public string baseTextCloneTagName;
    public TMP_Text baseTextButton;
    public GameObject scrollContent;
    private loadingLevelData loadingLevelData;

    /// <summary>
    /// Generates a list of clickable level text buttons
    /// </summary>
    /// <param name="gamemode">The name of level list to use</param>
    public void GenerateLevelSelect(string gamemode)
    {
        // Find and remove all previous text buttons
        GameObject[] oldTextButtons = GameObject.FindGameObjectsWithTag(baseTextCloneTagName);
        for (int counter = 0; counter < oldTextButtons.Length; counter++)
        {
            Destroy(oldTextButtons[counter]);
        }

        // Get the level folder path
        string levelFolderPath = "levels/";
        bool customLevelSelected = false;
        if (gamemode == "customLevels")
        {
            // Special switch for custom levels
            levelFolderPath = "/";
            customLevelSelected = true;
        }

        // Create a text button for each level
        int levelID = 1;
        while (true)
        {
            // Save the level id so it does not get overridden by the next level ids
            int newLevelID = levelID;

            // Get level from json
            Level levelInfo;
            try
            {
                // Get the next level file
                string levelFilePath = levelFolderPath + gamemode + "/" + newLevelID;
                string levelFile;
                if (customLevelSelected == false)
                {
                    // Regular levels
                    levelFile = Resources.Load<TextAsset>(levelFilePath).ToString();
                }
                else
                {
                    // Custom levels
                    levelFilePath = Application.persistentDataPath + levelFilePath + ".json";
                    levelFile = File.ReadAllText(levelFilePath);
                }

                // Get level's json file
                levelInfo = JsonUtility.FromJson<Level>(levelFile);
            }
            catch (NullReferenceException)
            {
                // Break the loop if there are no more files in the directory
                break;
            }
            catch (FileNotFoundException)
            {
                // Break the loop if there are no more files in the custom directory
                break;
            }

            // Create the text button
            TMP_Text newTextButton = Instantiate(baseTextButton);
            newTextButton.text = levelInfo.levelName;
            newTextButton.gameObject.tag = baseTextCloneTagName;
            // Enable the text button
            newTextButton.gameObject.SetActive(true);
            // Move the text button into the scroll viewport GUI
            Transform textTransform = newTextButton.transform;
            textTransform.SetParent(scrollContent.transform, false);

            // Create the click event that loads the player into a specific level
            EventTrigger trigger = newTextButton.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new()
            {
                eventID = EventTriggerType.PointerClick
            };
            entry.callback.AddListener((data) => {
                // Go to the specific level
                loadingLevelData.gamemode = gamemode;
                loadingLevelData.levelID = newLevelID;
                SceneManager.LoadScene("GameScene");
            });
            trigger.triggers.Add(entry);

            // Increase level ID
            levelID++;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the loading level data
        loadingLevelData = GameObject.FindGameObjectWithTag("LoadLevelInfo").GetComponent<loadingLevelData>();

        // Make the level select list for singleplayer
        GenerateLevelSelect("singleplayer");
    }
}
