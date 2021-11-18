using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    //get reference to NeedleSocketInteractorOnHand
    public GameObject memoryNeedle;
    //public GameObject memoryNeedlePrefab;
    public GameObject needleSocketInteractorOnHand;
    public Transform needleSocketTransform;
    private Vector3 memoryNeedleStartingPos;
    private Quaternion memoryNeedleStartingRot;
    private XRSocketInteractor socket;
    public Transform needleSocketVisual;

    private Rigidbody memoryNeedleRb;

    //editable string to name next scene to load. The Needle objects do this in their component.
    public string sceneName;

    public GameObject player;
    private Player playerScript;

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
        memoryNeedleStartingPos = needleSocketVisual.transform.localPosition;
        memoryNeedleStartingRot = needleSocketVisual.transform.localRotation;

        memoryNeedleRb = memoryNeedle.GetComponent<Rigidbody>();

        //get reference to script on XR Rig called Player
        playerScript = player.GetComponent<Player>();

        //save the Player pos to reset all pos data
        SavePlayer();
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

    //load a specifically named scene in the SceneLoader
    public void LoadScene()
    {
        SceneLoader.Instance.LoadNewScene(sceneName);
    }

    //activate memory needle object
    public void ActivateMemoryNeedle()
    {
        memoryNeedle.SetActive(true);
        //Debug.Log("Reseting memory needle position.");
        
        //reset memory needle so that it appears on hand next time it is activated
        memoryNeedleRb.velocity = Vector3.zero;
        memoryNeedleRb.angularVelocity = Vector3.zero;

        memoryNeedle.transform.localPosition = new Vector3(0, 0, 0);
        memoryNeedle.transform.localEulerAngles = new Vector3(-90, 0, 0);
    }

    //deactive memory needle object
    public void DeactivateMemoryNeedle()
    {
        memoryNeedle.SetActive(false);
        //Debug.Log("Reseting memory needle position.");
    }
    
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(playerScript);
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
    }
    
    //reset player pos to 0
    public void PlayerToZero()
    {
        player.transform.localPosition = new Vector3(0, 0, 0);
        player.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
