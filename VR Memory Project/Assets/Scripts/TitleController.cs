using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public Text DisplayedText;
    public Color FadedColor;
    public Color NonFadedColor;
    public int TitleDisplayLength = 5;
    public float WaitTime = 1f;
    public float FadeDuration = 5f;

    private void Start()
    {
        DisplayedText = GetComponentInChildren<Text>();
        DisplayedText.color = FadedColor;
        StartCoroutine(TitleFade(WaitTime, FadeDuration));
    }

    private IEnumerator TitleFade(float waitTime, float fadeDuration)
    {
        //Debug.Log("TitleFade coroutine starting. Now waiting " + waitTime + " seconds.");
        
        yield return new WaitForSeconds(waitTime);
        
        //Debug.Log("TitleFade coroutine done waiting to start. Now fading in over " + fadeDuration + " seconds.");

        float time = 0;
        while(time < fadeDuration)
        {
            DisplayedText.color = Color.Lerp(DisplayedText.color, NonFadedColor, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        //Debug.Log("TitleFade done fading in. Now waiting " + TitleDisplayLength + " seconds.");

        yield return new WaitForSeconds(TitleDisplayLength);

        //Debug.Log("TitleFade coroutine done showing title. Now fading out over " + fadeDuration + " seconds.");

        time = 0;
        while (time < fadeDuration)
        {
            DisplayedText.color = Color.Lerp(DisplayedText.color, FadedColor, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

    }
}
