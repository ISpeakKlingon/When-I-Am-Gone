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

    private float timeBeforeFirstLine = 3f;

    private AudioClip[] convoOne, convoTwo;

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

    private void Start()
    {
        GameEvents.current.onStartGame += OnPlayerWakesUp;
        GameEvents.current.onMemory2020TriggerEnter += OnMemory2020Proximity;

        convoOne = robot.GetComponent<PlayVoiceOvers>().convoOne;
        convoTwo = robot.GetComponent<PlayVoiceOvers>().convoTwo;
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

    private void OnPlayerWakesUp()
    {
        this.GetComponent<PlayVoiceOvers>().SpeakLines(convoOne);
        //this.GetComponent<PlaySubtitles>().ShowSubtitle(0);


    }

    private void OnMemory2020Proximity()
    {
        this.GetComponent<PlayVoiceOvers>().SpeakLine(convoTwo,0);
        //this.GetComponent<PlaySubtitles>().ShowSubtitle(convoTwo, 0);
    }

    private void OnDestroy()
    {
        GameEvents.current.onMemory2020TriggerEnter -= OnMemory2020Proximity;
        GameEvents.current.onStartGame -= OnPlayerWakesUp;
    }
}
