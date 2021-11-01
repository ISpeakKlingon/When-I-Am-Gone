using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabInteractableHelper : MonoBehaviour
{
    private XRSocketInteractor socket;
    bool isSocketActive;

    public GameObject needleSocket;

    private void Start()
    {
        XRSocketInteractor socket = needleSocket.GetComponent<XRSocketInteractor>();
    }

    public void DisableSocketWithDelay()
    {
        Invoke("DisableSocket", 0.5f);
    }

    private void DisableSocket()
    {
        needleSocket.SetActive(false);
        Debug.Log("Disabled socket after 0.5 seconds");
    }
}
