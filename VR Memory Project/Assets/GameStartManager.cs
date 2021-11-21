using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public GameObject timer;
    private TimerController timerController;

    private void Start()
    {
        //Debug.Log("GameStartManager has Started.");

        timerController = timer.GetComponent<TimerController>();

        //Debug.Log("Asking TimerController script to ActivateTimer().");

        timerController.ActivateTimer();

        //Debug.Log("Successfully asked TimerController script to ActivateTimer().");
    }

}
