using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InterfaceAudioController : MonoBehaviour
{
    [SerializeField] InputActionReference gripAction;
    public AudioClip[] servoSounds;
    private AudioSource audioSource;
    public bool Docked;

    public AudioClip DockingSound;
    public AudioClip SuccessSFX;
    public AudioClip DockingReleaseSFX;

    private void OnEnable()
    {
        gripAction.action.performed += GripAnimation;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void GripAnimation(InputAction.CallbackContext obj)
    {
        if (!Docked)
        {
            if (obj.ReadValue<float>() > 0.1f && obj.ReadValue<float>() < 0.9f)
            {
                //play sound
                if (!audioSource.isPlaying)
                {
                    //play random servo sound
                    PlaySFX(servoSounds, Random.Range(0, servoSounds.Length));
                }
            }
        }
    }

    private void PlaySFX(AudioClip[] servoSounds, int servoLineNumber)
    {
        StopAllCoroutines();
        audioSource.PlayOneShot(servoSounds[servoLineNumber], 3f);
    }

    public void PlayDockingSFX()
    {
        audioSource.PlayOneShot(DockingSound, .6f);
        audioSource.PlayOneShot(SuccessSFX, .7f);
    }

    public void PlayUndockingSFX()
    {
        audioSource.PlayOneShot(DockingSound, .6f);

        audioSource.PlayOneShot(DockingReleaseSFX, 0.6f);
    }
}
