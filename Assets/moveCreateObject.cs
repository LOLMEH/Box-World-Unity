using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class moveCreateObject : MonoBehaviour
{
    public int gridScale;
    public GameObject playerMarker;
    public Sprite playerImage;
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
    private string objectName;
    private SpriteRenderer createSprite;


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

        // Place sprite
        if (Input.GetMouseButtonUp(0))
        {
            // Get the box position on the tilemap grid
            int gridOffsetX = 4;
            int gridOffsetY = 2;
            Vector3Int objectPositon = new Vector3Int(
                (int)mousePos.x / gridScale + gridOffsetX,
                (int)mousePos.y / gridScale + gridOffsetY
            );

            // Check if the placed object can go though tiles
            string[] objectThatCanGoInTiles = { "powerUp", "greenKey", "redKey", "blueKey" };
            bool canPlacedObjectGoInTiles = false;
            for (int counter = 0; counter < objectThatCanGoInTiles.Length; counter++)
            {
                if (objectName == objectThatCanGoInTiles[counter])
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
            else
            {
                // Special check case for non-tile objects
                Vector2 newObjectPos = new Vector2(mousePos.x, mousePos.y);
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
            }

            // Create object
            switch (objectName)
            {
                case "regularBox":
                    regularBoxTilemap.SetTile(objectPositon, regularBoxTile);
                    break;
                case "steelBox":
                    steelBoxTilemap.SetTile(objectPositon, steelBoxTile);
                    break;
                case "lavaBox":
                    lavaBoxTilemap.SetTile(objectPositon, lavaBoxTile);
                    break;
                case "powerUp":
                    GameObject newClonePowerUp = Instantiate(powerUpObject, new Vector2(mousePos.x, mousePos.y), Quaternion.identity);
                    newClonePowerUp.transform.SetParent(powerUpGroup.transform);
                    break;
                case "player":
                    playerMarker.transform.position = new Vector2(mousePos.x, mousePos.y);
                    break;
                case "goal":
                    goalMarker.transform.position = new Vector2(mousePos.x, mousePos.y);
                    break;
                case "greenKeyDoor":
                    GameObject newCloneGreenKeyDoor = Instantiate(greenKeyDoorObject, new Vector2(mousePos.x, mousePos.y), Quaternion.identity);
                    newCloneGreenKeyDoor.transform.SetParent(greenKeyDoorGroup.transform);
                    break;
                case "redKeyDoor":
                    GameObject newCloneRedKeyDoor = Instantiate(redKeyDoorObject, new Vector2(mousePos.x, mousePos.y), Quaternion.identity);
                    newCloneRedKeyDoor.transform.SetParent(redKeyDoorGroup.transform);
                    break;
                case "blueKeyDoor":
                    GameObject newCloneBlueKeyDoor = Instantiate(blueKeyDoorObject, new Vector2(mousePos.x, mousePos.y), Quaternion.identity);
                    newCloneBlueKeyDoor.transform.SetParent(blueKeyDoorGroup.transform);
                    break;
                case "greenKey":
                    GameObject newCloneGreenKey = Instantiate(greenKeyObject, new Vector2(mousePos.x, mousePos.y), Quaternion.identity);
                    newCloneGreenKey.transform.SetParent(greenKeyGroup.transform);
                    break;
                case "redKey":
                    GameObject newCloneRedKey = Instantiate(redKeyObject, new Vector2(mousePos.x, mousePos.y), Quaternion.identity);
                    newCloneRedKey.transform.SetParent(redKeyGroup.transform);
                    break;
                case "blueKey":
                    GameObject newCloneBlueKey = Instantiate(blueKeyObject, new Vector2(mousePos.x, mousePos.y), Quaternion.identity);
                    newCloneBlueKey.transform.SetParent(blueKeyGroup.transform);
                    break;
            }
        }

        // Change object being placed
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            objectName = "air";
            createSprite.sprite = null;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            objectName = "regularBox";
            createSprite.sprite = regularBoxImage;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            objectName = "steelBox";
            createSprite.sprite = steelBoxImage;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            objectName = "lavaBox";
            createSprite.sprite = lavaBoxImage;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            objectName = "powerUp";
            createSprite.sprite = powerUpImage;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            objectName = "player";
            createSprite.sprite = playerImage;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            objectName = "goal";
            createSprite.sprite = goalImage;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            objectName = "greenKeyDoor";
            createSprite.sprite = greenKeyDoorImage;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            objectName = "redKeyDoor";
            createSprite.sprite = redKeyDoorImage;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            objectName = "blueKeyDoor";
            createSprite.sprite = blueKeyDoorImage;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            objectName = "greenKey";
            createSprite.sprite = greenKeyImage;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            objectName = "redKey";
            createSprite.sprite = redKeyImage;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            objectName = "blueKey";
            createSprite.sprite = blueKeyImage;
        }
    }
}
