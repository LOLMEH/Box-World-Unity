using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuButtonMethods : MonoBehaviour
{
    public GameObject mainMenuScreen;
    public GameObject chooseGameScreen;
    public GameObject levelSelectScreen;
    public GameObject levelInfoScreen;
    private loadingLevelData loadingLevelData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the loading level data
        loadingLevelData = GameObject.FindGameObjectWithTag("LoadLevelInfo").GetComponent<loadingLevelData>();
        loadingLevelData.levelID = 1;

        // If the scene loads with info to go to the level select menu load it first
        if (loadingLevelData.gamemode.Equals("levelSelect"))
        {
            GoToLevelSelectMenu();
        }
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

    public void GoToLevelSelectMenu()
    {
        // Go to the level select menu from the main menu or level info scren
        mainMenuScreen.SetActive(false);
        levelInfoScreen.SetActive(false);
        levelSelectScreen.SetActive(true);
    }

    public void GoBackToMainMenu()
    {
        // Return to the main menu from any submenu
        chooseGameScreen.SetActive(false);
        levelSelectScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    public void GoToTwoPlayerGame()
    {
        // Start game scene (two player)
        loadingLevelData.gamemode = "twoplayer";
        SceneManager.LoadScene("GameScene");
    }
}