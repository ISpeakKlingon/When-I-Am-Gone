using System.Collections;
using UnityEngine;

public class NeedleObject : MonoBehaviour
{
    public string sceneToLink;

    public void NameSceneToLoadInGameManager()
    {
        GameManager.Instance.sceneName = sceneToLink;
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
        yield return new WaitForSeconds(1.0f);
        NameSceneToLoadInGameManager();
        GameManager.Instance.LoadScene();
    }
}