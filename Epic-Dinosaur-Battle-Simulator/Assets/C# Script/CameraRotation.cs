using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    // Referencja do kamery
    public Camera cam;

    // Zmienna przechowuj¹ca poprzedni¹ orientacjê telefonu
    private DeviceOrientation previousOrientation;

    // Metoda wywo³ywana przy starcie skryptu
    void Start()
    {
        // Ustawienie pocz¹tkowej orientacji na aktualn¹ orientacjê telefonu
        previousOrientation = Input.deviceOrientation;
    }

    // Metoda wywo³ywana co klatkê
    void Update()
    {
        // Sprawdzenie, czy orientacja telefonu siê zmieni³a
        if (Input.deviceOrientation != previousOrientation)
        {
            // Jeœli tak, to obrócenie kamery o 180 stopni wokó³ osi Y
            cam.transform.Rotate(0f, 180f, 0f);

            // Zapisanie nowej orientacji jako poprzedniej
            previousOrientation = Input.deviceOrientation;
        }
    }
}
