using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryStartManager : MonoBehaviour
{
    public float memoryLength = 15f;
    public string sceneToLink = "Game";

    //load Game scene after ~10 seconds

    private void Awake()
    {

        StartCoroutine(ForceMemoryExit());
    }
    public IEnumerator ForceMemoryExit()
    {
        yield return new WaitForSeconds(memoryLength);
        GameManager.Instance.sceneName = sceneToLink;
        GameManager.Instance.LoadScene();
    }
}
