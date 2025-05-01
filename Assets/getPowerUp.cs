using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class getPowerUp : MonoBehaviour
{
    public CompositeCollider2D regularBoxCollider;

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
                Physics2D.IgnoreCollision(playerCollider, regularBoxCollider, true);
                // Destroy power up
                Destroy(gameObject);
            }
        }
    }
}
