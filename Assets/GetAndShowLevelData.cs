using System.IO;
using UnityEngine;
using static createLevel;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetAndShowLevelData : MonoBehaviour
{
    public int customLevelID;
    public TMP_Text levelNameText;
    public TMP_Text levelInfoText;
    public Image levelPreviewImage;
    public Button playButton;
    public Sprite unknownThumbnail;
    private loadingLevelData loadingLevelData;

    /// <summary>
    /// Changes the information of the screen to show a certain custom level
    /// </summary>
    /// <param name="levelID">The level ID of the custom level</param>
    private void ChangeLevelData(int levelID)
    {
        // Get the custom level file from the custom level directory
        string levelPath = Application.persistentDataPath + "/customLevels/" + levelID;
        string levelFilePath = levelPath + ".json";
        string levelFile = File.ReadAllText(levelFilePath);
        Level levelInfo = JsonUtility.FromJson<Level>(levelFile);

        // Change the level name text
        levelNameText.text = levelInfo.levelName;

        // Change the level info text
        levelInfoText.text =
            "Player Count:\n" + CheckPlayerCountOfLevel(levelInfo.playerStartPositions) + "\n"
            + "Level Zoom:\n" + levelInfo.bounds + "\n"
            + "Date Created:\n" + File.GetCreationTime(levelFilePath) + "\n"
            + "Date Modified:\n" + File.GetLastWriteTime(levelFilePath) + "\n"
            + "Game Version:\n" + levelInfo.versionCreated + "\n";

        // Get the custom level thumbnail
        string thumbnailPath = levelPath + ".png";
        Sprite thumbnail;
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
        // Load the thumbnail
        levelPreviewImage.sprite = thumbnail;

        // Make the play button send the player to the custom level
        playButton.onClick.AddListener(() =>
        {
            // Go to the specific level
            loadingLevelData.gamemode = "customLevels";
            loadingLevelData.levelID = levelID;
            SceneManager.LoadScene("GameScene");
        });
    }

    private void OnEnable()
    {
        ChangeLevelData(customLevelID);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the loading level data
        loadingLevelData = GameObject.FindGameObjectWithTag("LoadLevelInfo").GetComponent<loadingLevelData>();
    }
}
