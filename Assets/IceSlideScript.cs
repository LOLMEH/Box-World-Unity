using UnityEngine;

public class IceSlideScript : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the player is on ice
        GameObject collidingObject = collision.gameObject;
        if (collidingObject.CompareTag("Player"))
        {
            if (collidingObject.GetComponent<Rigidbody2D>().linearVelocity == new Vector2(0, 0)
                && collision.GetComponent<movementScript>().isOnIce == true)
            {
                // If the player is on ice and not moving then set the player as stuck
                collision.GetComponent<movementScript>().isStuckOnIce = true;
            }
            else if (collision.GetComponent<movementScript>().isOnIce == false)
            {
                collision.GetComponent<movementScript>().isOnIce = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player is on ice
        GameObject collidingObject = collision.gameObject;
        if (collidingObject.CompareTag("Player"))
        {
            collision.GetComponent<movementScript>().isOnIce = false;
        }
    }
}
