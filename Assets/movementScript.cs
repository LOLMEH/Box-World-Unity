using UnityEngine;

public class movementScript : MonoBehaviour
{
    public SpriteRenderer powerUpImage;
    public float maxSpeed;
    public Vector2 respawnPoint;
    public bool hasPowerUp;
    public int greenKeys;
    public int redKeys;
    public int blueKeys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Hide power up image on start
        powerUpImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D>();

        bool isUpButtonDown = Input.GetKey(KeyCode.W);
        bool isLeftButtonDown = Input.GetKey(KeyCode.A);
        bool isDownButtonDown = Input.GetKey(KeyCode.S);
        bool isRightButtonDown = Input.GetKey(KeyCode.D);
        bool isUpButtonUp = Input.GetKeyUp(KeyCode.W);
        bool isLeftButtonUp = Input.GetKeyUp(KeyCode.A);
        bool isDownButtonUp = Input.GetKeyUp(KeyCode.S);
        bool isRightButtonUp = Input.GetKeyUp(KeyCode.D);
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

        // Stop movement if a buttons is up
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
