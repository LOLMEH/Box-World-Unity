using UnityEngine;
using UnityEngine.Tilemaps;

public class moveCreateObject : MonoBehaviour
{
    public int gridScale;
    public GameObject playerMarker;
    public GameObject playerTwoMarker;
    public GameObject playerThreeMarker;
    public GameObject playerFourMarker;
    public GameObject goalMarker;
    public GameObject objectGroup;
    public Tilemap regularBoxTilemap;
    public TileBase regularBoxTile;
    public Sprite regularBoxImage;
    public Tilemap steelBoxTilemap;
    public TileBase steelBoxTile;
    public Sprite steelBoxImage;
    public Tilemap lavaBoxTilemap;
    public TileBase lavaBoxTile;
    public Sprite lavaBoxImage;
    public GameObject moveBoxObject;
    public GameObject powerUpObject;
    public GameObject greenKeyDoorObject;
    public GameObject blueKeyDoorObject;
    public GameObject redKeyDoorObject;
    public GameObject greenKeyObject;
    public GameObject blueKeyObject;
    public GameObject redKeyObject;
    public GameObject throwBoxObject;
    public GameObject throwBoxButtonObject;
    public Tilemap throwBoxTileTilemap;
    public TileBase throwBoxTileTile;
    public Sprite throwBoxTileImage;
    public GameObject unknownObject;
    public GameObject diagonalBoxTRObject;
    public GameObject diagonalBoxTLObject;
    public GameObject diagonalBoxBRObject;
    public GameObject diagonalBoxBLObject;
    public GameObject halfBoxBottomObject;
    public GameObject halfBoxRightObject;
    public GameObject halfBoxTopObject;
    public GameObject halfBoxLeftObject;
    public GameObject playerOneWallObject;
    public GameObject playerTwoWallObject;
    public GameObject playerThreeWallObject;
    public GameObject playerFourWallObject;
    public GameObject playerOneVertWallObject;
    public GameObject playerTwoVertWallObject;
    public GameObject playerThreeVertWallObject;
    public GameObject playerFourVertWallObject;
    public Tilemap iceTilemap;
    public TileBase iceTile;
    public Sprite iceImage;
    private string objectName;
    private SpriteRenderer createSprite;

