using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocker : MonoBehaviour
{
    public GameObject unlockingButton, priceLabel;
    // Start is called before the first frame update
    void Start()
    {
        // Sprawdzanie czy dany item jest odblokowany przez gracza (poprzez game menagera).
        // Je�li tak pomaluj t�o priceLabel na zielono, je�li nie to na czerwono.
        // 
    }

    // Update is called once per frame
    public void Buy()
    {
        // Sprawd� czy sta� gracza na zakup itemu. je�li tak, to zmie� kolor priceLabel na zielono, oraz zmie� w gamemenadzerze kase i stan wlasnosci
    }
    /*void Update()
    {
        
    }*/
}
