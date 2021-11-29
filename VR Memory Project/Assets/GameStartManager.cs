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

        if (GameManager.Instance.isMemory2020Complete)
        {
            Memory2020Complete();
        }

        if (GameManager.Instance.isMemory1945Complete)
        {
            Memory1945Complete();
        }
    }

    public void Memory2020Complete()
    {
        //if robot has not already spoken this dialogue
        //then robot talks about what the player saw in 2020

        //move robot to 1945 memory
    }

    public void Memory1945Complete()
    {
        //if robot has not already spoken this dialogue
        //then robot talks about what the player saw in 1945

        //move robot to lower hallway
    }

    public void FinalMinute()
    {
        //move robot to window
        robotController.SetDestination(windowView);
    }

}
