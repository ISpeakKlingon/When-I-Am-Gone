using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public GameObject timer;
    private TimerController timerController;

    private void Start()
    {
        timerController = timer.GetComponent<TimerController>();

        timerController.ActivateTimer();
    }

}
