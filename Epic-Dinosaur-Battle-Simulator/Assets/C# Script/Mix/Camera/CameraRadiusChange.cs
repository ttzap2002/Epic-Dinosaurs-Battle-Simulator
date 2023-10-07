using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraRadiusChange : MonoBehaviour
{
    //public Camera playerCamera;
    public float rotationSpeed = 0.25f;
    public bool isThisFakeCamera;
    public GameObject fakedCameraHolder;

    void Update()
    {
        //Debug.Log($"{Input.touchCount == 1} {Input.GetTouch(0).phase == TouchPhase.Moved} {GameManager.Instance.IsRun} {CamJoystick.isNotClicked}");
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && GameManager.Instance.IsRun && CamJoystick.isNotClicked /*&& !camera.IsUnityNull()*/)
        {
            // Pobierz pozycjê dotyku na ekranie
            Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            float rotationX = 0;
            // Oblicz kierunek i prêdkoœæ obrotu kamery
            if (!isThisFakeCamera)
                rotationX = -touchDeltaPosition.y * rotationSpeed;
            float rotationY = touchDeltaPosition.x * rotationSpeed;

            // Obróæ kamerê
            transform.localEulerAngles += new Vector3( rotationX, rotationY, 0f);
            //if (transform.localEulerAngles.x < -90 || transform.localEulerAngles.x > 90)
            //    transform.localEulerAngles = new Vector3(assistStartRadius.x,transform.localEulerAngles.y, transform.localEulerAngles.z);
            if (transform.localEulerAngles.z != 0)
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
            fakedCameraHolder.transform.localEulerAngles = new Vector3(fakedCameraHolder.transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
            //if(transform.rotation.y + rotationX < 180 && transform.rotation.y + rotationX > 0)
            //    transform.Rotate(rotationX, rotationY, 0, Space.Self);
            //else
            //    transform.Rotate(0, rotationY, /*0*/rotationX, Space.World);
        }
    }
}
