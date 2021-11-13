using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerOrientation : MonoBehaviour
{
    public enum WorldState
    {
        Grounded, //on ground
        InAir, //in the air
    }

    [HideInInspector]
    public WorldState States;

    private DetectCollision Colli;

    float delta;

    [Header("Physics")]
    public Transform[] GroundChecks;

    public LayerMask GroundLayers; //what layers the ground can be
    public float GravityRotationSpeed = 10f; //how fast we rotate to a new gravity direction

    [Header("Stats")]
    public float Speed = 15f; //max speed for basic movement
    public float Acceleration = 4f; //how quickly we build speed
    public float turnSpeed = 2f;
    private Vector3 MovDirection, movepos, targetDir, GroundDir; //where to move to

    public float gravity = -0.5f;
    public float additionalHeight = 0.2f;

    private float fallingSpeed;
    private XRRig rig;
    private CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Awake()
    {
        Colli = GetComponent<DetectCollision>();
        GroundDir = transform.up;
        SetGrounded();
    }

    private void FixedUpdate()
    {
        //CapsuleFollowHeadset();

        delta = Time.deltaTime;

        if(States == WorldState.Grounded)
        {
            Debug.Log("Grounded");
            float Spd = Speed;

            MoveSelf(delta, Spd, Acceleration);

            //switch to air
            bool Ground = Colli.CheckGround(-GroundDir);

            if (!Ground)
            {
                SetInAir();
                return;
            }
        }
        
        else if (States == WorldState.InAir)
        {
            Debug.Log("InAir");
            FallingCtrl(delta, Speed, Acceleration);

            //check for ground
            bool Ground = Colli.CheckGround(-GroundDir);

            if (Ground)
            {
                SetGrounded();
                return;
            }
        }
            
    }

    //transition to ground
    public void SetGrounded()
    {
        States = WorldState.Grounded;
        fallingSpeed = 0;
    }

    //transition to air
    public void SetInAir()
    {
        States = WorldState.InAir;
    }

    //check the angle of the floor we are stood on
    Vector3 FloorAngleCheck()
    {
        RaycastHit HitFront;
        RaycastHit HitCentre;
        RaycastHit HitBack;

        Physics.Raycast(GroundChecks[0].position, -GroundChecks[0].transform.up, out HitFront, 10f, GroundLayers);
        Physics.Raycast(GroundChecks[1].position, -GroundChecks[1].transform.up, out HitCentre, 10f, GroundLayers);
        Physics.Raycast(GroundChecks[2].position, -GroundChecks[2].transform.up, out HitBack, 10f, GroundLayers);

        Vector3 HitDir = transform.up;

        if (HitFront.transform != null)
        {
            HitDir += HitFront.normal;
        }
        if (HitCentre.transform != null)
        {
            HitDir += HitCentre.normal;
        }
        if (HitBack.transform != null)
        {
            HitDir += HitBack.normal;
        }

        Debug.DrawLine(transform.position, transform.position + (HitDir.normalized * 5f), Color.red);

        return HitDir.normalized;
    }

    //move our character
    void MoveSelf(float d, float Speed, float Accel)
    {
        float TurnSpd = turnSpeed;

        Vector3 SetGroundDir = FloorAngleCheck();
        GroundDir = Vector3.Lerp(GroundDir, SetGroundDir, d * GravityRotationSpeed);

        //lerp mesh slower when not on ground
        RotateSelf(SetGroundDir, d, GravityRotationSpeed);
    }

    //rotate the direction we face down
    void RotateSelf(Vector3 Direction, float d, float GravitySpd)
    {
        Vector3 LerpDir = Vector3.Lerp(transform.up, Direction, d * GravitySpd);
        transform.rotation = Quaternion.FromToRotation(transform.up, LerpDir) * transform.rotation;
    }

    void FallingCtrl(float d, float Speed, float Accel)
    {
        Debug.Log("Falling");
        Vector3 SetGroundDir = FloorAngleCheck();
        GroundDir = Vector3.Lerp(GroundDir, SetGroundDir, d * GravityRotationSpeed);

        //lerp mesh slower when not on ground
        RotateSelf(GroundDir, d, GravityRotationSpeed);

        //move character down with gravity
        fallingSpeed += gravity * Time.fixedDeltaTime;
        character.Move(transform.up * fallingSpeed * Time.deltaTime);
    }
}
