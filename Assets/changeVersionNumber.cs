using TMPro;
using UnityEngine;

public class changeVersionNumber : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the loading level data
        loadingLevelData loadingLevelData = GameObject.FindGameObjectWithTag("LoadLevelInfo").GetComponent<loadingLevelData>();

        // Change version number text
        TMP_Text text = GetComponent<TMP_Text>();
        text.text = loadingLevelData.gameVersion;
    }
}
