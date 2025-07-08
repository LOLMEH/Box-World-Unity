using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using static createLevel;

public class saveCustomLevel : MonoBehaviour
{
    public int customLevelLimit;
    public togglePauseGUI guis;
    public GameObject levelNameTextInput;
    public changeGameBounds levelSettings;
    public moveCreateObject createObject;
    private loadingLevelData loadingLevelData;
    private ObjectInformation[] levelData;
    private int levelDataIndex;

    void Start()
    {
        loadingLevelData = GameObject.FindGameObjectWithTag("LoadLevelInfo").GetComponent<loadingLevelData>();
        levelDataIndex = 0;
    }

    /// <summary>
    /// Saves the custom level into a json file
    /// </summary>
    /// <param name="overrideLevelID">The level ID of the custom level to override</param>
    public void OnClick(int overrideLevelID = -1)
    {
        // https://discussions.unity.com/t/count-the-amount-Of-a-certain-tile-in-a-tilemap/228363/5
        int getAmountOfTiles(Tilemap tilemap)
        {
            tilemap.CompressBounds(); // To only read the tiles that we have painted
            int amount = 0;
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                Tile tile = tilemap.GetTile<Tile>(pos);
                if (tile != null) 
                {
                    amount += 1;
                }
            }
            return amount;
        }

