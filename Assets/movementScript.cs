using UnityEngine;
using UnityEngine.SceneManagement;

public class movementScript : MonoBehaviour
{
    public int playerNumber;
    public SpriteRenderer powerUpImage;
    public float maxSpeed;
    public Vector2 respawnPoint;
    public bool hasPowerUp;
    public int greenKeys;
    public int redKeys;
    public int blueKeys;
    private KeyCode upKey;
    private KeyCode downKey;
    private KeyCode leftKey;
    private KeyCode rightKey;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Hide power up image on start
        powerUpImage.enabled = false;

        // Disable collision with all other players
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int counter = 0; counter < players.Length; counter++)
        {
            GameObject player = players[counter];
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        // Get the player's controls depending on their player number
        switch (playerNumber)
        {
            case 1:
                upKey = KeyCode.W;
                leftKey = KeyCode.A;
                downKey = KeyCode.S;
                rightKey = KeyCode.D;
                break;
            case 2:
                upKey = KeyCode.UpArrow;
                leftKey = KeyCode.LeftArrow;
                downKey = KeyCode.DownArrow;
                rightKey = KeyCode.RightArrow;
                break;
            case 3:
                upKey = KeyCode.T;
                leftKey = KeyCode.F;
                downKey = KeyCode.G;
                rightKey = KeyCode.H;
                break;
            case 4:
                upKey = KeyCode.I;
                leftKey = KeyCode.J;
                downKey = KeyCode.K;
                rightKey = KeyCode.L;
                break;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        // Stop moving the player when a collision stops
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D>();

        bool isUpButtonDown = Input.GetKey(upKey);
        bool isLeftButtonDown = Input.GetKey(leftKey);
        bool isDownButtonDown = Input.GetKey(downKey);
        bool isRightButtonDown = Input.GetKey(rightKey);
        bool isUpButtonUp = Input.GetKeyUp(upKey);
        bool isLeftButtonUp = Input.GetKeyUp(leftKey);
        bool isDownButtonUp = Input.GetKeyUp(downKey);
        bool isRightButtonUp = Input.GetKeyUp(rightKey);
        bool isAnyButtonUp = isUpButtonUp || isLeftButtonUp || isDownButtonUp || isRightButtonUp;

        // Move up
        if (isUpButtonDown && !isUpButtonUp)
        {
            rigidBody2D.AddForce(Vector2.up, ForceMode2D.Impulse);
        }

        // Move left
        if (isLeftButtonDown && !isLeftButtonUp)
        {
            rigidBody2D.AddForce(-Vector2.right, ForceMode2D.Impulse);
        }

        // Move down
        if (isDownButtonDown && !isDownButtonUp)
        {
            rigidBody2D.AddForce(-Vector2.up, ForceMode2D.Impulse);
        }

        // Move right
        if (isRightButtonDown && !isRightButtonUp)
        {
            rigidBody2D.AddForce(Vector2.right, ForceMode2D.Impulse);
        }

        // Stop movement if a button is up
        if (isAnyButtonUp)
        {
            rigidBody2D.linearVelocity = Vector2.zero;
        }

        // Cap at max speed (+X)
        if (rigidBody2D.linearVelocityX > maxSpeed)
        {
            rigidBody2D.linearVelocityX = maxSpeed;
        }

        // Cap at max speed (-X)
        if (rigidBody2D.linearVelocityX < -maxSpeed)
        {
            rigidBody2D.linearVelocityX = -maxSpeed;
        }

        // Cap at max speed (+Y)
        if (rigidBody2D.linearVelocityY > maxSpeed)
        {
            rigidBody2D.linearVelocityY = maxSpeed;
        }

        // Cap at max speed (-Y)
        if (rigidBody2D.linearVelocityY < -maxSpeed)
        {
            rigidBody2D.linearVelocityY = -maxSpeed;
        }
    }
}
