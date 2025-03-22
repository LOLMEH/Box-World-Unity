using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class createLevel : MonoBehaviour
{
    public TextAsset oldBoundsFile;
    public GameObject player;
    public Tilemap regularBoxTilemap;
    public TileBase regularBoxTile;
    public Tilemap steelBoxTilemap;
    public TileBase steelBoxTile;
    public Tilemap lavaBoxTilemap;
    public TileBase lavaBoxTile;
    public Tilemap moveBoxTilemap;
    public TileBase moveBoxTile;
    public GameObject greenKeyDoor;
    public GameObject redKeyDoor;
    public GameObject blueKeyDoor;
    public GameObject greenKey;
    public GameObject redKey;
    public GameObject blueKey;
    public GameObject powerUp;
    public GameObject goal;

    [System.Serializable]
    public class GridPosition
    {
        public int x;
        public int y;

        public GridPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    [System.Serializable]
    public class ObjectInformation
    {
        public String type;
        public GridPosition position;

        public ObjectInformation(string type, GridPosition position)
        {
            this.type = type;
            this.position = position;
        }
    }

    [System.Serializable]
    public class Level
    {
        public String levelName;
        public String bounds;
        public GridPosition playerStartPosition;
        public ObjectInformation[] levelData;

        public Level(string levelName, string bounds, GridPosition playerStartPosition, ObjectInformation[] levelData)
        {
            this.levelName = levelName;
            this.bounds = bounds;
            this.playerStartPosition = playerStartPosition;
            this.levelData = levelData;
        }
    }

    [System.Serializable]
    public class LevelBounds
    {
        public GridPosition[] boundData;

        public LevelBounds(GridPosition[] boundData)
        {
            this.boundData = boundData;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get level json file from the loading level object
        loadingLevelData loadingLevelData = GameObject.FindGameObjectWithTag("LoadLevelInfo").GetComponent<loadingLevelData>();
        string levelFilePath = "Assets/levels/" + loadingLevelData.gamemode + "/" + loadingLevelData.levelID + ".json";
        TextAsset levelFile = AssetDatabase.LoadAssetAtPath<TextAsset>(levelFilePath);
        
        // Get level from json
        Level levelInfo = JsonUtility.FromJson<Level>(levelFile.text);
        ObjectInformation[] levelBoxes = levelInfo.levelData;

        // Move player
        GridPosition playerStartPosition = levelInfo.playerStartPosition;
        Vector2 playerStartPositionVector = new Vector2(levelInfo.playerStartPosition.x, levelInfo.playerStartPosition.y);
        player.transform.position = playerStartPositionVector;

        // Get and generate level boundaries
        if (levelInfo.bounds == "old")
        {
            LevelBounds levelBounds = JsonUtility.FromJson<LevelBounds>(oldBoundsFile.text);
            GridPosition[] levelBoundData = levelBounds.boundData;
            for (int counter = 0; counter < levelBoundData.Length; counter++)
            {
                GridPosition boundPositon = levelBoundData[counter];
                Vector3Int boundPositonVector = new Vector3Int(boundPositon.x, boundPositon.y);
                steelBoxTilemap.SetTile(boundPositonVector, steelBoxTile);
            }
        }
        else
        {
            print("Error: bounds " + levelInfo.bounds + " not found");
        }

        // Generate level objects
        for (int counter = 0; counter < levelBoxes.Length; counter++)
        {
            String objectName = levelBoxes[counter].type;
            GridPosition objectPosition = levelBoxes[counter].position;
            Vector3Int objectPositonVector = new Vector3Int(objectPosition.x, objectPosition.y);

            if (objectName == "regularBox")
            {
                regularBoxTilemap.SetTile(objectPositonVector, regularBoxTile);
            }
            else if (objectName == "steelBox")
            {
                steelBoxTilemap.SetTile(objectPositonVector, steelBoxTile);
            }
            else if (objectName == "lavaBox")
            {
                lavaBoxTilemap.SetTile(objectPositonVector, lavaBoxTile);
            }
            else if (objectName == "moveBox")
            {
                moveBoxTilemap.SetTile(objectPositonVector, moveBoxTile);
            }
            else if (objectName == "powerUp")
            {
                Instantiate(powerUp, objectPositonVector, Quaternion.identity);
            }
            else if (objectName == "goal")
            {
                Instantiate(goal, objectPositonVector, Quaternion.identity);
            }
            else if (objectName == "greenKeyDoor")
            {
                Instantiate(greenKeyDoor, objectPositonVector, Quaternion.identity);
            }
            else if (objectName == "greenKey")
            {
                Instantiate(greenKey, objectPositonVector, Quaternion.identity);
            }
            else if (objectName == "redKeyDoor")
            {
                Instantiate(redKeyDoor, objectPositonVector, Quaternion.identity);
            }
            else if (objectName == "redKey")
            {
                Instantiate(redKey, objectPositonVector, Quaternion.identity);
            }
            else if (objectName == "blueKeyDoor")
            {
                Instantiate(blueKeyDoor, objectPositonVector, Quaternion.identity);
            }
            else if (objectName == "blueKey")
            {
                Instantiate(blueKey, objectPositonVector, Quaternion.identity);
            }
            else
            {
                print("Error: object " + objectName + " not found");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
