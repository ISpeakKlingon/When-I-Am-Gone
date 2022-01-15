using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class InjectorHapticController : MonoBehaviour
{
    public XRNode InjectorHandNode;
    public XRBaseController InjectorController;
    public XRBaseController InterfaceController;

    private void Start()
    {
        //InjectorController = GetComponentInParent<XRBaseController>();
        InjectorController = GameManager.Instance.rightHandBaseController.GetComponent<XRBaseController>();
        InterfaceController = GameManager.Instance.leftHandBaseController.GetComponent<XRBaseController>();
    }

    public void InjectorNeedleOn()
    {
        InjectorController.SendHapticImpulse(0.1f, 0.25f);
        //Debug.Log("sent haptic impulse for InjectorNeedleOn method.");
    }

    public void InjectorNeedleOff()
    {
        InjectorController.SendHapticImpulse(0.1f, 0.25f);
        //Debug.Log("sent haptic impulse for InjectorNeedleOff method.");
    }

    public void InjectorUndocking()
    {
        InterfaceController.SendHapticImpulse(.8f, .5f);
    }

    public void InjectorDocking()
    {
        InterfaceController.SendHapticImpulse(.8f, .5f);
    }

}
