using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class NeedleObject : MonoBehaviour
{
    public string sceneToLink;
    public GameObject needleCollider;
    private bool _injectorGrabbed;
    private bool _activeNeedle = true;
    private Animator _animator;
    //private XRBaseInteractable _xRBaseInteractable;
    public XRInteractionManager XRInteractionManager;
    private XRGrabInteractable _xRGrabInteractable;

    //used for debugging vector lines
    //[SerializeField] float hitDist = 1000;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        //_xRBaseInteractable = GetComponent<XRBaseInteractable>();
        _xRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void PrimaryPressed(InputAction.CallbackContext context)
    {
        if (context.performed && _injectorGrabbed)
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
        StopAllCoroutines();
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
        Debug.Log("Injector Needle toggled.");

        if (_activeNeedle)
        {
            //needleCollider.SetActive(false);
            _animator.SetTrigger("InjectorToggle");
            _activeNeedle = false;
        }
        else if (!_activeNeedle)
        {
            //needleCollider.SetActive(true);
            _animator.SetTrigger("InjectorToggle");
            _activeNeedle = true;
        }
    }

    public void InjectorGrabbed()
    {
        _injectorGrabbed = true;
    }

    public void InjectorReleased()
    {
        _injectorGrabbed = false;
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
}