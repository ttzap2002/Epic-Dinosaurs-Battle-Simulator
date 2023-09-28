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
    public GameObject fakedPlayerCamera;
    public static bool isNotClicked = true;
    private float osZ;
    private float osX;
    private Vector3 startingPositionOfCamera;
    private Quaternion startingRotationOfCamera;

    private void Start()
    {
        startingPositionOfCamera = playerCamera.transform.position;
        startingRotationOfCamera = playerCamera.transform.rotation;
    }

    public void FixedUpdate()
    {
        if (axisXZJoystick.Vertical != 0 || axisXZJoystick.Horizontal != 0 || axisYJoystick.Vertical != 0)
        {
            osX = axisXZJoystick.Vertical * Time.fixedDeltaTime;
            osZ = axisXZJoystick.Horizontal * Time.fixedDeltaTime;
            fakedPlayerCamera.transform.Translate(osZ * speed, 0, osX * speed,Space.Self);
            playerCamera.transform.position = new Vector3(fakedPlayerCamera.transform.position.x, playerCamera.transform.position.y + axisYJoystick.Vertical * speed * Time.fixedDeltaTime, fakedPlayerCamera.transform.position.z);
            CamJoystick.isNotClicked = false;
        }
        else
            CamJoystick.isNotClicked = true;
    }

    public void RestartCameraPosition()
    {
        playerCamera.transform.position = startingPositionOfCamera;
        playerCamera.transform.rotation = startingRotationOfCamera;
    }

}
