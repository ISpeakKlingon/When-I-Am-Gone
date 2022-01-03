using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Thanks to Nicky B from I Heart Gamedev YouTube "How to Program in Unity: State Machines Explained" video
public class InterfaceStateManager : MonoBehaviour
{
    InterfaceBaseState currentState;
    public InterfaceDockedState DockedState = new InterfaceDockedState();
    public InterfaceUndockedState UndockedState = new InterfaceUndockedState();

    void Start()
    {
        currentState = DockedState;

        currentState.EnterState(this);
    }

    void OnTriggerEnter(Collider collision)
    {
        currentState.OnTriggerEnter(this, collision);
    }

    private void OnTriggerExit(Collider collision)
    {
        currentState.OnTriggerExit(this, collision);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(InterfaceBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
