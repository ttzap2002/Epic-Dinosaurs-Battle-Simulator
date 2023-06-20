using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamJoystick : MonoBehaviour
{
    public float speed;
    public FixedJoystick axisXZJoystick;
    public FixedJoystick axisYJoystick;
    public GameObject playerCamera;
    public static bool isNotClicked = false;

    public void FixedUpdate()
    {
        Vector3 difference = playerCamera.transform.position;
        playerCamera.transform.position += new Vector3(axisXZJoystick.Vertical * speed * Time.fixedDeltaTime, 0f, -axisXZJoystick.Horizontal * speed * Time.fixedDeltaTime);
        playerCamera.transform.position += new Vector3(0f, axisYJoystick.Vertical * speed * Time.fixedDeltaTime, 0f);
        if (difference - playerCamera.transform.position == new Vector3(0f, 0f, 0f))
            CamJoystick.isNotClicked = true;
        else
            CamJoystick.isNotClicked = false;
    }
}
