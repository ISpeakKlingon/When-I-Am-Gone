using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onMemory2020TriggerEnter;
    public void Memory2020TriggerEnter()
    {
        if (onMemory2020TriggerEnter != null)
        {
            onMemory2020TriggerEnter();
        }
    }

    public void TriggerEvent(string passedEvent)
    {
        Invoke(passedEvent,0);
    }
}
