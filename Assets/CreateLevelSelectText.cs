using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static createLevel;

public class CreateLevelSelectText : MonoBehaviour
{
    public int maxTextButtonsOnLine;
    public float textButtonsSpaceDividend;
    public float textButtonsNextLineMultiple;
    public string textCloneTagName;
    public TMP_Text textButton;
    private loadingLevelData loadingLevelData;

    /// <summary>
    /// Generates a list of clickable level text buttons
    /// </summary>
    /// <param name="gamemode">The name of level list to use</param>
    public void GenerateLevelSelect(string gamemode)
    {
        // Find and remove all previous text buttons
        GameObject[] oldTextButtons = GameObject.FindGameObjectsWithTag(textCloneTagName);
        for (int counter = 0; counter < oldTextButtons.Length; counter++)
        {
            Destroy(oldTextButtons[counter]);
        }

        // Get the level folder path
        string levelFolderPath = "levels/";
        if (gamemode == "customLevels")
        {
            // Special folder for custom levels
            levelFolderPath = "";
        }

        // Create a text button for each level
        int levelID = 1;
        while (levelID < 30)
        {
            // Save the level id so it does not get overridden by the next level ids
            int newLevelID = levelID;

            // Get the next level file
            string levelFilePath = levelFolderPath + gamemode + "/" + newLevelID;
            TextAsset levelFile = Resources.Load<TextAsset>(levelFilePath);

            // Get level from json
            Level levelInfo;
            try
            {
                // Get level's json file
                levelInfo = JsonUtility.FromJson<Level>(levelFile.text);
            }
            catch (NullReferenceException)
            {
                // Break the loop if there are no more files in the directory
                break;
            }

            // Create the text button
            TMP_Text newTextButton = Instantiate(textButton);
            newTextButton.text = levelInfo.levelName;
            newTextButton.gameObject.tag = textCloneTagName;
            // Enable the text button
            newTextButton.gameObject.SetActive(true);
            // Move the text button into the GUI
            Transform textTransform = newTextButton.transform;
            textTransform.SetParent(transform, false);
            float newXPos;
            float newYPos;
            if (newLevelID > maxTextButtonsOnLine)
            {
                // Move to a new line if there the first line is full
                newXPos = textTransform.position.x - (textTransform.position.x * textButtonsNextLineMultiple);
                newYPos = textTransform.position.y - (textTransform.position.y / textButtonsSpaceDividend * (newLevelID - maxTextButtonsOnLine - 1));
            }
            else
            {
                newXPos = textTransform.position.x;
                newYPos = textTransform.position.y - (textTransform.position.y / textButtonsSpaceDividend * (newLevelID - 1));
            }
            textTransform.position = new Vector3(newXPos, newYPos, textTransform.position.z);

            // Create the click event that pushes the player to the specific level
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
