using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Thanks to Nicky B from I Heart Gamedev YouTube "How to Program in Unity: Hierarchical State Machine Refactor [Built-In Character Controller #5]" video
public class HandInterfaceStateMachine : MonoBehaviour
{
    [SerializeField] InputActionReference _gripAction;

    [SerializeField] Animator _interfaceAnimator;
    [SerializeField] Animator _handAnimator;
    private float _indicatorLightThreshhold = 0.99f;
    //public Renderer _socketLight;
    //public Material _indicatorOff, _indicatorRed, _indicatorGreen;
    [SerializeField] Renderer _dockingSocket;
    [SerializeField] Material[] _socketMaterials;
    [SerializeField] Material _greenLight;

    [SerializeField] NeedleObject _currentInjector;

    [SerializeField] InterfaceAudioController _interfaceAudio;

    [SerializeField] private Collider _interfaceCollider;

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
    public Animator HandAnimator { get { return _handAnimator; } }
    public float IndicatorLightThreshhold { get { return _indicatorLightThreshhold; } }
    //public Renderer SocketLight { get { return _socketLight; } }
    //public Material IndicatorOff { get { return _indicatorOff; } }
    //public Material IndicatorRed { get { return _indicatorRed; } }
    public Material[] SocketMaterials { get { return _socketMaterials; } set { _socketMaterials = value; } }
    public Material GreenLight { get { return _greenLight; } }
    public InterfaceAudioController InterfaceAudio { get { return _interfaceAudio; }  }


    private void Awake()
    {
        //setup state
        _states = new HandInterfaceStateFactory(this);
        _currentState = _states.Docked();
        _currentState.EnterState();

        _socketMaterials = _dockingSocket.materials;

        _interfaceAudio = GetComponent<InterfaceAudioController>();

        _interfaceAnimator.SetFloat("Grip", 1.0f);

        _interfaceCollider = GetComponent<Collider>();
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
            bool InFront = Dot > 0.85;

            Debug.Log("Needle entered Interface dock collider. Are we aligned? " + InFront + ". That is because the Dot value is currently " + Dot);

            //let's try how we did it in Hull Breach instead
            //float orientation = Vector3.Dot(endOfDock)
            
            if (InFront)
            {
                _currentInjector = other.gameObject.GetComponentInParent<NeedleObject>();
                Debug.Log("Defined the current Injector NeedleObject script.");

                Debug.Log("You loaded the " + _currentInjector.sceneToLink + " scene. Noice.");

                //disable the collider or something so no more collisions can happen until scene transition is done
                _interfaceCollider.enabled = false;




                //only drop if current injector is not Menu Injector
                //var name = other.gameObject.name;
                //Debug.Log("NAME OF CURRENT INJECTOR DOCKED IS " + name);

                //only drop is current injector's Scene To Link string is not "Game"
                if (_currentInjector.sceneToLink != "Game")
                {
                    _currentInjector.Drop();
                    Debug.Log("Asked NeedleObject to call Drop method.");

                    //set position and rotation of Injector to be angled properly.
                    //or...
                    //turn mesh off
                    _currentInjector.TurnOffMeshes();
                    //and enable the menu Injector instead
                    GameManager.Instance.ActivateMemoryNeedle();

                    //_currentInjector.TriggerDockingAnim();
                    //Debug.Log("Triggered Docking animation.");

                    _currentInjector.NameSceneToLoadInGameManager();
                    _currentInjector.StartSceneChange();
                }

                

                _interfaceAudio.Docked = true;

                if(_currentInjector.sceneToLink != "Game")
                {
                    _interfaceAudio.PlayDockingSFX(); //is there a way to not do this at game start?
                }

                _isNeedleDocked = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MemoryNeedle"))
        {
            //define ref to NeedleObject
            _currentInjector = other.gameObject.GetComponentInParent<NeedleObject>();

            Debug.Log("The Injector needle tip linking to " + _currentInjector.sceneToLink + " exited collider!");

            //only if this is bringing us back to the station
            if (_currentInjector.sceneToLink == "Game")
            {
                //call method on NeedleObject to PassName and StartSceneChange
                //StartCoroutine(UndockingProcedure(_currentInjector));
                
                _interfaceAudio.PlayUndockingSFX();


                //turn off collider so nothing can mess up the scene change until it's over
                _interfaceCollider.enabled = false;


                _currentInjector.NameSceneToLoadInGameManager();
                _currentInjector.TriggerUndockingAnim(); //this isn't happening when a memory time-out
                _currentInjector.StartSceneChange();

                _interfaceAudio.Docked = false; //this isn't happening when a memory time-out

                _isNeedleDocked = false; //moving this to UndockingProcedure //this isn't happening when a memory time-out

            }
        }
    }

    public void MemoryTimeOutProcedure()
    {
        //GameManager.Instance.sceneName = scene;
        _currentInjector = GameManager.Instance.leftHandBaseController.GetComponentInChildren<NeedleObject>();
        //Debug.Break();
        _currentInjector.NameSceneToLoadInGameManager();
        _currentInjector.InjectorGrabbed();
        _currentInjector.TriggerUndockingAnim();
        _currentInjector.StartSceneChange();
        _interfaceAudio.Docked = false;
        _isNeedleDocked = false;
    }
    /*
    private IEnumerator UndockingProcedure(NeedleObject _currentInjector)
    {
        StopAllCoroutines();

        _currentInjector.NameSceneToLoadInGameManager();

        //trigger animations
        _currentInjector.TriggerUndockingAnim();

        yield return new WaitForSeconds(0.5f);

        _currentInjector.StartSceneChange();

        _isNeedleDocked = false;
    }
    /*
    /*
    public IEnumerator RingsGreen()
    {
        Debug.Log("Received call to start Rings Green coroutine.");
        yield return new WaitForSeconds(0.5f);

        //change ring 1
        //var mats = SocketMaterials;
        //mats[1] = GreenLight;
        //SocketMaterials = mats;
        _socketMaterials[1] = _greenLight;
        _dockingSocket.materials = _socketMaterials;
        Debug.Log("Changed first ring to green.");

        //wait
        yield return new WaitForSeconds(0.3f);

        //change ring 2
        //mats[2] = GreenLight;
        //SocketMaterials = mats;
        _socketMaterials[2] = _greenLight;
        _dockingSocket.materials = _socketMaterials;
        Debug.Log("Changed second ring to green.");


        //wait
        yield return new WaitForSeconds(0.3f);

        //change ring 3
        //mats[3] = GreenLight;
        //SocketMaterials = mats;
        _socketMaterials[3] = _greenLight;
        _dockingSocket.materials = _socketMaterials;
        Debug.Log("Changed third ring to green.");

    }
    */
}
