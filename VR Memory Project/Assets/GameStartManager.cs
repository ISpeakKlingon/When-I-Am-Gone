using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public GameObject timer, robot;
    private TimerController timerController;
    private RobotController robotController;

    public Vector3 windowView;
    public Vector3 lowerLobby;
    //public Vector3 memory1945;
    public Vector3 memory2020;

    private void Start()
    {
        //Debug.Log("GameStartManager has Started.");

        timerController = timer.GetComponent<TimerController>();

        robotController = robot.GetComponent<RobotController>();

        //Debug.Log("Asking TimerController script to ActivateTimer().");

        timerController.ActivateTimer();

        //Debug.Log("Successfully asked TimerController script to ActivateTimer().");


        /*
        if (GameManager.Instance.isMemory2020Complete && !GameManager.Instance.isMemory1945Complete)
        {
            Memory2020Complete();
        }

        if (GameManager.Instance.isMemory1945Complete && GameManager.Instance.isMemory2020Complete)
        {
            BothMemoriesComplete();
        }
        */
    }

    public void ExitedStartingRoom()
    {
        //move robot to 2020 memory
        //
        //
        //
        //

        robotController.SetDestination(memory2020);
    }
    /*
    public void Memory2020Complete()
    {
        //if robot has not already spoken this dialogue
        //then robot talks about what the player saw in 2020

        //move robot to 1945 memory
        robotController.SetDestination(memory1945);
    }

    public void BothMemoriesComplete()
    {
        //if robot has not already spoken this dialogue
        //then robot talks about what the player saw in 1945

        //move robot to lower hallway
        robotController.SetDestination(lowerLobby);
    }
    */
    public void FinalMinute()
    {
        //move robot to window
        robotController.SetDestination(windowView);
    }

}
