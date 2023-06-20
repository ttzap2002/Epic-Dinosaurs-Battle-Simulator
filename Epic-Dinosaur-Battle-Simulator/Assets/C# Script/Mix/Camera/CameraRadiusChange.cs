using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraRadiusChange : MonoBehaviour
{
    //public Camera playerCamera;
    public float rotationSpeed = 0.25f;
    private Vector3 touchStart;

    void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && GameManager.Instance.IsRun && CamJoystick.isNotClicked /*&& !camera.IsUnityNull()*/)
        {
            // Pobierz pozycjê dotyku na ekranie
            Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            // Oblicz kierunek i prêdkoœæ obrotu kamery
            float rotationX = -touchDeltaPosition.y * rotationSpeed;
            float rotationY = touchDeltaPosition.x * rotationSpeed;

            // Obróæ kamerê
            transform.localEulerAngles += new Vector3( rotationX, rotationY, 0f);
            //if(transform.rotation.y + rotationX < 180 && transform.rotation.y + rotationX > 0)
            //    transform.Rotate(rotationX, rotationY, 0, Space.Self);
            //else
            //    transform.Rotate(0, rotationY, /*0*/rotationX, Space.World);
        }
    }
}
