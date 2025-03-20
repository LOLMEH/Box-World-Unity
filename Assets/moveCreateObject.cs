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
    public Tilemap powerUpTilemap;
    public TileBase powerUpTile;
    public Sprite powerUpImage;
    public Tilemap greenKeyDoorTilemap;
    public TileBase greenKeyDoorTile;
    public Sprite greenKeyDoorImage;
    public Tilemap blueKeyDoorTilemap;
    public TileBase blueKeyDoorTile;
    public Sprite blueKeyDoorImage;
    public Tilemap redKeyDoorTilemap;
    public TileBase redKeyDoorTile;
    public Sprite redKeyDoorImage;
    public Tilemap greenKeyTilemap;
    public TileBase greenKeyTile;
    public Sprite greenKeyImage;
    public Tilemap blueKeyTilemap;
    public TileBase blueKeyTile;
    public Sprite blueKeyImage;
    public Tilemap redKeyTilemap;
    public TileBase redKeyTile;
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
        if (Input.GetMouseButton(0))
        {
            // Get the box position on the tilemap grid
            int gridOffsetX = 4;
            int gridOffsetY = 2;
            Vector3Int objectPositon = new Vector3Int(
                (int)mousePos.x / gridScale + gridOffsetX,
                (int)mousePos.y / gridScale + gridOffsetY
            );

            // Delete object if space is occupied
            if (regularBoxTilemap.GetTile(objectPositon))
            {
                regularBoxTilemap.SetTile(objectPositon, null);
            }
            else if (steelBoxTilemap.GetTile(objectPositon))
            {
                steelBoxTilemap.SetTile(objectPositon, null);
            }
            else if (lavaBoxTilemap.GetTile(objectPositon))
            {
                lavaBoxTilemap.SetTile(objectPositon, null);
            }
            else if (powerUpTilemap.GetTile(objectPositon))
            {
                powerUpTilemap.SetTile(objectPositon, null);
            }
            else if (greenKeyDoorTilemap.GetTile(objectPositon))
            {
                greenKeyDoorTilemap.SetTile(objectPositon, null);
            }
            else if (redKeyDoorTilemap.GetTile(objectPositon))
            {
                redKeyDoorTilemap.SetTile(objectPositon, null);
            }
            else if (blueKeyDoorTilemap.GetTile(objectPositon))
            {
                blueKeyDoorTilemap.SetTile(objectPositon, null);
            }
            else if (greenKeyTilemap.GetTile(objectPositon))
            {
                greenKeyTilemap.SetTile(objectPositon, null);
            }
            else if (redKeyTilemap.GetTile(objectPositon))
            {
                redKeyTilemap.SetTile(objectPositon, null);
            }
            else if (blueKeyTilemap.GetTile(objectPositon))
            {
                blueKeyTilemap.SetTile(objectPositon, null);
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
                    powerUpTilemap.SetTile(objectPositon, lavaBoxTile);
                    break;
                case "player":
                    playerMarker.transform.position = new Vector2(mousePos.x, mousePos.y);
                    break;
                case "goal":
                    goalMarker.transform.position = new Vector2(mousePos.x, mousePos.y);
                    break;
                case "greenKeyDoor":
                    greenKeyDoorTilemap.SetTile(objectPositon, greenKeyDoorTile);
                    break;
                case "redKeyDoor":
                    redKeyDoorTilemap.SetTile(objectPositon, redKeyDoorTile);
                    break;
                case "blueKeyDoor":
                    blueKeyDoorTilemap.SetTile(objectPositon, blueKeyDoorTile);
                    break;
                case "greenKey":
                    greenKeyTilemap.SetTile(objectPositon, greenKeyTile);
                    break;
                case "redKey":
                    redKeyTilemap.SetTile(objectPositon, redKeyTile);
                    break;
                case "blueKey":
                    blueKeyTilemap.SetTile(objectPositon, blueKeyTile);
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
