using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cheat(string cheat)
    {
        string assist = cheat.ToUpper();
        if(assist is not null && assist == "PROGRAMMERS")
            gameObject.SetActive(true);
    }
}
