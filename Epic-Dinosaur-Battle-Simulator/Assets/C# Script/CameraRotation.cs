using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    // Referencja do kamery
    public Camera cam;

    // Zmienna przechowuj�ca poprzedni� orientacj� telefonu
    private DeviceOrientation previousOrientation;

    // Metoda wywo�ywana przy starcie skryptu
    void Start()
    {
        // Ustawienie pocz�tkowej orientacji na aktualn� orientacj� telefonu
        previousOrientation = Input.deviceOrientation;
    }

    // Metoda wywo�ywana co klatk�
    void Update()
    {
        // Sprawdzenie, czy orientacja telefonu si� zmieni�a
        if (Input.deviceOrientation != previousOrientation)
        {
            // Je�li tak, to obr�cenie kamery o 180 stopni wok� osi Y
            cam.transform.Rotate(0f, 180f, 0f);

            // Zapisanie nowej orientacji jako poprzedniej
            previousOrientation = Input.deviceOrientation;
        }
    }
}
