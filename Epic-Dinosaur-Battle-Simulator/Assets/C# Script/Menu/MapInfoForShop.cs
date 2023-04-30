using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfoForShop : MonoBehaviour
{
    public int mapId = 999; //999 oznacza mape ktora nie istnieje. zastosowane po to, zeby podczas implementacji nie bylo wpisane od razu 0 co odblokowywalo by pierwsza mape
    public GameObject button; //jezeli obiekt kupiony to przycisk do zakupu znika
    public GameObject money; // po zakupie zmienia sie stan gotowki
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Buy() 
    {
        
    }
}
