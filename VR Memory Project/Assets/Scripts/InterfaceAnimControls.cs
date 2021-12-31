using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InterfaceAnimControls : HandAnimationControl
{
    [SerializeField] Animator interfaceAnimator;
    private float indicatorLightThreshhold = 0.99f;
    public Renderer indicatorLight;
    public Material indicatorOff, indicatorRed, indicatorGreen;

    public override void GripAnimation(InputAction.CallbackContext obj)
    {
        base.GripAnimation(obj);
        interfaceAnimator.SetFloat("Grip", obj.ReadValue<float>());
        if(obj.ReadValue<float>() > indicatorLightThreshhold)
        {
            //turn indicator light on
            if(indicatorLight.material != indicatorRed)
            {
                indicatorLight.material = indicatorRed;
            }
        }
    }
}
