using UnityEngine;

public class NeedleObject : MonoBehaviour
{
    public void LoadGame()
    {
        SceneLoader.Instance.LoadNewScene("Menu");
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