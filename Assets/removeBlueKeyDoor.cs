using UnityEngine;

public class removeBlueKeyDoor : MonoBehaviour
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

        int playerBlueKeys = player.GetComponent<movementScript>().blueKeys;

        if (keyDoorCollider.IsTouching(playerCollider) && playerBlueKeys > 0)
        {
            player.GetComponent<movementScript>().blueKeys--;
            Destroy(gameObject);
        }
    }
}
