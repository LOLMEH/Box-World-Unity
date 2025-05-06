using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuButtonMethods : MonoBehaviour
{
    public void goToGame()
    {
        // Start game scene
        print("test");
        SceneManager.LoadScene("GameScene");
    }

    public void goToLevelCreator()
    {
        // Start create scene
        SceneManager.LoadScene("CreateScene");
    }
}