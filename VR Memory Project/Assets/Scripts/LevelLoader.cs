using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    //public int transtionTime;
    public float transitionTime = 3f;

    public void LoadNextLevel()
    {
        Debug.Log("Successfully called LoadNextLevel");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadPreviousLevel()
    {
        Debug.Log("Successfully called LoadNextLevel");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        Debug.Log("IEnumerator LoadLevel successfully started.");
        Debug.Log("About to start transition.");

        transition.SetTrigger("Start");
        Debug.Log("SetTrigger for transition Animator to Start was successful.");

        //Wait
        yield return new WaitForSeconds(transitionTime);
        Debug.Log("Successfully waited for transitionTime.");

        //Load scene
        SceneManager.LoadScene(levelIndex);
    }

    public void ReloadLevel()
    {
        //fade restart button and game over text out
        //reload current level
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadTitleScreen()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void LoadLevelOne()
    {
        StartCoroutine(LoadLevel(1));
    }
}
