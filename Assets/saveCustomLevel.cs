using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using static createLevel;

public class saveCustomLevel : MonoBehaviour
{
    public GameObject levelNameTextInput;
    public changeGameBounds levelSettings;
    public moveCreateObject createObject;
    private ObjectInformation[] levelData;
    private int levelDataIndex;

    void Start()
    {
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
                Vector2 objectPos = group.transform.GetChild(counter).position;
                GridPosition gridPos = new GridPosition((int)objectPos.x, (int)objectPos.y);
                levelData[levelDataIndex] = new ObjectInformation(name, gridPos);
                levelDataIndex += 1;
            }
        }

        // Save level to json file
        string levelName = levelNameTextInput.GetComponent<TMP_InputField>().text;

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
        GridPosition goalGridPos = new GridPosition((int)goalMarker.transform.position.x, (int)goalMarker.transform.position.y);
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
        GridPosition playerPosition = new GridPosition((int)playerMarker.transform.position.x, (int)playerMarker.transform.position.y);
        GridPosition playerTwoPosition = new GridPosition((int)playerTwoMarker.transform.position.x, (int)playerTwoMarker.transform.position.y);
        GridPosition playerThreePosition = new GridPosition((int)playerThreeMarker.transform.position.x, (int)playerThreeMarker.transform.position.y);
        GridPosition playerFourPosition = new GridPosition((int)playerFourMarker.transform.position.x, (int)playerFourMarker.transform.position.y);


        // Add player positions depending on how many players there are
        GridPosition[] playerStartPositions = {
            new GridPosition(-99, -99),
            new GridPosition(-99, -99),
            new GridPosition(-99, -99),
            new GridPosition(-99, -99)
        };

        // Player 1 position
        playerStartPositions[0] = playerPosition;
        if (playerCount > 1)
        {
            // Player 2 position
            playerStartPositions[1] = playerTwoPosition;
        }
        if (playerCount > 2)
        {
            // Player 3 position
            playerStartPositions[2] = playerThreePosition;
        }
        if (playerCount > 3)
        {
            // Player 4 position
            playerStartPositions[3] = playerFourPosition;
        }

        // Save level to new json file
        Level level = new Level(levelName, levelBounds, playerStartPositions, levelData);
        string levelJson = JsonUtility.ToJson(level);
        string filePath = Application.dataPath + "/customLevel.json";
        System.IO.File.WriteAllText(filePath, levelJson);
        print("File saved to " + filePath);

        // Reset level values
        levelData = new ObjectInformation[0];
        levelDataIndex = 0;
    }
}
