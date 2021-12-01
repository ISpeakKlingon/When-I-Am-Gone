using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitStartingRoom : MonoBehaviour
{
    public GameObject gameStartManagerObject;
    private GameStartManager gameStartManager;
    // Start is called before the first frame update
    void Start()
    {
        gameStartManager = gameStartManagerObject.GetComponent<GameStartManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.isExitedStartingRoom)
        {
            gameStartManager.ExitedStartingRoom(); //scene events get activated, like robot moving etc.
            GameManager.Instance.ExitedStartingRoom(); //sets bool to true
        }
    }
}
