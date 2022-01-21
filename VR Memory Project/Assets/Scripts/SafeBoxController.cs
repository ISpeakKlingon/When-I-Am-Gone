using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeBoxController : MonoBehaviour
{
    //[SerializeField] private BoxCollider _insideBox;

    private void Start()
    {
        //_insideBox = GetComponentInChildren<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PocketWatch")
        {
            GameManager.Instance.PocketWatchSaved = true;
        }
    }
}
