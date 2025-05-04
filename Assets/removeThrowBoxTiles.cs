using UnityEngine;
using UnityEngine.Tilemaps;

public class removeThrowBoxTiles : MonoBehaviour
{
    public TilemapRenderer throwBoxTileTilemap;
    public TilemapCollider2D throwBoxTileTilemapCollider;
    public Sprite activeButtonImage;

    void Start()
    {
        // Disable collision with players
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int counter = 0; counter < players.Length; counter++)
        {
            GameObject player = players[counter];
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if a throw box is colliding with the throw box button
        GameObject collidingObject = collision.gameObject;
        if (collidingObject.CompareTag("ThrowBox"))
        {
            // Toggle the throw box tiles, remove the colliding throw box, and show that the button is activated
            if (throwBoxTileTilemap.enabled == false)
            {
                throwBoxTileTilemap.enabled = true;
                throwBoxTileTilemapCollider.enabled = true;
            }
            else
            {
                throwBoxTileTilemap.enabled = false;
                throwBoxTileTilemapCollider.enabled = false;
            }
            Destroy(collidingObject);
            GetComponent<SpriteRenderer>().sprite = activeButtonImage;
            Destroy(this);
        }
    }
}
