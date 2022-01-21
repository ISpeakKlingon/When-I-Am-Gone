using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PocketWatchController : MonoBehaviour
{
    public Material[] Saved;
    public Material[] Charred;
    [SerializeField] private MeshRenderer _pocketWatch;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Memory1945")
        {
            _pocketWatch = GetComponentInChildren<MeshRenderer>();

            if (GameManager.Instance.PocketWatchSaved)
            {
                _pocketWatch.materials = Saved;
            }
            else
            {
                _pocketWatch.materials = Charred;
            }
        }
    }
}
