using System;
using System.IO;
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
        public String versionCreated;
        public ObjectInformation[] levelData;

        public Level(string levelName, string bounds, GridPosition[] playerStartPositions, string versionCreated, ObjectInformation[] levelData)
        {
            this.levelName = levelName;
            this.bounds = bounds;
            this.playerStartPositions = playerStartPositions;
            this.versionCreated = versionCreated;
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

    /// <summary>
    /// Checks the player count of a level
    /// </summary>
    /// <param name="levelInfo">The level data</param>
    /// <returns></returns>
    public static int CheckPlayerCountOfLevel(GridPosition[] playerStartPositions)
    {
        // Get player positions
        GridPosition playerTwoPosition = playerStartPositions[1];
        GridPosition playerThreePosition = playerStartPositions[2];
        GridPosition playerFourPosition = playerStartPositions[3];
        GridPosition invalidPlayerPosition = new GridPosition(-99, -99);

        // A level is a multiplayer level if the player position is valid (not equal to -99, -99)
        int foundPlayerCount = 1;
        if (playerTwoPosition.x != invalidPlayerPosition.x && playerTwoPosition.y != invalidPlayerPosition.y)
        {
            // Two players
            foundPlayerCount = 2;
        }
        if (playerThreePosition.x != invalidPlayerPosition.x && playerThreePosition.y != invalidPlayerPosition.y)
        {
            // Three players
            foundPlayerCount = 3;
        }
        if (playerFourPosition.x != invalidPlayerPosition.x && playerFourPosition.y != invalidPlayerPosition.y)
        {
            // Four players
            foundPlayerCount = 4;
        }

        return foundPlayerCount;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Load the level json file from the loading level object
        loadingLevelData loadingLevelData = GameObject.FindGameObjectWithTag("LoadLevelInfo").GetComponent<loadingLevelData>();
        string levelFilePath = "levels/" + loadingLevelData.gamemode + "/" + loadingLevelData.levelID;
        string levelFile;
        // Get the raw level json data
        if (loadingLevelData.gamemode == "customLevels")
        {
            // Go to another folder to load custom levels
            string customLevelFilePath =
                Application.persistentDataPath + "/" + loadingLevelData.gamemode + "/" + loadingLevelData.levelID + ".json";
            try
            {
                levelFile = File.ReadAllText(customLevelFilePath);
            }
            catch (FileNotFoundException)
            {
                // Load the default level's json file if the custom level does not exist
                print("Error: Unknown level ID " + loadingLevelData.levelID + ". Loading default level...");
                levelFile = Resources.Load<TextAsset>("levels/nick's/1").ToString();
            }
        }
        else
        {
            try
            {
                levelFile = Resources.Load<TextAsset>(levelFilePath).ToString();
            }
            catch (NullReferenceException)
            {
                // Load the default level's json file if the level does not exist
                print("Error: Unknown level ID " + loadingLevelData.levelID + ". Loading default level...");
                levelFile = Resources.Load<TextAsset>("levels/nick's/2").ToString();
            }
        }

        // Convert to a level from json
        Level levelInfo;
        levelInfo = JsonUtility.FromJson<Level>(levelFile);

        // Get the level's object information
        ObjectInformation[] levelBoxes = levelInfo.levelData;

        // Get player positions
        GridPosition[] playerStartPositions = levelInfo.playerStartPositions;
        GridPosition playerPosition = playerStartPositions[0];
        GridPosition playerTwoPosition = playerStartPositions[1];
        GridPosition playerThreePosition = playerStartPositions[2];
        GridPosition playerFourPosition = playerStartPositions[3];
        GridPosition invalidPlayerPosition = new GridPosition(-99, -99);

        // Move player one to positions
        GridPosition playerStartPosition = playerPosition;
        Vector2 playerStartPositionVector = new Vector2(playerPosition.x * 2, playerPosition.y * 2);
        player.transform.position = playerStartPositionVector;

        // Move player two to position
        void movePlayerTwo(GridPosition position)
        {
            playerTwo.SetActive(true);
            GridPosition playerTwoStartPosition = position;
            Vector2 playerTwoStartPositionVector = new Vector2(playerTwoPosition.x * 2, playerTwoPosition.y * 2);
            playerTwo.transform.position = playerTwoStartPositionVector;
        }

        // Move player three to position
        void movePlayerThree(GridPosition position)
        {
            playerThree.SetActive(true);
            GridPosition playerThreeStartPosition = position;
            Vector2 playerThreeStartPositionVector = new Vector2(playerThreePosition.x * 2, playerThreePosition.y * 2);
            playerThree.transform.position = playerThreeStartPositionVector;
        }

        // Move player four to position
        void movePlayerFour(GridPosition position)
        {
            playerFour.SetActive(true);
            GridPosition playerFourStartPosition = position;
            Vector2 playerFourStartPositionVector = new Vector2(playerFourPosition.x * 2, playerFourPosition.y * 2);
            playerFour.transform.position = playerFourStartPositionVector;
        }

        // Move the other players to positions if it is a multiplayer level
        playerCount = CheckPlayerCountOfLevel(playerStartPositions);
        if (playerCount == 2)
        {
            // Two players
            movePlayerTwo(playerTwoPosition);
        }
        if (playerCount == 3)
        {
            // Three players
            movePlayerTwo(playerTwoPosition);
            movePlayerThree(playerThreePosition);
        }
        if (playerCount == 4)
        {
            // Four players
            movePlayerTwo(playerTwoPosition);
            movePlayerThree(playerThreePosition);
            movePlayerFour(playerThreePosition);
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
                // Makes sure the object keeps its z value
                // Objects that are not tilemaps need to have their position multiplied by 2
                Vector3Int moveBoxPositonVector = new Vector3Int(
                    objectPosition.x * 2, objectPosition.y * 2, (int)moveBox.transform.position.z
                );
                Instantiate(moveBox, moveBoxPositonVector, Quaternion.identity);
            }
            else if (objectName == "powerUp")
            {
                Vector3Int powerUpPositonVector = new Vector3Int(
                    objectPosition.x * 2, objectPosition.y * 2, (int)powerUp.transform.position.z
                );
                Instantiate(powerUp, powerUpPositonVector, Quaternion.identity);
            }
            else if (objectName == "goal")
            {
                Vector3Int goalPositonVector = new Vector3Int(
                    objectPosition.x * 2, objectPosition.y * 2, (int)goal.transform.position.z
                );
                Instantiate(goal, goalPositonVector, Quaternion.identity);
            }
            else if (objectName == "greenKeyDoor")
            {
                Vector3Int greenKeyDoorPositonVector = new Vector3Int(
                    objectPosition.x * 2, objectPosition.y * 2, (int)greenKeyDoor.transform.position.z
                );
                Instantiate(greenKeyDoor, greenKeyDoorPositonVector, Quaternion.identity);
            }
            else if (objectName == "greenKey")
            {
                Vector3Int greenKeyPositonVector = new Vector3Int(
                    objectPosition.x * 2, objectPosition.y * 2, (int)greenKey.transform.position.z
                );
                Instantiate(greenKey, greenKeyPositonVector, Quaternion.identity);
            }
            else if (objectName == "redKeyDoor")
            {
                Vector3Int redKeyDoorPositonVector = new Vector3Int(
                    objectPosition.x * 2, objectPosition.y * 2, (int)redKeyDoor.transform.position.z
                );
                Instantiate(redKeyDoor, redKeyDoorPositonVector, Quaternion.identity);
            }
            else if (objectName == "redKey")
            {
                Vector3Int redKeyPositonVector = new Vector3Int(
                    objectPosition.x * 2, objectPosition.y * 2, (int)redKey.transform.position.z
                );
                Instantiate(redKey, redKeyPositonVector, Quaternion.identity);
            }
            else if (objectName == "blueKeyDoor")
            {
                Vector3Int blueKeyDoorPositonVector = new Vector3Int(
                    objectPosition.x * 2, objectPosition.y * 2, (int)blueKeyDoor.transform.position.z
                );
                Instantiate(blueKeyDoor, blueKeyDoorPositonVector, Quaternion.identity);
            }
            else if (objectName == "blueKey")
            {
                Vector3Int blueKeyPositonVector = new Vector3Int(
                    objectPosition.x * 2, objectPosition.y * 2, (int)blueKey.transform.position.z
                );
                Instantiate(blueKey, blueKeyPositonVector, Quaternion.identity);
            }
            else if (objectName == "throwBox")
            {
                Vector3Int throwBoxPositonVector = new Vector3Int(
                    objectPosition.x * 2, objectPosition.y * 2, (int)throwBox.transform.position.z
                );
                Instantiate(throwBox, throwBoxPositonVector, Quaternion.identity);
            }
            else if (objectName == "throwBoxButton")
            {
                Vector3Int throwBoxButtonPositonVector = new Vector3Int(
                    objectPosition.x * 2, objectPosition.y * 2, (int)throwBoxButton.transform.position.z
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
