using UnityEngine;

public class stopMoveBox : MonoBehaviour
{
    void OnCollisionExit2D(Collision2D collision)
    {
        // Stop moving the box when the collision stops
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
    }
}
