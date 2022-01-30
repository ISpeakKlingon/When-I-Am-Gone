using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _intensity = 0.0f;
    [SerializeField] private Color _color = Color.black;
    [SerializeField] private Material _fadeMaterial = null;

    public Animator transition;

    public float TimeToWaitBeforeLongFade = 25f;

    /*
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        _fadeMaterial.SetFloat("_Intensity", _intensity);
        _fadeMaterial.SetColor("_FadeColor", _color);
        Graphics.Blit(source, destination, _fadeMaterial);
    }
    */

    private void Start()
    {
        GameEvents.current.onFinalView += OnFinalView;
        GameEvents.current.onMemory1945Awaken += OnMemory1945Awaken;

    }

    private void OnMemory1945Awaken()
    {
        if (GameManager.Instance.PocketWatchSaved)
        {
            StartCoroutine(LongFadeOut(18f));
        }
    }

    private void OnFinalView()
    {
        StartCoroutine(LongFadeOut(TimeToWaitBeforeLongFade));
    }

    private IEnumerator LongFadeOut(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        transition.SetTrigger("EndGame");
    }

    public Coroutine StartFadeIn()
    {
        StopAllCoroutines();
        return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        //yield return new WaitForSeconds(1f);

        // trigger animation
        transition.SetTrigger("Start");

        //yield return new WaitForSeconds(2f);
        /*
        while (_intensity <= 1.0f)
        {
            _intensity += _speed = Time.deltaTime;
            yield return null;
        }
        */
        yield return null;
    }

    public Coroutine StartFadeOut()
    {
        StopAllCoroutines();
        return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        //yield return new WaitForSeconds(3f);

        // trigger animation
        transition.SetTrigger("Start");

        //yield return new WaitForSeconds(3f);

        /*
        while (_intensity >= 0.0f)
        {
            _intensity -= _speed = Time.deltaTime;
            yield return null;
        }
        */
        yield return null;
    }

    private void OnDestroy()
    {
        GameEvents.current.onFinalView -= OnFinalView;
        GameEvents.current.onMemory1945Awaken -= OnMemory1945Awaken;
    }
}
