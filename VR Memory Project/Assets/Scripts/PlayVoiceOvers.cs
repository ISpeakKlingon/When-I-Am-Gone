using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayVoiceOvers : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip[] convoOne;

    //public List<AudioClip[]> listOfLineChunks = new List<AudioClip[]>();

    //public AudioClip[][] audioChunks = new AudioClip[3][];

    //public int[][] numberList = new int[3][];

    public AudioClip[] convoTwo;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SpeakLine(AudioClip[] convo, int lineNumber)
    {
        
        audioSource.PlayOneShot(convo[lineNumber], 1);
    }

    public void SpeakLines(AudioClip[] convo)
    {
        StartCoroutine(SpeakMultipleLines(convo));
    }
    
    IEnumerator SpeakMultipleLines(AudioClip[] convo)
    {
        for(int i = 0; i < convo.Length; i++)
        {
            audioSource.PlayOneShot(convo[i]);
            while (audioSource.isPlaying)
            {
                yield return null;
            }
        }
    }
    
}
