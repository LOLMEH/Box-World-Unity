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
        resetKey = KeyCode.R;
        pauseKey = KeyCode.Escape;
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
