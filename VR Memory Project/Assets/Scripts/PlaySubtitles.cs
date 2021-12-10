using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySubtitles : MonoBehaviour
{
    //private AudioSource audioSource;
    private SubtitleGuiManager guiManager;

    private PlayVoiceOvers playVoiceOversScript;

    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
        guiManager = FindObjectOfType<SubtitleGuiManager>();

        playVoiceOversScript = GetComponent<PlayVoiceOvers>();
    }
    /*
    public void ShowSubtitle(AudioClip[] convo, int lineNumber)
    {
        StartCoroutine(DoSubtitle(convo, lineNumber));
    }

    private IEnumerator DoSubtitle(AudioClip[] convo, int lineNumber)
    {
        //var script = ScriptManager.Instance.GetText(audioSource.clip.name);
        
        convo = playVoiceOversScript.convoOne;

        var script = ScriptManager.Instance.GetText(playVoiceOversScript.);

        //var lineDuration = audioSource.clip.length / script.Length;
        var lineDuration = playVoiceOversScript.convo[lineNumber].length / script.Length;

        foreach (var line in script)
        {
            guiManager.SetText(line);
            yield return new WaitForSeconds(lineDuration);
        }
        
        guiManager.SetText(string.Empty);
        Debug.Log("Subtitle text should be set to empty now.");
        
    }
    */
}
