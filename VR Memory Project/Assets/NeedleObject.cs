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

    [SerializeField] private Renderer _injectorMesh, _needleMesh, _buttonMesh, _lockmesh, _needleTip;

    public PromptCanvasController _leftHandDisplayPrompt;
    [SerializeField] private PromptCanvasController _displayPrompt;
    [SerializeField] private bool _promptProximity;
    [SerializeField] private bool _meshesOff = false;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _thisJustCollidedWithProxTrigger;

    [SerializeField] private AudioSource _injectorAudioSource;
    public AudioClip ActivateSound;
    public AudioClip DeactivateSound;
    public AudioClip SuccessDocking;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        //_xRBaseInteractable = GetComponent<XRBaseInteractable>();
        _xRGrabInteractable = GetComponent<XRGrabInteractable>();
        _rb = GetComponent<Rigidbody>();

        _injectorAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        //this line is causing problems as there are 2 PromptCanvasControllers inside left hand
        if(sceneToLink != "Game")
        {
            _leftHandDisplayPrompt = GameManager.Instance.leftHandBaseController.GetComponentInChildren<PromptCanvasController>();
        }

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
            _injectorAudioSource.PlayOneShot(DeactivateSound, 0.2f);

            if(sceneToLink != "Game" && _injectorGrabbed)
            {
                StartCoroutine(ChangeInjectorPrompt("To activate Memory Injector, press 'A'."));

                if (!_promptProximity)
                {
                    var waitTime = 0f;
                    var fadeDuration = 0.5f;
                    _leftHandDisplayPrompt.FadeOutText(waitTime, fadeDuration);
                }
            }
        }
        else if (!_activeNeedle)
        {
            //needleCollider.SetActive(true);
            _animator.SetTrigger("InjectorToggle");
            _activeNeedle = true;
            _injectorAudioSource.PlayOneShot(ActivateSound, 0.2f);

            if(sceneToLink != "Game" && _injectorGrabbed)
            {
                StartCoroutine(ChangeInjectorPrompt("Insert Memory Injector into Hand Interface."));

                _leftHandDisplayPrompt.SetText("Squeeze fist to activate Hand Interface.");
                if (!_promptProximity)
                {
                    var waitTime = 0f;
                    var fadeDuration = 0.5f;
                    _leftHandDisplayPrompt.FadeInText(waitTime, fadeDuration);
                }
            }
        }
    }

    private IEnumerator ChangeInjectorPrompt(string txt)
    {
        if (!_promptProximity && !_meshesOff)
        {
            var waitTime = 0f;
            var fadeDuration = 0.5f;
            _displayPrompt.FadeOutText(waitTime, fadeDuration);
        }

        //wait a moment
        yield return new WaitForSeconds(1);

        // send text to PromptCanvasController to display
        _displayPrompt.SetText(txt);

        if (!_promptProximity && !_meshesOff)
        {
            var waitTime = 0f;
            var fadeDuration = 0.5f;
            _displayPrompt.FadeInText(waitTime, fadeDuration);
        }

    }

    public void InjectorGrabbed()
    {
        _injectorGrabbed = true;

        //turn rb kinematic off
        //_rb.isKinematic = false;

        //turn off left hand prompt text if this is the memory injector
        if (sceneToLink == "Game")
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
            if (!_promptProximity)
            {
                _displayPrompt.FadeInText(waitTime, fadeDuration);
            }
        }
        else if(sceneToLink != "Game")
        {
            var waitTime = 0f;
            var fadeDuration = 0.5f;
            // send text to PromptCanvasController to display
            _displayPrompt.SetText("Insert Memory Injector into Hand Interface.");
            if (!_promptProximity)
            {
                _displayPrompt.FadeInText(waitTime, fadeDuration);
                _leftHandDisplayPrompt.FadeInText(waitTime, fadeDuration);
            }

        }
    }

    public void InjectorReleased()
    {
        _injectorGrabbed = false;

        //_rb.isKinematic = false;

        if (sceneToLink != "Game")
        {
            if (!_promptProximity)
            {
                if (!_meshesOff)
                {
                    var waitTime = 0f;
                    var fadeDuration = 0.5f;
                    // send text to PromptCanvasController to display
                    //_displayPrompt.FadeOutText(waitTime, fadeDuration);

                    _leftHandDisplayPrompt.FadeOutText(waitTime, fadeDuration);
                }
            }
        }

    }

    public void TriggerDockingAnim()
    {
        _animator.SetTrigger("InjectorDocking");
        //_injectorAudioSource.PlayOneShot(SuccessDocking, 1f);
    }

    public void TriggerUndockingAnim()
    {
        _animator.SetTrigger("InjectorUndocking");
        _injectorAudioSource.PlayOneShot(DeactivateSound, 0.2f);
        Debug.Log("Injector just played deactivate sound.");
    }

    public void Drop()
    {
        //Debug.Log("Starting the Drop method call in NeedleObject script.");
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
        _meshesOff = true;
        _injectorMesh.enabled = false;
        _needleMesh.enabled = false;
        _buttonMesh.enabled = false;
        _lockmesh.enabled = false;
        _needleTip.enabled = false;
        _displayPrompt.SetText("");
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
            if(_leftHandDisplayPrompt != null)
            {
                _leftHandDisplayPrompt.SetText("To exit memory, remove inserted Memory Injector by grabbing with opposite hand.");
                var waitTime = 5f;
                var fadeDuration = 7f;
                _leftHandDisplayPrompt.FadeInText(waitTime, fadeDuration);
            }
        }

    }

    public void ProxTriggerEnter(Collider other)
    {
        _thisJustCollidedWithProxTrigger = other;

        if(sceneToLink != "Game" && other.tag == "LeftHandPrompt" && _injectorGrabbed)
        {
            _promptProximity = true;
            var waitTime = 0f;
            var fadeDuration = 0.5f;
            _leftHandDisplayPrompt.FadeOutText(waitTime, fadeDuration);
            _displayPrompt.FadeOutText(waitTime, fadeDuration);
        }
    }

    public void ProxTriggerExit(Collider other)
    {
        if (sceneToLink != "Game" && other.tag == "LeftHandPrompt" && _injectorGrabbed )
        {
            _promptProximity = false;
            var waitTime = 0f;
            var fadeDuration = 0.5f;
            
            _displayPrompt.FadeInText(waitTime, fadeDuration);

            if (_activeNeedle)
            {
                _leftHandDisplayPrompt.FadeInText(waitTime, fadeDuration);
            }
        }
    }
}