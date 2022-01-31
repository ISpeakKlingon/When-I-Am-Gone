using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

//Thanks to Linkira Studios' YouTube video "UnityXR Tutorial - Creating a Wrist Menu with new Input System"
public class WristMenu : MonoBehaviour
{
    public GameObject wristUI;
    public GameObject rayController;
    public bool activeWristUI = true;
    public bool uIRaycast = true;

    public XRNode uIRayCastHandNode;

    public XRBaseController uIRaycastController;

    public ScriptManager ScriptManager;

    public GameObject EnglishButton, FrenchButton, RussianButton;

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

        //turn on language buttons
        EnglishButton.SetActive(true);
        FrenchButton.SetActive(true);
        RussianButton.SetActive(true);
    }

    public void SubtitlesOff()
    {
        GameManager.Instance.subtitles = false;
        GameEvents.current.SubtitlesOff();

        uIRaycastController.SendHapticImpulse(0.5f, 0.2f);

        //turn off language buttons
        EnglishButton.SetActive(false);
        FrenchButton.SetActive(false);
        RussianButton.SetActive(false);
    }

    public void SubtitlesEnglish()
    {
        ScriptManager.overrideLanguage = "";
        ScriptManager.ChangeLanguage();
        uIRaycastController.SendHapticImpulse(0.5f, 0.2f);
    }

    public void SubtitlesFrench()
    {
        ScriptManager.overrideLanguage = "fr";
        ScriptManager.ChangeLanguage();
        uIRaycastController.SendHapticImpulse(0.5f, 0.2f);
    }

    public void SubtitlesRussian()
    {
        ScriptManager.overrideLanguage = "ru";
        ScriptManager.ChangeLanguage();
        uIRaycastController.SendHapticImpulse(0.5f, 0.2f);
    }
}
