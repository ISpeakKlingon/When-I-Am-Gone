using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravityTestCapsule : MonoBehaviour
{
    public Transform gravityTarget;
    public Vector3 location;
    public GameObject gravityCenter;

    //public float power = 100f;
    //public float torque = 10f;

    public float gravity = 9.81f;

    public bool autoOrient = false;
    public float autoOrientSpeed = 1f;

    Rigidbody rb;
    Collider col;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = gravityCenter.GetComponent<Collider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ProcessInput();
        ProcessGravity();
    }

    /*void ProcessInput()
    {
        //Accelerate
        float vt = Input.GetAxis("Vertical");
        Vector3 force = new Vector3(0f, 0f, vt = power);
        rb.AddRelativeForce(force);

        //Turn
        float hz = Input.GetAxis("Horizontal");
        Vector3 rforce = new Vector3(0f, hz * torque, 0f);
        rb.AddRelativeTorque(rforce);
    }*/

    void ProcessGravity()
    {
        //define closest point
        Vector3 closestPoint = col.ClosestPoint(location);

        Vector3 diff = transform.position - closestPoint;
        rb.AddForce(diff.normalized * gravity * (rb.mass));
        Debug.DrawRay(transform.position, diff.normalized, Color.red);

        if (autoOrient) { AutoOrient(-diff); }
    }

    void AutoOrient(Vector3 down)
    {
        Quaternion orientationDirection = Quaternion.FromToRotation(-transform.up, down) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, orientationDirection, autoOrientSpeed * Time.deltaTime);
    }
}
