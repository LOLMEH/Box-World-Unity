using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class moveCreateObject : MonoBehaviour
{
    public int gridScale;
    public GameObject playerMarker;
    public Sprite playerImage;
    public GameObject playerTwoMarker;
    public Sprite playerTwoImage;
    public GameObject playerThreeMarker;
    public Sprite playerThreeImage;
    public GameObject playerFourMarker;
    public Sprite playerFourImage;
    public GameObject goalMarker;
    public Sprite goalImage;
    public Tilemap regularBoxTilemap;
    public TileBase regularBoxTile;
    public Sprite regularBoxImage;
    public Tilemap steelBoxTilemap;
    public TileBase steelBoxTile;
    public Sprite steelBoxImage;
    public Tilemap lavaBoxTilemap;
    public TileBase lavaBoxTile;
    public Sprite lavaBoxImage;
    public GameObject moveBoxGroup;
    public GameObject moveBoxObject;
    public Sprite moveBoxImage;
    public GameObject powerUpGroup;
    public GameObject powerUpObject;
    public Sprite powerUpImage;
    public GameObject greenKeyDoorGroup;
    public GameObject greenKeyDoorObject;
    public Sprite greenKeyDoorImage;
    public GameObject blueKeyDoorGroup;
    public GameObject blueKeyDoorObject;
    public Sprite blueKeyDoorImage;
    public GameObject redKeyDoorGroup;
    public GameObject redKeyDoorObject;
    public Sprite redKeyDoorImage;
    public GameObject greenKeyGroup;
    public GameObject greenKeyObject;
    public Sprite greenKeyImage;
    public GameObject blueKeyGroup;
    public GameObject blueKeyObject;
    public Sprite blueKeyImage;
    public GameObject redKeyGroup;
    public GameObject redKeyObject;
    public Sprite redKeyImage;
    public GameObject throwBoxGroup;
    public GameObject throwBoxObject;
    public Sprite throwBoxImage;
    public GameObject throwBoxButtonGroup;
    public GameObject throwBoxButtonObject;
    public Sprite throwBoxButtonImage;
    public Tilemap throwBoxTileTilemap;
    public TileBase throwBoxTileTile;
    public Sprite throwBoxTileImage;
    public GameObject unknownGroup;
    public GameObject unknownObject;
    public GameObject diagonalBoxBLGroup;
    public GameObject diagonalBoxBRGroup;
    public GameObject diagonalBoxTRGroup;
    public GameObject diagonalBoxTLGroup;
    public GameObject diagonalBoxObject;
    public Sprite diagonalBoxImage;
    public GameObject halfBoxBGroup;
    public GameObject halfBoxRGroup;
    public GameObject halfBoxTGroup;
    public GameObject halfBoxLGroup;
    public GameObject halfBoxObject;
    public Sprite halfBoxImage;
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
        Vector3Int objectPositon = new Vector3Int(
            mousePosX / gridScale,
            mousePosY / gridScale
        );
        int objectScale = 1;
        if (useObjectScale == true)
        {
            objectPositon = new Vector3Int(mousePosX, mousePosY);
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
            }
        }

        // Check if the placed object can go though tiles
        string[] objectThatCanGoInTiles = { "powerUp", "greenKey", "redKey", "blueKey" };
        bool canPlacedObjectGoInTiles = false;
        for (int counter = 0; counter < objectThatCanGoInTiles.Length; counter++)
        {
            if (placeObjectName == objectThatCanGoInTiles[counter])
            {
                canPlacedObjectGoInTiles = true;
                break;
            }
        }

        // Delete object if space is occupied
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
        else
        {
            // Special check case for non-tile objects
            Vector2 newObjectPos = new Vector2(mousePosX, mousePosY);
            int amountOfMoveBoxes = moveBoxGroup.transform.childCount;
            for (int counter = 0; counter < amountOfMoveBoxes; counter++)
            {
                GameObject moveBox = moveBoxGroup.transform.GetChild(counter).gameObject;
                Vector2 moveBoxVector = moveBox.transform.position;
                if (moveBoxVector == newObjectPos)
                {
                    Destroy(moveBox);
                }
            }
            int amountOfPowerUps = powerUpGroup.transform.childCount;
            for (int counter = 0; counter < amountOfPowerUps; counter++)
            {
                GameObject powerUp = powerUpGroup.transform.GetChild(counter).gameObject;
                Vector2 powerUpVector = powerUp.transform.position;
                if (powerUpVector == newObjectPos)
                {
                    Destroy(powerUp);
                }
            }
            int amountOfGreenKeyDoors = greenKeyDoorGroup.transform.childCount;
            for (int counter = 0; counter < amountOfGreenKeyDoors; counter++)
            {
                GameObject greenKeyDoor = greenKeyDoorGroup.transform.GetChild(counter).gameObject;
                Vector2 greenKeyDoorVector = greenKeyDoor.transform.position;
                if (greenKeyDoorVector == newObjectPos)
                {
                    Destroy(greenKeyDoor);
                }
            }
            int amountOfRedKeyDoors = redKeyDoorGroup.transform.childCount;
            for (int counter = 0; counter < amountOfRedKeyDoors; counter++)
            {
                GameObject redKeyDoor = redKeyDoorGroup.transform.GetChild(counter).gameObject;
                Vector2 redKeyDoorVector = redKeyDoor.transform.position;
                if (redKeyDoorVector == newObjectPos)
                {
                    Destroy(redKeyDoor);
                }
            }
            int amountOfBlueKeyDoors = blueKeyDoorGroup.transform.childCount;
            for (int counter = 0; counter < amountOfBlueKeyDoors; counter++)
            {
                GameObject blueKeyDoor = blueKeyDoorGroup.transform.GetChild(counter).gameObject;
                Vector2 blueKeyDoorVector = blueKeyDoor.transform.position;
                if (blueKeyDoorVector == newObjectPos)
                {
                    Destroy(blueKeyDoor);
                }
            }
            int amountOfGreenKeys = greenKeyGroup.transform.childCount;
            for (int counter = 0; counter < amountOfGreenKeys; counter++)
            {
                GameObject greenKey = greenKeyGroup.transform.GetChild(counter).gameObject;
                Vector2 greenKeyVector = greenKey.transform.position;
                if (greenKeyVector == newObjectPos)
                {
                    Destroy(greenKey);
                }
            }
            int amountOfRedKeys = redKeyGroup.transform.childCount;
            for (int counter = 0; counter < amountOfRedKeys; counter++)
            {
                GameObject redKey = redKeyGroup.transform.GetChild(counter).gameObject;
                Vector2 redKeyVector = redKey.transform.position;
                if (redKeyVector == newObjectPos)
                {
                    Destroy(redKey);
                }
            }
            int amountOfBlueKeys = blueKeyGroup.transform.childCount;
            for (int counter = 0; counter < amountOfBlueKeys; counter++)
            {
                GameObject blueKey = blueKeyGroup.transform.GetChild(counter).gameObject;
                Vector2 blueKeyVector = blueKey.transform.position;
                if (blueKeyVector == newObjectPos)
                {
                    Destroy(blueKey);
                }
            }
            int amountOfThrowBoxes = throwBoxGroup.transform.childCount;
            for (int counter = 0; counter < amountOfThrowBoxes; counter++)
            {
                GameObject throwBox = throwBoxGroup.transform.GetChild(counter).gameObject;
                Vector2 throwBoxVector = throwBox.transform.position;
                if (throwBoxVector == newObjectPos)
                {
                    Destroy(throwBox);
                }
            }
            int amountOfThrowBoxButtons = throwBoxButtonGroup.transform.childCount;
            for (int counter = 0; counter < amountOfThrowBoxButtons; counter++)
            {
                GameObject throwBoxButton = throwBoxButtonGroup.transform.GetChild(counter).gameObject;
                Vector2 throwBoxButtonVector = throwBoxButton.transform.position;
                if (throwBoxButtonVector == newObjectPos)
                {
                    Destroy(throwBoxButton);
                }
            }
            int amountOfUnknowns = unknownGroup.transform.childCount;
            for (int counter = 0; counter < amountOfUnknowns; counter++)
            {
                GameObject unknown = unknownGroup.transform.GetChild(counter).gameObject;
                Vector2 unknownVector = unknown.transform.position;
                if (unknownVector == newObjectPos)
                {
                    Destroy(unknown);
                }
            }
            int amountOfDiagTLBoxes = diagonalBoxTLGroup.transform.childCount;
            for (int counter = 0; counter < amountOfDiagTLBoxes; counter++)
            {
                GameObject diagBox = diagonalBoxTLGroup.transform.GetChild(counter).gameObject;
                Vector2 diagBoxVector = diagBox.transform.position;
                if (diagBoxVector == newObjectPos)
                {
                    Destroy(diagBox);
                }
            }
            int amountOfDiagTRBoxes = diagonalBoxTRGroup.transform.childCount;
            for (int counter = 0; counter < amountOfDiagTRBoxes; counter++)
            {
                GameObject diagBox = diagonalBoxTRGroup.transform.GetChild(counter).gameObject;
                Vector2 diagBoxVector = diagBox.transform.position;
                if (diagBoxVector == newObjectPos)
                {
                    Destroy(diagBox);
                }
            }
            int amountOfDiagBLBoxes = diagonalBoxBLGroup.transform.childCount;
            for (int counter = 0; counter < amountOfDiagBLBoxes; counter++)
            {
                GameObject diagBox = diagonalBoxBLGroup.transform.GetChild(counter).gameObject;
                Vector2 diagBoxVector = diagBox.transform.position;
                if (diagBoxVector == newObjectPos)
                {
                    Destroy(diagBox);
                }
            }
            int amountOfDiagBRBoxes = diagonalBoxBRGroup.transform.childCount;
            for (int counter = 0; counter < amountOfDiagBRBoxes; counter++)
            {
                GameObject diagBox = diagonalBoxBRGroup.transform.GetChild(counter).gameObject;
                Vector2 diagBoxVector = diagBox.transform.position;
                if (diagBoxVector == newObjectPos)
                {
                    Destroy(diagBox);
                }
            }
            int amountOfHalfBBoxes = halfBoxBGroup.transform.childCount;
            for (int counter = 0; counter < amountOfHalfBBoxes; counter++)
            {
                GameObject halfBox = halfBoxBGroup.transform.GetChild(counter).gameObject;
                Vector2 halfBoxVector = halfBox.transform.position;
                if (halfBoxVector == newObjectPos)
                {
                    Destroy(halfBox);
                }
            }
            int amountOfHalfRBoxes = halfBoxRGroup.transform.childCount;
            for (int counter = 0; counter < amountOfHalfRBoxes; counter++)
            {
                GameObject halfBox = halfBoxRGroup.transform.GetChild(counter).gameObject;
                Vector2 halfBoxVector = halfBox.transform.position;
                if (halfBoxVector == newObjectPos)
                {
                    Destroy(halfBox);
                }
            }
            int amountOfHalfTBoxes = halfBoxTGroup.transform.childCount;
            for (int counter = 0; counter < amountOfHalfTBoxes; counter++)
            {
                GameObject halfBox = halfBoxTGroup.transform.GetChild(counter).gameObject;
                Vector2 halfBoxVector = halfBox.transform.position;
                if (halfBoxVector == newObjectPos)
                {
                    Destroy(halfBox);
                }
            }
            int amountOfHalfLBoxes = halfBoxLGroup.transform.childCount;
            for (int counter = 0; counter < amountOfHalfLBoxes; counter++)
            {
                GameObject halfBox = halfBoxLGroup.transform.GetChild(counter).gameObject;
                Vector2 halfBoxVector = halfBox.transform.position;
                if (halfBoxVector == newObjectPos)
                {
                    Destroy(halfBox);
                }
            }
        }

        // Create object
        switch (placeObjectName)
        {
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
                newCloneMoveBox.transform.SetParent(moveBoxGroup.transform);
                break;
            case "powerUp":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newClonePowerUp = Instantiate(powerUpObject, new Vector2(mousePosX, mousePosY), Quaternion.identity);
                newClonePowerUp.transform.SetParent(powerUpGroup.transform);
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
                GameObject newCloneGreenKeyDoor = Instantiate(greenKeyDoorObject, new Vector2(mousePosX, mousePosY), Quaternion.identity);
                newCloneGreenKeyDoor.transform.SetParent(greenKeyDoorGroup.transform);
                break;
            case "redKeyDoor":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newCloneRedKeyDoor = Instantiate(redKeyDoorObject, new Vector2(mousePosX, mousePosY), Quaternion.identity);
                newCloneRedKeyDoor.transform.SetParent(redKeyDoorGroup.transform);
                break;
            case "blueKeyDoor":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newCloneBlueKeyDoor = Instantiate(blueKeyDoorObject, new Vector2(mousePosX, mousePosY), Quaternion.identity);
                newCloneBlueKeyDoor.transform.SetParent(blueKeyDoorGroup.transform);
                break;
            case "greenKey":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newCloneGreenKey = Instantiate(greenKeyObject, new Vector2(mousePosX, mousePosY), Quaternion.identity);
                newCloneGreenKey.transform.SetParent(greenKeyGroup.transform);
                break;
            case "redKey":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newCloneRedKey = Instantiate(redKeyObject, new Vector2(mousePosX, mousePosY), Quaternion.identity);
                newCloneRedKey.transform.SetParent(redKeyGroup.transform);
                break;
            case "blueKey":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newCloneBlueKey = Instantiate(blueKeyObject, new Vector2(mousePosX, mousePosY), Quaternion.identity);
                newCloneBlueKey.transform.SetParent(blueKeyGroup.transform);
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
                GameObject newCloneThrowBox = Instantiate(throwBoxObject, new Vector2(mousePosX, mousePosY), Quaternion.identity);
                newCloneThrowBox.transform.SetParent(throwBoxGroup.transform);
                break;
            case "throwBoxButton":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newCloneThrowBoxButton = Instantiate(throwBoxButtonObject, new Vector2(mousePosX, mousePosY), Quaternion.identity);
                newCloneThrowBoxButton.transform.SetParent(throwBoxButtonGroup.transform);
                break;
            case "throwBoxTile":
                throwBoxTileTilemap.SetTile(objectPositon, throwBoxTileTile);
                break;
            case "diagBoxBL":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newDiagBoxBL = Instantiate(diagonalBoxObject, new Vector2(mousePosX, mousePosY), Quaternion.identity);
                newDiagBoxBL.transform.SetParent(diagonalBoxBLGroup.transform);
                break;
            case "diagBoxBR":
                // Rotate this variants
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newDiagBoxBR = Instantiate(diagonalBoxObject, new Vector2(mousePosX, mousePosY), Quaternion.Euler(0, 0, 90));
                newDiagBoxBR.transform.SetParent(diagonalBoxBRGroup.transform);
                break;
            case "diagBoxTR":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newDiagBoxTR = Instantiate(diagonalBoxObject, new Vector2(mousePosX, mousePosY), Quaternion.Euler(0, 0, 180));
                newDiagBoxTR.transform.SetParent(diagonalBoxTRGroup.transform);
                break;
            case "diagBoxTL":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newDiagBoxTL = Instantiate(diagonalBoxObject, new Vector2(mousePosX, mousePosY), Quaternion.Euler(0, 0, 270));
                newDiagBoxTL.transform.SetParent(diagonalBoxTLGroup.transform);
                break;
            case "halfBoxB":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newHalfBoxB = Instantiate(halfBoxObject, new Vector2(mousePosX, mousePosY), Quaternion.identity);
                newHalfBoxB.transform.SetParent(halfBoxBGroup.transform);
                break;
            case "halfBoxR":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newHalfBoxR = Instantiate(halfBoxObject, new Vector2(mousePosX, mousePosY), Quaternion.Euler(0, 0, 90));
                newHalfBoxR.transform.SetParent(halfBoxRGroup.transform);
                break;
            case "halfBoxT":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newHalfBoxT = Instantiate(halfBoxObject, new Vector2(mousePosX, mousePosY), Quaternion.Euler(0, 0, 180));
                newHalfBoxT.transform.SetParent(halfBoxTGroup.transform);
                break;
            case "halfBoxL":
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newHalfBoxL = Instantiate(halfBoxObject, new Vector2(mousePosX, mousePosY), Quaternion.Euler(0, 0, 270));
                newHalfBoxL.transform.SetParent(halfBoxLGroup.transform);
                break;
            default:
                // Default object
                mousePosX *= objectScale;
                mousePosY *= objectScale;
                GameObject newUnknown = Instantiate(unknownObject, new Vector2(mousePosX, mousePosY), Quaternion.identity);
                newUnknown.transform.SetParent(unknownGroup.transform);
                break;
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        GetComponent<Transform>().position = new Vector2(mousePos.x, mousePos.y);

        // Place sprite when the player clicks
        if (Input.GetMouseButton(0))
        {
            PlaceObject((int)mousePos.x, (int)mousePos.y, false);
        }

        // Change object being placed
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            objectName = "air";
            createSprite.sprite = null;
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            objectName = "regularBox";
            createSprite.sprite = regularBoxImage;
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            objectName = "steelBox";
            createSprite.sprite = steelBoxImage;
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            objectName = "lavaBox";
            createSprite.sprite = lavaBoxImage;
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            objectName = "powerUp";
            createSprite.sprite = powerUpImage;
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            objectName = "player";
            createSprite.sprite = playerImage;
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            objectName = "goal";
            createSprite.sprite = goalImage;
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            // Go into more variants
            switch (objectName)
            {
                case "greenKeyDoor":
                    objectName = "redKeyDoor";
                    createSprite.sprite = redKeyDoorImage;
                    break;
                case "redKeyDoor":
                    objectName = "blueKeyDoor";
                    createSprite.sprite = blueKeyDoorImage;
                    break;
                default:
                    objectName = "greenKeyDoor";
                    createSprite.sprite = greenKeyDoorImage;
                    break;
            }
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            switch (objectName)
            {
                case "greenKey":
                    objectName = "redKey";
                    createSprite.sprite = redKeyImage;
                    break;
                case "redKey":
                    objectName = "blueKey";
                    createSprite.sprite = blueKeyImage;
                    break;
                default:
                    objectName = "greenKey";
                    createSprite.sprite = greenKeyImage;
                    break;
            }
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            objectName = "moveBox";
            createSprite.sprite = moveBoxImage;
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            objectName = "player2";
            createSprite.sprite = playerTwoImage;
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            objectName = "player3";
            createSprite.sprite = playerThreeImage;
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            objectName = "player4";
            createSprite.sprite = playerFourImage;
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            objectName = "throwBox";
            createSprite.sprite = throwBoxImage;
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            objectName = "throwBoxButton";
            createSprite.sprite = throwBoxButtonImage;
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            objectName = "throwBoxTile";
            createSprite.sprite = throwBoxTileImage;
            createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
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
                    createSprite.sprite = diagonalBoxImage;
                    createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
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
                    createSprite.sprite = halfBoxImage;
                    createSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
            }
        }
    }
}
