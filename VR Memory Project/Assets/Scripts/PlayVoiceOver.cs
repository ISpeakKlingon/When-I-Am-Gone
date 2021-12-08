using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayVoiceOver : MonoBehaviour
{
    private AudioSource audioSource;
    //private bool isAlreadyTriggered;
    //public string nameOfVoiceOver;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!ScriptManager.Instance.CheckIfVOPlayed())
            {
                audioSource.Play();

                //set bool to true
                //isAlreadyTriggered = true;
                ScriptManager.Instance.MarkVOPlayed(this.name);
            }
        }
    }
}
