using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public int variantID;
    public Sprite greenKeySprite;
    public Sprite redKeySprite;
    public Sprite blueKeySprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Change the appearance of the key depending on the variant ID
        switch (variantID)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = greenKeySprite;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = redKeySprite;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = blueKeySprite;
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if a player is touching the object
        if (collision.gameObject.CompareTag("Player"))
        {
            // Give the correct key to the player
            GameObject player = collision.gameObject;
            try
            {
                player.GetComponent<movementScript>().keysHeld[variantID]++;
            }
            catch (KeyNotFoundException)
            {
                // If the player does not have a slot for this key, add it
                player.GetComponent<movementScript>().keysHeld.Add(variantID, 1);
            }
            Destroy(gameObject);
        }
    }
}
