using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public UnityEvent OnLoadBegin = new UnityEvent();
    public UnityEvent OnLoadEnd = new UnityEvent();
    public ScreenFader screenFader = null;

    private bool isLoading = false;

    public GameObject xrRig;
    private PlayerOrientation playerOrientation;

    private void Awake()
    {
        SceneManager.sceneLoaded += SetActiveScene;
        playerOrientation = xrRig.GetComponent<PlayerOrientation>();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SetActiveScene;
    }

    public void LoadNewScene(string sceneName)
    {
        if (!isLoading)
            StartCoroutine(LoadScene(sceneName));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        isLoading = true;

        OnLoadBegin?.Invoke();
        yield return screenFader.StartFadeIn();

        // Turn off gravity so player doesn't fall or...
        // Disable Player Orientation script in XR Rig?
        playerOrientation.enabled = false;

        yield return StartCoroutine(UnloadCurrent());

        // For testing
        yield return new WaitForSeconds(1.0f);

        //turn on memory needle object if entering memory
        if (sceneName == "Game")
        {
            GameManager.Instance.DeactivateMemoryNeedle();
            GameManager.Instance.LoadPlayer(); //load the player's position and time remaining they were in when they entered memory
            //GameManager.Instance.GameStart(); //start the game
        }
        else
        {
            //deactivate needle socket "is active"
            GameManager.Instance.TurnOffLeftHandSocket();

            //turn off needle if game over
            if (sceneName != "GameOver")
            {
                //turn on scene change indicator
                GameManager.Instance.TurnOnIndicator();
                GameManager.Instance.ActivateMemoryNeedle();
                GameManager.Instance.SavePlayer(); //remember player pos and time remaining before entering memory
            }

            GameManager.Instance.PlayerToZero();
            GameManager.Instance.TurnOnRightHandDirectInteractor();
        }

        yield return StartCoroutine(LoadNew(sceneName));

        yield return screenFader.StartFadeOut();
        OnLoadEnd?.Invoke();

        isLoading = false;

        // Turn on gravity so player can fall
        playerOrientation.enabled = true;
    }

    private IEnumerator UnloadCurrent()
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        while (!unloadOperation.isDone)
            yield return null;
    }

    private IEnumerator LoadNew(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!loadOperation.isDone)
            yield return null;
    }

    private void SetActiveScene(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
    }
}
