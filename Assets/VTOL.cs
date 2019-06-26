using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VTOL : MonoBehaviour
{
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
    }

    void ExecuteInputs(float deltaTime)
    {
        Vector3 force = verticalForce * VerticalThrust * transform.up + forwardForce * ForwardThrust * transform.forward;
        Vector3 torque = pitchForce * Pitch * transform.right + yawForce * Yaw * transform.up + rollForce * Roll * transform.forward;

        rigidbody.AddForce(force * deltaTime * 60);
        rigidbody.AddTorque(torque * deltaTime * 60);
    }

    void SmoothInputs()
    {

    }
}
