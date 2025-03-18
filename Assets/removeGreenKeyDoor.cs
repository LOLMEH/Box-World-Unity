using UnityEngine;

public class removeGreenKeyDoor : MonoBehaviour
{
    public GameObject player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
        BoxCollider2D keyDoorCollider = GetComponent<BoxCollider2D>();

        int playerGreenKeys = player.GetComponent<movementScript>().greenKeys;

        if (keyDoorCollider.IsTouching(playerCollider) && playerGreenKeys > 0)
        {
            player.GetComponent<movementScript>().greenKeys--;
            Destroy(gameObject);
        }
    }
}