    /// <summary>
    /// Places an object at the selected mouse position
    /// </summary>
    /// <param name="mousePosX">The X of the mouse pointer</param>
    /// <param name="mousePosY">The Y of the mouse pointer</param>
    /// <param name="useObjectScale">If the object being placed uses the object scale</param>
    /// <param name="objectNameOverride">The name of the object to override the chosen object</param>
    public void PlaceObject(int mousePosX, int mousePosY, bool useObjectScale, string objectNameOverride = "", int variantID = 0)
    {
        // Get the box position on the tilemap grid
        Vector3Int objectPositon = new(
            mousePosX / gridScale,
            mousePosY / gridScale
        );
        int objectScale = 1;
        if (useObjectScale == true)
        {
            objectPositon = new(mousePosX, mousePosY);
            objectScale = 2;
        }

        // Get the object name to use
        string placeObjectName = objectName;
        if (!objectNameOverride.Equals(""))
        {
            // Override the object name if it is not blank
            placeObjectName = objectNameOverride;

            // If the object has a variant, set the name to the corresponding variant
            switch (placeObjectName)
            {
                case "keyDoor":
                    switch (variantID)
                    {
                        case 1:
                            placeObjectName = "greenKeyDoor";
                            break;
                        case 2:
                            placeObjectName = "redKeyDoor";
                            break;
                        case 3:
                            placeObjectName = "blueKeyDoor";
                            break;
                    }
                    break;
                case "key":
                    switch (variantID)
                    {
                        case 1:
                            placeObjectName = "greenKey";
                            break;
                        case 2:
                            placeObjectName = "redKey";
                            break;
                        case 3:
                            placeObjectName = "blueKey";
                            break;
                    }
                    break;
                case "diagBox":
                    switch (variantID)
                    {
                        case 1:
                            placeObjectName = "diagBoxBL";
                            break;
                        case 2:
                            placeObjectName = "diagBoxBR";
                            break;
                        case 3:
                            placeObjectName = "diagBoxTR";
                            break;
                        case 4:
                            placeObjectName = "diagBoxTL";
                            break;
                    }
                    break;
                case "halfBox":
                    switch (variantID)
                    {
                        case 1:
                            placeObjectName = "halfBoxB";
                            break;
                        case 2:
                            placeObjectName = "halfBoxR";
                            break;
                        case 3:
                            placeObjectName = "halfBoxT";
                            break;
                        case 4:
                            placeObjectName = "halfBoxL";
                            break;
                    }
                    break;
                case "playerWallHorizontal":
                    switch (variantID)
                    {
                        case 1:
                            placeObjectName = "playerOneWall";
                            break;
                        case 2:
                            placeObjectName = "playerTwoWall";
                            break;
                        case 3:
                            placeObjectName = "playerThreeWall";
                            break;
                        case 4:
                            placeObjectName = "playerFourWall";
                            break;
                    }
                    break;
                case "playerWallVertical":
                    switch (variantID)
                    {
                        case 1:
                            placeObjectName = "playerOneWallVertical";
                            break;
                        case 2:
                            placeObjectName = "playerTwoWallVertical";
                            break;
                        case 3:
                            placeObjectName = "playerThreeWallVertical";
                            break;
                        case 4:
                            placeObjectName = "playerFourWallVertical";
                            break;
                    }
                    break;
            }
        }

        // Check if the placed object can go though tiles
        string[] objectThatCanGoInTiles = { 
            "powerUp", "greenKey", "redKey", "blueKey",
            "playerOneWall", "playerTwoWall", "playerThreeWall", "playerFourWall",
            "playerOneWallVertical", "playerTwoWallVertical", "playerThreeWallVertical", "playerFourWallVertical"
        };
        bool canPlacedObjectGoInTiles = false;
        for (int counter = 0; counter < objectThatCanGoInTiles.Length; counter++)
        {
            if (placeObjectName == objectThatCanGoInTiles[counter])
            {
                canPlacedObjectGoInTiles = true;
                break;
            }
        }

        // Delete object if space is occupied (tilemaps)
        if (regularBoxTilemap.GetTile(objectPositon) && !canPlacedObjectGoInTiles)
        {
            regularBoxTilemap.SetTile(objectPositon, null);
        }
        else if (steelBoxTilemap.GetTile(objectPositon) && !canPlacedObjectGoInTiles)
        {
            steelBoxTilemap.SetTile(objectPositon, null);
        }
        else if (lavaBoxTilemap.GetTile(objectPositon) && !canPlacedObjectGoInTiles)
        {
            lavaBoxTilemap.SetTile(objectPositon, null);
        }
        else if (throwBoxTileTilemap.GetTile(objectPositon) && !canPlacedObjectGoInTiles)
        {
            throwBoxTileTilemap.SetTile(objectPositon, null);
        }
        else if (iceTilemap.GetTile(objectPositon) && !canPlacedObjectGoInTiles)
        {
            iceTilemap.SetTile(objectPositon, null);
        }
        else
        {
            // Delete object if space is occupied (other objects)
            Vector2 newObjectPos = new(mousePosX, mousePosY);
            int amountOfObjects = objectGroup.transform.childCount;
            for (int counter = 0; counter < amountOfObjects; counter++)
            {
                GameObject gameObject = objectGroup.transform.GetChild(counter).gameObject;
                Vector2 gameObjectVector = gameObject.transform.position;
                if (gameObjectVector == newObjectPos)
                {
                    Destroy(gameObject);
                }
            }
        }

        switch (placeObjectName)
        {
            // Check which object to place
            case "air":
                // Do nothing
                break;
            case "regularBox":
                // Create tile object
                regularBoxTilemap.SetTile(objectPositon, regularBoxTile);
                break;
            case "steelBox":
                steelBoxTilemap.SetTile(objectPositon, steelBoxTile);
                break;
            case "lavaBox":
                lavaBoxTilemap.SetTile(objectPositon, lavaBoxTile);
                break;
            case "moveBox":
                // Create regular object
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newCloneMoveBox = Instantiate(moveBoxObject, new Vector2(mousePosX, mousePosY), Quaternion.identity);
                newCloneMoveBox.transform.SetParent(objectGroup.transform);
                break;
            case "powerUp":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newClonePowerUp = Instantiate(powerUpObject, new Vector2(mousePosX, mousePosY), Quaternion.identity);
                newClonePowerUp.transform.SetParent(objectGroup.transform);
                break;
            case "player":
                // Move the only critical object
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                playerMarker.transform.position = new Vector2(mousePosX, mousePosY);
                break;
            case "goal":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                goalMarker.transform.position = new Vector2(mousePosX, mousePosY);
                break;
            case "greenKeyDoor":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newCloneGreenKeyDoor = Instantiate(greenKeyDoorObject, new Vector2(mousePosX, mousePosY), greenKeyDoorObject.transform.rotation);
                newCloneGreenKeyDoor.transform.SetParent(objectGroup.transform);
                break;
            case "redKeyDoor":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newCloneRedKeyDoor = Instantiate(redKeyDoorObject, new Vector2(mousePosX, mousePosY), redKeyDoorObject.transform.rotation);
                newCloneRedKeyDoor.transform.SetParent(objectGroup.transform);
                break;
            case "blueKeyDoor":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newCloneBlueKeyDoor = Instantiate(blueKeyDoorObject, new Vector2(mousePosX, mousePosY), blueKeyDoorObject.transform.rotation);
                newCloneBlueKeyDoor.transform.SetParent(objectGroup.transform);
                break;
            case "greenKey":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newCloneGreenKey = Instantiate(greenKeyObject, new Vector2(mousePosX, mousePosY), greenKeyObject.transform.rotation);
                newCloneGreenKey.transform.SetParent(objectGroup.transform);
                break;
            case "redKey":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newCloneRedKey = Instantiate(redKeyObject, new Vector2(mousePosX, mousePosY), redKeyObject.transform.rotation);
                newCloneRedKey.transform.SetParent(objectGroup.transform);
                break;
            case "blueKey":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newCloneBlueKey = Instantiate(blueKeyObject, new Vector2(mousePosX, mousePosY), blueKeyObject.transform.rotation);
                newCloneBlueKey.transform.SetParent(objectGroup.transform);
                break;
            case "player2":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                playerTwoMarker.transform.position = new Vector2(mousePosX, mousePosY);
                break;
            case "player3":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                playerThreeMarker.transform.position = new Vector2(mousePosX, mousePosY);
                break;
            case "player4":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                playerFourMarker.transform.position = new Vector2(mousePosX, mousePosY);
                break;
            case "throwBox":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newCloneThrowBox = Instantiate(throwBoxObject, new Vector2(mousePosX, mousePosY), throwBoxObject.transform.rotation);
                newCloneThrowBox.transform.SetParent(objectGroup.transform);
                break;
            case "throwBoxButton":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newCloneThrowBoxButton = Instantiate(throwBoxButtonObject, new Vector2(mousePosX, mousePosY), throwBoxButtonObject.transform.rotation);
                newCloneThrowBoxButton.transform.SetParent(objectGroup.transform);
                break;
            case "throwBoxTile":
                throwBoxTileTilemap.SetTile(objectPositon, throwBoxTileTile);
                break;
            case "diagBoxBL":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newDiagBoxBL = Instantiate(diagonalBoxBLObject, new Vector2(mousePosX, mousePosY), diagonalBoxBLObject.transform.rotation);
                newDiagBoxBL.transform.SetParent(objectGroup.transform);
                break;
            case "diagBoxBR":
                // Rotate this variants
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newDiagBoxBR = Instantiate(diagonalBoxBRObject, new Vector2(mousePosX, mousePosY), diagonalBoxBRObject.transform.rotation);
                newDiagBoxBR.transform.SetParent(objectGroup.transform);
                break;
            case "diagBoxTR":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newDiagBoxTR = Instantiate(diagonalBoxTRObject, new Vector2(mousePosX, mousePosY), diagonalBoxTRObject.transform.rotation);
                newDiagBoxTR.transform.SetParent(objectGroup.transform);
                break;
            case "diagBoxTL":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newDiagBoxTL = Instantiate(diagonalBoxTLObject, new Vector2(mousePosX, mousePosY), diagonalBoxTLObject.transform.rotation);
                newDiagBoxTL.transform.SetParent(objectGroup.transform);
                break;
            case "halfBoxB":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newHalfBoxB = Instantiate(halfBoxBottomObject, new Vector2(mousePosX, mousePosY), halfBoxBottomObject.transform.rotation);
                newHalfBoxB.transform.SetParent(objectGroup.transform);
                break;
            case "halfBoxR":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newHalfBoxR = Instantiate(halfBoxRightObject, new Vector2(mousePosX, mousePosY), halfBoxRightObject.transform.rotation);
                newHalfBoxR.transform.SetParent(objectGroup.transform);
                break;
            case "halfBoxT":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newHalfBoxT = Instantiate(halfBoxTopObject, new Vector2(mousePosX, mousePosY), halfBoxTopObject.transform.rotation);
                newHalfBoxT.transform.SetParent(objectGroup.transform);
                break;
            case "halfBoxL":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newHalfBoxL = Instantiate(halfBoxLeftObject, new Vector2(mousePosX, mousePosY), halfBoxLeftObject.transform.rotation);
                newHalfBoxL.transform.SetParent(objectGroup.transform);
                break;
            case "playerOneWall":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newPlayerOneWall = Instantiate(playerOneWallObject, new Vector2(mousePosX, mousePosY), playerOneWallObject.transform.rotation);
                newPlayerOneWall.transform.SetParent(objectGroup.transform);
                break;
            case "playerTwoWall":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newPlayerTwoWall = Instantiate(playerTwoWallObject, new Vector2(mousePosX, mousePosY), playerTwoWallObject.transform.rotation);
                newPlayerTwoWall.transform.SetParent(objectGroup.transform);
                break;
            case "playerThreeWall":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newPlayerThreeWall = Instantiate(playerThreeWallObject, new Vector2(mousePosX, mousePosY), playerThreeWallObject.transform.rotation);
                newPlayerThreeWall.transform.SetParent(objectGroup.transform);
                break;
            case "playerFourWall":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newPlayerFourWall = Instantiate(playerFourWallObject, new Vector2(mousePosX, mousePosY), playerFourWallObject.transform.rotation);
                newPlayerFourWall.transform.SetParent(objectGroup.transform);
                break;
            case "playerOneWallVertical":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newPlayerOneWallVertical = Instantiate(playerOneVertWallObject, new Vector2(mousePosX, mousePosY), playerOneVertWallObject.transform.rotation);
                newPlayerOneWallVertical.transform.SetParent(objectGroup.transform);
                break;
            case "playerTwoWallVertical":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newPlayerTwoWallVertical = Instantiate(playerTwoVertWallObject, new Vector2(mousePosX, mousePosY), playerTwoVertWallObject.transform.rotation);
                newPlayerTwoWallVertical.transform.SetParent(objectGroup.transform);
                break;
            case "playerThreeWallVertical":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newPlayerThreeWallVertical = Instantiate(playerThreeVertWallObject, new Vector2(mousePosX, mousePosY), playerThreeVertWallObject.transform.rotation);
                newPlayerThreeWallVertical.transform.SetParent(objectGroup.transform);
                break;
            case "playerFourWallVertical":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newPlayerFourWallVertical = Instantiate(playerFourVertWallObject, new Vector2(mousePosX, mousePosY), playerFourVertWallObject.transform.rotation);
                newPlayerFourWallVertical.transform.SetParent(objectGroup.transform);
                break;
            case "ice":
                iceTilemap.SetTile(objectPositon, iceTile);
                break;
            default:
                // Default object
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newUnknown = Instantiate(unknownObject, new Vector2(mousePosX, mousePosY), unknownObject.transform.rotation);
                newUnknown.transform.SetParent(objectGroup.transform);
                break;
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Default configurations
        objectName = "regularBox";
        createSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Create grid snap
        mousePos.x = Mathf.Floor(mousePos.x);
        mousePos.y = Mathf.Floor(mousePos.y);
        if (mousePos.x % gridScale != 0)
        {
            mousePos.x += 1;
        }
        if (mousePos.y % gridScale != 0)
        {
            mousePos.y += 1;
        }

        // Go to mouse position
        GetComponent<Transform>().position = (Vector2)mousePos;

        // Place sprite when the player clicks
        if (Input.GetMouseButton(0))
        {
            PlaceObject((int)mousePos.x, (int)mousePos.y, false);
        }

        // Reset the create object image's rotation
        static void resetCreateObjectRotation(SpriteRenderer createSprite)
        {
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // Change object being placed
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            objectName = "air";
            createSprite.sprite = null;
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            objectName = "regularBox";
            createSprite.sprite = regularBoxImage;
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            objectName = "steelBox";
            createSprite.sprite = steelBoxImage;
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            objectName = "lavaBox";
            createSprite.sprite = lavaBoxImage;
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            objectName = "powerUp";
            createSprite.sprite = powerUpObject.GetComponent<SpriteRenderer>().sprite;
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            objectName = "player";
            createSprite.sprite = playerMarker.GetComponent<SpriteRenderer>().sprite;
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            objectName = "goal";
            createSprite.sprite = goalMarker.GetComponent<SpriteRenderer>().sprite;
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            // Go into more variants
            switch (objectName)
            {
                case "greenKeyDoor":
                    objectName = "redKeyDoor";
                    createSprite.sprite = redKeyDoorObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                case "redKeyDoor":
                    objectName = "blueKeyDoor";
                    createSprite.sprite = blueKeyDoorObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                default:
                    objectName = "greenKeyDoor";
                    createSprite.sprite = greenKeyDoorObject.GetComponent<SpriteRenderer>().sprite;
                    break;
            }
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            switch (objectName)
            {
                case "greenKey":
                    objectName = "redKey";
                    createSprite.sprite = redKeyObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                case "redKey":
                    objectName = "blueKey";
                    createSprite.sprite = blueKeyObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                default:
                    objectName = "greenKey";
                    createSprite.sprite = greenKeyObject.GetComponent<SpriteRenderer>().sprite;
                    break;
            }
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            objectName = "moveBox";
            createSprite.sprite = moveBoxObject.GetComponent<SpriteRenderer>().sprite;
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            objectName = "player2";
            createSprite.sprite = playerTwoMarker.GetComponent<SpriteRenderer>().sprite;
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            objectName = "player3";
            createSprite.sprite = playerThreeMarker.GetComponent<SpriteRenderer>().sprite;
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            objectName = "player4";
            createSprite.sprite = playerFourMarker.GetComponent<SpriteRenderer>().sprite;
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            objectName = "throwBox";
            createSprite.sprite = throwBoxObject.GetComponent<SpriteRenderer>().sprite;
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            objectName = "throwBoxButton";
            createSprite.sprite = throwBoxButtonObject.GetComponent<SpriteRenderer>().sprite;
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            objectName = "throwBoxTile";
            createSprite.sprite = throwBoxTileImage;
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            switch (objectName)
            {
                case "diagBoxBL":
                    objectName = "diagBoxBR";
                    createSprite.transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
                case "diagBoxBR":
                    objectName = "diagBoxTR";
                    createSprite.transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
                case "diagBoxTR":
                    objectName = "diagBoxTL";
                    createSprite.transform.rotation = Quaternion.Euler(0, 0, 270);
                    break;
                default:
                    objectName = "diagBoxBL";
                    createSprite.sprite = diagonalBoxBLObject.GetComponent<SpriteRenderer>().sprite;
                    resetCreateObjectRotation(createSprite);
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            switch (objectName)
            {
                case "halfBoxB":
                    objectName = "halfBoxR";
                    createSprite.transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
                case "halfBoxR":
                    objectName = "halfBoxT";
                    createSprite.transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
                case "halfBoxT":
                    objectName = "halfBoxL";
                    createSprite.transform.rotation = Quaternion.Euler(0, 0, 270);
                    break;
                default:
                    objectName = "halfBoxB";
                    createSprite.sprite = halfBoxBottomObject.GetComponent<SpriteRenderer>().sprite;
                    resetCreateObjectRotation(createSprite);
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            switch (objectName)
            {
                case "playerOneWall":
                    objectName = "playerTwoWall";
                    createSprite.sprite = playerTwoWallObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                case "playerTwoWall":
                    objectName = "playerThreeWall";
                    createSprite.sprite = playerThreeWallObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                case "playerThreeWall":
                    objectName = "playerFourWall";
                    createSprite.sprite = playerFourWallObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                default:
                    objectName = "playerOneWall";
                    createSprite.sprite = playerOneWallObject.GetComponent<SpriteRenderer>().sprite;
                    break;
            }
            resetCreateObjectRotation(createSprite);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            switch (objectName)
            {
                case "playerOneWallVertical":
                    objectName = "playerTwoWallVertical";
                    createSprite.sprite = playerTwoWallObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                case "playerTwoWallVertical":
                    objectName = "playerThreeWallVertical";
                    createSprite.sprite = playerThreeWallObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                case "playerThreeWallVertical":
                    objectName = "playerFourWallVertical";
                    createSprite.sprite = playerFourWallObject.GetComponent<SpriteRenderer>().sprite;
                    break;
                default:
                    objectName = "playerOneWallVertical";
                    createSprite.sprite = playerOneWallObject.GetComponent<SpriteRenderer>().sprite;
                    break;
            }
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            objectName = "ice";
            createSprite.sprite = iceImage;
            resetCreateObjectRotation(createSprite);
        }
    }
}
