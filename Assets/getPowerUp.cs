using UnityEngine;
using UnityEngine.Tilemaps;

public class getPowerUp : MonoBehaviour
{
    public movementScript player;
    public CompositeCollider2D regularBoxCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
        BoxCollider2D powerUpCollider = GetComponent<BoxCollider2D>();

        if (powerUpCollider.IsTouching(playerCollider))
        {
            // Enable power up
            player.hasPowerUp = true;
            player.powerUpImage.enabled = true;
            Physics2D.IgnoreCollision(playerCollider, regularBoxCollider, true);
            // Destroy power up
            Destroy(gameObject);
        }
    }
}
