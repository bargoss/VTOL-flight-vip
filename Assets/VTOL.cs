using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VTOL : MonoBehaviour
{
    public ParticleSystem[] forwardBoostEffects;
    public ParticleSystem[] verticalBoostEffects;

    float _verticalThrust;
    float _forwardThrust;

    public float VerticalThrust
    {
        get
        {
            return _verticalThrust;
        }
        set
        {
            _verticalThrust = Mathf.Clamp(value, -1, 1);
        }
    }
    public float ForwardThrust
    {
        get
        {
            return _forwardThrust;
        }
        set
        {
            _forwardThrust = Mathf.Clamp(value, -1, 1);
        }
    }

    public float Pitch { get => _pitch; set => _pitch = Mathf.Clamp( value,-1,1); }
    public float Yaw { get => _yaw; set => _yaw = Mathf.Clamp(value, -1, 1); }
    public float Roll { get => _roll; set => _roll = Mathf.Clamp(value, -1, 1); }

    float _pitch; // mouse up, down
    float _yaw; // A,D
    float _roll; // mouse right,left;

    public float verticalForce;
    public float forwardForce;
    public float pitchForce;
    public float yawForce;
    public float rollForce;


    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ExecuteInputs(Time.deltaTime);
        Forces();
        UpdateEffects();
    }
    void Forces()
    {
        WingLift();
    }
    void WingLift()
    {
        {
            Vector3 wingUp = transform.up;
            Vector3 verticalVelocityOnWing = Vector3.Project(-rigidbody.velocity, wingUp);
            Vector3 liftForce = verticalVelocityOnWing * Time.deltaTime * 100;
            Debug.DrawRay(transform.position + transform.forward * 0.1f, liftForce, Color.red, 0.1f * 0.5f);
            rigidbody.AddForce(liftForce);
        }
        {
            Vector3 wingForward = transform.forward;
            Vector3 forwardVelocityOnWing = Vector3.Project(rigidbody.velocity, transform.forward);
            Vector3 liftForce = forwardVelocityOnWing.magnitude * transform.up * Time.deltaTime * 25 * 0.5f;
            Debug.DrawRay(transform.position, liftForce, Color.green, 0.1f);
            rigidbody.AddForce(liftForce);
        }
        {
            Vector3 wingRight = transform.right;
            Vector3 rightVelocityOnWing = Vector3.Project(rigidbody.velocity, transform.right);
            Vector3 liftForce = -rightVelocityOnWing.magnitude * transform.right * Time.deltaTime * 12.5f * 0.5f;
            Debug.DrawRay(transform.position, liftForce, Color.green, 0.1f);
            rigidbody.AddForce(liftForce);
        }
    }
    void ExecuteInputs(float deltaTime)
    {
        Vector3 force = verticalForce * VerticalThrust * transform.up + forwardForce * ForwardThrust * transform.forward;
        Vector3 torque = pitchForce * Pitch * transform.right + yawForce * Yaw * transform.up + rollForce * Roll * transform.forward;

        rigidbody.AddForce(force * deltaTime * 60);
        rigidbody.AddTorque(torque * deltaTime * 60);
    }
    void UpdateEffects()
    {
        {
            bool forwardBoosting = (ForwardThrust > 0.2f);
            foreach (ParticleSystem ps in forwardBoostEffects)
            {
                ParticleSystem.EmissionModule emission = ps.emission;
                emission.enabled = forwardBoosting;
            }
        }
        {
            bool verticalBoosting = (VerticalThrust > 0.2f);
            foreach (ParticleSystem ps in verticalBoostEffects)
            {
                ParticleSystem.EmissionModule emission = ps.emission;
                emission.enabled = verticalBoosting;
            }
        }
    }
}
