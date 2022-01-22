using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private bool memory2020TriggerEventOccurred = false;
    private bool memory1945TriggerEventOccurred = false;
    private bool lobbyBridgeTriggerEventOccurred = false;
    private bool startGameEventOccurred = false;
    private bool playerExitedStartingRoom = false;
    private bool smallTalkEventOccurred = false;
    private bool memory2020AwakenEventOccurred = false;
    private bool memory1945AwakenEventOccurred = false;
    private bool finalMinuteEventOccurred = false;
    private bool windowOpenEventOccurred = false;
    private bool isMemory2020Complete = false;
    private bool isMemory1945Complete = false;
    private bool _primaryPressed = false;
    private bool _givingUpEventOccurred = false;
    private bool _finalViewEventOccurred = false;

    private void Awake()
    {
        current = this;
    }

    public void MarkMemoryComplete(string nameOfMemory)
    {
        if (nameOfMemory == "Memory2020")
            isMemory2020Complete = true;
        else if (nameOfMemory == "Memory1945")
            isMemory1945Complete = true;
    }

    public event Action onSubtitlesOn;
    public void SubtitlesOn()
    {
        if (onSubtitlesOn != null)
        {
            onSubtitlesOn();
        }
    }

    public event Action onSubtitlesOff;
    public void SubtitlesOff()
    {
        if (onSubtitlesOff != null)
        {
            onSubtitlesOff();
        }
    }

    public event Action onStartGame;
    public void StartGame()
    {
        if(onStartGame!=null && !startGameEventOccurred)
        {
            onStartGame();
            startGameEventOccurred = true;
        }
    }

    public event Action onExitedStartingRoom;
    public void ExitedStartingRoom()
    {
        if(onExitedStartingRoom !=null && !playerExitedStartingRoom)
        {
            onExitedStartingRoom();
            playerExitedStartingRoom = true;
        }
    }

    public event Action onMemory2020TriggerEnter;
    public event Action onMemory2020Awaken;
    public void Memory2020TriggerEnter()
    {
        if (onMemory2020TriggerEnter != null && !memory2020TriggerEventOccurred)
        {
            onMemory2020TriggerEnter();
            memory2020TriggerEventOccurred = true;
        }
        else if (onMemory2020TriggerEnter != null && !memory2020AwakenEventOccurred && isMemory2020Complete)
        {
            onMemory2020Awaken();
            memory2020AwakenEventOccurred = true;
        }
    }

    public event Action onMemory1945TriggerEnter;
    public event Action onMemory1945Awaken;
    public void Memory1945TriggerEnter()
    {
        if(onMemory1945TriggerEnter !=null && !memory1945TriggerEventOccurred)
        {
            onMemory1945TriggerEnter();
            memory1945TriggerEventOccurred = true;
        }
        else if (onMemory1945TriggerEnter != null && !memory1945AwakenEventOccurred && isMemory1945Complete)
        {
            onMemory1945Awaken();
            memory1945AwakenEventOccurred = true;
        }
    }

    public event Action onLobbyBridgeTiggerEnter;
    public void LobbyBridgeTriggerEnter()
    {
        if(onLobbyBridgeTiggerEnter != null && !lobbyBridgeTriggerEventOccurred)
        {
            onLobbyBridgeTiggerEnter();
            lobbyBridgeTriggerEventOccurred = true;
        }
    }

    public event Action onSmallTalk;
    public void SmallTalk()
    {
        if(onSmallTalk != null && !smallTalkEventOccurred)
        {
            onSmallTalk();
            smallTalkEventOccurred = true;
        }
    }

    public event Action onFinalMinute;
    public void FinalMinute()
    {
        if(onFinalMinute != null && !finalMinuteEventOccurred)
        {
            onFinalMinute();
            finalMinuteEventOccurred = true;
        }
    }

    public event Action onWindowOpen;
    public void WindowOpen()
    {
        if(onWindowOpen != null && !windowOpenEventOccurred)
        {
            onWindowOpen();
            windowOpenEventOccurred = true;
        }
    }

    public event Action onGivingUp;
    public void GivingUp()
    {
        if(onGivingUp != null && !_givingUpEventOccurred)
        {
            onGivingUp();
            _givingUpEventOccurred = true;
        }
    }

    public event Action onFinalView;
    public void FinalView()
    {
        if(onFinalView != null && !_finalViewEventOccurred)
        {
            onFinalView();
            _finalViewEventOccurred = true;
        }
    }

    public void ResetAllEvents()
    {
        memory2020TriggerEventOccurred = false;
        memory1945TriggerEventOccurred = false;
        lobbyBridgeTriggerEventOccurred = false;
        startGameEventOccurred = false;
        playerExitedStartingRoom = false;
        smallTalkEventOccurred = false;
        memory2020AwakenEventOccurred = false;
        memory1945AwakenEventOccurred = false;
        finalMinuteEventOccurred = false;
        windowOpenEventOccurred = false;
        isMemory2020Complete = false;
        isMemory1945Complete = false;
        _givingUpEventOccurred = false;
        _finalViewEventOccurred = false;
}

    public void TriggerEvent(string passedEvent)
    {
        Invoke(passedEvent,1);
    }

    public event Action onPrimaryPressed;
    public void PrimaryPressed(InputAction.CallbackContext context)
    {
        if(context.performed && !_primaryPressed)
        {
            _primaryPressed = true;
            onPrimaryPressed();
            StartCoroutine(PrimaryCooldown());
        }
    }

    private IEnumerator PrimaryCooldown()
    {
        yield return new WaitForSeconds(1f);
        _primaryPressed = false;
    }
}
