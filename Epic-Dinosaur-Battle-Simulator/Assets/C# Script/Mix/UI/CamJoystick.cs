using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamJoystick : MonoBehaviour
{
    public float speed;
    public FixedJoystick axisXZJoystick;
    public FixedJoystick axisYJoystick;
    public Camera playerCamera;

    public void FixedUpdate()
    {
        playerCamera.transform.position += new Vector3(axisXZJoystick.Vertical * speed * Time.fixedDeltaTime, 0f, -axisXZJoystick.Horizontal * speed * Time.fixedDeltaTime);
        playerCamera.transform.position += new Vector3(0f, axisYJoystick.Vertical * speed * Time.fixedDeltaTime, 0f);
    }
}
