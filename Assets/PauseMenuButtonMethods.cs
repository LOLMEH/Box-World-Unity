using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtonMethods : MonoBehaviour
{
    private loadingLevelData loadingLevelData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loadingLevelData = GameObject.FindGameObjectWithTag("LoadLevelInfo").GetComponent<loadingLevelData>();
    }

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

    public void GoToLevelSelectMenu()
    {
        // Start main menu scene and ask to go to the level select menu
        loadingLevelData.gamemode = "levelSelect";
        SceneManager.LoadScene("MainMenuScene");
    }
}