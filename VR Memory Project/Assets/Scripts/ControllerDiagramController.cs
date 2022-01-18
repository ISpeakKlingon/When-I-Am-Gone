using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerDiagramController : MonoBehaviour
{
    //public Text DisplayedText;
    public Image _diagram;

    public Color FadedColor;
    public Color NonFadedColor;
    //public int DiagramDisplayLength = 5;
    public float WaitTime = 5f;
    public float FadeDuration = 5f;

    private void Start()
    {
        _diagram = GetComponentInChildren<Image>();
        _diagram.color = FadedColor;
        StartCoroutine(DiagramFade(WaitTime, FadeDuration));
    }

    private IEnumerator DiagramFade(float waitTime, float fadeDuration)
    {
        //Debug.Log("TitleFade coroutine starting. Now waiting " + waitTime + " seconds.");

        yield return new WaitForSeconds(waitTime);

        //Debug.Log("TitleFade coroutine done waiting to start. Now fading in over " + fadeDuration + " seconds.");

        float time = 0;
        while (time < fadeDuration)
        {
            _diagram.color = Color.Lerp(_diagram.color, NonFadedColor, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        /*
        //Debug.Log("TitleFade done fading in. Now waiting " + TitleDisplayLength + " seconds.");

        yield return new WaitForSeconds(DiagramDisplayLength);

        //Debug.Log("TitleFade coroutine done showing title. Now fading out over " + fadeDuration + " seconds.");

        time = 0;
        while (time < fadeDuration)
        {
            DisplayedText.color = Color.Lerp(DisplayedText.color, FadedColor, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        */
    }
}
