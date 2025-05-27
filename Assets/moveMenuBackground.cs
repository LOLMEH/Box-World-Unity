using UnityEngine;
using UnityEngine.UI;

public class moveMenuBackground : MonoBehaviour
{
    public float scrollSpeed;
    private RawImage rawImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the raw image
        rawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        // Scroll the background
        rawImage.uvRect =
            new Rect(scrollSpeed * Time.time, rawImage.uvRect.y, rawImage.uvRect.width, rawImage.uvRect.height);
    }
}
