using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class NeedleObject : MonoBehaviour
{
    public string sceneToLink;
    public GameObject needleCollider;
    [SerializeField] private bool _injectorGrabbed;
    private bool _activeNeedle = false;
    private Animator _animator;
    //private XRBaseInteractable _xRBaseInteractable;
    //public XRInteractionManager XRInteractionManager;
    private XRGrabInteractable _xRGrabInteractable;

    //used for debugging vector lines
    //[SerializeField] float hitDist = 1000;

    public bool Docked = false;

    [SerializeField] private Renderer _injectorMesh, _needleMesh, _buttonMesh, _lockmesh;

    [SerializeField] private PromptCanvasController _leftHandDisplayPrompt;
    [SerializeField] private PromptCanvasController _displayPrompt;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        //_xRBaseInteractable = GetComponent<XRBaseInteractable>();
        _xRGrabInteractable = GetComponent<XRGrabInteractable>();

    }

    private void Start()
    {
        _leftHandDisplayPrompt = GameManager.Instance.leftHandBaseController.GetComponentInChildren<PromptCanvasController>();
        
        if (sceneToLink == "Game")
        {
            /*
            StartCoroutine(ChangeLayerMaskWithDelay());
            _activeNeedle = true;
            _animator.SetTrigger("InjectorToggle"); //moving this to happen after deactivation
            _animator.SetTrigger("InjectorDocking");
            //needleCollider.SetActive(true);
            */
            //display prompt text for how to exit memory
            _leftHandDisplayPrompt.SetText("To exit memory, remove inserted Memory Injector by grabbing with opposite hand.");
            var waitTime = 5f;
            var fadeDuration = 7f;
            _leftHandDisplayPrompt.FadeInText(waitTime, fadeDuration);
        }
        
        GameEvents.current.onPrimaryPressed += OnPrimaryPressed;
        if(sceneToLink == "Game")
        {
            _animator.SetTrigger("InjectorToggle");
        }

        else
        {
            _displayPrompt = GetComponentInChildren<PromptCanvasController>();
        }

    }
    
    private void OnEnable()
    {
        //_leftHandDisplayPrompt = GameManager.Instance.leftHandBaseController.GetComponentInChildren<PromptCanvasController>();

        if (sceneToLink == "Game")
        {
            StartCoroutine(ChangeLayerMaskWithDelay());
            _activeNeedle = true;
            _animator.SetTrigger("InjectorToggle"); //moving this to happen after deactivation
            _animator.SetTrigger("InjectorDocking");
            //needleCollider.SetActive(true);
            /*
            //display prompt text for how to exit memory
            var waitTime = 5f;
            var fadeDuration = 7f;
            _leftHandDisplayPrompt.FadeInText(waitTime, fadeDuration);
            */
        }

    }
    
    private IEnumerator ChangeLayerMaskWithDelay()
    {
        yield return new WaitForSeconds(5f);
        _xRGrabInteractable.interactionLayerMask = 15;

    }

    /*
    public void PrimaryPressed(InputAction.CallbackContext context)
    {
        if (context.performed && _injectorGrabbed)
        {
            ToggleInjectorNeedle();
        }
    }
    */
    //alternative primary press method
    public void OnPrimaryPressed()
    {
        if (_injectorGrabbed)
        {
            ToggleInjectorNeedle();
        }
    }

    public void NameSceneToLoadInGameManager()
    {
        GameManager.Instance.sceneName = sceneToLink;
        //Debug.Log("Passed " + sceneToLink + " scene to Game Manager for loading.");
    }

    public void TurnOnLeftHandSocket()
    {
        GameManager.Instance.TurnOnLeftHandSocket();
    }

    public void TurnOffLeftHandSocket()
    {
        GameManager.Instance.TurnOffLeftHandSocket();
    }

    public void StartSceneChange()
    {
        //StopAllCoroutines();
        StartCoroutine(SceneChange());
    }

    private IEnumerator SceneChange()
    {
        //NameSceneToLoadInGameManager();
        //Debug.Log("Passing " + sceneToLink + " scene name to Game Manager for loading.");
        yield return new WaitForSeconds(1.0f);
        GameManager.Instance.LoadScene();
    }

    public void ToggleInjectorNeedle()
    {
        //Debug.Log("Injector Needle toggled.");

        if (_activeNeedle)
        {
            //needleCollider.SetActive(false);
            _animator.SetTrigger("InjectorToggle");
            _activeNeedle = false;

            if(sceneToLink != "Game" && _injectorGrabbed)
            {
                StartCoroutine(ChangeInjectorPrompt("To activate Memory Injector, press 'A'."));

                var waitTime = 0f;
                var fadeDuration = 0.5f;
                _leftHandDisplayPrompt.FadeOutText(waitTime, fadeDuration);
            }
        }
        else if (!_activeNeedle)
        {
            //needleCollider.SetActive(true);
            _animator.SetTrigger("InjectorToggle");
            _activeNeedle = true;

            if(sceneToLink != "Game" && _injectorGrabbed)
            {
                StartCoroutine(ChangeInjectorPrompt("Insert Memory Injector into Hand Interface."));

                _leftHandDisplayPrompt.SetText("Squeeze fist to activate Hand Interface.");
                var waitTime = 0f;
                var fadeDuration = 0.5f;
                _leftHandDisplayPrompt.FadeInText(waitTime, fadeDuration);
            }
        }
    }

    private IEnumerator ChangeInjectorPrompt(string txt)
    {
        var waitTime = 0f;
        var fadeDuration = 0.5f;
        _displayPrompt.FadeOutText(waitTime, fadeDuration);

        //wait a moment
        yield return new WaitForSeconds(1);

        // send text to PromptCanvasController to display
        _displayPrompt.SetText(txt);
        _displayPrompt.FadeInText(waitTime, fadeDuration);

    }

    public void InjectorGrabbed()
    {
        _injectorGrabbed = true;

        //turn off left hand prompt text if this is the menu injector
        if(sceneToLink == "Game")
        {
            var waitTime = 0f;
            var fadeDuration = 0.5f;
            _leftHandDisplayPrompt.FadeOutText(waitTime, fadeDuration);
        }
        
        if (!_activeNeedle && sceneToLink != "Game")
        {
            var waitTime = 0f;
            var fadeDuration = 0.5f;
            // send text to PromptCanvasController to display
            _displayPrompt.SetText("To activate Memory Injector, press 'A'.");
            _displayPrompt.FadeInText(waitTime, fadeDuration);
        }
        else if(sceneToLink != "Game")
        {
            var waitTime = 0f;
            var fadeDuration = 0.5f;
            // send text to PromptCanvasController to display
            _displayPrompt.SetText("Insert Memory Injector into Hand Interface.");
            _displayPrompt.FadeInText(waitTime, fadeDuration);
            _leftHandDisplayPrompt.FadeInText(waitTime, fadeDuration);

        }
    }

    public void InjectorReleased()
    {
        _injectorGrabbed = false;

        if (sceneToLink != "Game")
        {
            var waitTime = 0f;
            var fadeDuration = 0.5f;
            // send text to PromptCanvasController to display
            _displayPrompt.FadeOutText(waitTime, fadeDuration);

            _leftHandDisplayPrompt.FadeOutText(waitTime, fadeDuration);
        }

    }

    public void TriggerDockingAnim()
    {
        _animator.SetTrigger("InjectorDocking");
    }

    public void TriggerUndockingAnim()
    {
        _animator.SetTrigger("InjectorUndocking");
    }

    public void Drop()
    {
        Debug.Log("Starting the Drop method call in NeedleObject script.");
        //XRInteractionManager.CancelInteractableSelection(_xRGrabInteractable); //this isn't working right.
        //Debug.Log("Asked XRInteractionManager to CancelInteractableSelection.");
        _xRGrabInteractable.interactionLayerMask = 0;
        _injectorGrabbed = false;
    }

    private void OnDestroy()
    {
        GameEvents.current.onPrimaryPressed -= OnPrimaryPressed;

    }

    public void TurnOffMeshes()
    {
        _injectorMesh.enabled = false;
        _needleMesh.enabled = false;
        _buttonMesh.enabled = false;
        _lockmesh.enabled = false;
    }

    private void OnDisable()
    {
        if(sceneToLink == "Game")
        {
            _xRGrabInteractable.interactionLayerMask = 0;

            _animator.SetTrigger("InjectorToggle");
        }
        else
        {
            /*
            StartCoroutine(ChangeLayerMaskWithDelay());
            _activeNeedle = true;
            _animator.SetTrigger("InjectorToggle"); //moving this to happen after deactivation
            _animator.SetTrigger("InjectorDocking");
            //needleCollider.SetActive(true);
            */
            //display prompt text for how to exit memory
            _leftHandDisplayPrompt.SetText("To exit memory, remove inserted Memory Injector by grabbing with opposite hand.");
            var waitTime = 5f;
            var fadeDuration = 7f;
            _leftHandDisplayPrompt.FadeInText(waitTime, fadeDuration);
        }

    }
}