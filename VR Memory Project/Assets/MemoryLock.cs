using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryLock : MonoBehaviour
{
    public List<GameObject> memoryButtons;

    private int sequenceLength = 6;
    private int seqIndex = -1;

    public int[] lockSequence;

    private int globalIndex = -1;

    public bool canPress = true;

    private void Start()
    {
        //show or hide objects?

        //lockSequence = new int[sequenceLength];
        //SetUpLockSequence(); //this might be breaking unity so commenting out for now

        //just go ahead and set values for array until I can figure out a better way
        lockSequence = new int[6] { 0, 1, 2, 3, 4, 5 };
        Debug.Log("The first lockSequence integer is " + lockSequence[0]);
    }

    private void SetUpLockSequence()
    {
        int number = 0;
        int last_number = -1;
        int index = 0;

        do
        {
            number = Random.Range(0, sequenceLength + 1); //this picks a random number
            //leaving out the above line should hopefully generate a lockSequence of 0,1,2,3,4,5?
            //ok i think this is not working and is breaking unity lol
            if (number == last_number) continue;
            lockSequence[index] = number;
            index++;
            last_number = number;
        } while (index < sequenceLength);
    }

    //return helper functions
    public int[] getLockSequence()
    {
        return lockSequence;
    }
    
    public int getGlobalIndex()
    {
        return globalIndex;
    }

    public void setGlobalIndex(int num)
    {
        globalIndex = num;
    }

    public void MemoryUnlocked()
    {
        //play sound
        //show objects
        //load new memory
    }

    public bool getCanPress()
    {
        return canPress;
    }
}
