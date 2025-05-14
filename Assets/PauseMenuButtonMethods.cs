using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtonMethods : MonoBehaviour
{
    public void GoToMainMenuScene()
    {
        // Start main menu scene
        SceneManager.LoadScene("MainMenuScene");
    }
    public void ResetLevel()
    {
        // Start game scene
        SceneManager.LoadScene("GameScene");
    }
}