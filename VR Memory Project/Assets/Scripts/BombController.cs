using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject AtomicBomb;
    [SerializeField] private float _timeToDetonation = 10f;
    [SerializeField] private AudioSource _audioSource;
    public AudioClip Explosion;
    [SerializeField] Animator _crossfadeAnim;

    public GameObject RoomLight;
    
    private void Awake()
    {
        AtomicBomb.SetActive(false);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(Detonate(_timeToDetonation));
        _crossfadeAnim = GameManager.Instance.CrossfadeAnimator;
    }

    IEnumerator Detonate(float time)
    {
        yield return new WaitForSeconds(time);
        _crossfadeAnim.SetTrigger("Flash");
        yield return new WaitForSeconds(0.5f);
        // turn off room light
        RoomLight.SetActive(false);
        AtomicBomb.SetActive(true);
        _audioSource.PlayOneShot(Explosion);
    }


}
