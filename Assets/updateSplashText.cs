using TMPro;
using UnityEngine;

public class updateSplashText : MonoBehaviour
{
    public TextAsset splashFile;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Convert the splash text file into a list
        string[] splashTextList = splashFile.text.Split('\n');

        // Get a random splash index
        int randomSplashIndex = Random.Range(0, splashTextList.Length - 1);

        // Set the text of the GUI element to the random splash text
        GetComponent<TMP_Text>().text = splashTextList[randomSplashIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
