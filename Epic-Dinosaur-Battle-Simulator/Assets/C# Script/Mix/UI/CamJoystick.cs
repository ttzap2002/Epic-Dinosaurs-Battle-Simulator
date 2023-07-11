using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamJoystick : MonoBehaviour
{
    public float speed;
    public FixedJoystick axisXZJoystick;
    public FixedJoystick axisYJoystick;
    public GameObject playerCamera;
    public static bool isNotClicked = true;
    private float osZ;
    private float osX;

    public void FixedUpdate()
    {
        if (axisXZJoystick.Vertical != 0 || axisXZJoystick.Horizontal != 0 || axisYJoystick.Vertical != 0)
        {
            Vector3 difference = playerCamera.transform.position;
            osX = axisXZJoystick.Vertical * Time.fixedDeltaTime;
            osZ = axisXZJoystick.Horizontal * Time.fixedDeltaTime;
            playerCamera.transform.Translate(osZ * speed, 0, osX * speed * 3,Space.Self);
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, difference.y, playerCamera.transform.position.z);
            playerCamera.transform.position += new Vector3(0f, axisYJoystick.Vertical * speed * Time.fixedDeltaTime, 0f);
            CamJoystick.isNotClicked = false;
        }
        else
            CamJoystick.isNotClicked = true;
    }

}
