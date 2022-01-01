using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class InterfaceHapticControls : MonoBehaviour
{
    public XRNode interfaceHandNode;
    public XRBaseController interfaceController;

    private void Start()
    {
        interfaceController = GetComponentInParent<XRBaseController>();
    }

    public void InterfaceDoorsOpen()
    {
        interfaceController.SendHapticImpulse(0.1f, 0.05f);
        //Debug.Log("sent haptic impulse for InterfaceDoorsOpen method.");
    }

    public void InterfaceDoorsClose()
    {
        interfaceController.SendHapticImpulse(0.1f, 0.05f);
        //Debug.Log("sent haptic impulse for InterfaceDoorsClose method.");
    }

}
