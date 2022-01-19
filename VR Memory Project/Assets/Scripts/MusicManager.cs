using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource MainCameraAudio;

    public AudioClip StationReveal;

    private void Start()
    {
        GameEvents.current.onExitedStartingRoom += OnExitedStartingRoom;
    }

    private void OnExitedStartingRoom()
    {
        MainCameraAudio.PlayOneShot(StationReveal);
    }

    public void StartFadeOutMusic(float fadeTime)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusic(fadeTime));
    }

    private IEnumerator FadeOutMusic(float fadeTime)
    {
        float startVolume = MainCameraAudio.volume;
        while(MainCameraAudio.volume > 0)
        {
            MainCameraAudio.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        MainCameraAudio.Stop();
        MainCameraAudio.volume = startVolume;
    }

    private void OnDestroy()
    {
        GameEvents.current.onExitedStartingRoom -= OnExitedStartingRoom;

    }
}
