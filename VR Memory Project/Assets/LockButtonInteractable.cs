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

    private void Update()
    {
        lockSequence = memoryLock.GetComponent<MemoryLock>().getLockSequence();
        globalIndex = memoryLock.GetComponent<MemoryLock>().getGlobalIndex();
    }

    /**
     * Function called when the user touches a button.
     */
    public void Pressed()
    {
        globalIndex++;
        //Get the lock sequence
        memoryLock.GetComponent<MemoryLock>().setGlobalIndex(globalIndex);

        switch (lockSequence[globalIndex])
        {
            case 0:
                if(gameObject.name == "D_Sphere")
                {
                    correctPress();
                    Debug.Log("Correct!");
                }
                else
                {
                    incorrectPress();
                    Debug.Log("Incorrect! :(");
                }
                break;
            case 1:
                if (gameObject.name == "Y_Sphere")
                {
                    correctPress();
                }
                else
                {
                    incorrectPress();
                }
                break;
            case 2:
                if (gameObject.name == "Z_Sphere")
                {
                    correctPress();
                }
                else
                {
                    incorrectPress();
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

    /**
     * Function called if the user presses the correct
     * button in the sequence.
     */
    private void correctPress()
    {
        //haptic event
        //update sign or visual
        
        //If at the end of the lock sequence
        if(globalIndex == lockSequence.Length - 1)
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
    }

    private IEnumerator resetGlobalIndex( int i)
    {
        yield return new WaitForSeconds(i);
        globalIndex = -1;
        memoryLock.GetComponent<MemoryLock>().setGlobalIndex(globalIndex);
    }
}
