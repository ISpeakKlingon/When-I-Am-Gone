using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour
{
    [SerializeField] private AudioSource _radio;

    public AudioClip[] RadioPlayList;

    private void Start()
    {
        _radio = GetComponent<AudioSource>();
        StartCoroutine(RadioBroadcast());
    }

    IEnumerator RadioBroadcast()
    {
        _radio.PlayOneShot(RadioPlayList[0]);
        yield return new WaitForSeconds(10f);
        _radio.PlayOneShot(RadioPlayList[1]);
    }
}
