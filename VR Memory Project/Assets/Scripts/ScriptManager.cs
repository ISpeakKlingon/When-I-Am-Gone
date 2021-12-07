using System;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    private static ScriptManager _instance;

    private Dictionary<string, string[]> lines = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

    public string resourceFile = "script";

    //public bool isThisVOAlreadyPlayed;

    public bool Memory2020_Collider = false;

    public static ScriptManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Script Manager is NULL");

            return _instance;
        }
    }

    public string[] GetText(string textKey)
    {
        string[] tmp = new string[] { };
        if (lines.TryGetValue(textKey, out tmp))
            return tmp;

        return new string[] { "<color=#ff00ff>MISSING TEXT FOR '" + textKey + "'</color>" };
    }

    private void Awake()
    {
        _instance = this;

        var textAsset = Resources.Load<TextAsset>(resourceFile);
        var voText = JsonUtility.FromJson<VoiceOverText>(textAsset.text);

        foreach(var t in voText.lines)
        {
            lines[t.key] = t.line;
        }
    }

    public void MarkVOPlayed(string nameOfVO)
    {
        if(nameOfVO == "Memory2020_Collider")
        {
            Memory2020_Collider = true;
        }


    }

    public bool CheckIfVOPlayed(string nameOfVO)
    {
        return nameOfVO == 
    }
}
