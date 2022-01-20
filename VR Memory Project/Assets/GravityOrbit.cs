using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOrbit : MonoBehaviour
{
    //the center that all objects orbit

    public float Gravity;

    public bool FixedDirection; //if the gravity of this section is only pushing the player down

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<GravityCtrl>())
        {
            //if this object has a gravity script, set this as the planet
            other.GetComponentInParent<GravityCtrl>().Gravity = this.GetComponent<GravityOrbit>();
        }
    }
}
