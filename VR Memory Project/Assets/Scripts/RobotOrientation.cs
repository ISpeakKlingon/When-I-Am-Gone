using UnityEngine;

public class RobotOrientation : MonoBehaviour
{
    public enum WorldState
    {
        RobotGrounded,
        RobotInAir,
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
    private Vector3 GroundDir; //where to move to

    public float gravity = -0.5f;
    public float additionalHeight = 02f;

    private float fallingSpeed;
    private CharacterController robot;

    private void Awake()
    {
        Colli = GetComponent<DetectCollision>();
        robot = GetComponent<CharacterController>();
        GroundDir = transform.up;
        SetRobotGrounded();
    }

    private void FixedUpdate()
    {
        delta = Time.deltaTime;

        if(States == WorldState.RobotGrounded)
        {
            float Spd = Speed;

            MoveSelf(delta, Spd, Acceleration);

            bool Ground = Colli.CheckGround(-GroundDir);

            if (!Ground)
            {
                SetRobotInAir();
                return;
            }
        }

        else if (States == WorldState.RobotInAir)
        {
            FallingCtrl(delta, Speed, Acceleration);

            bool Ground = Colli.CheckGround(-GroundDir);

            if (Ground)
            {
                SetRobotGrounded();
                return;
            }
        }
    }

    public void SetRobotGrounded()
    {
        States = WorldState.RobotGrounded;
        fallingSpeed = 0;
    }

    public void SetRobotInAir()
    {
        States = WorldState.RobotInAir;
    }

    void MoveSelf(float d, float Speed, float Accel)
    {
        Vector3 SetGroundDir = FloorAngleCheck();
        GroundDir = Vector3.Lerp(GroundDir, SetGroundDir, d * GravityRotationSpeed);

        RotateSelf(SetGroundDir, d, GravityRotationSpeed);
    }

    Vector3 FloorAngleCheck()
    {
        RaycastHit HitFront;
        RaycastHit HitCenter;
        RaycastHit HitBack;

        Physics.Raycast(GroundChecks[0].position, -GroundChecks[0].transform.up, out HitFront, 10f, GroundLayers);
        Physics.Raycast(GroundChecks[1].position, -GroundChecks[0].transform.up, out HitCenter, 10f, GroundLayers);
        Physics.Raycast(GroundChecks[2].position, -GroundChecks[0].transform.up, out HitBack, 10f, GroundLayers);

        Vector3 HitDir = transform.up;

        if(HitFront.transform != null)
        {
            HitDir += HitFront.normal;
        }
        if(HitCenter.transform != null)
        {
            HitDir += HitCenter.normal;
        }
        if(HitBack.transform != null)
        {
            HitDir += HitBack.normal;
        }

        return HitDir.normalized;
    }

    void RotateSelf(Vector3 Direction, float d, float GravitySpd)
    {
        Vector3 LerpDir = Vector3.Lerp(transform.up, Direction, d * GravitySpd);
        transform.rotation = Quaternion.FromToRotation(transform.up, LerpDir) * transform.rotation;
    }

    void FallingCtrl(float d, float Speed, float Accel)
    {
        Vector3 SetGroundDir = FloorAngleCheck();
        GroundDir = Vector3.Lerp(GroundDir, SetGroundDir, d * GravityRotationSpeed);

        RotateSelf(GroundDir, d, GravityRotationSpeed);

        fallingSpeed += gravity * Time.fixedDeltaTime;
        //robot.Move(transform.up * fallingSpeed * Time.deltaTime);
    }
}
