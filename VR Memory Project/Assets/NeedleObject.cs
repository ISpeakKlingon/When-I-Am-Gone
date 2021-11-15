using UnityEngine;

public class NeedleObject : MonoBehaviour
{
    public string sceneToLink;

    public void LoadGame()
    {
        SceneLoader.Instance.LoadNewScene(sceneToLink);
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