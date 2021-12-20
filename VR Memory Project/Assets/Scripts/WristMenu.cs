using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class WristMenu : MonoBehaviour
{
    public GameObject wristUI;
    public GameObject rayController;
    public bool activeWristUI = true;
    public bool uIRaycast = true;

    public XRNode uIRayCastHandNode;

    public XRBaseController uIRaycastController;

    private void Start()
    {
        DisplayWristUI();
        EnableUIRaycast();


    }

    public void MenuPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DisplayWristUI();
            EnableUIRaycast();
        }
    }

    public void DisplayWristUI()
    {
        if (activeWristUI)
        {
            wristUI.SetActive(false);
            activeWristUI = false;
        }
        else if (!activeWristUI)
        {
            wristUI.SetActive(true);
            activeWristUI = true;
        }
    }

    public void EnableUIRaycast()
    {
        if (uIRaycast)
        {
            rayController.SetActive(false);
            uIRaycast = false;
        }
        else if (!uIRaycast)
        {
            rayController.SetActive(true);
            uIRaycast = true;
        }
    }

    public void SubtitlesOn()
    {
        GameManager.Instance.subtitles = true;
        GameEvents.current.SubtitlesOn();

        uIRaycastController.SendHapticImpulse(0.5f, 0.2f);
    }

    public void SubtitlesOff()
    {
        GameManager.Instance.subtitles = false;
        GameEvents.current.SubtitlesOff();

        uIRaycastController.SendHapticImpulse(0.5f, 0.2f);
    }
}
