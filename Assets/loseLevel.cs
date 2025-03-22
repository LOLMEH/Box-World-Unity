using UnityEngine;
using UnityEngine.SceneManagement;

public class loseLevel : MonoBehaviour
{
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
        CompositeCollider2D lavaBoxCollider = GetComponent<CompositeCollider2D>();

        if (lavaBoxCollider.IsTouching(playerCollider))
        {
            // Reset scene on touch
            SceneManager.LoadScene("GameScene");
        }
    }
}
