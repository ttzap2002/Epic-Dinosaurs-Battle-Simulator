using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    // Referencja do kamery
    public Camera cam;

    // Zmienna przechowujaca poprzednia orientacje telefonu
    private DeviceOrientation previousOrientation;

    // Metoda wywo�ywana przy starcie skryptu
    void Start()
    {
        // Ustawienie poczatkowej orientacji na aktualna orientacje telefonu
        previousOrientation = Input.deviceOrientation;
    }

    // Metoda wywolywana co klatke
    void Update()
    {
        // Sprawdzenie, czy orientacja telefonu si� zmienila
        if (Input.deviceOrientation != previousOrientation)
        {
            // Je�li tak, to obr�cenie kamery o 180 stopni wok� osi Y
            cam.transform.Rotate(0f, 180f, 0f);

            // Zapisanie nowej orientacji jako poprzedniej
            previousOrientation = Input.deviceOrientation;
        }
    }
}
