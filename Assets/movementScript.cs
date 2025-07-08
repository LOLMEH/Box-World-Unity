using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    public int playerNumber;
    public SpriteRenderer powerUpImage;
    public float movementSpeed;
    private float speedMultiplier = 1;
    public Vector2 respawnPoint;
    public bool hasPowerUp;
    public bool isOnIce = false;
    public bool isStuckOnIce = false;
    public Dictionary<int, int> keysHeld = new();
    private Rigidbody2D rigidBody2D;
    private KeyCode upKey;
    private KeyCode downKey;
    private KeyCode leftKey;
    private KeyCode rightKey;
    private KeyCode slowWalkKey;

    /// <summary>
    /// Resets the player's keybinds
    /// </summary>
    public void ResetKeyBinds()
    {
        // Get the keybinds
        loadingLevelData loadingLevelData = GameObject.FindGameObjectWithTag("LoadLevelInfo").GetComponent<loadingLevelData>();
        Dictionary<string, KeyCode> keybinds = loadingLevelData.keybinds;

        // Get the player's controls depending on their player number
        switch (playerNumber)
        {
            case 1:
                upKey = keybinds["PlayerOne.MoveUp"];
                leftKey = keybinds["PlayerOne.MoveLeft"];
                downKey = keybinds["PlayerOne.MoveDown"];
                rightKey = keybinds["PlayerOne.MoveRight"];
                slowWalkKey = keybinds["PlayerOne.SlowWalk"];
                break;
            case 2:
                upKey = keybinds["PlayerTwo.MoveUp"];
                leftKey = keybinds["PlayerTwo.MoveLeft"];
                downKey = keybinds["PlayerTwo.MoveDown"];
                rightKey = keybinds["PlayerTwo.MoveRight"];
                slowWalkKey = keybinds["PlayerTwo.SlowWalk"];
                break;
            case 3:
                upKey = keybinds["PlayerThree.MoveUp"];
                leftKey = keybinds["PlayerThree.MoveLeft"];
                downKey = keybinds["PlayerThree.MoveDown"];
                rightKey = keybinds["PlayerThree.MoveRight"];
                slowWalkKey = keybinds["PlayerThree.SlowWalk"];
                break;
            case 4:
                upKey = keybinds["PlayerFour.MoveUp"];
                leftKey = keybinds["PlayerFour.MoveLeft"];
                downKey = keybinds["PlayerFour.MoveDown"];
                rightKey = keybinds["PlayerFour.MoveRight"];
                slowWalkKey = keybinds["PlayerFour.SlowWalk"];
                break;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Hide power up image on start
        powerUpImage.enabled = false;

        // Get the rigid body
        rigidBody2D = GetComponent<Rigidbody2D>();

        // Disable collision with all other players
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int counter = 0; counter < players.Length; counter++)
        {
            GameObject player = players[counter];
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        // Get the player's controls
        ResetKeyBinds();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Allow movement if the player is pressed against a wall due to sliding on ice
        GameObject collidingObject = collision.gameObject;
        if (
                (collidingObject.CompareTag("StopWall") || collidingObject.CompareTag("ThrowBoxButton")
                || collidingObject.CompareTag("PhaseForPowerUp"))
                && isOnIce == true
            )
        {
            isStuckOnIce = true;
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
        // Moves the player depending on the x and y values
        void movePlayer(float x, float y)
        {
            // Move the player
            rigidBody2D.linearVelocity = new Vector2(x, y);

            // Make the player not stuck anymore
            if (isStuckOnIce)
            {
                isStuckOnIce = false;
            }
        }
        

        // Press button variables
        bool isUpButtonDown = Input.GetKey(upKey);
        bool isLeftButtonDown = Input.GetKey(leftKey);
        bool isDownButtonDown = Input.GetKey(downKey);
        bool isRightButtonDown = Input.GetKey(rightKey);
        bool isSlowWalkButtonDown = Input.GetKey(slowWalkKey);
        bool isUpButtonUp = Input.GetKeyUp(upKey);
        bool isLeftButtonUp = Input.GetKeyUp(leftKey);
        bool isDownButtonUp = Input.GetKeyUp(downKey);
        bool isRightButtonUp = Input.GetKeyUp(rightKey);
        bool isSlowWalkButtonUp = Input.GetKeyUp(slowWalkKey);

        // Hold button variables
        bool upButtonHold = isUpButtonDown && !isUpButtonUp;
        bool downButtonHold = isDownButtonDown && !isDownButtonUp;
        bool leftButtonHold = isLeftButtonDown && !isLeftButtonUp;
        bool rightButtonHold = isRightButtonDown && !isRightButtonUp;
        bool slowWalkButtonHold = isSlowWalkButtonDown && !isSlowWalkButtonUp;

        // Exclusive hold button variables
        bool upButtonHoldOnly = upButtonHold && !downButtonHold && !leftButtonHold && !rightButtonHold;
        bool downButtonHoldOnly = !upButtonHold && downButtonHold && !leftButtonHold && !rightButtonHold;
        bool leftButtonHoldOnly = !upButtonHold && !downButtonHold && leftButtonHold && !rightButtonHold;
        bool rightButtonHoldOnly = !upButtonHold && !downButtonHold && !leftButtonHold && rightButtonHold;
        bool upLeftButtonHoldOnly = upButtonHold && !downButtonHold && leftButtonHold && !rightButtonHold;
        bool downLeftButtonHoldOnly = !upButtonHold && downButtonHold && leftButtonHold && !rightButtonHold;
        bool upRightButtonHoldOnly = upButtonHold && !downButtonHold && !leftButtonHold && rightButtonHold;
        bool downRightButtonHoldOnly = !upButtonHold && downButtonHold && !leftButtonHold && rightButtonHold;

        // Movement controls
        float speed = movementSpeed * speedMultiplier;

        // Only allow movement if the player is not on ice or if the player is stuck on a wall due to ice
        if (isOnIce == false || isStuckOnIce == true)
        {
            if (slowWalkButtonHold)
            {
                // Slow the player down
                speedMultiplier = 0.5f;
            }
            else
            {
                speedMultiplier = 1;
            }

            if (upLeftButtonHoldOnly)
            {
                // Move up left
                movePlayer(-speed, speed);
            }
            else if (downLeftButtonHoldOnly)
            {
                // Move down left
                movePlayer(-speed, -speed);
            }
            else if (upRightButtonHoldOnly)
            {
                // Move up right
                movePlayer(speed, speed);
            }
            else if (downRightButtonHoldOnly)
            {
                // Move down right
                movePlayer(speed, -speed);
            }
            else if (upButtonHoldOnly)
            {
                // Move up
                movePlayer(0, speed);
            }
            else if (leftButtonHoldOnly)
            {
                // Move left
                movePlayer(-speed, 0);
            }
            else if (downButtonHoldOnly)
            {
                // Move down
                movePlayer(0, -speed);
            }
            else if (rightButtonHoldOnly)
            {
                // Move right
                movePlayer(speed, 0);
            }
            else
            {
                // Stop movement if no buttons are down and the player is not on ice (or if no valid movements are made)
                rigidBody2D.linearVelocity = new Vector2(0, 0);
            }
        }

        // Cap at max speed (+X)
        if (rigidBody2D.linearVelocityX > speed)
        {
            rigidBody2D.linearVelocityX = speed;
        }

        // Cap at max speed (-X)
        if (rigidBody2D.linearVelocityX < -speed)
        {
            rigidBody2D.linearVelocityX = -speed;
        }

        // Cap at max speed (+Y)
        if (rigidBody2D.linearVelocityY > speed)
        {
            rigidBody2D.linearVelocityY = speed;
        }

        // Cap at max speed (-Y)
        if (rigidBody2D.linearVelocityY < -speed)
        {
            rigidBody2D.linearVelocityY = -speed;
        }
    }
}
