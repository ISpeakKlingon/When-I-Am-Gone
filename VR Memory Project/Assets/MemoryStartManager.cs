using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryStartManager : MonoBehaviour
{
    public float memoryLength = 30f;
    public string sceneToLink = "Game";

    public string nameOfThisScene;

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

    private void OnDestroy()
    {
        if(nameOfThisScene == "Memory1945")
        {
            GameManager.Instance.isMemory1945Complete = true;
        }
        else if (nameOfThisScene == "Memory2020")
        {
            GameManager.Instance.isMemory2020Complete = true;
        }
    }
}
