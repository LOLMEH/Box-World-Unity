using TMPro;
using UnityEngine;

public class updateSplashText : MonoBehaviour
{
    public float scrollSpeed;
    public TextAsset splashFile;
    private RectTransform rectTransform;
    private float originalXPos;
    private float resetXPos;
    private float textMeshProTextSize;

    /// <summary>
    /// Gets a random splash text from the splash file and loads it into the text object
    /// </summary>
    private void UpdateSplashRandom()
    {
        // Convert the splash text file into a list
        string[] splashTextList = splashFile.text.Split('\n');

        // Get a random splash index
        int randomSplashIndex = Random.Range(0, splashTextList.Length - 1);
        string splashText = splashTextList[randomSplashIndex];

        // Get the text object size
        textMeshProTextSize = GetComponent<TMP_Text>().GetPreferredValues(splashText).x;

        // Set the text of the GUI element to the random splash text
        GetComponent<TMP_Text>().text = splashText;

        // Calculate the x pos to reset the text
        resetXPos = -(originalXPos + textMeshProTextSize);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Save the original x position
        rectTransform = GetComponent<RectTransform>();
        originalXPos = rectTransform.anchoredPosition.x;

        // Set a random splash text
        UpdateSplashRandom();
    }

    // Update is called once per frame
    void Update()
    {
        // Get current text position
        Vector2 position = rectTransform.anchoredPosition;

        // Move the text to the left
        rectTransform.Translate(-scrollSpeed * Time.deltaTime, 0, 0);

        // If the text is finished scolling, set it back to the original position and change the splash text
        if (position.x <= resetXPos)
        {
            rectTransform.anchoredPosition = new Vector2(originalXPos, position.y);
            UpdateSplashRandom();
        }
    }
}
