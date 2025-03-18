using UnityEngine;

public class getRedKey : MonoBehaviour
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
        BoxCollider2D keyCollider = GetComponent<BoxCollider2D>();

        if (keyCollider.IsTouching(playerCollider))
        {
            player.GetComponent<movementScript>().redKeys++;
            Destroy(gameObject);
        }
    }
}
