using System.Collections;
using UnityEngine;

public class NeedleObject : MonoBehaviour
{
    public string sceneToLink;

    
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
}