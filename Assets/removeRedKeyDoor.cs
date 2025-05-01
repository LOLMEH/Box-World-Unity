using UnityEngine;

public class removeRedKeyDoor : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if a player is touching the object
        if (collision.gameObject.CompareTag("Player"))
        {
            // Remove the key door if the player has more than one key
            GameObject player = collision.gameObject;
            int playerRedKeys = player.GetComponent<movementScript>().redKeys;

            if (playerRedKeys > 0)
            {
                player.GetComponent<movementScript>().redKeys--;
                Destroy(gameObject);
            }
        }
    }
}
