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
}