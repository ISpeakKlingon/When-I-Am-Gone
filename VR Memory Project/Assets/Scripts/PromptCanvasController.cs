using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptCanvasController : MonoBehaviour
{
    public Text DisplayedText;
    public Color FadedColor;
    public Color NonFadedColor;

    private void Start()
    {
        DisplayedText = GetComponentInChildren<Text>();
        DisplayedText.color = FadedColor;
    }

    public void FadeOutText(float waitTime, float fadeDuration)
    {
        StopAllCoroutines();
        StartCoroutine(StartFadeOutText(waitTime, fadeDuration));
    }

    private IEnumerator StartFadeOutText(float waitTime, float fadeDuration)
    {
        yield return new WaitForSeconds(waitTime);
        float time = 0;
        while(time < fadeDuration)
        {
            DisplayedText.color = Color.Lerp(DisplayedText.color, FadedColor, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void FadeInText(float waitTime, float fadeDuration)
    {
        StopAllCoroutines();
        StartCoroutine(StartFadeInText(waitTime, fadeDuration));
    }

    private IEnumerator StartFadeInText(float waitTime, float fadeDuration)
    {
        yield return new WaitForSeconds(waitTime);
        float time = 0;
        while(time < fadeDuration)
        {
            DisplayedText.color = Color.Lerp(DisplayedText.color, NonFadedColor, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
