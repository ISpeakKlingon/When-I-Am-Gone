using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private bool memory2020TriggerEventOccurred = false;
    private bool lobbyBridgeTriggerEventOccurred = false;
    private bool startGameEventOccurred = false;

    private void Awake()
    {
        current = this;
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

    public event Action onMemory2020TriggerEnter;
    public void Memory2020TriggerEnter()
    {
        if (onMemory2020TriggerEnter != null && !memory2020TriggerEventOccurred)
        {
            onMemory2020TriggerEnter();
            memory2020TriggerEventOccurred = true;
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

    public void TriggerEvent(string passedEvent)
    {
        Invoke(passedEvent,1);
    }
}
