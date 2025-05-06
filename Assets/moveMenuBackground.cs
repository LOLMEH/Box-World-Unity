using UnityEngine;

public class moveMenuBackground : MonoBehaviour
{
    public float scrollSpeed;
    private float originalXPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Save the original x position
        originalXPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the image to the left
        transform.Translate(-scrollSpeed * Time.deltaTime, 0, 0);

        // If the image is finished scolling, set it back to the original position
        if (transform.position.x <= -originalXPos)
        {
            transform.position = new Vector3(originalXPos, 0);
        }
    }
}
