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
        // Jeœli tak pomaluj t³o priceLabel na zielono, jeœli nie to na czerwono.
        // 
    }

    // Update is called once per frame
    public void Buy()
    {
        // SprawdŸ czy staæ gracza na zakup itemu. jeœli tak, to zmieñ kolor priceLabel na zielono, oraz zmieñ w gamemenadzerze kase i stan wlasnosci
    }
    /*void Update()
    {
        
    }*/
}
