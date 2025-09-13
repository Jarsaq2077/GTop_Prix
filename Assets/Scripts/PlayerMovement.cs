using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float motorForce;
    public float brakeForce;
    public float maxSteerAngle;

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

    public AudioSource engine;
    public AudioSource drift;
    public AudioMixer carMixer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1.0f, 0);
        if (engine != null)
        {
            engine.loop = true;
            engine.Play();
        }
    }

    private void Update()
    {
        GetInput();
        HandleMotor();
        HadleSteering();
        UpdateWheels();
        HandleSounds();
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
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
        
        //backLeftWheelCollider.brakeTorque = currentBrakeForce;
        //backRightWheelCollider.brakeTorque = currentBrakeForce; 
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
    private void OnTriggerEnter(Collider other)
    {
        
    }
     private void HandleSounds()
    {
        float speed = rb.linearVelocity.magnitude;
        float dB = Mathf.Lerp(0f, 3f, speed / 50f);
        carMixer.SetFloat("MasterEngine", dB);

        if (drift != null)
        {
            if (isBraking && !drift.isPlaying)
            {
                drift.Play();
            }
            else if (!isBraking && drift.isPlaying)
            {
                drift.Stop();
            }
        }
    }
}
