using UnityEngine;

//Thanks to Nicky B from I Heart Gamedev YouTube "How to Program in Unity: State Machines Explained" video
public abstract class InterfaceBaseState
{
    public abstract void EnterState(InterfaceStateManager _interface);

    public abstract void UpdateState(InterfaceStateManager _iterface);

    public abstract void OnTriggerEnter(InterfaceStateManager _interface, Collider collision);

    public abstract void OnTriggerExit(InterfaceStateManager _interface, Collider collision);
}
