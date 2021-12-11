using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    private AudioClip[] convoOne, convoTwo;
    private PlayVoiceOvers playVoiceOversScript;
    private PlaySubtitles playSubtitlesScript;

    private void Start()
    {
        GameEvents.current.onLobbyBridgeTiggerEnter += OnLobbyBridgeProximity;
        playVoiceOversScript = GetComponent<PlayVoiceOvers>();
        convoOne = playVoiceOversScript.convoOne;
        convoTwo = playVoiceOversScript.convoTwo;
        playSubtitlesScript = GetComponent<PlaySubtitles>();

    }

    private void OnLobbyBridgeProximity()
    {
        playVoiceOversScript.SpeakLines(convoOne);
        playSubtitlesScript.ShowSubtitles(convoOne);
    }

    private void OnDestroy()
    {
        GameEvents.current.onLobbyBridgeTiggerEnter -= OnLobbyBridgeProximity;
    }
}
