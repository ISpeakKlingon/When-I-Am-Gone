using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotController : MonoBehaviour
{
    //[SerializeField] private Transform movePositionTransform;
    [SerializeField] private Vector3 robotPos;
    private NavMeshAgent navMeshAgent;
    public GameObject robot;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (!GameManager.Instance.isGameStarted)
        {
            RobotToZero();

        }
        else
            RobotToPrevious();
    }

    

    void Update()
    {
        //navMeshAgent.destination = movePositionTransform.position;
        robotPos = robot.transform.position;
        GameManager.Instance.robotPos = robotPos;
    }

    public void SetDestination(Vector3 newDestination)
    {
        Debug.Log("New robot destination set.");
        navMeshAgent.destination = newDestination;
    }

    public void RobotToZero()
    {
        robot.transform.position = new Vector3(-2, 0, 0);
        robot.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    public void RobotToPrevious()
    {
        robot.transform.position = GameManager.Instance.robotPos;
    }
}
