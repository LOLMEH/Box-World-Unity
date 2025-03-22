using UnityEngine;

public class stopMoveBox : MonoBehaviour
{
    private bool isPlayerTouching;

    private void Start()
    {
        isPlayerTouching = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if player is touching object
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerTouching = true;
        }
    }

    void Update()
    {
        if (!isPlayerTouching)
        {
            // Only move if a player is pushing the object
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
        }
        isPlayerTouching = false;
    }
}
