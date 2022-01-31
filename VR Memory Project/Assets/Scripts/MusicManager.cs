using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource MainCameraAudio;

    public AudioClip StationReveal;
    public AudioClip GivingUp;
    public AudioClip GameWin;
    public AudioClip MemoryTransition;
    public AudioClip MemoryExit;

    private void Start()
    {
        GameEvents.current.onExitedStartingRoom += OnExitedStartingRoom;
        GameEvents.current.onGivingUp += OnGivingUp;
        GameEvents.current.onMemory1945Awaken += OnMemory1945Awaken;
    }

    public void Transition()
    {
        MainCameraAudio.PlayOneShot(MemoryTransition, 1f);

        if(GameManager.Instance.sceneName == "Game")
        {
            MainCameraAudio.PlayOneShot(MemoryExit, 1f);
        }
    }

    private void OnExitedStartingRoom()
    {
        MainCameraAudio.PlayOneShot(StationReveal, 0.25f);
    }

    private void OnGivingUp()
    {
        MainCameraAudio.PlayOneShot(GivingUp, 0.25f);
    }

    private void OnMemory1945Awaken()
    {
        Debug.Log("Music Manager heard the onMemory1945Awaken event.");
        if (GameManager.Instance.PocketWatchSaved)
        {
            MainCameraAudio.PlayOneShot(GameWin, 0.25f);
            Debug.Log("Music Manager played the GameWin cue since PocketWachedSaved is " + GameManager.Instance.PocketWatchSaved);
        }
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
        GameEvents.current.onGivingUp -= OnGivingUp;
        GameEvents.current.onMemory1945Awaken -= OnMemory1945Awaken;
    }
}
