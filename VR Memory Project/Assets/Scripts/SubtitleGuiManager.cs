using UnityEngine;
using TMPro;

public class SubtitleGuiManager : MonoBehaviour
{
    public TMP_Text textBox;

    public void Clear()
    {
        textBox.text = string.Empty;
    }

    public void SetText(string text)
    {
        textBox.text = text;
    }
}
