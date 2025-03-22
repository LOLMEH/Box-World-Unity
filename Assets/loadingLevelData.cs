using UnityEngine;

public class loadingLevelData : MonoBehaviour
{
    public int levelID;
    public string gamemode;

    void Awake()
    {
        // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Object.DontDestroyOnLoad.html
        GameObject[] objs = GameObject.FindGameObjectsWithTag("LoadLevelInfo");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
