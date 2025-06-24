using UnityEngine;

public class PlayerWallScript : MonoBehaviour
{
    public int variantID;
    public Sprite playerOneWallSprite;
    public Sprite playerTwoWallSprite;
    public Sprite playerThreeWallSprite;
    public Sprite playerFourWallSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Change the appearance of the player wall depending on the variant ID
        switch (variantID)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = playerOneWallSprite;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = playerTwoWallSprite;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = playerThreeWallSprite;
                break;
            case 4:
                GetComponent<SpriteRenderer>().sprite = playerFourWallSprite;
                break;
        }

        // Disable collision with the right player
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int counter = 0; counter < players.Length; counter++)
        {
            GameObject player = players[counter];
            int playerNumber = player.GetComponent<movementScript>().playerNumber;
            
            // If the player number matches the variant ID disable collision for that player
            if (playerNumber == variantID)
            {
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
    }
}
