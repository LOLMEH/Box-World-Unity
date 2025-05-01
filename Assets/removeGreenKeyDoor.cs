using UnityEngine;

public class removeGreenKeyDoor : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if a player is touching the object
        if (collision.gameObject.CompareTag("Player"))
        {
            // Remove the key door if the player has more than one key
            GameObject player = collision.gameObject;
            int playerGreenKeys = player.GetComponent<movementScript>().greenKeys;

            if (playerGreenKeys > 0)
            {
                player.GetComponent<movementScript>().greenKeys--;
                Destroy(gameObject);
            }
        }
    }
}
