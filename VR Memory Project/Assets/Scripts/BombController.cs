using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject AtomicBomb;
    [SerializeField] private float _timeToDetonation = 10f;

    private void Awake()
    {
        AtomicBomb.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(Detonate(_timeToDetonation));
    }

    IEnumerator Detonate(float time)
    {
        yield return new WaitForSeconds(time);
        AtomicBomb.SetActive(true);
    }
}
