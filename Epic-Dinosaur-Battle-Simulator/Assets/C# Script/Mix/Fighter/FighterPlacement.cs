using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FighterPlacement: MonoBehaviour
{
    
    public int row;
    public int col;
    
    private MeleeFighter me=null;
    void Start()
    {
        row = CheckWhichSquare()[0];
        col = CheckWhichSquare()[1]; 
        try 
        {
            me = gameObject.GetComponent<MeleeFighter>();
        }
        catch 
        {
            me = gameObject.GetComponent<StunningFigter>() as MeleeFighter;
        }
    }
    public void CreateForSpawner() 
    {
        row = CheckWhichSquare()[0];
        col = CheckWhichSquare()[1];
        try
        {
            me = gameObject.GetComponent<MeleeFighter>();
        }
        catch
        {
            me = gameObject.GetComponent<StunningFigter>() as MeleeFighter;
        }
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
        if (me != null && me.target!=null)
        {
            if (Vector3.Distance(transform.position, obj.transform.position) < Vector3.Distance(transform.position, me.target.transform.position))
            {
                me.target = obj;
            }
        }
        
    }
}