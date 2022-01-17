using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class MemoryInfoController : MonoBehaviour
{
    [SerializeField] private Text _displayedText;
    public Color FadedColor;
    public Color NonFadedColor;
    public string MemoryDate;

    //date time vars
    [SerializeField] private DateTime _localDate = DateTime.Now;
    [SerializeField] private DateTime _utcDate = DateTime.UtcNow;
    [SerializeField] private String[] _cultureNames = { "en-US"};

    private void OnEnable()
    {
        _displayedText = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        _displayedText.color = FadedColor;
        _displayedText.text = MemoryDate;
        //StartCoroutine(DisplayMemoryInfo());
        if(MemoryDate == "[Today's Date]")
        {
            SetIRLDate();
        }
        else
        {
            SetText(MemoryDate);
        }

        StartCoroutine(FadeInMemoryInfo(3f));
    }

    private void SetIRLDate()
    {
        foreach(var _cultureNames in _cultureNames)
        {
            var culture = new CultureInfo(_cultureNames);

            string date = culture.NativeName + " , " + _localDate.ToString(culture) + " , " + _localDate.Kind;

            SetText(date);
        }
    }

    private IEnumerator DisplayMemoryInfo()
    {
        Debug.Log("DisplayMemoryInfo coroutine called. Preparing to wait 2 seconds.");
        yield return new WaitForSeconds(2);
        Debug.Log("Finished waiting. Now setting vars and calling FadeInMemoryInfo.");
        var fadeDuration = 1f;
        StartCoroutine(FadeInMemoryInfo(fadeDuration));
        Debug.Log("Coroutine DisplaymemoryInfo preparing to wait a moment.");
        yield return new WaitForSeconds(6);
        Debug.Log("Finished waiting. Now setting vars and calling FadeOutMemoryInfo.");
        fadeDuration = 5f;
        //StartCoroutine(FadeOutMemoryInfo(fadeDuration));
    }

    public void StartFadeOutMemoryInfo(float fadeDuration)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMemoryInfo(fadeDuration));
    }

    private IEnumerator FadeOutMemoryInfo(float fadeDuration)
    {
        Debug.Log("FadeOutMemoryInfo successfully called.");

        float time = 0;
        while(time < fadeDuration)
        {
            _displayedText.color = Color.Lerp(_displayedText.color, FadedColor, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
       
        Debug.Log("Done fading out memory info.");
    }

    public void StartFadeInMemoryInfo(float fadeDuration)
    {
        StopAllCoroutines();
        StartCoroutine(FadeInMemoryInfo(fadeDuration));
    }

    private IEnumerator FadeInMemoryInfo(float fadeDuration)
    {
        Debug.Log("FadeInMemoryInfo successfully called.");
        float time = 0;
        while(time < fadeDuration)
        {
            _displayedText.color = Color.Lerp(_displayedText.color, NonFadedColor, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Done fading in memory info.");
    }

    public void SetText(string txt)
    {
        _displayedText.text = txt;
    }
}
