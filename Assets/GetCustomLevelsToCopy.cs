using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static createLevel;

public class GetCustomLevelsToCopy : MonoBehaviour
{
    public string mode;
    public string baseTextCloneTagName;
    public moveCreateObject createObject;
    public togglePauseGUI togglePauseGUI;
    public saveCustomLevel customLevel;
    public GameObject baseLevelPreviewContent;
    public GameObject scrollContent;
    public Sprite unknownThumbnail;

    void OnEnable()
    {
        // Find and remove all previous text buttons
        GameObject[] oldTextButtons = GameObject.FindGameObjectsWithTag(baseTextCloneTagName);
        for (int counter = 0; counter < oldTextButtons.Length; counter++)
        {
            Destroy(oldTextButtons[counter]);
        }

        // Get the custom level folder path
        string levelFolderPath = "/";
        string gamemode = "customLevels";

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
                string levelFileCheckPath = levelFolderPath + gamemode + "/" + newLevelID;
                string levelFile;
                levelFileCheckPath = Application.persistentDataPath + levelFileCheckPath + ".json";
                levelFile = File.ReadAllText(levelFileCheckPath);

                // Get level's json file
                levelInfo = JsonUtility.FromJson<Level>(levelFile);
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

            // Get the custom level thumbnail (loads a custom image from the custom level path)
            Sprite thumbnail;
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
            if (mode == "copy")
            {
                entry.callback.AddListener((data) =>
                {
                    // Copy the custom level's data into the level editor
                    ObjectInformation[] levelBoxes = levelInfo.levelData;

                    // Move the players
                    createObject.PlaceObject(
                        levelInfo.playerStartPositions[0].x, levelInfo.playerStartPositions[0].y, true, "player"
                    );
                    createObject.PlaceObject(
                        levelInfo.playerStartPositions[1].x, levelInfo.playerStartPositions[1].y, true, "player2"
                    );
                    createObject.PlaceObject(
                        levelInfo.playerStartPositions[2].x, levelInfo.playerStartPositions[2].y, true, "player3"
                    );
                    createObject.PlaceObject(
                        levelInfo.playerStartPositions[3].x, levelInfo.playerStartPositions[3].y, true, "player4"
                    );

                    // Place level objects
                    for (int counter = 0; counter < levelBoxes.Length; counter++)
                    {
                        // Get object information
                        String objectName = levelBoxes[counter].type;
                        GridPosition objectPosition = levelBoxes[counter].position;

                        // Place object
                        createObject.PlaceObject(objectPosition.x, objectPosition.y, true, objectName);
                    }

                    // Go back to the pause menu
                    togglePauseGUI.TogglePauseGUI();
                });
            }
            else if (mode == "save")
            {
                entry.callback.AddListener((data) =>
                {
                    // Save the custom level on the selected level ID
                    customLevel.OnClick(newLevelID);
                });
            }
            trigger.triggers.Add(entry);

            // Increase level ID
            levelID++;
        }
    }
}
