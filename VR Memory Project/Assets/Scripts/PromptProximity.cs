using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptProximity : MonoBehaviour
{
    [SerializeField] private NeedleObject _injector;

    private void Start()
    {
        _injector = GetComponentInParent<NeedleObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _injector.ProxTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _injector.ProxTriggerExit(other);
    }
}
