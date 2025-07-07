using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class loadingLevelData : MonoBehaviour
{
    public int levelID;
    public string gamemode;
    public string gameVersion;
    public TextAsset defaultKeybindFile;
    public Dictionary<string, KeyCode> keybinds = new();

    /// <summary>
    /// Returns a keycode depending on the string sent
    /// </summary>
    /// <param name="str">String to convert</param>
    static KeyCode StringToKeyCode(string str)
    {
        Dictionary<string, KeyCode> stringtoKeycode = new()
            {
                // https://gist.github.com/b-cancel/c516990b8b304d47188a7fa8be9a1ad9
                //-------------------------LOGICAL mappings-------------------------
  
                // Lower Case Letters
                {"A", KeyCode.A},
                {"B", KeyCode.B},
                {"C", KeyCode.C},
                {"D", KeyCode.D},
                {"E", KeyCode.E},
                {"F", KeyCode.F},
                {"G", KeyCode.G},
                {"H", KeyCode.H},
                {"I", KeyCode.I},
                {"J", KeyCode.J},
                {"K", KeyCode.K},
                {"L", KeyCode.L},
                {"M", KeyCode.M},
                {"N", KeyCode.N},
                {"O", KeyCode.O},
                {"P", KeyCode.P},
                {"Q", KeyCode.Q},
                {"R", KeyCode.R},
                {"S", KeyCode.S},
                {"T", KeyCode.T},
                {"U", KeyCode.U},
                {"V", KeyCode.V},
                {"W", KeyCode.W},
                {"X", KeyCode.X},
                {"Y", KeyCode.Y},
                {"Z", KeyCode.Z},

                // Regular Numbers
                {"Alpha1", KeyCode.Alpha1},
                {"Alpha2", KeyCode.Alpha2},
                {"Alpha3", KeyCode.Alpha3},
                {"Alpha4", KeyCode.Alpha4},
                {"Alpha5", KeyCode.Alpha5},
                {"Alpha6", KeyCode.Alpha6},
                {"Alpha7", KeyCode.Alpha7},
                {"Alpha8", KeyCode.Alpha8},
                {"Alpha9", KeyCode.Alpha9},
                {"Alpha0", KeyCode.Alpha0},

                // Arrow Keys
                {"UpArrow", KeyCode.UpArrow},
                {"DownArrow", KeyCode.DownArrow},
                {"LeftArrow", KeyCode.LeftArrow},
                {"RightArrow", KeyCode.RightArrow},
  
                // Keypad Keys
                {"Keypad1", KeyCode.Keypad1},
                {"Keypad2", KeyCode.Keypad2},
                {"Keypad3", KeyCode.Keypad3},
                {"Keypad4", KeyCode.Keypad4},
                {"Keypad5", KeyCode.Keypad5},
                {"Keypad6", KeyCode.Keypad6},
                {"Keypad7", KeyCode.Keypad7},
                {"Keypad8", KeyCode.Keypad8},
                {"Keypad9", KeyCode.Keypad9},
                {"Keypad0", KeyCode.Keypad0},
                {"KeypadPeriod", KeyCode.KeypadPeriod},
                {"KeypadDivide", KeyCode.KeypadDivide},
                {"KeypadMultiply", KeyCode.KeypadMultiply},
                {"KeypadMinus", KeyCode.KeypadMinus},
                {"KeypadPlus", KeyCode.KeypadPlus},
                {"KeypadEquals", KeyCode.KeypadEquals},
                {"KeypadEnter", KeyCode.KeypadEnter},
                {"Home", KeyCode.Home},
                {"End", KeyCode.End},
                {"PageUp", KeyCode.PageUp},
                {"PageDown", KeyCode.PageDown},
  
                // Function Keys
                {"F1", KeyCode.F1},
                {"F2", KeyCode.F2},
                {"F3", KeyCode.F3},
                {"F4", KeyCode.F4},
                {"F5", KeyCode.F5},
                {"F6", KeyCode.F6},
                {"F7", KeyCode.F7},
                {"F8", KeyCode.F8},
                {"F9", KeyCode.F9},
                {"F10", KeyCode.F10},
                {"F11", KeyCode.F11},
                {"F12", KeyCode.F12},
                {"F13", KeyCode.F13},
                {"F14", KeyCode.F14},
                {"F15", KeyCode.F15},
                {"F16", KeyCode.F16},
                {"F17", KeyCode.F17},
                {"F18", KeyCode.F18},
                {"F19", KeyCode.F19},
                {"F20", KeyCode.F20},
                {"F21", KeyCode.F21},
                {"F22", KeyCode.F22},
                {"F23", KeyCode.F23},
                {"F24", KeyCode.F24},

                // Mouse keys
                {"WheelUp", KeyCode.WheelUp},
                {"WheelDown", KeyCode.WheelDown},
                {"Mouse0", KeyCode.Mouse0},
                {"Mouse1", KeyCode.Mouse1},
                {"Mouse2", KeyCode.Mouse2},
                {"Mouse3", KeyCode.Mouse3},
                {"Mouse4", KeyCode.Mouse4},
                {"Mouse5", KeyCode.Mouse5},
                {"Mouse6", KeyCode.Mouse6},

                // Game Controller 1 Buttons
                {"Joypad1Button0", KeyCode.Joystick1Button0},
                {"Joypad1Button1", KeyCode.Joystick1Button1},
                {"Joypad1Button2", KeyCode.Joystick1Button2},
                {"Joypad1Button3", KeyCode.Joystick1Button3},
                {"Joypad1Button4", KeyCode.Joystick1Button4},
                {"Joypad1Button5", KeyCode.Joystick1Button5},
                {"Joypad1Button6", KeyCode.Joystick1Button6},
                {"Joypad1Button7", KeyCode.Joystick1Button7},
                {"Joypad1Button8", KeyCode.Joystick1Button8},
                {"Joypad1Button9", KeyCode.Joystick1Button9},
                {"Joypad1Button10", KeyCode.Joystick1Button10},
                {"Joypad1Button11", KeyCode.Joystick1Button11},
                {"Joypad1Button12", KeyCode.Joystick1Button12},
                {"Joypad1Button13", KeyCode.Joystick1Button13},
                {"Joypad1Button14", KeyCode.Joystick1Button14},
                {"Joypad1Button15", KeyCode.Joystick1Button15},
                {"Joypad1Button16", KeyCode.Joystick1Button16},
                {"Joypad1Button17", KeyCode.Joystick1Button17},
                {"Joypad1Button18", KeyCode.Joystick1Button18},
                {"Joypad1Button19", KeyCode.Joystick1Button19},

                // Game Controller 2 Buttons
                {"Joypad2Button0", KeyCode.Joystick2Button0},
                {"Joypad2Button1", KeyCode.Joystick2Button1},
                {"Joypad2Button2", KeyCode.Joystick2Button2},
                {"Joypad2Button3", KeyCode.Joystick2Button3},
                {"Joypad2Button4", KeyCode.Joystick2Button4},
                {"Joypad2Button5", KeyCode.Joystick2Button5},
                {"Joypad2Button6", KeyCode.Joystick2Button6},
                {"Joypad2Button7", KeyCode.Joystick2Button7},
                {"Joypad2Button8", KeyCode.Joystick2Button8},
                {"Joypad2Button9", KeyCode.Joystick2Button9},
                {"Joypad2Button10", KeyCode.Joystick2Button10},
                {"Joypad2Button11", KeyCode.Joystick2Button11},
                {"Joypad2Button12", KeyCode.Joystick2Button12},
                {"Joypad2Button13", KeyCode.Joystick2Button13},
                {"Joypad2Button14", KeyCode.Joystick2Button14},
                {"Joypad2Button15", KeyCode.Joystick2Button15},
                {"Joypad2Button16", KeyCode.Joystick2Button16},
                {"Joypad2Button17", KeyCode.Joystick2Button17},
                {"Joypad2Button18", KeyCode.Joystick2Button18},
                {"Joypad2Button19", KeyCode.Joystick2Button19},

                // Game Controller 3 Buttons
                {"Joypad3Button0", KeyCode.Joystick3Button0},
                {"Joypad3Button1", KeyCode.Joystick3Button1},
                {"Joypad3Button2", KeyCode.Joystick3Button2},
                {"Joypad3Button3", KeyCode.Joystick3Button3},
                {"Joypad3Button4", KeyCode.Joystick3Button4},
                {"Joypad3Button5", KeyCode.Joystick3Button5},
                {"Joypad3Button6", KeyCode.Joystick3Button6},
                {"Joypad3Button7", KeyCode.Joystick3Button7},
                {"Joypad3Button8", KeyCode.Joystick3Button8},
                {"Joypad3Button9", KeyCode.Joystick3Button9},
                {"Joypad3Button10", KeyCode.Joystick3Button10},
                {"Joypad3Button11", KeyCode.Joystick3Button11},
                {"Joypad3Button12", KeyCode.Joystick3Button12},
                {"Joypad3Button13", KeyCode.Joystick3Button13},
                {"Joypad3Button14", KeyCode.Joystick3Button14},
                {"Joypad3Button15", KeyCode.Joystick3Button15},
                {"Joypad3Button16", KeyCode.Joystick3Button16},
                {"Joypad3Button17", KeyCode.Joystick3Button17},
                {"Joypad3Button18", KeyCode.Joystick3Button18},
                {"Joypad3Button19", KeyCode.Joystick3Button19},

                // Game Controller 4 Buttons
                {"Joypad4Button0", KeyCode.Joystick4Button0},
                {"Joypad4Button1", KeyCode.Joystick4Button1},
                {"Joypad4Button2", KeyCode.Joystick4Button2},
                {"Joypad4Button3", KeyCode.Joystick4Button3},
                {"Joypad4Button4", KeyCode.Joystick4Button4},
                {"Joypad4Button5", KeyCode.Joystick4Button5},
                {"Joypad4Button6", KeyCode.Joystick4Button6},
                {"Joypad4Button7", KeyCode.Joystick4Button7},
                {"Joypad4Button8", KeyCode.Joystick4Button8},
                {"Joypad4Button9", KeyCode.Joystick4Button9},
                {"Joypad4Button10", KeyCode.Joystick4Button10},
                {"Joypad4Button11", KeyCode.Joystick4Button11},
                {"Joypad4Button12", KeyCode.Joystick4Button12},
                {"Joypad4Button13", KeyCode.Joystick4Button13},
                {"Joypad4Button14", KeyCode.Joystick4Button14},
                {"Joypad4Button15", KeyCode.Joystick4Button15},
                {"Joypad4Button16", KeyCode.Joystick4Button16},
                {"Joypad4Button17", KeyCode.Joystick4Button17},
                {"Joypad4Button18", KeyCode.Joystick4Button18},
                {"Joypad4Button19", KeyCode.Joystick4Button19},

                // Game Controller 5 Buttons
                {"Joypad5Button0", KeyCode.Joystick5Button0},
                {"Joypad5Button1", KeyCode.Joystick5Button1},
                {"Joypad5Button2", KeyCode.Joystick5Button2},
                {"Joypad5Button3", KeyCode.Joystick5Button3},
                {"Joypad5Button4", KeyCode.Joystick5Button4},
                {"Joypad5Button5", KeyCode.Joystick5Button5},
                {"Joypad5Button6", KeyCode.Joystick5Button6},
                {"Joypad5Button7", KeyCode.Joystick5Button7},
                {"Joypad5Button8", KeyCode.Joystick5Button8},
                {"Joypad5Button9", KeyCode.Joystick5Button9},
                {"Joypad5Button10", KeyCode.Joystick5Button10},
                {"Joypad5Button11", KeyCode.Joystick5Button11},
                {"Joypad5Button12", KeyCode.Joystick5Button12},
                {"Joypad5Button13", KeyCode.Joystick5Button13},
                {"Joypad5Button14", KeyCode.Joystick5Button14},
                {"Joypad5Button15", KeyCode.Joystick5Button15},
                {"Joypad5Button16", KeyCode.Joystick5Button16},
                {"Joypad5Button17", KeyCode.Joystick5Button17},
                {"Joypad5Button18", KeyCode.Joystick5Button18},
                {"Joypad5Button19", KeyCode.Joystick5Button19},

                // Game Controller 6 Buttons
                {"Joypad6Button0", KeyCode.Joystick6Button0},
                {"Joypad6Button1", KeyCode.Joystick6Button1},
                {"Joypad6Button2", KeyCode.Joystick6Button2},
                {"Joypad6Button3", KeyCode.Joystick6Button3},
                {"Joypad6Button4", KeyCode.Joystick6Button4},
                {"Joypad6Button5", KeyCode.Joystick6Button5},
                {"Joypad6Button6", KeyCode.Joystick6Button6},
                {"Joypad6Button7", KeyCode.Joystick6Button7},
                {"Joypad6Button8", KeyCode.Joystick6Button8},
                {"Joypad6Button9", KeyCode.Joystick6Button9},
                {"Joypad6Button10", KeyCode.Joystick6Button10},
                {"Joypad6Button11", KeyCode.Joystick6Button11},
                {"Joypad6Button12", KeyCode.Joystick6Button12},
                {"Joypad6Button13", KeyCode.Joystick6Button13},
                {"Joypad6Button14", KeyCode.Joystick6Button14},
                {"Joypad6Button15", KeyCode.Joystick6Button15},
                {"Joypad6Button16", KeyCode.Joystick6Button16},
                {"Joypad6Button17", KeyCode.Joystick6Button17},
                {"Joypad6Button18", KeyCode.Joystick6Button18},
                {"Joypad6Button19", KeyCode.Joystick6Button19},

                // Game Controller 7 Buttons
                {"Joypad7Button0", KeyCode.Joystick7Button0},
                {"Joypad7Button1", KeyCode.Joystick7Button1},
                {"Joypad7Button2", KeyCode.Joystick7Button2},
                {"Joypad7Button3", KeyCode.Joystick7Button3},
                {"Joypad7Button4", KeyCode.Joystick7Button4},
                {"Joypad7Button5", KeyCode.Joystick7Button5},
                {"Joypad7Button6", KeyCode.Joystick7Button6},
                {"Joypad7Button7", KeyCode.Joystick7Button7},
                {"Joypad7Button8", KeyCode.Joystick7Button8},
                {"Joypad7Button9", KeyCode.Joystick7Button9},
                {"Joypad7Button10", KeyCode.Joystick7Button10},
                {"Joypad7Button11", KeyCode.Joystick7Button11},
                {"Joypad7Button12", KeyCode.Joystick7Button12},
                {"Joypad7Button13", KeyCode.Joystick7Button13},
                {"Joypad7Button14", KeyCode.Joystick7Button14},
                {"Joypad7Button15", KeyCode.Joystick7Button15},
                {"Joypad7Button16", KeyCode.Joystick7Button16},
                {"Joypad7Button17", KeyCode.Joystick7Button17},
                {"Joypad7Button18", KeyCode.Joystick7Button18},
                {"Joypad7Button19", KeyCode.Joystick7Button19},

                // Game Controller 8 Buttons
                {"Joypad8Button0", KeyCode.Joystick8Button0},
                {"Joypad8Button1", KeyCode.Joystick8Button1},
                {"Joypad8Button2", KeyCode.Joystick8Button2},
                {"Joypad8Button3", KeyCode.Joystick8Button3},
                {"Joypad8Button4", KeyCode.Joystick8Button4},
                {"Joypad8Button5", KeyCode.Joystick8Button5},
                {"Joypad8Button6", KeyCode.Joystick8Button6},
                {"Joypad8Button7", KeyCode.Joystick8Button7},
                {"Joypad8Button8", KeyCode.Joystick8Button8},
                {"Joypad8Button9", KeyCode.Joystick8Button9},
                {"Joypad8Button10", KeyCode.Joystick8Button10},
                {"Joypad8Button11", KeyCode.Joystick8Button11},
                {"Joypad8Button12", KeyCode.Joystick8Button12},
                {"Joypad8Button13", KeyCode.Joystick8Button13},
                {"Joypad8Button14", KeyCode.Joystick8Button14},
                {"Joypad8Button15", KeyCode.Joystick8Button15},
                {"Joypad8Button16", KeyCode.Joystick8Button16},
                {"Joypad8Button17", KeyCode.Joystick8Button17},
                {"Joypad8Button18", KeyCode.Joystick8Button18},
                {"Joypad8Button19", KeyCode.Joystick8Button19},

                // Other Keys
                {"Backspace", KeyCode.Backspace},
                {"Escape", KeyCode.Escape},
                {"Delete", KeyCode.Delete},
                {"Clear", KeyCode.Clear},
                {"Pause", KeyCode.Pause},
                {"Return", KeyCode.Return},
                {"Tab", KeyCode.Tab},
                {"CapsLock", KeyCode.CapsLock},
                {"LeftShift", KeyCode.LeftShift},
                {"RightShift", KeyCode.RightShift},
                {"LeftControl", KeyCode.LeftControl},
                {"RightControl", KeyCode.RightControl},
                {"LeftCommand", KeyCode.LeftCommand},
                {"RightCommand", KeyCode.RightCommand},
                {"Space", KeyCode.Space},
                {"LeftAlt", KeyCode.LeftAlt},
                {"RightAlt", KeyCode.RightAlt},
                {"NumLock", KeyCode.Numlock},
                {"ScrollLock", KeyCode.ScrollLock},
                {"Menu", KeyCode.Menu},
                {"Insert", KeyCode.Insert},
                {"Help", KeyCode.Help},
                {"Print", KeyCode.Print},
                {"SysReq", KeyCode.SysReq},
                {"Break", KeyCode.Break},
  
                // Other Symbols
                {"Exclaim", KeyCode.Exclaim}, //1
                {"DoubleQuote", KeyCode.DoubleQuote}, //remember the special forward slash rule... this isnt wrong
                {"Hash", KeyCode.Hash}, //3
                {"Dollar", KeyCode.Dollar}, //4
                {"Ampersand", KeyCode.Ampersand}, //7
                {"Quote", KeyCode.Quote},
                {"LeftParen", KeyCode.LeftParen}, //9
                {"RightParen", KeyCode.RightParen}, //0
                {"Asterisk", KeyCode.Asterisk}, //8
                {"Plus", KeyCode.Plus},
                {"Comma", KeyCode.Comma},
                {"Minus", KeyCode.Minus},
                {"Period", KeyCode.Period},
                {"Slash", KeyCode.Slash},
                {"Colon", KeyCode.Colon},
                {"Semicolon", KeyCode.Semicolon},
                {"Less", KeyCode.Less},
                {"Equals", KeyCode.Equals},
                {"Greater", KeyCode.Greater},
                {"Question", KeyCode.Question},
                {"At", KeyCode.At}, //2
                {"LeftBracket", KeyCode.LeftBracket},
                {"Backslash", KeyCode.Backslash}, //remember the special forward slash rule... this isnt wrong
                {"RightBracket", KeyCode.RightBracket},
                {"Caret", KeyCode.Caret}, //6
                {"Underscore", KeyCode.Underscore},
                {"BackQuote", KeyCode.BackQuote},
                {"Tilde", KeyCode.Tilde},
                {"LeftCurlyBracket", KeyCode.LeftCurlyBracket},
                {"RightCurlyBracket", KeyCode.RightCurlyBracket},
                {"Percent", KeyCode.Percent},
                {"Pipe", KeyCode.Pipe},
            };

        KeyCode keyCode = stringtoKeycode[str];

        return keyCode;
    }

    /// <summary>
    /// Updates every keybind
    /// </summary>
    public void UpdateKeybinds()
    {
        // Go to the local folder to read the keybind config file
        string keybindFile;
        string keybindFilePath = Application.persistentDataPath + "/config.txt";
        try
        {
            // Read the keybind file and split it
            keybindFile = File.ReadAllText(keybindFilePath);

        }
        catch (FileNotFoundException)
        {
            // Create a new file with the default controls the default keybinds if the config file does not exist
            print("Config file not found. Creating new keybind file at " + keybindFilePath);
            keybindFile = defaultKeybindFile.ToString();
            File.WriteAllText(keybindFilePath, keybindFile);
        }

        // Convert each keybind to a keycode
        string[] keybindData;
        keybindData = keybindFile.Split("\n");
        foreach (string keybindLine in keybindData)
        {
            // Find the keybind depending on string
            string[] splitKeybind = keybindLine.Split(":");
            string keybindName;
            string keybindString;
            KeyCode keyCode;
            try
            {
                // Find keybind information
                keybindName = splitKeybind[0].Trim();
                keybindString = splitKeybind[1].Trim();
                keyCode = StringToKeyCode(keybindString);
            }
            catch (IndexOutOfRangeException) { break; }
            try
            {
                // Add the information in the keybind variable
                keybinds.Add(keybindName, keyCode);
            }
            catch (ArgumentException)
            {
                // Replace the information in the keybind variable if it already exists
                keybinds[keybindName] = keyCode;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateKeybinds();
    }

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
