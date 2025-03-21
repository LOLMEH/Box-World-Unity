using UnityEngine;

public class togglePauseGUI : MonoBehaviour
{
    public GameObject pauseGui;
    public GameObject createCursor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Hide GUI and disable editing
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGui.activeSelf == false )
            {
                pauseGui.SetActive(true);
                createCursor.SetActive(false);
            } else
            {
                pauseGui.SetActive(false);
                createCursor.SetActive(true);
            }
        }
    }
}
