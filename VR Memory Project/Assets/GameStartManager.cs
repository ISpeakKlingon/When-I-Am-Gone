using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public GameObject timer, robot;
    private TimerController timerController;
    private RobotController robotController;

    public Vector3 windowView;

    private void Start()
    {
        //Debug.Log("GameStartManager has Started.");

        timerController = timer.GetComponent<TimerController>();

        robotController = robot.GetComponent<RobotController>();

        //Debug.Log("Asking TimerController script to ActivateTimer().");

        timerController.ActivateTimer();

        //Debug.Log("Successfully asked TimerController script to ActivateTimer().");
    }

    public void FinalMinute()
    {
        //move robot to window
        robotController.SetDestination(windowView);
    }

}
