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
        guiManager.SetText(script);

        yield return new WaitForSeconds(audioSource.clip.length);
        guiManager.SetText(string.Empty);
    }
}