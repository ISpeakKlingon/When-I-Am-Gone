using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InterfaceAnimControls : HandAnimationControl
{
    [SerializeField] Animator interfaceAnimator;

    public override void GripAnimation(InputAction.CallbackContext obj)
    {
        base.GripAnimation(obj);
        interfaceAnimator.SetFloat("Grip", obj.ReadValue<float>());
    }
}
