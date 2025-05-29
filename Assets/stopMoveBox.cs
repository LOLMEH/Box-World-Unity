using UnityEngine;

public class stopMoveBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Disable collision with key objects
        GameObject[] phaseObjects = GameObject.FindGameObjectsWithTag("Key");
        for (int counter = 0; counter < phaseObjects.Length; counter++)
        {
            GameObject phaseObject = phaseObjects[counter];
            Physics2D.IgnoreCollision(phaseObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        // Disable collision with throw box buttons
        GameObject[] throwBoxButtonObjects = GameObject.FindGameObjectsWithTag("ThrowBoxButton");
        for (int counter = 0; counter < throwBoxButtonObjects.Length; counter++)
        {
            GameObject throwBoxButtonObject = throwBoxButtonObjects[counter];
            Physics2D.IgnoreCollision(throwBoxButtonObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Stop moving the box when the collision stops
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
    }
}
