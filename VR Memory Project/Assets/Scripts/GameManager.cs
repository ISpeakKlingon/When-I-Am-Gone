using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    //get reference to NeedleSocketInteractorOnHand
    public GameObject memoryNeedle;
    public GameObject needleSocketInteractorOnHand;
    private XRSocketInteractor socket;

    //editable string to name next scene to load. The Needle objects do this in their component.
    public string sceneName;


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
    }

    //deactive memory needle object
    public void DeactivateMemoryNeedle()
    {
        memoryNeedle.SetActive(false);
    }
}