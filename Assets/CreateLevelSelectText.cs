using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static createLevel;

public class CreateLevelSelectText : MonoBehaviour
{
    public string baseTextCloneTagName;
    public GameObject levelSelectScreen;
    public GameObject baseLevelPreviewContent;
    public GameObject scrollContent;
    public Sprite unknownThumbnail;
    public Sprite onePlayerThumbnail;
    public Sprite twoPlayerThumbnail;
    public Sprite threePlayerThumbnail;
    public Sprite fourPlayerThumbnail;
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

            // Create the level button
            GameObject newLevelButton = Instantiate(baseLevelPreviewContent);
            newLevelButton.tag = baseTextCloneTagName;

            // Move the level button into the scroll viewport GUI
            Transform levelTransform = newLevelButton.transform;
            levelTransform.SetParent(scrollContent.transform, false);

            // Change the text on the button
            levelTransform.GetChild(1).GetComponent<TMP_Text>().text = levelInfo.levelName;

            // Get the level thumbnail
            Sprite thumbnail;
            if (customLevelSelected == true)
            {
                // Custom level thumbnails (loads a custom image from the custom level path)
                string levelFilePath = levelFolderPath + gamemode + "/" + newLevelID;
                string thumbnailPath = Application.persistentDataPath + levelFilePath + ".png";
                if (File.Exists(thumbnailPath))
                {
                    // Load the custom level thumbnail texture from the file path
                    byte[] thumbnailTextureData = File.ReadAllBytes(thumbnailPath);
                    Texture2D thumbnailTexture = new Texture2D(350, 150);
                    thumbnailTexture.LoadImage(thumbnailTextureData);

                    // Turn the texture into a sprite
                    Rect rect = new Rect(0, 0, thumbnailTexture.width, thumbnailTexture.height);
                    thumbnail = Sprite.Create(thumbnailTexture, rect, new Vector2(0.5f, 0.5f), 100f);
                }
                else
                {
                    // If the thumbnail does not exist load the default one
                    thumbnail = unknownThumbnail;
                }
            }
            else
            {
                // Main level thumbnails (loads an image from resources)
                string levelFilePath = levelFolderPath + gamemode + "/" + newLevelID;

                // Load the main level thumbnail texture from the file path
                thumbnail = Resources.Load<Sprite>(levelFilePath);
            }
            // Change the level thumbnail
            levelTransform.GetChild(0).GetComponent<Image>().sprite = thumbnail;

            // Enable the level button
            newLevelButton.SetActive(true);

            // Create the click event that loads the player into a specific level
            EventTrigger trigger = newLevelButton.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new()
            {
                eventID = EventTriggerType.PointerClick
            };
            if (customLevelSelected == true)
            {
                entry.callback.AddListener((data) =>
                {
                    // Go to the custom level info screen for the specific custom level
                    gameObject.SetActive(false);
                    levelSelectScreen.GetComponent<GetAndShowLevelData>().customLevelID = newLevelID;
                    levelSelectScreen.SetActive(true);
                });
            }
            else
            {
                entry.callback.AddListener((data) =>
                {
                    // Go to the specific level
                    loadingLevelData.gamemode = gamemode;
                    loadingLevelData.levelID = newLevelID;
                    SceneManager.LoadScene("GameScene");
                });
            }
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
