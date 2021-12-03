using System;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    private static ScriptManager _instance;

    private Dictionary<string, string> lines = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    public string resourceFile = "script";

    public static ScriptManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Script Manager is NULL");

            return _instance;
        }
    }

    public string GetText(string textKey)
    {
        string tmp = "";
        if (lines.TryGetValue(textKey, out tmp))
            return tmp;

        return "<color=#ff00ff>MISSING TEXT FOR '" + textKey + "'</color>";
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
}
