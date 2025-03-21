using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using static createLevel;
using static UnityEditor.PlayerSettings;

public class saveCustomLevel : MonoBehaviour
{
    public GameObject levelNameTextInput;
    public GameObject playerMarker;
    public GameObject goalMarker;
    public Tilemap regularBoxTilemap;
    public Tilemap steelBoxTilemap;
    public Tilemap lavaBoxTilemap;
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

        // https://discussions.unity.com/t/count-the-amount-Of-a-certain-tile-in-a-tilemap/228363/5
        int createObjectAtPositionTilemap(Tilemap tilemap, string name)
        {
            tilemap.CompressBounds(); // To only read the tiles that we have painted
            int amount = 0;
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
            return amount;
        }

        // https://discussions.unity.com/t/count-the-amount-Of-a-certain-tile-in-a-tilemap/228363/5
        int createObjectAtPositionObject(GameObject group, string name)
        {
            int amount = group.transform.childCount;
            for (int counter = 0; counter < amount; counter++)
            {
                Vector2 objectPos = group.transform.GetChild(counter).position;
                GridPosition gridPos = new GridPosition((int)objectPos.x, (int)objectPos.y);
                levelData[levelDataIndex] = new ObjectInformation(name, gridPos);
                levelDataIndex += 1;
            }
            return amount;
        }

        // Save level to json file
        string levelName = levelNameTextInput.GetComponent<TMP_InputField>().text;

        print("Attempting to save level " + levelName + "...");

        // Get player position
        GridPosition playerPosition = new GridPosition((int)playerMarker.transform.position.x, (int)playerMarker.transform.position.y);

        // Count amount Of objects placed
        int amountOfRegularBoxes = getAmountOfTiles(regularBoxTilemap);
        int amountOfSteelBoxes = getAmountOfTiles(steelBoxTilemap);
        int amountOfLavaBoxes = getAmountOfTiles(lavaBoxTilemap);
        int amountOfPowerUps = powerUpGroup.transform.childCount;
        int amountOfGreenKeyDoors = greenKeyDoorGroup.transform.childCount;
        int amountOfRedKeyDoors = redKeyDoorGroup.transform.childCount;
        int amountOfBlueKeyDoors = blueKeyDoorGroup.transform.childCount;
        int amountOfGreenKeys = greenKeyGroup.transform.childCount;
        int amountOfRedKeys = redKeyGroup.transform.childCount;
        int amountOfBlueKeys = blueKeyGroup.transform.childCount;
        int totalObjectCount = 1 + amountOfRegularBoxes + amountOfSteelBoxes
            + amountOfLavaBoxes + amountOfPowerUps + amountOfGreenKeyDoors + amountOfRedKeyDoors + amountOfBlueKeyDoors
            + amountOfGreenKeys + amountOfRedKeys + amountOfBlueKeys;

        // Create objects
        levelData = new ObjectInformation[totalObjectCount];
        GridPosition goalGridPos = new GridPosition((int)goalMarker.transform.position.x, (int)goalMarker.transform.position.y);
        levelData[levelDataIndex] = new ObjectInformation("goal", goalGridPos);
        levelDataIndex += 1;
        for (int counter = 0; counter < amountOfRegularBoxes; counter++)
        {
            createObjectAtPositionTilemap(regularBoxTilemap, "regularBox");
        }
        for (int counter = 0; counter < amountOfSteelBoxes; counter++)
        {
            createObjectAtPositionTilemap(steelBoxTilemap, "steelBox");
        }
        for (int counter = 0; counter < amountOfLavaBoxes; counter++)
        {
            createObjectAtPositionTilemap(lavaBoxTilemap, "lavaBox");
        }
        for (int counter = 0; counter < amountOfPowerUps; counter++)
        {
            createObjectAtPositionObject(powerUpGroup, "powerUp");
        }
        for (int counter = 0; counter < amountOfGreenKeyDoors; counter++)
        {
            createObjectAtPositionObject(greenKeyDoorGroup, "greenKeyDoor");
        }
        for (int counter = 0; counter < amountOfRedKeyDoors; counter++)
        {
            createObjectAtPositionObject(redKeyDoorGroup, "redKeyDoor");
        }
        for (int counter = 0; counter < amountOfBlueKeyDoors; counter++)
        {
            createObjectAtPositionObject(blueKeyDoorGroup, "blueKeyDoor");
        }
        for (int counter = 0; counter < amountOfGreenKeys; counter++)
        {
            createObjectAtPositionObject(greenKeyGroup, "greenKey");
        }
        for (int counter = 0; counter < amountOfRedKeys; counter++)
        {
            createObjectAtPositionObject(redKeyGroup, "redKey");
        }
        for (int counter = 0; counter < amountOfBlueKeys; counter++)
        {
            createObjectAtPositionObject(blueKeyGroup, "blueKey");
        }

        // Save level to new json file
        Level level = new Level(levelName, "old", playerPosition, levelData);
        string levelJson = JsonUtility.ToJson(level);
        string filePath = Application.dataPath + "/customLevel.json";
        System.IO.File.WriteAllText(filePath, levelJson);
        print("File saved to " + filePath);

        // Reset level values
        levelData = new ObjectInformation[0];
        levelDataIndex = 0;
    }
}
