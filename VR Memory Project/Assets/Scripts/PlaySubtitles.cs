using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySubtitles : MonoBehaviour
{
    //private AudioSource audioSource;
    private SubtitleGuiManager guiManager;

    private PlayVoiceOvers playVoiceOversScript;

    private AudioSource audioSource;

    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
        guiManager = FindObjectOfType<SubtitleGuiManager>();

        playVoiceOversScript = GetComponent<PlayVoiceOvers>();

        audioSource = this.GetComponent<AudioSource>();
    }
    
    public void ShowSubtitle(AudioClip[] convo, int lineNumber)
    {
        StartCoroutine(DoSubtitle(convo, lineNumber));
    }

    private IEnumerator DoSubtitle(AudioClip[] convo, int lineNumber)
    {
        //StopAllCoroutines();
        //var script = ScriptManager.Instance.GetText(audioSource.clip.name);

        //convo = playVoiceOversScript.convoOne;

        //define convo in playVoiceOverScript
        playVoiceOversScript.SetCurrentConvo(convo);

        var script = ScriptManager.Instance.GetText(playVoiceOversScript.currentConvo[lineNumber].name);

        //var lineDuration = audioSource.clip.length / script.Length;
        var lineDuration = playVoiceOversScript.currentConvo[lineNumber].length / script.Length;

        foreach (var line in script)
        {
            guiManager.SetText(line);
            yield return new WaitForSeconds(lineDuration);
        }
        
        guiManager.SetText(string.Empty);
        Debug.Log("Subtitle text should be set to empty now.");
        
    }
    
    public void ShowSubtitles(AudioClip[] convo)
    {
        StartCoroutine(DoSubtitles(convo));
    }

    private IEnumerator DoSubtitles(AudioClip[] convo)
    {
        //StopAllCoroutines();
        playVoiceOversScript.SetCurrentConvo(convo);

        for(int i = 0; i<convo.Length; i++)
        {
            var script = ScriptManager.Instance.GetText(playVoiceOversScript.currentConvo[i].name);
            var lineDuration = playVoiceOversScript.currentConvo[i].length / script.Length;

            foreach (var line in script)
            {
                guiManager.SetText(line);
                yield return new WaitForSeconds(lineDuration);
            }

            guiManager.SetText(string.Empty);
            /*
            while (audioSource.isPlaying)
            {
                yield return null;
            }
            */
        }
    }
}
