using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WristMenu : MonoBehaviour
{
    public GameObject wristUI;
    public bool activeWristUI = true;

    private void Start()
    {
        DisplayWristUI();
    }

    public void MenuPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
            DisplayWristUI();
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

    public void SubtitlesOn()
    {
        GameManager.Instance.subtitles = true;
    }

    public void SubtitlesOff()
    {
        GameManager.Instance.subtitles = false;
    }
}
