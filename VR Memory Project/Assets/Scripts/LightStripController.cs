using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStripController : MonoBehaviour
{
    private Animator _lightStripAnimator;

    private void Start()
    {
        _lightStripAnimator = GetComponent<Animator>();
    }

    public void StartStripAnim(float convoTime)
    {
        StartCoroutine(StripAnim(convoTime));
    }

    private IEnumerator StripAnim(float convoTime)
    {
        _lightStripAnimator.SetBool("Speaking", true);
        yield return new WaitForSeconds(convoTime);
        _lightStripAnimator.SetBool("Speaking", false);
    }
}
