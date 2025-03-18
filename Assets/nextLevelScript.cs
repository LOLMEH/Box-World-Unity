using UnityEngine;
using UnityEngine.Tilemaps;

public class nextLevelScript : MonoBehaviour
{
    public GameObject player;
    public GameObject gameCamera;
    public GameObject gameBackground;
    public CompositeCollider2D regularBoxCollider;
    public Vector2 newLevelPlayerPositon;
    public int newLevelCameraX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
        BoxCollider2D goalCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer playerPowerUpImage = player.GetComponent<movementScript>().powerUpImage;
        movementScript playerScript = player.GetComponent<movementScript>();

        if (goalCollider.IsTouching(playerCollider))
        {
            // Update object positions
            gameCamera.transform.position = new Vector3(newLevelCameraX, 0, -10);
            gameBackground.transform.position = new Vector3 (newLevelCameraX, 0, 10);
            player.GetComponent<movementScript>().respawnPoint = newLevelPlayerPositon;
            player.transform.position = newLevelPlayerPositon;
            // Remove power up from player
            playerScript.hasPowerUp = false;
            playerPowerUpImage.enabled = false;
            Physics2D.IgnoreCollision(playerCollider, regularBoxCollider, false);
            // Destroy goal
            Destroy(gameObject);
        }
    }
}
