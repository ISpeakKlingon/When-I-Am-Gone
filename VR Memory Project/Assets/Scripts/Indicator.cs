using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Indicator : MonoBehaviour
{
    //StackTrace stackTrace; //used to help find out who is calling method

    private Animator animator = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Deactivate();
    }

    public void Show()
    {
        //the following two lines will tell who is calling this method
        //stackTrace = new StackTrace();
        //print("stackTrace !! " + stackTrace.GetFrame(1).GetMethod().Name);

        gameObject.SetActive(true);
        animator.SetBool("Show", true);
    }

    public void Hide()
    {
        animator.SetBool("Show", false);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
