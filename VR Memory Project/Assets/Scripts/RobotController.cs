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

    private AudioClip[] convoOne, convoTwo, convoThree;
    private PlayVoiceOvers playVoiceOversScript;
    private PlaySubtitles playSubtitlesScript;

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
        GameEvents.current.onSmallTalk += OnSmallTalk;
        playVoiceOversScript = GetComponent<PlayVoiceOvers>();
        convoOne = playVoiceOversScript.convoOne;
        convoTwo = playVoiceOversScript.convoTwo;
        convoThree = playVoiceOversScript.convoThree;
        playSubtitlesScript = GetComponent<PlaySubtitles>();
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
        playVoiceOversScript.SpeakLines(convoOne);
        playSubtitlesScript.ShowSubtitles(convoOne);
    }

    private void OnSmallTalk()
    {
        playVoiceOversScript.SpeakLines(convoTwo);
        playSubtitlesScript.ShowSubtitles(convoTwo);

    }

    private void OnMemory2020Proximity()
    {
        playVoiceOversScript.SpeakLine(convoThree,0);
        playSubtitlesScript.ShowSubtitle(convoThree, 0);
    }

    private void OnDestroy()
    {
        GameEvents.current.onMemory2020TriggerEnter -= OnMemory2020Proximity;
        GameEvents.current.onStartGame -= OnPlayerWakesUp;
        GameEvents.current.onSmallTalk -= OnSmallTalk;
    }
}
