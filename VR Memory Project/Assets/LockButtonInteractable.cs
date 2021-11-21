using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 * Class that handles the logic in Game when
 * player presses a button to unlock memory.
 */

public class LockButtonInteractable : MonoBehaviour
{
    [Header("Memory Lock game Objects")]
    public GameObject memoryLock;

    private int[] lockSequence;
    private int globalIndex;

    private bool canPress;

    private void Update()
    {
        lockSequence = memoryLock.GetComponent<MemoryLock>().getLockSequence();
        canPress = memoryLock.GetComponent<MemoryLock>().getCanPress();
        globalIndex = memoryLock.GetComponent<MemoryLock>().getGlobalIndex();
    }

    /**
     * Function called when the user touches a button.
     */
    public void Pressed()
    {
        if (canPress)
        {
            //turn off canPress so other buttons cannot accidentally be pressed
            //turn canPress back on after a moment
            pauseButton();

            Debug.Log("Button was pressed. globalIndex is currently " + globalIndex + " but we are going to bump that up now.");
            globalIndex++;
            Debug.Log("globalIndex increased by 1. globalIndex is now " + globalIndex);
            //Get the lock sequence
            memoryLock.GetComponent<MemoryLock>().setGlobalIndex(globalIndex);

            switch (lockSequence[globalIndex])
            {
                case 0:
                    if (gameObject.name == "D_Sphere")
                    {
                        correctPress();
                        Debug.Log("Correct! " + gameObject + " was pressed and correctPress method was called!");
                    }
                    else
                    {
                        incorrectPress();
                        Debug.Log("Incorrect! :( You pressed " + gameObject + " but you should have pressed D_Sphere.");
                    }
                    break;
                case 1:
                    if (gameObject.name == "Y_Sphere")
                    {
                        correctPress();
                        Debug.Log("Correct! " + gameObject + " was pressed and correctPress method was called!");
                    }
                    else
                    {
                        incorrectPress();
                        Debug.Log("Incorrect! :( You pressed " + gameObject + " but you should have pressed Y_Sphere.");
                    }
                    break;
                case 2:
                    if (gameObject.name == "Z_Sphere")
                    {
                        correctPress();
                        Debug.Log("Correct! " + gameObject + " was pressed and correctPress method was called!");
                    }
                    else
                    {
                        incorrectPress();
                        Debug.Log("Incorrect! :( You pressed " + gameObject + " but you should have pressed Z_Sphere.");
                    }
                    break;
                case 3:
                    if (gameObject.name == "A_Sphere")
                    {
                        correctPress();
                    }
                    else
                    {
                        incorrectPress();
                    }
                    break;
                case 4:
                    if (gameObject.name == "4_Sphere")
                    {
                        correctPress();
                    }
                    else
                    {
                        incorrectPress();
                    }
                    break;
                case 5:
                    if (gameObject.name == "H_Sphere")
                    {
                        correctPress();
                    }
                    else
                    {
                        incorrectPress();
                    }
                    break;
                default:
                    Debug.Log("Default whaaaat");
                    break;
            }
        }
    }

    /**
     * Function called if the user presses the correct
     * button in the sequence.
     */
    private void correctPress()
    {
        Debug.Log("The correctPress method was called.");
        //haptic event
        //update sign or visual

        Debug.Log("globalIndex is currently " + globalIndex + " and lockSequence.length is currently " + lockSequence.Length);

        //If at the end of the lock sequence
        if(globalIndex == lockSequence.Length)
        {
            // Unlocked memory
            memoryLock.GetComponent<MemoryLock>().MemoryUnlocked();
        }
    }

    /**
     * Function called in the user presses the incorrect
     * button in the sequence.
     */
    private void incorrectPress()
    {
        //incorrect haptic
        //update sign
        StartCoroutine(resetGlobalIndex(1));
        Debug.Log("Incorrect button was pressed. Reseting globalIndex.");
    }

    private IEnumerator resetGlobalIndex( int i)
    {
        yield return new WaitForSeconds(i);
        globalIndex = -1;
        memoryLock.GetComponent<MemoryLock>().setGlobalIndex(globalIndex);
        Debug.Log("globalIndex was reset to " + globalIndex);
    }

    private void pauseButton()
    {
        memoryLock.GetComponent<MemoryLock>().canPress = false;
        //visual cue of some kind?
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        StartCoroutine(resetCanPress(1));
    }

    private IEnumerator resetCanPress( int i)
    {
        yield return new WaitForSeconds(i);
        memoryLock.GetComponent<MemoryLock>().canPress = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        Debug.Log("Reset canPress to " + canPress);
    }
}
