using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverStartManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.isGameStarted = false;
    }

    //turn on GameOver text and other info like credits
}
