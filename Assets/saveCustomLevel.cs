using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using static createLevel;

public class saveCustomLevel : MonoBehaviour
{
    public int customLevelLimit;
    public GameObject pauseGUI;
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

    public void OnClick()
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
                    GridPosition gridPos = new GridPosition(pos.x, pos.y);
                    levelData[levelDataIndex] = new ObjectInformation(name, gridPos);
                    levelDataIndex += 1;
                }
            }
        }

        // https://discussions.unity.com/t/count-the-amount-Of-a-certain-tile-in-a-tilemap/228363/5
        void createObjectAtPositionObject(GameObject group, string name)
        {
            int amount = group.transform.childCount;
            for (int counter = 0; counter < amount; counter++)
            {
                // Since object positions are double what they should be, convert them to the tile position
                Vector2 objectPos = group.transform.GetChild(counter).position;
                int objectPosX = (int)objectPos.x / 2;
                int objectPosY = (int)objectPos.y / 2;
                GridPosition gridPos = new GridPosition(objectPosX, objectPosY);
                levelData[levelDataIndex] = new ObjectInformation(name, gridPos);
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

        print("Attempting to save " + playerCount + " player level " + levelName + " with " + levelBounds + " bounds...");

        // Get markers for the objects
        GameObject playerMarker = createObject.playerMarker;
        GameObject goalMarker = createObject.goalMarker;
        Tilemap regularBoxTilemap = createObject.regularBoxTilemap;
        Tilemap steelBoxTilemap = createObject.steelBoxTilemap;
        Tilemap lavaBoxTilemap = createObject.lavaBoxTilemap;
        GameObject moveBoxGroup = createObject.moveBoxGroup;
        GameObject powerUpGroup = createObject.powerUpGroup;
        GameObject greenKeyDoorGroup = createObject.greenKeyDoorGroup;
        GameObject blueKeyDoorGroup = createObject.blueKeyDoorGroup;
        GameObject redKeyDoorGroup = createObject.redKeyDoorGroup;
        GameObject greenKeyGroup = createObject.greenKeyGroup;
        GameObject blueKeyGroup = createObject.blueKeyGroup;
        GameObject redKeyGroup = createObject.redKeyGroup;
        GameObject playerTwoMarker = createObject.playerTwoMarker;
        GameObject playerThreeMarker = createObject.playerThreeMarker;
        GameObject playerFourMarker = createObject.playerFourMarker;
        GameObject throwBoxGroup = createObject.throwBoxGroup;
        GameObject throwBoxButtonGroup = createObject.throwBoxButtonGroup;
        Tilemap throwBoxTileTilemap = createObject.throwBoxTileTilemap;

        // Count amount Of objects placed (tilemaps)
        int amountOfRegularBoxes = getAmountOfTiles(regularBoxTilemap);
        int amountOfSteelBoxes = getAmountOfTiles(steelBoxTilemap);
        int amountOfLavaBoxes = getAmountOfTiles(lavaBoxTilemap);
        int amountOfThrowBoxTiles = getAmountOfTiles(throwBoxTileTilemap);
        // Count amount Of objects placed (regular objects)
        int amountOfMoveBoxes = moveBoxGroup.transform.childCount;
        int amountOfPowerUps = powerUpGroup.transform.childCount;
        int amountOfGreenKeyDoors = greenKeyDoorGroup.transform.childCount;
        int amountOfRedKeyDoors = redKeyDoorGroup.transform.childCount;
        int amountOfBlueKeyDoors = blueKeyDoorGroup.transform.childCount;
        int amountOfGreenKeys = greenKeyGroup.transform.childCount;
        int amountOfRedKeys = redKeyGroup.transform.childCount;
        int amountOfBlueKeys = blueKeyGroup.transform.childCount;
        int amountOfThrowBoxes = throwBoxGroup.transform.childCount;
        int amountOfThrowBoxButtons = throwBoxButtonGroup.transform.childCount;
        int totalObjectCount = 1 + amountOfRegularBoxes + amountOfSteelBoxes
            + amountOfLavaBoxes + amountOfPowerUps + amountOfGreenKeyDoors + amountOfRedKeyDoors
            + amountOfBlueKeyDoors + amountOfGreenKeys + amountOfRedKeys + amountOfBlueKeys
            + amountOfMoveBoxes + amountOfThrowBoxes + amountOfThrowBoxButtons + amountOfThrowBoxTiles;

        // Create objects in the level data
        levelData = new ObjectInformation[totalObjectCount];
        GridPosition goalGridPos = new GridPosition((int)goalMarker.transform.position.x / 2, (int)goalMarker.transform.position.y / 2);
        levelData[levelDataIndex] = new ObjectInformation("goal", goalGridPos);
        levelDataIndex += 1;
        // Create objects (tilemaps)
        createObjectAtPositionTilemap(regularBoxTilemap, "regularBox");
        createObjectAtPositionTilemap(steelBoxTilemap, "steelBox");
        createObjectAtPositionTilemap(lavaBoxTilemap, "lavaBox");
        createObjectAtPositionTilemap(throwBoxTileTilemap, "throwBoxTile");
        // Create objects (non-tilemaps)
        createObjectAtPositionObject(moveBoxGroup, "moveBox");
        createObjectAtPositionObject(powerUpGroup, "powerUp");
        createObjectAtPositionObject(greenKeyDoorGroup, "greenKeyDoor");
        createObjectAtPositionObject(redKeyDoorGroup, "redKeyDoor");
        createObjectAtPositionObject(blueKeyDoorGroup, "blueKeyDoor");
        createObjectAtPositionObject(greenKeyGroup, "greenKey");
        createObjectAtPositionObject(redKeyGroup, "redKey");
        createObjectAtPositionObject(blueKeyGroup, "blueKey");
        createObjectAtPositionObject(throwBoxGroup, "throwBox");
        createObjectAtPositionObject(throwBoxButtonGroup, "throwBoxButton");

        // Get all of the player positions
        GridPosition playerPosition = new GridPosition((int)playerMarker.transform.position.x / 2, (int)playerMarker.transform.position.y / 2);
        GridPosition playerTwoPosition = new GridPosition((int)playerTwoMarker.transform.position.x / 2, (int)playerTwoMarker.transform.position.y / 2);
        GridPosition playerThreePosition = new GridPosition((int)playerThreeMarker.transform.position.x / 2, (int)playerThreeMarker.transform.position.y / 2);
        GridPosition playerFourPosition = new GridPosition((int)playerFourMarker.transform.position.x / 2, (int)playerFourMarker.transform.position.y / 2);


        // Save player positions depending on how many players there are
        GridPosition[] playerStartPositions = {
            new GridPosition(-99, -99), // Invalid player position
            new GridPosition(-99, -99),
            new GridPosition(-99, -99),
            new GridPosition(-99, -99)
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

        // Find the file path from the next unused level id
        string customLevelFolderPath = Application.persistentDataPath + "/customLevels/";
        if (!Directory.Exists(customLevelFolderPath))
        {
            // If the custom level directory does not exist, create it
            Directory.CreateDirectory(customLevelFolderPath);
        }
        string customLevelExtension = ".json";
        int fileID = 1;
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
        string filePath = customLevelFolderPath + fileID + customLevelExtension;

        // Save level thumbnail
        string thumbnailPath = customLevelFolderPath + fileID + ".png";
        ScreenCapture.CaptureScreenshot(thumbnailPath);

        // Save level to a new json file
        pauseGUI.SetActive(false);
        File.WriteAllText(filePath, levelJson);
        print("File saved to " + filePath);

        // Reset level values
        levelData = new ObjectInformation[0];
        levelDataIndex = 0;
    }
}
