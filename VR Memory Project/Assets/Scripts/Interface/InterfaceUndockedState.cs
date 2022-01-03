using UnityEngine;

//Thanks to Nicky B from I Heart Gamedev YouTube "How to Program in Unity: State Machines Explained" video
public class InterfaceUndockedState : InterfaceBaseState
{
    public override void EnterState(InterfaceStateManager _interface)
    {
        Debug.Log("Hello from the Undocked State!");
        //turn on InterfaceAnimControls
        //turn on InterfaceAudioController
        //turn on InterfaceHapticController
    }

    public override void UpdateState(InterfaceStateManager _interface)
    {
        
    }

    public override void OnTriggerEnter(InterfaceStateManager _interface, Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("MemoryNeedle"))
        {
            _interface.SwitchState(_interface.DockedState);
        }
    }

    public override void OnTriggerExit(InterfaceStateManager _interface, Collider collision)
    {

    }
}
