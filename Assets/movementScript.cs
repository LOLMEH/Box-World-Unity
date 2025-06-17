using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    public int playerNumber;
    public SpriteRenderer powerUpImage;
    public float maxSpeed;
    public Vector2 respawnPoint;
    public bool hasPowerUp;
    public Dictionary<int, int> keysHeld = new();
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
        // Moves the player depending on the x and y values
        void movePlayer(Rigidbody2D rigidBody2D, float x, float y)
        {
            rigidBody2D.linearVelocity = new Vector2(x, y);
        }
        
        Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D>();

        // Press button variables
        bool isUpButtonDown = Input.GetKey(upKey);
        bool isLeftButtonDown = Input.GetKey(leftKey);
        bool isDownButtonDown = Input.GetKey(downKey);
        bool isRightButtonDown = Input.GetKey(rightKey);
        bool isUpButtonUp = Input.GetKeyUp(upKey);
        bool isLeftButtonUp = Input.GetKeyUp(leftKey);
        bool isDownButtonUp = Input.GetKeyUp(downKey);
        bool isRightButtonUp = Input.GetKeyUp(rightKey);

        // Hold button variables
        bool upButtonHold = isUpButtonDown && !isUpButtonUp;
        bool downButtonHold = isDownButtonDown && !isDownButtonUp;
        bool leftButtonHold = isLeftButtonDown && !isLeftButtonUp;
        bool rightButtonHold = isRightButtonDown && !isRightButtonUp;

        // Exclusive hold button variables
        bool upButtonHoldOnly = upButtonHold && !downButtonHold && !leftButtonHold && !rightButtonHold;
        bool downButtonHoldOnly = !upButtonHold && downButtonHold && !leftButtonHold && !rightButtonHold;
        bool leftButtonHoldOnly = !upButtonHold && !downButtonHold && leftButtonHold && !rightButtonHold;
        bool rightButtonHoldOnly = !upButtonHold && !downButtonHold && !leftButtonHold && rightButtonHold;
        bool upLeftButtonHoldOnly = upButtonHold && !downButtonHold && leftButtonHold && !rightButtonHold;
        bool downLeftButtonHoldOnly = !upButtonHold && downButtonHold && leftButtonHold && !rightButtonHold;
        bool upRightButtonHoldOnly = upButtonHold && !downButtonHold && !leftButtonHold && rightButtonHold;
        bool downRightButtonHoldOnly = !upButtonHold && downButtonHold && !leftButtonHold && rightButtonHold;

        if (upLeftButtonHoldOnly)
        {
            // Move up left
            movePlayer(rigidBody2D, -maxSpeed, maxSpeed);
        }
        else if (downLeftButtonHoldOnly)
        {
            // Move down left
            movePlayer(rigidBody2D, -maxSpeed, -maxSpeed);
        }
        else if (upRightButtonHoldOnly)
        {
            // Move up right
            movePlayer(rigidBody2D, maxSpeed, maxSpeed);
        }
        else if (downRightButtonHoldOnly)
        {
            // Move down right
            movePlayer(rigidBody2D, maxSpeed, -maxSpeed);
        }
        else if (upButtonHoldOnly)
        {
            // Move up
            movePlayer(rigidBody2D, 0, maxSpeed);
        }
        else if (leftButtonHoldOnly)
        {
            // Move left
            movePlayer(rigidBody2D, -maxSpeed, 0);
        }
        else if (downButtonHoldOnly)
        {
            // Move down
            movePlayer(rigidBody2D, 0, -maxSpeed);
        }
        else if (rightButtonHoldOnly)
        {
            // Move right
            movePlayer(rigidBody2D, maxSpeed, 0);
        }
        else
        {
            // Stop movement if no buttons are down (or if no valid movements are made)
            movePlayer(rigidBody2D, 0, 0);
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
