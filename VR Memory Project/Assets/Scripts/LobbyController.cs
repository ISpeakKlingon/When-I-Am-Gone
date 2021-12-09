using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    private void Start()
    {
        GameEvents.current.onLobbyBridgeTiggerEnter += OnLobbyBridgeProximity;
    }

    private void OnLobbyBridgeProximity()
    {
        this.GetComponent<PlayVoiceOvers>().SpeakLine(0);
        this.GetComponent<PlaySubtitles>().ShowSubtitle(0);
    }

    private void OnDestroy()
    {
        GameEvents.current.onLobbyBridgeTiggerEnter -= OnLobbyBridgeProximity;
    }
}
