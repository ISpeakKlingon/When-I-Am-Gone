using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    //events
    //public delegate void OnGameStart();
    //public static event OnGameStart StartGame;

    //get reference to NeedleSocketInteractorOnHand
    public GameObject memoryNeedle;
    //public GameObject memoryNeedlePrefab;
    public GameObject needleSocketInteractorOnHand;
    public Transform needleSocketTransform;
    //private Vector3 memoryNeedleStartingPos; //not needed?
    //private Quaternion memoryNeedleStartingRot; //not needed?
    private XRSocketInteractor socket;
    public Transform needleSocketVisual;

    private Rigidbody memoryNeedleRb;

    //editable string to name next scene to load. The Needle objects do this in their component.
    public string sceneName;

    public GameObject player;
    private Player playerScript;

    public float gameTime;
    public Vector3 robotPos;

    public bool isGameStarted = false;
    public bool isExitedStartingRoom = false;
    public bool isMemory1945Complete = false;
    public bool isMemory2020Complete = false;

    public GameObject leftHandBaseController;
    private XRDirectInteractor leftDirectInteractor;
    public GameObject rightHandBaseController;
    private XRDirectInteractor rightDirectInteractor;

    public GameObject indicator;

    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("Game Manager is NULL");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        //get reference to xr socket interactor
        socket = needleSocketInteractorOnHand.GetComponent<XRSocketInteractor>();
    }

    private void Start()
    {
        //memoryNeedleStartingPos = needleSocketVisual.transform.localPosition; //not needed?
        //memoryNeedleStartingRot = needleSocketVisual.transform.localRotation; //not needed?

        memoryNeedleRb = memoryNeedle.GetComponent<Rigidbody>();

        //get reference to script on XR Rig called Player
        playerScript = player.GetComponent<Player>();

        //get reference to hand direct interactors
        leftDirectInteractor = leftHandBaseController.GetComponent<XRDirectInteractor>();
        leftDirectInteractor.enabled = false;
        rightDirectInteractor = rightHandBaseController.GetComponent<XRDirectInteractor>();

        //save the Player pos to reset all pos data
        SavePlayer();

        //reset memory needle
        ActivateMemoryNeedle();
    }

    //Did the player unlock the first puzzle? Yes or No?
    public bool GotFirstCode { get; set; }

    //turn left hand socket interactor active
    public void TurnOnLeftHandSocket()
    {
        socket.socketActive = true;
    }

    //turn left hand socket interactor inactive
    public void TurnOffLeftHandSocket()
    {
        socket.socketActive = false;
    }

    //turn right hand direct interactor on
    public void TurnOnRightHandDirectInteractor()
    {
        rightDirectInteractor.enabled = true;
    }

    //turn right hand direct interactor off
    public void TurnOffRightHandDirectInteractor()
    {
        rightDirectInteractor.enabled = false;
    }

    //load a specifically named scene in the SceneLoader
    public void LoadScene()
    {
        SceneLoader.Instance.LoadNewScene(sceneName);
    }

    //activate memory needle object
    public void ActivateMemoryNeedle()
    {
        //turn off left hand xr direct interactor until I can figure out why layer masking the needle isn't working
        leftDirectInteractor.enabled = false;

        memoryNeedle.SetActive(true);
        //Debug.Log("Reseting memory needle position.");
        
        //reset memory needle so that it appears on hand next time it is activated
        memoryNeedleRb.velocity = Vector3.zero;
        memoryNeedleRb.angularVelocity = Vector3.zero;

        //make needle child of correct of NeedleSocketInteractorOnHand
        memoryNeedle.transform.SetParent(needleSocketTransform);

        memoryNeedle.transform.localPosition = new Vector3(0, 0, 0);
        memoryNeedle.transform.localEulerAngles = new Vector3(-90, 0, 0);
    }

    //deactive memory needle object
    public void DeactivateMemoryNeedle()
    {
        memoryNeedle.SetActive(false);
        //Debug.Log("Reseting memory needle position.");

        //turn on left hand xr direct interactor
        //leftDirectInteractor.enabled = true;
    }
    
    public void SavePlayer()
    {
        UpdatePlayerTime();
        UpdateRobotLocation();
        SaveSystem.SavePlayer(playerScript);
        //Debug.Log("Saved " + gameTime + " in PlayerData.");
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        playerScript.level = data.level;
        playerScript.health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        player.transform.position = position;

        playerScript.timeRemaining = data.timeRemaining;

        Vector3 robotPosition;
        robotPosition.x = data.robotPosition[0];
        robotPosition.y = data.robotPosition[1];
        robotPosition.z = data.robotPosition[2];
        robotPos = robotPosition;

        //Debug.Log("Loaded " + gameTime + " from PlayerData.");
    }
    
    //reset player pos to 0
    public void PlayerToZero()
    {
        player.transform.localPosition = new Vector3(0, 0, 0);
        player.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void GameStart()
    {
        //Debug.Log("GameManager has changed isGameStarted bool to true.");
        isGameStarted = true;
    }
    
    public void GameOver()
    {
        Debug.Log("Game over.");

        //turn off scene change indicator
        //TurnOffIndicator();

        //load Game Over scene
        sceneName = "GameOver";

        LoadScene();

        //disable hands or needle
        //memoryNeedle.SetActive(false);
    }

    public void UpdatePlayerTime()
    {
        playerScript.timeRemaining = gameTime;
        //Debug.Log("GameManager passed gameTime of " + gameTime + " to Player script.");
    }

    public void UpdateRobotLocation()
    {
        playerScript.robotPosition = robotPos;
    }

    public void TurnOnIndicator()
    {
        indicator.SetActive(true);
    }

    public void TurnOffIndicator()
    {
        //Debug.Log("Turning off the scene change indicator.");
        indicator.SetActive(false);
    }

    public void Complete1945Memory()
    {
        isMemory1945Complete = true;
    }
    public void Complete2020Memory()
    {
        isMemory2020Complete = true;
    }

    public void ExitedStartingRoom()
    {
        isExitedStartingRoom = true;
    }

    public void SetRobotLocationToPlayer()
    {
        //send robot info to player class

    }
}
