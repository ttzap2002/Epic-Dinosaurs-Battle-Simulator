using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WhichSquare: MonoBehaviour
{
    public int row;
    public int col;
    void Start()
    {
        CheckWhichSquare();
    }

    public int[] CheckWhichSquare() 
    {
        int[] list = new int[2];
        list[0] = (int)(transform.position.x / 10);
        list[1] = (int)(transform.position.z / 10);
        return list;
    }

    public void TryChangeTarget(GameObject obj)
    {
        Debug.Log("pies");
        MeleeFighter fighter =gameObject.GetComponent<MeleeFighter>();
        if (Vector3.Distance(transform.position, obj.transform.position) < Vector3.Distance(transform.position, fighter.target.transform.position))
        {
            fighter.target=obj;
        }
    }
}