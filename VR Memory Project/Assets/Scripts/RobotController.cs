using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotController : MonoBehaviour
{
    //[SerializeField] private Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //navMeshAgent.destination = movePositionTransform.position;
    }

    public void SetDestination(Vector3 newDestination)
    {
        Debug.Log("New robot destination set.");
        navMeshAgent.destination = newDestination;
    }
}
