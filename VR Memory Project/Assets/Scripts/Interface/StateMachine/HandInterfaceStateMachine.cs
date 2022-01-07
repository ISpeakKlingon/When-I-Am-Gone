using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Thanks to Nicky B from I Heart Gamedev YouTube "How to Program in Unity: Hierarchical State Machine Refactor [Built-In Character Controller #5]" video
public class HandInterfaceStateMachine : MonoBehaviour
{
    [SerializeField] InputActionReference _gripAction;

    [SerializeField] Animator _interfaceAnimator;
    private float _indicatorLightThreshhold = 0.99f;
    //public Renderer _socketLight;
    //public Material _indicatorOff, _indicatorRed, _indicatorGreen;

    //docking variables
    bool _isNeedleDocked = false;
    public Transform InterfaceDock;

    //state variables
    HandInterfaceBaseState _currentState;
    HandInterfaceStateFactory _states;

    //getters and setters
    public HandInterfaceBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public bool IsNeedleDocked { get { return _isNeedleDocked; } }
    public InputActionReference GripAction { get { return _gripAction; } }
    public Animator InterfaceAnimator { get { return _interfaceAnimator; } }
    public float IndicatorLightThreshhold { get { return _indicatorLightThreshhold; } }
    //public Renderer SocketLight { get { return _socketLight; } }
    //public Material IndicatorOff { get { return _indicatorOff; } }
    //public Material IndicatorRed { get { return _indicatorRed; } }
    //public Material IndicatorGreen { get { return _indicatorGreen; } }


    private void Awake()
    {
        //setup state
        _states = new HandInterfaceStateFactory(this);
        _currentState = _states.Docked();
        _currentState.EnterState();
    }

    private void Update()
    {
        _currentState.UpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MemoryNeedle"))
        {
            Debug.Log("needle collided!");
            //get reference to collider object
            var needle = other.gameObject.transform;

            //check if needle is aligned with interface dock
            var dirToTarget = Vector3.Normalize(InterfaceDock.position - needle.position);

            var Dot = Vector3.Dot(needle.forward, dirToTarget);

            //.707 = 45 degrees
            bool InFront = Dot > 0.707;

            Debug.Log("Needle entered Interface dock collider. Are we aligned? " + InFront + ". That is because the Dot value is currently " + Dot);

            //let's try how we did it in Hull Breach instead
            //float orientation = Vector3.Dot(endOfDock)
            
            if (InFront)
            {
                _isNeedleDocked = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MemoryNeedle"))
        {
            _isNeedleDocked = false;
        }
    }
}
