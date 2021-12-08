using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayVoiceOvers : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip[] audioClips;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SpeakLine(int lineNumber)
    {
        audioSource.PlayOneShot(audioClips[lineNumber], 1);
    }
}
