using JetBrains.Rider.Unity.Editor;
using UnityEngine;

public class throwBoxScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Disable collision with all other throw boxes
        GameObject[] throwBoxes = GameObject.FindGameObjectsWithTag("ThrowBox");
        for (int counter = 0; counter < throwBoxes.Length; counter++)
        {
            GameObject throwBox = throwBoxes[counter];
            Physics2D.IgnoreCollision(throwBox.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if a player or another throw box or a throw box button is not colliding with the throw box
        GameObject collidingObject = collision.gameObject;
        if (!(collidingObject.CompareTag("Player") || collidingObject.CompareTag("ThrowBox") || collidingObject.CompareTag("ThrowBoxButton")))
        {
            // Make the throw box unmoveable
            GetComponent<SpriteRenderer>().color = Color.gray;
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(this);
        }
    }
}
