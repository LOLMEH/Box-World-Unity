using System.Collections.Generic;
using UnityEngine;

public class togglePauseGUI : MonoBehaviour
{
    public GameObject pauseGui;
    public GameObject copyLevelGui;
    public GameObject saveLevelGui;
    public GameObject createCursor;
    private KeyCode pauseKey;

    /// <summary>
    /// Moves the player to the copy level screen from the level save screen
    /// </summary>
    public void GoToCopyLevelScreen()
    {
        pauseGui.SetActive(false);
        copyLevelGui.SetActive(true);
    }

    /// <summary>
    /// Moves the player to the save level screen from the level save screen
    /// </summary>
    public void GoToSaveLevelScreen()
    {
        pauseGui.SetActive(false);
        saveLevelGui.SetActive(true);
    }

    /// <summary>
    /// Toggles the pause GUI on and off
    /// </summary>
    public void TogglePauseGUI()
    {
        // Hide the save and copy level gui
        saveLevelGui.SetActive(false);
        copyLevelGui.SetActive(false);
        if (pauseGui.activeSelf == false)
        {
            // Show the pause gui
            pauseGui.SetActive(true);
            createCursor.SetActive(false);
        }
        else
        {
            // Hide the pause gui
            pauseGui.SetActive(false);
            createCursor.SetActive(true);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the pause keybind
        loadingLevelData loadingLevelData = GameObject.FindGameObjectWithTag("LoadLevelInfo").GetComponent<loadingLevelData>();
        Dictionary<string, KeyCode> keybinds = loadingLevelData.keybinds;

        pauseKey = keybinds["General.Pause"];
    }

    // Update is called once per frame
    void Update()
    {
        // Hide GUI and disable editing
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePauseGUI();
        }
    }
}
