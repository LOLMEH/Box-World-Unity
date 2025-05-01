using UnityEngine;

public class getGreenKey : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if a player is touching the object
        if (collision.gameObject.CompareTag("Player"))
        {
            // Give the key to the player
            GameObject player = collision.gameObject;
            player.GetComponent<movementScript>().greenKeys++;
            Destroy(gameObject);
        }
    }
}
