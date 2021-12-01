using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //see brackeys save & load system vid for additional features
    public int level;
    public int health;
    public float[] position;
    public float[] robotPosition;

    public float timeRemaining;

    public PlayerData(Player player)
    {
        level = player.level;
        health = player.health;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        timeRemaining = player.timeRemaining;

        robotPosition = new float[3];
        robotPosition[0] = player.robotPosition.x;
        robotPosition[1] = player.robotPosition.y;
        robotPosition[2] = player.robotPosition.z;

    }
}
