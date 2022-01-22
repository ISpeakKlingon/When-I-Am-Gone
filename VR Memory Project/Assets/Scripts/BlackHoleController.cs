using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    [SerializeField] private Animator _blackHoleAnim;

    private void Start()
    {
        _blackHoleAnim = GetComponentInChildren<Animator>();
        GameEvents.current.onFinalView += OnFinalView;

    }

    private void OnFinalView()
    {
        _blackHoleAnim.SetTrigger("BlackHole");
    }

    private void OnDestroy()
    {
        GameEvents.current.onFinalView -= OnFinalView;

    }
}
