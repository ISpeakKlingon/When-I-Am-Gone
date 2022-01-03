using UnityEngine;

//Thanks to Nicky B from I Heart Gamedev YouTube "How to Program in Unity: State Machines Explained" video
public class InterfaceDockedState : InterfaceBaseState
{
    private InterfaceAnimControls animControls;

    
    public override void EnterState(InterfaceStateManager _interface)
    {
        Debug.Log("Hello from the Docked State!");
        //turn off InterfaceAnimControls
        
        //turn off InterfaceAudioController
        //turn off InterfaceHapticController
    }

    public override void UpdateState(InterfaceStateManager _interface)
    {
        //when interface needle is removed

        //immediately switch to the UndockedState
        //_interface.SwitchState(_interface.UndockedState);
    }

    public override void OnTriggerEnter(InterfaceStateManager _interface, Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("MemoryNeedle"))
        {
            Debug.Log("Memory needle collided with interface collider.");
        }
    }

    public override void OnTriggerExit(InterfaceStateManager _interface, Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("MemoryNeedle"))
        {
            _interface.SwitchState(_interface.UndockedState);
        }
    }
}
