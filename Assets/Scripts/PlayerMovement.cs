using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float motorForce = 500f;
    public float brakeForce = 1000f;
    public float maxSteerAngle = 15f;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider backLeftWheelCollider;
    public WheelCollider backRightWheelCollider;

    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform backLeftWheelTransform;
    public Transform backRightWheelTransform;

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentBrakeForce;
    private bool isBraking;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1.0f, 0);
    }

    private void Update()
    {
        GetInput();
        HandleMotor();
        HadleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = -verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = -verticalInput * motorForce;
        backLeftWheelCollider.motorTorque = -verticalInput * motorForce;
        backRightWheelCollider.motorTorque = -verticalInput * motorForce;

        currentBrakeForce =isBraking ? brakeForce : 0f;
        ApplyBrake();
    }

    private void ApplyBrake()
    {
        frontRightWheelCollider.brakeTorque = currentBrakeForce;
        frontLeftWheelCollider.brakeTorque = currentBrakeForce; 
        backLeftWheelCollider.brakeTorque = currentBrakeForce;
        backRightWheelCollider.brakeTorque = currentBrakeForce; 
    }

    private void HadleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void updateOneWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
    private void UpdateWheels()
    {
        updateOneWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        updateOneWheel(frontRightWheelCollider, frontRightWheelTransform);
        updateOneWheel(backLeftWheelCollider, backLeftWheelTransform);
        updateOneWheel(backRightWheelCollider, backRightWheelTransform);
    }
}
