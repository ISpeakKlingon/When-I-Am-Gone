using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverStartManager : MonoBehaviour
{

    public PromptCanvasController _leftHandDisplayPrompt;

    private void Awake()
    {
        GameManager.Instance.isGameStarted = false;
    }

    //turn on GameOver text and other info like credits

    private void Start()
    {
        GameEvents.current.ResetAllEvents();

        //reset ispocketwatchsaved bool in game manager
        GameManager.Instance.PocketWatchSaved = false;

        //reset isgamestarted bool in game manager
        GameManager.Instance.isGameStarted = false;


        _leftHandDisplayPrompt = GameManager.Instance.leftHandBaseController.GetComponentInChildren<PromptCanvasController>();

        float waitTime = 0f;
        float fadeDuration = 0.1f;
        _leftHandDisplayPrompt.FadeOutText(waitTime, fadeDuration);

    }
}
