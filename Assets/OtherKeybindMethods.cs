using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OtherKeybindMethods : MonoBehaviour
{
    public GameObject pauseMenu;
    private KeyCode resetKey;
    private KeyCode pauseKey;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set up pause menu keybinds
        loadingLevelData loadingLevelData = GameObject.FindGameObjectWithTag("LoadLevelInfo").GetComponent<loadingLevelData>();
        Dictionary<string, KeyCode> keybinds = loadingLevelData.keybinds;

        resetKey = keybinds["General.Reset"];
        pauseKey = keybinds["General.Pause"];
    }

    // Update is called once per frame
    void Update()
    {
        bool isResetButtonDown = Input.GetKeyDown(resetKey);
        bool isPauseButtonDown = Input.GetKeyDown(pauseKey);

        // Reset current level
        if (isResetButtonDown)
        {
            // Reload the game scene
            SceneManager.LoadScene("GameScene");
        }

        // Pause the game
        if (isPauseButtonDown)
        {
            // Toggle the pause menu
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }
}
