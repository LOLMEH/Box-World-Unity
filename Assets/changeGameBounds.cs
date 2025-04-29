using TMPro;
using UnityEngine;

public class changeGameBounds : MonoBehaviour
{
    public Camera createCamera;
    public TMP_Dropdown boundDropdown;
    public string boundString;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boundString = "old";
    }

    public void onChange()
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
}
