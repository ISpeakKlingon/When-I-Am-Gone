using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySubtitle : MonoBehaviour
{
    private AudioSource audioSource;
    private SubtitleGuiManager guiManager;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        guiManager = FindObjectOfType<SubtitleGuiManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DoSubtitle());
        }
    }

    private IEnumerator DoSubtitle()
    {
        var script = ScriptManager.Instance.GetText(audioSource.clip.name);
        var lineDuration = audioSource.clip.length / script.Length;

        foreach(var line in script)
        {
            guiManager.SetText(line);
            yield return new WaitForSeconds(lineDuration);
        }

        guiManager.SetText(string.Empty);
        Debug.Log("Subtitle text should be set to empty now.");
    }
}