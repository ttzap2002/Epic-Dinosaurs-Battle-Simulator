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
            //przesuwamy pierwotnie falszywa kamere wzgledem widoku gracza
            osX = axisXZJoystick.Vertical * Time.fixedDeltaTime;
            osZ = axisXZJoystick.Horizontal * Time.fixedDeltaTime;
            fakedPlayerCamera.transform.Translate(osZ * speed, 0, osX * speed,Space.Self);
            fakedPlayerCamera.transform.position = new Vector3(fakedPlayerCamera.transform.position.x, playerCamera.transform.position.y + axisYJoystick.Vertical * speed * Time.fixedDeltaTime, fakedPlayerCamera.transform.position.z);

            //jesli kamera by przeszla granice, to jej zmiana jest cofana
            if (fakedPlayerCamera.transform.position.x < 2.5f || fakedPlayerCamera.transform.position.x > 95f)
                fakedPlayerCamera.transform.position = new Vector3(playerCamera.transform.position.x, fakedPlayerCamera.transform.position.y, fakedPlayerCamera.transform.position.z);
            if (fakedPlayerCamera.transform.position.y < 0.5f || fakedPlayerCamera.transform.position.y > 105f)
                fakedPlayerCamera.transform.position = new Vector3(fakedPlayerCamera.transform.position.x, playerCamera.transform.position.y, fakedPlayerCamera.transform.position.z);
            if (fakedPlayerCamera.transform.position.z < 6.5f || fakedPlayerCamera.transform.position.z > 90f)
                fakedPlayerCamera.transform.position = new Vector3(fakedPlayerCamera.transform.position.x, fakedPlayerCamera.transform.position.y, playerCamera.transform.position.z);

            //przepisujemy wartosci faked camera holder do prawdziwej
            playerCamera.transform.position = new Vector3(fakedPlayerCamera.transform.position.x, fakedPlayerCamera.transform.position.y, fakedPlayerCamera.transform.position.z);
            
            CamJoystick.isNotClicked = false;
        }
        else
            CamJoystick.isNotClicked = true;
    }

    public void RestartCameraPosition()
    {
        playerCamera.transform.position = startingPositionOfCamera;
        playerCamera.transform.rotation = startingRotationOfCamera;
        fakedPlayerCamera.transform.position = startingPositionOfCamera;
        fakedPlayerCamera.transform.rotation = new Quaternion(0, startingRotationOfCamera.y, startingRotationOfCamera.z,startingRotationOfCamera.w);
    }

}
