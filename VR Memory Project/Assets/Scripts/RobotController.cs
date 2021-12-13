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

    private AudioClip[] convoOne, convoTwo, convoThree, convoFour, convoFive, convoSix, convoSeven, convoEight;
    private PlayVoiceOvers playVoiceOversScript;
    private PlaySubtitles playSubtitlesScript;

    private Vector3 memory1945 = new Vector3(-43.84f,-0.62f,-13.93f);
    private Vector3 lowerLobby = new Vector3(-20.65f, -5.472931f, -1.181621f);
    private Vector3 windowView = new Vector3(-17.45188f, -5.72f, -15.24f);

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
        GameEvents.current.onMemory2020Awaken += OnMemory2020Awaken;
        GameEvents.current.onMemory1945TriggerEnter += OnMemory1945Proximity;
        GameEvents.current.onMemory1945Awaken += OnMemory1945Awaken;
        GameEvents.current.onFinalMinute += OnFinalMinute;
        GameEvents.current.onWindowOpen += OnWindowOpen;

        playVoiceOversScript = GetComponent<PlayVoiceOvers>();
        convoOne = playVoiceOversScript.convoOne;
        convoTwo = playVoiceOversScript.convoTwo;
        convoThree = playVoiceOversScript.convoThree;
        convoFour = playVoiceOversScript.convoFour;
        convoFive = playVoiceOversScript.convoFive;
        convoSix = playVoiceOversScript.convoSix;
        convoSeven = playVoiceOversScript.convoSeven;
        convoEight = playVoiceOversScript.convoEight;
        playSubtitlesScript = GetComponent<PlaySubtitles>();
    }

    void Update()
    {
        //navMeshAgent.destination = movePositionTransform.position;
        robotPos = robot.transform.position;
        GameManager.Instance.robotPos = robotPos;
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

    private void OnMemory2020Awaken()
    {
        playVoiceOversScript.SpeakLines(convoFour);
        playSubtitlesScript.ShowSubtitles(convoFour);
        StartCoroutine(NewRobotDestination(SumArray(convoFour), memory1945));
    }

    private void OnMemory1945Proximity()
    {
        playVoiceOversScript.SpeakLines(convoFive);
        playSubtitlesScript.ShowSubtitles(convoFive);
    }

    private void OnMemory1945Awaken()
    {
        playVoiceOversScript.SpeakLines(convoSix);
        playSubtitlesScript.ShowSubtitles(convoSix);
        StartCoroutine(NewRobotDestination(SumArray(convoSix), lowerLobby));
    }

    private void OnFinalMinute()
    {
        playVoiceOversScript.SpeakLines(convoSeven);
        playSubtitlesScript.ShowSubtitles(convoSeven);
        StartCoroutine(NewRobotDestination(SumArray(convoSeven), windowView));
    }

    private void OnWindowOpen()
    {
        playVoiceOversScript.SpeakLines(convoEight);
        playSubtitlesScript.ShowSubtitles(convoEight);
    }

    public float SumArray(AudioClip[] audioClipArray)
    {
        float sum = 0;
        foreach (AudioClip item in audioClipArray)
        {
            sum += item.length;
        }
        return sum;
    }

    IEnumerator NewRobotDestination(float waitTime, Vector3 destination)
    {
        yield return new WaitForSeconds(waitTime);
        SetDestination(destination);
    }

    public void SetDestination(Vector3 newDestination)
    {
        //Debug.Log("New robot destination set.");
        navMeshAgent.destination = newDestination;
    }

    private void OnDestroy()
    {
        GameEvents.current.onMemory2020TriggerEnter -= OnMemory2020Proximity;
        GameEvents.current.onStartGame -= OnPlayerWakesUp;
        GameEvents.current.onSmallTalk -= OnSmallTalk;
        GameEvents.current.onMemory2020Awaken -= OnMemory2020Awaken;
        GameEvents.current.onMemory1945TriggerEnter -= OnMemory1945Proximity;
        GameEvents.current.onMemory1945Awaken -= OnMemory1945Awaken;
        GameEvents.current.onFinalMinute -= OnFinalMinute;
        GameEvents.current.onWindowOpen -= OnWindowOpen;
    }
}
