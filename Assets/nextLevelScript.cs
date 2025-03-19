using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class nextLevelScript : MonoBehaviour
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
        BoxCollider2D goalCollider = GetComponent<BoxCollider2D>();

        if (goalCollider.IsTouching(playerCollider))
        {
            // Reset scene
            SceneManager.LoadScene("GameScene");
        }
    }
}