        void createObjectAtPositionTilemap(Tilemap tilemap, string name)
        {
            tilemap.CompressBounds(); // To only read the tiles that we have painted
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                Tile tile = tilemap.GetTile<Tile>(pos);
                if (tile != null)
                {
                    GridPosition gridPos = new(pos.x, pos.y);
                    levelData[levelDataIndex] = new(name, gridPos, 0);
                    levelDataIndex += 1;
                }
            }
        }

        // https://discussions.unity.com/t/count-the-amount-Of-a-certain-tile-in-a-tilemap/228363/5
        void createObjectAtPositionObject(GameObject group)
        {
            int amount = group.transform.childCount;
            for (int counter = 0; counter < amount; counter++)
            {
                // Since object positions are double what they should be, convert them to the tile position
                Transform newObjectTransform = group.transform.GetChild(counter);
                CreateObjectData newObject = newObjectTransform.gameObject.GetComponent<CreateObjectData>();
                Vector2 objectPos = newObjectTransform.position;
                int objectPosX = (int)objectPos.x / 2;
                int objectPosY = (int)objectPos.y / 2;
                GridPosition gridPos = new(objectPosX, objectPosY);
                levelData[levelDataIndex] = new(newObject.objectString, gridPos, newObject.variantId);
                levelDataIndex += 1;
            }
        }

        // Save level to json file
        string levelName = levelNameTextInput.GetComponent<TMP_InputField>().text;

        // Get the version the level was created in
        string versionCreated = loadingLevelData.gameVersion;

        // Get level settings
        string levelBounds = levelSettings.boundString;
        int playerCount = levelSettings.playerCount;

        print("Attempting to save " + playerCount + " player level '" + levelName + "' with " + levelBounds + " bounds...");

        // Get markers for the objects
        GameObject playerMarker = createObject.playerMarker;
        GameObject goalMarker = createObject.goalMarker;
        GameObject playerTwoMarker = createObject.playerTwoMarker;
        GameObject playerThreeMarker = createObject.playerThreeMarker;
        GameObject playerFourMarker = createObject.playerFourMarker;
        Tilemap regularBoxTilemap = createObject.regularBoxTilemap;
        Tilemap steelBoxTilemap = createObject.steelBoxTilemap;
        Tilemap lavaBoxTilemap = createObject.lavaBoxTilemap;
        Tilemap throwBoxTileTilemap = createObject.throwBoxTileTilemap;
        Tilemap iceTilemap = createObject.iceTilemap;
        GameObject objectGroup = createObject.objectGroup;

        // Count amount Of objects placed (tilemaps)
        int amountOfRegularBoxes = getAmountOfTiles(regularBoxTilemap);
        int amountOfSteelBoxes = getAmountOfTiles(steelBoxTilemap);
        int amountOfLavaBoxes = getAmountOfTiles(lavaBoxTilemap);
        int amountOfThrowBoxTiles = getAmountOfTiles(throwBoxTileTilemap);
        int amountOfIceTiles = getAmountOfTiles(iceTilemap);
        int amountOfObjects = objectGroup.transform.childCount;
        // Count amount Of objects placed (regular objects)
        // Add an extra 1 due since the goal is an object
        int totalObjectCount = 1 + amountOfRegularBoxes + amountOfSteelBoxes + amountOfLavaBoxes
            + amountOfThrowBoxTiles + amountOfIceTiles + amountOfObjects;

        // Create objects in the level data
        levelData = new ObjectInformation[totalObjectCount];
        GridPosition goalGridPos = new((int)goalMarker.transform.position.x / 2, (int)goalMarker.transform.position.y / 2);
        levelData[levelDataIndex] = new ObjectInformation("goal", goalGridPos, 0);
        levelDataIndex += 1;
        // Create objects (tilemaps)
        createObjectAtPositionTilemap(regularBoxTilemap, "regularBox");
        createObjectAtPositionTilemap(steelBoxTilemap, "steelBox");
        createObjectAtPositionTilemap(lavaBoxTilemap, "lavaBox");
        createObjectAtPositionTilemap(throwBoxTileTilemap, "throwBoxTile");
        createObjectAtPositionTilemap(iceTilemap, "ice");
        // Create objects (non-tilemaps)
        createObjectAtPositionObject(objectGroup);

        // Get all of the player positions
        GridPosition playerPosition = new((int)playerMarker.transform.position.x / 2, (int)playerMarker.transform.position.y / 2);
        GridPosition playerTwoPosition = new((int)playerTwoMarker.transform.position.x / 2, (int)playerTwoMarker.transform.position.y / 2);
        GridPosition playerThreePosition = new((int)playerThreeMarker.transform.position.x / 2, (int)playerThreeMarker.transform.position.y / 2);
        GridPosition playerFourPosition = new((int)playerFourMarker.transform.position.x / 2, (int)playerFourMarker.transform.position.y / 2);

        // Save player positions depending on how many players there are
        GridPosition[] playerStartPositions = {
            new(-99, -99), // Invalid player position
            new(-99, -99),
            new(-99, -99),
            new(-99, -99)
        };

        // Save player 1 position
        playerStartPositions[0] = playerPosition;
        if (playerCount > 1)
        {
            // Save player 2 position
            playerStartPositions[1] = playerTwoPosition;
        }
        if (playerCount > 2)
        {
            // Save player 3 position
            playerStartPositions[2] = playerThreePosition;
        }
        if (playerCount > 3)
        {
            // Save player 4 position
            playerStartPositions[3] = playerFourPosition;
        }

        // Create the level's json file
        Level level = new (levelName, levelBounds, playerStartPositions, versionCreated, levelData);
        string levelJson = JsonUtility.ToJson(level);

        // Find the file path from the level id
        string customLevelFolderPath = Application.persistentDataPath + "/customLevels/";
        if (!Directory.Exists(customLevelFolderPath))
        {
            // If the custom level directory does not exist, create it
            Directory.CreateDirectory(customLevelFolderPath);
        }
        string customLevelExtension = ".json";
        // Find the level ID
        int fileID = 1;
        if (overrideLevelID == -1)
        {
            // If the level file ID is invalid save the custom level to the next unused ID
            while (fileID < customLevelLimit)
            {
                // Get the next level file
                string checkFilePath = customLevelFolderPath + fileID + customLevelExtension;

                // Check if the next level file does not exist
                bool doesFileExist = File.Exists(checkFilePath);
                if (!doesFileExist)
                {
                    // If the level file does not exist, use the current level ID to save this custom level file
                    break;
                }

                fileID++;
            }
        } else
        {
            fileID = overrideLevelID;
        }
            string filePath = customLevelFolderPath + fileID + customLevelExtension;

        // Save level thumbnail
        guis.pauseGui.SetActive(false);
        guis.saveLevelGui.SetActive(false);
        guis.copyLevelGui.SetActive(false);
        string thumbnailPath = customLevelFolderPath + fileID + ".png";
        ScreenCapture.CaptureScreenshot(thumbnailPath);

        // Save level to a new json file
        File.WriteAllText(filePath, levelJson);
        print("Successfully saved level to " + filePath);

        // Reset level values
        levelData = new ObjectInformation[0];
        levelDataIndex = 0;
    }
}
