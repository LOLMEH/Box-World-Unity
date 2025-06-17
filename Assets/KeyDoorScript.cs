using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorScript : MonoBehaviour
{
    public int variantID;
    public Sprite greenKeyDoorSprite;
    public Sprite redKeyDoorSprite;
    public Sprite blueKeyDoorSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Change the appearance of the key door depending on the variant ID
        switch (variantID)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = greenKeyDoorSprite;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = redKeyDoorSprite;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = blueKeyDoorSprite;
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if a player is touching the object
        if (collision.gameObject.CompareTag("Player"))
        {
            // Remove the key door if the player has more than one key matching this key door's variant
            GameObject player = collision.gameObject;
            int playerKeys;
            try
            {
                playerKeys = player.GetComponent<movementScript>().keysHeld[variantID];
            }
            catch (KeyNotFoundException)
            {
                // If the player does not have a slot for the wanted key, add it
                player.GetComponent<movementScript>().keysHeld.Add(variantID, 0);
                playerKeys = 0;
            }

            if (playerKeys > 0)
            {
                player.GetComponent<movementScript>().keysHeld[variantID]--;
                Destroy(gameObject);
            }
        }
    }
}
