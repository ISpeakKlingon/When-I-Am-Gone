using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    //get reference to NeedleSocketInteractorOnHand
    public GameObject needleSocketInteractorOnHand;
    private XRSocketInteractor socket;

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
}
