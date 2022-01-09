using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Thanks to Nicky B from I Heart Gamedev YouTube "How to Program in Unity: Hierarchical State Machine Refactor [Built-In Character Controller #5]" video
public class HandInterfaceDockedState : HandInterfaceBaseState
{
    public HandInterfaceDockedState(HandInterfaceStateMachine currentContext, HandInterfaceStateFactory handInterfaceStateFactory): base (currentContext, handInterfaceStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("HELLO FROM THE DOCKED STATE");
        _ctx.InterfaceAnimator.SetFloat("Grip", 1.0f);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {

    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        //if player has needle in interface and the needle is pulled out, switch to undocked state
        if (!_ctx.IsNeedleDocked)
        {
            SwitchState(_factory.Undocked());
        }
    }
}
