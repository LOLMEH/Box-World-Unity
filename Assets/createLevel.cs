using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;

public class createLevel : MonoBehaviour
{
    public Camera gameCamera;
    public TextAsset oldBoundsFile;
    public TextAsset x2BoundsFile;
    public GameObject player;
    public GameObject playerTwo;
    public GameObject playerThree;
    public GameObject playerFour;
    public Tilemap regularBoxTilemap;
    public TileBase regularBoxTile;
    public Tilemap steelBoxTilemap;
    public TileBase steelBoxTile;
    public Tilemap lavaBoxTilemap;
    public TileBase lavaBoxTile;
    public Tilemap throwBoxTileTilemap;
    public TileBase throwBoxTileTile;
    public GameObject moveBox;
    public GameObject greenKeyDoor;
    public GameObject redKeyDoor;
    public GameObject blueKeyDoor;
    public GameObject greenKey;
    public GameObject redKey;
    public GameObject blueKey;
    public GameObject powerUp;
    public GameObject goal;
    public GameObject throwBox;
    public GameObject throwBoxButton;
    public int playerCount;

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
        public GridPosition[] playerStartPositions;
        public ObjectInformation[] levelData;

        public Level(string levelName, string bounds, GridPosition[] playerStartPositions, ObjectInformation[] levelData)
        {
            this.levelName = levelName;
            this.bounds = bounds;
            this.playerStartPositions = playerStartPositions;
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
        string levelFilePath = "Assets/gameAssets/levels/" + loadingLevelData.gamemode + "/" + loadingLevelData.levelID + ".json";
        TextAsset levelFile = AssetDatabase.LoadAssetAtPath<TextAsset>(levelFilePath);

        // Get level from json
        Level levelInfo;
        try
        {
            // Get level's json file
            levelInfo = JsonUtility.FromJson<Level>(levelFile.text);
        }
        catch (NullReferenceException)
        {
            // Get the default level's json file if the level does not exist
            print("Error: Unknown level ID " + loadingLevelData.levelID + ". Loading default level...");
            TextAsset defaultFile = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/gameAssets/levels/nick's/2.json");
            levelInfo = JsonUtility.FromJson<Level>(defaultFile.text);
        }
        ObjectInformation[] levelBoxes = levelInfo.levelData;

        // Get player positions
        GridPosition[] playerStartPositions = levelInfo.playerStartPositions;
        GridPosition playerPosition = playerStartPositions[0];
        GridPosition playerTwoPosition = playerStartPositions[1];
        GridPosition playerThreePosition = playerStartPositions[2];
        GridPosition playerFourPosition = playerStartPositions[3];
        GridPosition invalidPlayerPosition = new GridPosition(-99, -99);

        // Move player to positions
        GridPosition playerStartPosition = playerPosition;
        Vector2 playerStartPositionVector = new Vector2(playerPosition.x, playerPosition.y);
        player.transform.position = playerStartPositionVector;

        // Move the other players to positions if it is a multiplayer level
        // A level is a multiplayer level if the player position is valid
        playerCount = 1;
        if (playerTwoPosition.x != invalidPlayerPosition.x && playerTwoPosition.y != invalidPlayerPosition.y)
        {
            // Player two
            playerTwo.SetActive(true);
            GridPosition playerTwoStartPosition = playerTwoPosition;
            Vector2 playerTwoStartPositionVector = new Vector2(playerTwoPosition.x, playerTwoPosition.y);
            playerTwo.transform.position = playerTwoStartPositionVector;
            playerCount = 2;
        }
        if (playerThreePosition.x != invalidPlayerPosition.x && playerThreePosition.y != invalidPlayerPosition.y)
        {
            // Player three
            playerThree.SetActive(true);
            GridPosition playerThreeStartPosition = playerThreePosition;
            Vector2 playerThreeStartPositionVector = new Vector2(playerThreePosition.x, playerThreePosition.y);
            playerThree.transform.position = playerThreeStartPositionVector;
            playerCount = 3;
        }
        if (playerFourPosition.x != invalidPlayerPosition.x && playerFourPosition.y != invalidPlayerPosition.y)
        {
            // Player four
            playerFour.SetActive(true);
            GridPosition playerFourStartPosition = playerFourPosition;
            Vector2 playerFourStartPositionVector = new Vector2(playerFourPosition.x, playerFourPosition.y);
            playerFour.transform.position = playerFourStartPositionVector;
            playerCount = 4;
        }

        // Generate level boundaries
        void createLevelBounds(LevelBounds levelBounds, GridPosition[] levelBoundData)
        {
            for (int counter = 0; counter < levelBoundData.Length; counter++)
            {
                GridPosition boundPositon = levelBoundData[counter];
                Vector3Int boundPositonVector = new Vector3Int(boundPositon.x, boundPositon.y);
                steelBoxTilemap.SetTile(boundPositonVector, steelBoxTile);
            }
        }

        // Get and call for the generation of level boundaries
        LevelBounds levelBounds;
        GridPosition[] levelBoundData;
        switch (levelInfo.bounds)
        {
            case "old":
                levelBounds = JsonUtility.FromJson<LevelBounds>(oldBoundsFile.text);
                levelBoundData = levelBounds.boundData;
                createLevelBounds(levelBounds, levelBoundData);
                break;
            case "x2":
                levelBounds = JsonUtility.FromJson<LevelBounds>(x2BoundsFile.text);
                levelBoundData = levelBounds.boundData;
                createLevelBounds(levelBounds, levelBoundData);
                // Change camera zoom and position
                float cameraZ = gameCamera.transform.position.z;
                gameCamera.orthographicSize = 10;
                gameCamera.transform.position = new Vector3(1, -1, cameraZ);
                break;
            default:
                print("Error: bounds " + levelInfo.bounds + " not found");
                break;
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
                // Make sure the object keeps its z value
                Vector3Int moveBoxPositonVector = new Vector3Int(
                    objectPosition.x, objectPosition.y, (int)moveBox.transform.position.z
                );
                Instantiate(moveBox, moveBoxPositonVector, Quaternion.identity);
            }
            else if (objectName == "powerUp")
            {
                Vector3Int powerUpPositonVector = new Vector3Int(
                    objectPosition.x, objectPosition.y, (int)powerUp.transform.position.z
                );
                Instantiate(powerUp, powerUpPositonVector, Quaternion.identity);
            }
            else if (objectName == "goal")
            {
                Vector3Int goalPositonVector = new Vector3Int(
                    objectPosition.x, objectPosition.y, (int)goal.transform.position.z
                );
                Instantiate(goal, goalPositonVector, Quaternion.identity);
            }
            else if (objectName == "greenKeyDoor")
            {
                Vector3Int greenKeyDoorPositonVector = new Vector3Int(
                    objectPosition.x, objectPosition.y, (int)greenKeyDoor.transform.position.z
                );
                Instantiate(greenKeyDoor, greenKeyDoorPositonVector, Quaternion.identity);
            }
            else if (objectName == "greenKey")
            {
                Vector3Int greenKeyPositonVector = new Vector3Int(
                    objectPosition.x, objectPosition.y, (int)greenKey.transform.position.z
                );
                Instantiate(greenKey, greenKeyPositonVector, Quaternion.identity);
            }
            else if (objectName == "redKeyDoor")
            {
                Vector3Int redKeyDoorPositonVector = new Vector3Int(
                    objectPosition.x, objectPosition.y, (int)redKeyDoor.transform.position.z
                );
                Instantiate(redKeyDoor, redKeyDoorPositonVector, Quaternion.identity);
            }
            else if (objectName == "redKey")
            {
                Vector3Int redKeyPositonVector = new Vector3Int(
                    objectPosition.x, objectPosition.y, (int)redKey.transform.position.z
                );
                Instantiate(redKey, redKeyPositonVector, Quaternion.identity);
            }
            else if (objectName == "blueKeyDoor")
            {
                Vector3Int blueKeyDoorPositonVector = new Vector3Int(
                    objectPosition.x, objectPosition.y, (int)blueKeyDoor.transform.position.z
                );
                Instantiate(blueKeyDoor, blueKeyDoorPositonVector, Quaternion.identity);
            }
            else if (objectName == "blueKey")
            {
                Vector3Int blueKeyPositonVector = new Vector3Int(
                    objectPosition.x, objectPosition.y, (int)blueKey.transform.position.z
                );
                Instantiate(blueKey, blueKeyPositonVector, Quaternion.identity);
            }
            else if (objectName == "throwBox")
            {
                Vector3Int throwBoxPositonVector = new Vector3Int(
                    objectPosition.x, objectPosition.y, (int)throwBox.transform.position.z
                );
                Instantiate(throwBox, throwBoxPositonVector, Quaternion.identity);
            }
            else if (objectName == "throwBoxButton")
            {
                Vector3Int throwBoxButtonPositonVector = new Vector3Int(
                    objectPosition.x, objectPosition.y, (int)throwBoxButton.transform.position.z
                );
                Instantiate(throwBoxButton, throwBoxButtonPositonVector, Quaternion.identity);
            }
            else if (objectName == "throwBoxTile")
            {
                throwBoxTileTilemap.SetTile(objectPositonVector, throwBoxTileTile);
            }
            else
            {
                print("Error: object " + objectName + " not found");
            }
        }
    }
}
