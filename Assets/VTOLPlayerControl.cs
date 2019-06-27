using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VTOLPlayerControl : MonoBehaviour
{
    public Camera camera;
    Vector3 camRod;
    Vector3 follower;
    float followDistance = 10;

    VTOL vtol;

    float verticalThrust;
    float forwardThrust;

    float pitch; // mouse up, down
    float yaw; // A,D
    float roll; // mouse right,left;

    // Start is called before the first frame update
    void Start()
    {
        vtol = GetComponent<VTOL>();
        follower = transform.position - transform.forward * 20;
    }

    // Update is called once per frame
    void Update()
    {
        verticalThrust = Input.GetAxis("Vertical");
        forwardThrust = Input.GetAxis("Forward");

        pitch = -Input.GetAxis("Mouse Y");
        yaw = Input.GetAxis("Horizontal");
        roll = -Input.GetAxis("Mouse X");

        vtol.VerticalThrust = verticalThrust;
        vtol.ForwardThrust = forwardThrust;

        vtol.Pitch = pitch;
        vtol.Yaw = yaw;
        vtol.Roll = roll;

        CameraUpdate();
    }
    
    void CameraUpdate2()
    {
        follower.y = transform.position.y;
        Vector3 toFollower = follower - transform.position;
        Vector3 followerTargetPos = transform.position + Vector3.ClampMagnitude(toFollower, followDistance);

        follower = followerTargetPos;
        //follower = Vector3.Lerp(follower, followerTargetPos, Time.deltaTime);

        camera.transform.position = follower + Vector3.up * 3;
        camera.transform.LookAt(transform.position + transform.forward * 3);
    }
    void CameraUpdate()
    {
        Vector3 camrod = transform.forward * -7;
        camrod.y = 0;
        camera.transform.position = transform.position + camrod + Vector3.up * 3f;
        camera.transform.LookAt(transform.position + transform.forward * 1);
    }
}
