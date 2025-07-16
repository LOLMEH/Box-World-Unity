using System.Collections;
using TMPro;
using UnityEngine;

public class LevelBannerScript : MonoBehaviour
{
    public createLevel levelData;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        // Start the animation after one second
        StartCoroutine(StartAnimation());
    }

    // https://stackoverflow.com/questions/30056471/how-to-make-the-script-wait-sleep-in-a-simple-way-in-unity
    IEnumerator StartAnimation()
    {
        // Wait for a quarter of a second
        yield return new WaitForSeconds(1/4);

        // Change the text to match the level name
        transform.GetChild(0).GetComponent<TMP_Text>().text = levelData.levelName;

        // Get the rect transform
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Start level banner show animation
        float moveMax = 200;
        float counter = 0;
        float moveCount = 1;
        float waitTime = moveCount / 500;
        while (counter < moveMax)
        {
            // Wait for the wait time's seconds
            yield return new WaitForSeconds(waitTime);

            // Move the button
            rectTransform.anchoredPosition = new(
                rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - moveCount
            );

            // Up the move counter
            counter += moveCount;
        }

        // Wait for 5 seconds
        yield return new WaitForSeconds(1);

        // Start level banner hide animation
        counter = 0;
        while (counter < moveMax)
        {
            // Wait for the wait time's seconds
            yield return new WaitForSeconds(waitTime);

            // Move the button
            rectTransform.anchoredPosition = new(
                rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + moveCount
            );

            // Up the move counter
            counter += moveCount;
        }
    }
}
