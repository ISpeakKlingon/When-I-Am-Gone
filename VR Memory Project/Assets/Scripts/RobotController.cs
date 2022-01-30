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

    //private float timeBeforeFirstLine = 3f;

    private AudioClip[] convoOne, convoTwo, convoThree, convoFour, convoFive, convoSix, convoSeven, convoEight;
    private PlayVoiceOvers playVoiceOversScript;
    private PlaySubtitles playSubtitlesScript;

    [SerializeField] private Vector3 lobbyBridge = new Vector3(-9.86f, 0.14f, -6.33f);
    [SerializeField] private Vector3 memory2020 = new Vector3(-31.62f, -0.55f, -5.19f);
    [SerializeField] private Vector3 memory1945 = new Vector3(-43.84f,-0.62f,-13.93f);
    [SerializeField] private Vector3 lowerLobby = new Vector3(-20.65f, -5.472931f, -1.181621f);
    [SerializeField] private Vector3 windowView = new Vector3(-17.45188f, -5.72f, -15.24f);
    [SerializeField] private Vector3 outsideNewtonRoom = new Vector3(-3.69f, 0.55f, -5.1f);
    [SerializeField] private Vector3 _lobbyBridgeEnd;
    [SerializeField] private Vector3 _finalLookout;
    [SerializeField] private Vector3 _dreamOfDance;
    [SerializeField] private Vector4 _pocketWatchPos;

    public GameObject LobbyBridge;
    public GameObject Memory2020;
    public GameObject Memory1945;
    public GameObject LowerLobby;
    public GameObject WindowView;
    public GameObject OutSideNewtonRoom;
    public GameObject FinalView;
    public GameObject LobbyBridgeEnd;
    public GameObject FinalLookout;
    public GameObject GivingUpCollider;
    public GameObject DreamOfDancePos;
    public GameObject PocketWatchPos;

    [SerializeField] private LightStripController _lightstrip;

    public GameObject Collider1945;

    [SerializeField] private PromptCanvasController _displayPrompt;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (!GameManager.Instance.isGameStarted)
        {
            RobotToZero();

        }
        else
            RobotToPrevious();

        //define locations of navmesh spots
        lobbyBridge = LobbyBridge.transform.position;
        memory2020 = Memory2020.transform.position;
        memory1945 = Memory1945.transform.position;
        lowerLobby = LowerLobby.transform.position;
        windowView = WindowView.transform.position;
        outsideNewtonRoom = OutSideNewtonRoom.transform.position;
        _lobbyBridgeEnd = LobbyBridgeEnd.transform.position;
        _finalLookout = FinalLookout.transform.position;
        _dreamOfDance = DreamOfDancePos.transform.position;
        _pocketWatchPos = PocketWatchPos.transform.position;
    }

    private void Start()
    {
        GameEvents.current.onStartGame += OnPlayerWakesUp;
        GameEvents.current.onMemory2020TriggerEnter += OnMemory2020Proximity;
        GameEvents.current.onExitedStartingRoom += OnPlayerExitStartingRoom;
        GameEvents.current.onSmallTalk += OnSmallTalk;
        GameEvents.current.onMemory2020Awaken += OnMemory2020Awaken;
        GameEvents.current.onMemory1945TriggerEnter += OnMemory1945Proximity;
        GameEvents.current.onMemory1945Awaken += OnMemory1945Awaken;
        GameEvents.current.onFinalMinute += OnFinalMinute;
        GameEvents.current.onWindowOpen += OnWindowOpen;
        GameEvents.current.onGivingUp += OnGivingUp;
        GameEvents.current.onFinalView += OnFinalView;


        //do I need to subscribe to events that have already occurred?

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

        _lightstrip = GetComponentInChildren<LightStripController>();

        _displayPrompt = GetComponentInChildren<PromptCanvasController>();

    }

    void Update()
    {
        //navMeshAgent.destination = movePositionTransform.position;
        robotPos = robot.transform.position;
        GameManager.Instance.robotPos = robotPos;
    }

    public void RobotToZero()
    {
        robot.transform.position = new Vector3(-1.169f, -0.377f, -0.578f);
        robot.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    public void RobotToPrevious()
    {
        robot.transform.position = GameManager.Instance.robotPos;
    }

    private void OnPlayerWakesUp()
    {
        StopAllCoroutines();
        playVoiceOversScript.SpeakLines(convoOne);
        playSubtitlesScript.ShowSubtitles(convoOne);
        _lightstrip.StartStripAnim(SumArray(convoOne));
        StartCoroutine(NewRobotDestination(SumArray(convoOne), outsideNewtonRoom));
    }

    private void OnPlayerExitStartingRoom()
    {
        StopAllCoroutines();
        playVoiceOversScript.SpeakLine(convoSeven, 8);
        playSubtitlesScript.ShowSubtitle(convoSeven, 8);
        _lightstrip.StartStripAnim(1f);
        StartCoroutine(NewRobotDestination(convoSeven[8].length, lobbyBridge));
    }

    private void OnSmallTalk()
    {
        StopAllCoroutines();
        StartCoroutine(NewRobotDestination(0, _dreamOfDance));
        playVoiceOversScript.SpeakLines(convoTwo);
        playSubtitlesScript.ShowSubtitles(convoTwo);
        _lightstrip.StartStripAnim(SumArray(convoTwo));
        StartCoroutine(NewRobotDestination(SumArray(convoTwo), memory1945));
    }

    private void OnMemory2020Proximity()
    {
        /*
        StopAllCoroutines();
        playVoiceOversScript.SpeakLine(convoThree,0);
        playSubtitlesScript.ShowSubtitle(convoThree, 0);
        */
    }

    private void OnMemory2020Awaken()
    {
        StopAllCoroutines();
        playVoiceOversScript.SpeakLines(convoFour);
        playSubtitlesScript.ShowSubtitles(convoFour);
        _lightstrip.StartStripAnim(SumArray(convoFour));
        StartCoroutine(NewRobotDestination(SumArray(convoFour), memory1945));
    }

    private void OnMemory1945Proximity()
    {
        StopAllCoroutines();
        StartCoroutine(NewRobotDestination(0, memory1945));
        playVoiceOversScript.SpeakLines(convoThree);
        playSubtitlesScript.ShowSubtitles(convoThree);
        _lightstrip.StartStripAnim(SumArray(convoThree));
    }

    private void OnMemory1945Awaken()
    {
        StopAllCoroutines();
        if (!GameManager.Instance.PocketWatchSaved)
        {
            Debug.Log("On Memory1945 Awaken pocket watched saved.");
            StartCoroutine(NewRobotDestination(0f, _pocketWatchPos));
            Debug.Log("New robot destination set to pocket watch.");
            playVoiceOversScript.SpeakLines(convoFour);
            playSubtitlesScript.ShowSubtitles(convoFour);
            _lightstrip.StartStripAnim(SumArray(convoFour));
            float timeToWait = SumArray(convoFour) + 1f;
            StartCoroutine(NewRobotDestination(timeToWait, _lobbyBridgeEnd));
            //eneable the GivingUp Collider
            GivingUpCollider.SetActive(true);

            float waitTime = SumArray(convoFour) - 2;
            float fadeDuration = 1f;
            _displayPrompt.FadeInText(waitTime, fadeDuration);
        }
        else
        {
            StartCoroutine(NewRobotDestination(0f, _pocketWatchPos));
            playVoiceOversScript.SpeakLines(convoSeven);
            playSubtitlesScript.ShowSubtitles(convoSeven);
            _lightstrip.StartStripAnim(SumArray(convoSeven));
            float timeBeforeEnd = SumArray(convoSeven) + 2f;
            StartCoroutine(EndVerticalSlice(timeBeforeEnd));

            //turn off 1945 awaken collider. this is not a good place for this but I AM OUT OF TIME!
            Collider1945.SetActive(false);
        }
    }

    private void OnGivingUp()
    {
        StopAllCoroutines();
        playVoiceOversScript.SpeakLines(convoFive);
        playSubtitlesScript.ShowSubtitles(convoFive);
        _lightstrip.StartStripAnim(SumArray(convoFive));
        //enable the Final View Collider
        FinalView.SetActive(true);
        StartCoroutine(NewRobotDestination(SumArray(convoFive), _finalLookout));

        float waitTime = 0f;
        float fadeDuration = 1f;
        _displayPrompt.FadeOutText(waitTime, fadeDuration);
    }

    private void OnFinalView()
    {
        StopAllCoroutines();
        playVoiceOversScript.SpeakLines(convoSix);
        playSubtitlesScript.ShowSubtitles(convoSix);
        _lightstrip.StartStripAnim(SumArray(convoSix));
        float timeBeforeEnd = SumArray(convoSix) + 2f;
        StartCoroutine(EndVerticalSlice(timeBeforeEnd));
    }

    private IEnumerator EndVerticalSlice(float timeBeforeEnd)
    {
        //fade out to black slowly


        //float timeBeforeEnd = SumArray(convoSix);
        yield return new WaitForSeconds(timeBeforeEnd);
        GameManager.Instance.GameOver();
    }

    private void OnFinalMinute()
    {
        /*
        StopAllCoroutines();
        playVoiceOversScript.SpeakLines(convoSeven);
        playSubtitlesScript.ShowSubtitles(convoSeven);
        StartCoroutine(NewRobotDestination(SumArray(convoSeven), windowView));
        */
    }

    private void OnWindowOpen()
    {
        /*
        StopAllCoroutines();
        playVoiceOversScript.SpeakLines(convoEight);
        playSubtitlesScript.ShowSubtitles(convoEight);
        */
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
        StopAllCoroutines();
        GameEvents.current.onMemory2020TriggerEnter -= OnMemory2020Proximity;
        GameEvents.current.onStartGame -= OnPlayerWakesUp;
        GameEvents.current.onExitedStartingRoom -= OnPlayerExitStartingRoom;
        GameEvents.current.onSmallTalk -= OnSmallTalk;
        GameEvents.current.onMemory2020Awaken -= OnMemory2020Awaken;
        GameEvents.current.onMemory1945TriggerEnter -= OnMemory1945Proximity;
        GameEvents.current.onMemory1945Awaken -= OnMemory1945Awaken;
        GameEvents.current.onFinalMinute -= OnFinalMinute;
        GameEvents.current.onWindowOpen -= OnWindowOpen;
        GameEvents.current.onGivingUp -= OnGivingUp;
        GameEvents.current.onFinalView -= OnFinalView;
    }
}
