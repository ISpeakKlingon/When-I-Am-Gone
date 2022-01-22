using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    private bool isWindowOpening = false;
    private float openSpeed = 0.5f;
    public AudioClip windowOpening;
    public AudioSource audioSource;
    public GameObject Window;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onWindowOpen += OnWindowOpen;
        audioSource = GetComponent <AudioSource>();
        if (GameEvents.current.WindowOpenEventOccurred)
        {
            //disable window object
            Window.SetActive(false);
        }
    }

    private void Update()
    {
        if (isWindowOpening)
        {
            transform.Translate(Vector3.up * openSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnWindowOpen()
    {
        //slowly move window up
        isWindowOpening = true;
        audioSource.PlayOneShot(windowOpening, 2);
        //Debug.Log("Window is now opening. Enjoy the view!");
    }

    private void OnDestroy()
    {
        GameEvents.current.onWindowOpen -= OnWindowOpen;
    }
}
