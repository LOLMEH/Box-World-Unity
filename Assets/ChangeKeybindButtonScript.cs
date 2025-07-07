using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ChangeKeybindButtonScript : MonoBehaviour
{
    public string keybindName;
    public GameObject remapPopUp;
    private loadingLevelData loadingLevelData;
    private string configFilePath;
    private bool currentlyRemapping = false;
    private TMP_Text textObject;

    public void RemapKey()
    {
        remapPopUp.SetActive(true);
        currentlyRemapping = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get config file path
        configFilePath = Application.persistentDataPath + "/";
    }

    void OnEnable()
    {
        // Get the keybind the button is for
        loadingLevelData = GameObject.FindGameObjectWithTag("LoadLevelInfo").GetComponent<loadingLevelData>();
        Dictionary<string, KeyCode> keybinds = loadingLevelData.keybinds;
        string keyCodeString = keybinds[keybindName].ToString();

        // Change the text on the buttom
        textObject = transform.GetChild(0).GetComponent<TMP_Text>();
        textObject.text = textObject.text + " " + keyCodeString;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for input if remapping
        if (currentlyRemapping)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(keyCode))
                {
                    try
                    {
                        // Look for the line the keybind is stored on
                        string[] configFileLines = File.ReadAllLines(configFilePath + "config.txt");
                        for (int counter = 0; counter < configFileLines.Length; counter++)
                        {
                            string configFileLine = configFileLines[counter];
                            string controlName = configFileLine.Split(":")[0];
                            if (controlName.Equals(keybindName))
                            {
                                // Change the line to use the new keybind
                                string keyCodeString = keyCode.ToString();
                                configFileLines[counter] = keybindName + ":" + keyCodeString;

                                // Change the text on the button
                                textObject.text = textObject.text[..textObject.text.LastIndexOf(":")];
                                textObject.text = textObject.text + ": " + keyCodeString;
                                break;
                            }
                        }
                        // Save new config
                        File.WriteAllLines(configFilePath + "config.txt", configFileLines);

                        // Update keybinds
                        loadingLevelData.UpdateKeybinds();
                    }
                    catch (FileNotFoundException)
                    {
                        print("Error: Config file not found.");
                    }
                    remapPopUp.SetActive(false);
                    currentlyRemapping = false;
                    break;
                }
            }
        }
    }
}
