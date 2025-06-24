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
    public GameObject keyDoor;
    public GameObject key;
    public GameObject powerUp;
    public GameObject goal;
    public GameObject throwBox;
    public GameObject throwBoxButton;
    public GameObject unknownObject;
    public GameObject diagonalBox;
    public GameObject halfBox;
    public GameObject playerWall;
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
        public string type;
        public GridPosition position;
        public int variantID;

        public ObjectInformation(string type, GridPosition position, int variantID = 0)
        {
            this.type = type;
            this.position = position;
            this.variantID = variantID;
        }
    }

    [System.Serializable]
    public class Level
    {
        public string levelName;
        public string bounds;
        public GridPosition[] playerStartPositions;
        public string versionCreated;
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
        GridPosition invalidPlayerPosition = new(-99, -99);

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

        // Move player one to positions
        Vector2 playerStartPositionVector = new(playerPosition.x * 2, playerPosition.y * 2);
        player.transform.position = playerStartPositionVector;

        // Move player two to position
        void movePlayerTwo(GridPosition position)
        {
            playerTwo.SetActive(true);
            Vector2 playerTwoStartPositionVector = new(playerTwoPosition.x * 2, playerTwoPosition.y * 2);
            playerTwo.transform.position = playerTwoStartPositionVector;
        }

        // Move player three to position
        void movePlayerThree(GridPosition position)
        {
            playerThree.SetActive(true);
            Vector2 playerThreeStartPositionVector = new(playerThreePosition.x * 2, playerThreePosition.y * 2);
            playerThree.transform.position = playerThreeStartPositionVector;
        }

        // Move player four to position
        void movePlayerFour(GridPosition position)
        {
            playerFour.SetActive(true);
            Vector2 playerFourStartPositionVector = new(playerFourPosition.x * 2, playerFourPosition.y * 2);
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
                Vector3Int boundPositonVector = new(boundPositon.x, boundPositon.y);
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
            ObjectInformation objectInformation = levelBoxes[counter];
            String objectName = objectInformation.type;
            GridPosition objectPosition = objectInformation.position;
            int objectVariantID = objectInformation.variantID;
            Vector3Int objectPositonVector = new(objectPosition.x, objectPosition.y);

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
                Vector3Int moveBoxPositonVector = new(
                    objectPosition.x * 2, objectPosition.y * 2, (int)moveBox.transform.position.z
                );
                Instantiate(moveBox, moveBoxPositonVector, Quaternion.identity);
            }
            else if (objectName == "powerUp")
            {
                Vector3Int powerUpPositonVector = new(
                    objectPosition.x * 2, objectPosition.y * 2, (int)powerUp.transform.position.z
                );
                Instantiate(powerUp, powerUpPositonVector, Quaternion.identity);
            }
            else if (objectName == "goal")
            {
                Vector3Int goalPositonVector = new(
                    objectPosition.x * 2, objectPosition.y * 2, (int)goal.transform.position.z
                );
                Instantiate(goal, goalPositonVector, Quaternion.identity);
            }
            else if (objectName == "keyDoor")
            {
                Vector3Int keyDoorPositonVector = new(
                    objectPosition.x * 2, objectPosition.y * 2, (int)keyDoor.transform.position.z
                );
                // Give regular objects with a variant option their variant ID
                GameObject keyDoorClone = Instantiate(keyDoor, keyDoorPositonVector, Quaternion.identity);
                keyDoorClone.GetComponent<KeyDoorScript>().variantID = objectVariantID;
            }
            else if (objectName == "key")
            {
                Vector3Int keyPositonVector = new(
                    objectPosition.x * 2, objectPosition.y * 2, (int)key.transform.position.z
                );
                GameObject keyClone = Instantiate(key, keyPositonVector, Quaternion.identity);
                keyClone.GetComponent<KeyScript>().variantID = objectVariantID;
            }
            else if (objectName == "throwBox")
            {
                Vector3Int throwBoxPositonVector = new(
                    objectPosition.x * 2, objectPosition.y * 2, (int)throwBox.transform.position.z
                );
                Instantiate(throwBox, throwBoxPositonVector, Quaternion.identity);
            }
            else if (objectName == "throwBoxButton")
            {
                Vector3Int throwBoxButtonPositonVector = new(
                    objectPosition.x * 2, objectPosition.y * 2, (int)throwBoxButton.transform.position.z
                );
                Instantiate(throwBoxButton, throwBoxButtonPositonVector, Quaternion.identity);
            }
            else if (objectName == "throwBoxTile")
            {
                throwBoxTileTilemap.SetTile(objectPositonVector, throwBoxTileTile);
            }
            else if (objectName == "diagBox")
            {
                Vector3Int diagBoxPositonVector = new(
                    objectPosition.x * 2, objectPosition.y * 2, (int)diagonalBox.transform.position.z
                );
                Quaternion quaternion = Quaternion.identity;
                switch (objectVariantID)
                {
                    // Change rotation depending on variant ID
                    case 2:
                        quaternion = Quaternion.Euler(0, 0, 90);
                        break;
                    case 3:
                        quaternion = Quaternion.Euler(0, 0, 180);
                        break;
                    case 4:
                        quaternion = Quaternion.Euler(0, 0, 270);
                        break;
                }
                Instantiate(diagonalBox, diagBoxPositonVector, quaternion);
            }
            else if (objectName == "halfBox")
            {
                Vector3Int halfBoxPositonVector = new(
                    objectPosition.x * 2, objectPosition.y * 2, (int)halfBox.transform.position.z
                );
                Quaternion quaternion = Quaternion.identity;
                switch (objectVariantID)
                {
                    // Change rotation depending on variant ID
                    case 2:
                        quaternion = Quaternion.Euler(0, 0, 90);
                        break;
                    case 3:
                        quaternion = Quaternion.Euler(0, 0, 180);
                        break;
                    case 4:
                        quaternion = Quaternion.Euler(0, 0, 270);
                        break;
                }
                Instantiate(halfBox, halfBoxPositonVector, quaternion);
            }
            else if (objectName == "playerWallHorizontal")
            {
                Vector3Int playerWallPositonVector = new(
                    objectPosition.x * 2, objectPosition.y * 2, (int)key.transform.position.z
                );
                GameObject playerWallClone = Instantiate(playerWall, playerWallPositonVector, Quaternion.identity);
                playerWallClone.GetComponent<PlayerWallScript>().variantID = objectVariantID;
            }
            else if (objectName == "playerWallVertical")
            {
                Vector3Int playerWallVerticalPositonVector = new(
                    objectPosition.x * 2, objectPosition.y * 2, (int)key.transform.position.z
                );
                GameObject playerWallVerticalClone = Instantiate(playerWall, playerWallVerticalPositonVector, Quaternion.Euler(new(0, 0, 90)));
                playerWallVerticalClone.GetComponent<PlayerWallScript>().variantID = objectVariantID;
            }
            else
            {
                Vector3Int unknownVector = new(
                    objectPosition.x * 2, objectPosition.y * 2, (int)throwBoxButton.transform.position.z
                );
                Instantiate(unknownObject, unknownVector, Quaternion.identity);
            }
        }
    }
}
