using UnityEngine;

public class getPowerUp : MonoBehaviour
{
    public CompositeCollider2D regularBoxCollider;
    public BoxCollider2D regularHalfBoxCollider;
    public PolygonCollider2D regularDiagBoxCollider;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if a player is touching the object
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player that touched the powerup
            movementScript player = collision.gameObject.GetComponent<movementScript>();
            // Check if the player does not already have the powerup
            if (player.hasPowerUp == false)
            {
                // Enable power up for the player
                Collider2D playerCollider = collision.collider;
                player.hasPowerUp = true;
                player.powerUpImage.enabled = true;
                // Disable collisions with regular boxes
                Physics2D.IgnoreCollision(playerCollider, regularBoxCollider, true);
                GameObject[] boxObjects = GameObject.FindGameObjectsWithTag("PhaseForPowerUp");
                for (int counter = 0;  counter < boxObjects.Length; ++counter)
                {
                    GameObject boxObject = boxObjects[counter];
                    Collider2D boxCollider = boxObject.GetComponent<Collider2D>();
                    Physics2D.IgnoreCollision(playerCollider, boxCollider, true);
                }
                // Destroy power up
                Destroy(gameObject);
            }
        }
    }
}
