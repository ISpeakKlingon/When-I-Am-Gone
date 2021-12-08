using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public string eventToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //GameEvents.current.Memory2020TriggerEnter();

            //instead pass name of event to trigger to Game Events?
            GameEvents.current.TriggerEvent(eventToTrigger);
        }

    }
}
