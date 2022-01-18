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

    [SerializeField] private HandInterfaceStateMachine _handInterface;

    private void Awake()
    {
        StartCoroutine(ForceMemoryExit());
    }

    private void Start()
    {
        _handInterface = GameManager.Instance.leftHandBaseController.GetComponentInChildren<HandInterfaceStateMachine>();
    }

    public IEnumerator ForceMemoryExit()
    {
        yield return new WaitForSeconds(memoryLength);
        //GameManager.Instance.sceneName = sceneToLink;

        //trigger undocking anim in HandInterfaceStateMachine
        //switch _interfaceAudio.Docked bool to false in  HandInterfaceStateMachine
        //switch _isNeedleDocked bool to false in HandInterfaceStateMachine
        _handInterface.MemoryTimeOutProcedure();

        //GameManager.Instance.LoadScene();
    }

    private void OnDestroy()
    {
        /*
        if(nameOfThisScene == "Memory1945")
        {
            GameManager.Instance.isMemory1945Complete = true;
        }
        else if (nameOfThisScene == "Memory2020")
        {
            GameManager.Instance.isMemory2020Complete = true;
        }
        */

        //pass name of this memory to Game Events to switch bool as complete
        GameEvents.current.MarkMemoryComplete(nameOfThisScene);
    }
}
