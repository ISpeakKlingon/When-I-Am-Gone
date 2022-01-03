using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Thanks to Nicky B from I Heart Gamedev YouTube "How to Program in Unity: Hierarchical State Machine Refactor [Built-In Character Controller #5]" video
public class HandInterfaceUndockedState : HandInterfaceBaseState
{
    public HandInterfaceUndockedState(HandInterfaceStateMachine currentContext, HandInterfaceStateFactory handInterfaceStateFactory): base (currentContext, handInterfaceStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("HELLO FROM THE UNDOCKED STATE");
        _ctx.GripAction.action.performed += InterfaceAnimation;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        _ctx.GripAction.action.performed -= InterfaceAnimation;
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        //if player has needle in hand and the needle is inserted, switch to docked state
        if (_ctx.IsNeedleDocked)
        {
            SwitchState(_factory.Docked());
        }
    }

    private void InterfaceAnimation(InputAction.CallbackContext obj)
    {
        _ctx.InterfaceAnimator.SetFloat("Grip", obj.ReadValue<float>());
        if (obj.ReadValue<float>() > _ctx.IndicatorLightThreshhold)
        {
            //turn indicator light on
            if (_ctx.IndicatorLight.material != _ctx.IndicatorRed)
            {
                _ctx.IndicatorLight.material = _ctx.IndicatorRed;
            }
        }
        else if (obj.ReadValue<float>() < _ctx.IndicatorLightThreshhold)
        {
            if (_ctx.IndicatorLight.material != _ctx.IndicatorOff)
            {
                _ctx.IndicatorLight.material = _ctx.IndicatorOff;
            }
        }

    }
}
