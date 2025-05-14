using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuButtonMethods : MonoBehaviour
{
    public GameObject mainMenuScreen;
    public GameObject chooseGameScreen;
    private loadingLevelData loadingLevelData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loadingLevelData = GameObject.FindGameObjectWithTag("LoadLevelInfo").GetComponent<loadingLevelData>();
    }

    public void goToGame()
    {
        // Start game scene (singleplayer)
        loadingLevelData.gamemode = "singleplayer";
        SceneManager.LoadScene("GameScene");
    }

    public void goToLevelCreator()
    {
        // Start create scene
        SceneManager.LoadScene("CreateScene");
    }

    public void GoToChooseGameMenu()
    {
        // Go to the choose game menu from the main menu
        mainMenuScreen.SetActive(false);
        chooseGameScreen.SetActive(true);
    }

    public void GoBackToMainMenu()
    {
        // Return to the main menu
        chooseGameScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    public void GoToTwoPlayerGame()
    {
        // Start game scene (singleplayer)
        loadingLevelData.gamemode = "twoplayer";
        SceneManager.LoadScene("GameScene");
    }
}