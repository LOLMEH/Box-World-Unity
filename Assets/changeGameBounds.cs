using TMPro;
using UnityEngine;

public class changeGameBounds : MonoBehaviour
{
    public Camera createCamera;
    public TMP_Dropdown boundDropdown;
    public string boundString;
    public TMP_Dropdown playerCountDropdown;
    public int playerCount;
    public moveCreateObject createObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boundString = "old";
        playerCount = 1;
    }

    public void onChangeBounds()
    {
        // Get selected bound index
        int boundIndex = boundDropdown.value;
        float cameraZ = createCamera.transform.position.z;

        // Change camera zoom, position, and bound string to match the selection in the dropdown
        switch (boundIndex)
        {
            case 0:
                // Old bounds
                createCamera.orthographicSize = 5;
                createCamera.transform.position = new Vector3(0, 0, cameraZ);
                boundString = "old";
                break;
            case 1:
                // x2 bounds
                createCamera.orthographicSize = 10;
                createCamera.transform.position = new Vector3(1, -1, cameraZ);
                boundString = "x2";
                break;
        }
    }

    public void onChangePlayers()
    {
        // Get selected player count index
        int playerCountIndex = playerCountDropdown.value;

        // Change the amount of players the level is made for and hide or show the other spawn points
        switch (playerCountIndex)
        {
            case 0:
                // 1 player
                playerCount = 1;
                createObjects.playerTwoMarker.SetActive(false);
                createObjects.playerThreeMarker.SetActive(false);
                createObjects.playerFourMarker.SetActive(false);
                break;
            case 1:
                // 2 players
                playerCount = 2;
                createObjects.playerTwoMarker.SetActive(true);
                createObjects.playerThreeMarker.SetActive(false);
                createObjects.playerFourMarker.SetActive(false);
                break;
            case 2:
                // 3 players
                playerCount = 3;
                createObjects.playerTwoMarker.SetActive(true);
                createObjects.playerThreeMarker.SetActive(true);
                createObjects.playerFourMarker.SetActive(false);
                break;
            case 3:
                // 4 players
                playerCount = 4;
                createObjects.playerTwoMarker.SetActive(true);
                createObjects.playerThreeMarker.SetActive(true);
                createObjects.playerFourMarker.SetActive(true);
                break;
        }
    }
}
