using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using static createLevel;

public class saveCustomLevel : MonoBehaviour
{
    public GameObject levelNameTextInput;
    public changeGameBounds levelBoundsInput;
    public GameObject playerMarker;
    public GameObject goalMarker;
    public Tilemap regularBoxTilemap;
    public Tilemap steelBoxTilemap;
    public Tilemap lavaBoxTilemap;
    public GameObject moveBoxGroup;
    public GameObject powerUpGroup;
    public GameObject greenKeyDoorGroup;
    public GameObject blueKeyDoorGroup;
    public GameObject redKeyDoorGroup;
    public GameObject greenKeyGroup;
    public GameObject blueKeyGroup;
    public GameObject redKeyGroup;
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

        // Get level bounds
        string levelBounds = levelBoundsInput.boundString;

        print("Attempting to save level '" + levelName + "' with '" + levelBounds + "' bounds...");

        // Get player position
        GridPosition playerPosition = new GridPosition((int)playerMarker.transform.position.x, (int)playerMarker.transform.position.y);

        // Count amount Of objects placed
        int amountOfRegularBoxes = getAmountOfTiles(regularBoxTilemap);
        int amountOfSteelBoxes = getAmountOfTiles(steelBoxTilemap);
        int amountOfLavaBoxes = getAmountOfTiles(lavaBoxTilemap);
        int amountOfMoveBoxes = moveBoxGroup.transform.childCount;
        int amountOfPowerUps = powerUpGroup.transform.childCount;
        int amountOfGreenKeyDoors = greenKeyDoorGroup.transform.childCount;
        int amountOfRedKeyDoors = redKeyDoorGroup.transform.childCount;
        int amountOfBlueKeyDoors = blueKeyDoorGroup.transform.childCount;
        int amountOfGreenKeys = greenKeyGroup.transform.childCount;
        int amountOfRedKeys = redKeyGroup.transform.childCount;
        int amountOfBlueKeys = blueKeyGroup.transform.childCount;
        int totalObjectCount = 1 + amountOfRegularBoxes + amountOfSteelBoxes
            + amountOfLavaBoxes + amountOfPowerUps + amountOfGreenKeyDoors + amountOfRedKeyDoors + amountOfBlueKeyDoors
            + amountOfGreenKeys + amountOfRedKeys + amountOfBlueKeys + amountOfMoveBoxes;

        // Create objects
        levelData = new ObjectInformation[totalObjectCount];
        GridPosition goalGridPos = new GridPosition((int)goalMarker.transform.position.x, (int)goalMarker.transform.position.y);
        levelData[levelDataIndex] = new ObjectInformation("goal", goalGridPos);
        levelDataIndex += 1;
        // Create objects (tilemaps)
        createObjectAtPositionTilemap(regularBoxTilemap, "regularBox");
        createObjectAtPositionTilemap(steelBoxTilemap, "steelBox");
        createObjectAtPositionTilemap(lavaBoxTilemap, "lavaBox");
        // Create objects (non-tilemaps)
        createObjectAtPositionObject(moveBoxGroup, "moveBox");
        createObjectAtPositionObject(powerUpGroup, "powerUp");
        createObjectAtPositionObject(greenKeyDoorGroup, "greenKeyDoor");
        createObjectAtPositionObject(redKeyDoorGroup, "redKeyDoor");
        createObjectAtPositionObject(blueKeyDoorGroup, "blueKeyDoor");
        createObjectAtPositionObject(greenKeyGroup, "greenKey");
        createObjectAtPositionObject(redKeyGroup, "redKey");
        createObjectAtPositionObject(blueKeyGroup, "blueKey");

        // Save level to new json file
        Level level = new Level(levelName, levelBounds, playerPosition, levelData);
        string levelJson = JsonUtility.ToJson(level);
        string filePath = Application.dataPath + "/customLevel.json";
        System.IO.File.WriteAllText(filePath, levelJson);
        print("File saved to " + filePath);

        // Reset level values
        levelData = new ObjectInformation[0];
        levelDataIndex = 0;
    }
}
