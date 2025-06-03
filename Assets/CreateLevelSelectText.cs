using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static createLevel;

public class CreateLevelSelectText : MonoBehaviour
{
    public string baseTextCloneTagName;
    public int levelTextIndex;
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
            levelTransform.GetChild(levelTextIndex).GetComponent<TMP_Text>().text = levelInfo.levelName;

            // Change the thumbnail on the button
            Sprite thumbnail;
            if (customLevelSelected == true)
            {
                // Custom level thumbnails (loads a custom image)
                string levelFilePath = levelFolderPath + gamemode + "/" + newLevelID;
                string thumbnailPath = Application.persistentDataPath + levelFilePath + ".png";

                if (File.Exists(thumbnailPath))
                {
                    // Load the custom level thumbnail texture from the file path
                    byte[] thumbnilTextureData = File.ReadAllBytes(thumbnailPath);
                    Texture2D thumbnilTexture = new Texture2D(350, 150);
                    thumbnilTexture.LoadImage(thumbnilTextureData);

                    // Turn the texture into a sprite
                    Rect rect = new Rect(0, 0, thumbnilTexture.width, thumbnilTexture.height);
                    thumbnail = Sprite.Create(thumbnilTexture, rect, new Vector2(0.5f, 0.5f), 100f);
                }
                else
                {
                    thumbnail = unknownThumbnail;
                }
            }
            else
            {
                // Regular level thumbnails (based off of player count)
                int playerCount = CheckPlayerCountOfLevel(levelInfo.playerStartPositions);
                switch (playerCount)
                {
                    case 1:
                        thumbnail = onePlayerThumbnail;
                        break;
                    case 2:
                        thumbnail = twoPlayerThumbnail;
                        break;
                    case 3:
                        thumbnail = threePlayerThumbnail;
                        break;
                    case 4:
                        thumbnail = fourPlayerThumbnail;
                        break;
                    default:
                        thumbnail = unknownThumbnail;
                        break;
                }
            }
            levelTransform.GetChild(levelTextIndex - 1).GetComponent<Image>().sprite = thumbnail;

            // Enable the level button
            newLevelButton.SetActive(true);

            // Create the click event that loads the player into a specific level
            EventTrigger trigger = newLevelButton.GetComponent<EventTrigger>();
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
