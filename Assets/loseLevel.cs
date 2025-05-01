using UnityEngine;
using UnityEngine.SceneManagement;

public class loseLevel : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if player is touching object
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reset scene on touch
            SceneManager.LoadScene("GameScene");
        }
    }
}
