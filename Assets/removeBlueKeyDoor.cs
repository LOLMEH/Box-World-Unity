using UnityEngine;

public class removeBlueKeyDoor : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if a player is touching the object
        if (collision.gameObject.CompareTag("Player"))
        {
            // Remove the key door if the player has more than one key
            GameObject player = collision.gameObject;
            int playerBlueKeys = player.GetComponent<movementScript>().blueKeys;

            if (playerBlueKeys > 0)
            {
                player.GetComponent<movementScript>().blueKeys--;
                Destroy(gameObject);
            }
        }
    }
}
