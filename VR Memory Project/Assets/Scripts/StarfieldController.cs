using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfieldController : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.left, 1f * Time.deltaTime);
    }
}
