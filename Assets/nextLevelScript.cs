using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevelScript : MonoBehaviour
{
    public createLevel levelInfo;
    public int playersPassed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playersPassed = 0;
    }

    private void goToNextLevel()
    {
        // Reset scene with new level id and save level loading information
        loadingLevelData loadingLevelData = GameObject.FindGameObjectWithTag("LoadLevelInfo").GetComponent<loadingLevelData>();
        loadingLevelData.GetComponent<loadingLevelData>().levelID++;
        SceneManager.LoadScene("GameScene");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if player is touching object
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get and remove player
            GameObject player = collision.gameObject;
            playersPassed++;
            Destroy(player);

            // Check if the right amount of players have passed the level
            int playerCount = levelInfo.playerCount;

            if (playersPassed == playerCount)
            {
                goToNextLevel();
            }
        }
    }
}
