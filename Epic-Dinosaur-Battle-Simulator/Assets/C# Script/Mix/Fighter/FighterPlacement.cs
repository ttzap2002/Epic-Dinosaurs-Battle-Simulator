using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FighterPlacement: MonoBehaviour
{
    public int row;
    public int col;
    void Start()
    {
        row = CheckWhichSquare()[0];
        col = CheckWhichSquare()[1]; 
    }

    public int[] CheckWhichSquare() 
    {
        int[] list = new int[2];
        list[0] = (int)(transform.position.x / 10);
        list[1] = (int)(transform.position.z / 10);
        return list;
    }

    public void TryChangeTarget(GameObject obj, MeleeFighter fighter)
    {
        
        if (Vector3.Distance(transform.position, obj.transform.position) < Vector3.Distance(transform.position, fighter.target.transform.position))
        {
            fighter.target=obj;
         
        }
    }
}