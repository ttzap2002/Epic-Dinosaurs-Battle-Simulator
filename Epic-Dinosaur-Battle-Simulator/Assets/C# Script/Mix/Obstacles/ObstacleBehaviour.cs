using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleBehaviour: MonoBehaviour
{
    private SquaresToCheck[] list;
    private int range;

    private void Start()
    {
        SetListOfSquaresToCheck();
    }

    private void Update()
    {
        if (GameManager.Instance.IsRun)
        {
            CheckIfAnyTroopInvade();
        }
    }

    private void CheckIfAnyTroopInvade()
    {
        foreach(var item in list)
        {
            CheckIfAnyTroopInSquareHasToChangeSquare(item);
        }
    }

    private void CheckIfAnyTroopInSquareHasToChangeSquare(SquaresToCheck item)
    {
        foreach (var k in GameManager.Instance.battleManager.EnemyFighters[item.row, item.col])
        {
            if (Vector3.Distance(transform.position, k.transform.position) <= range)
            {
                //zrob cos
            }
        }

        foreach (var k in GameManager.Instance.battleManager.BlueFighters[item.row, item.col])
        {
            if (Vector3.Distance(transform.position, k.transform.position) <= range)
            {
                //zrob cos
            }
        }
    }


    private void SetListOfSquaresToCheck() 
    {
        List<SquaresToCheck> listtemporary = new List<SquaresToCheck>();
        listtemporary.Add(new SquaresToCheck((int)(transform.position.x / 10),
        (int)(transform.position.z / 10)));
        int a = 10;
        for(int i = 0; i < 4;i++) 
        {

        }



    }



}

public struct SquaresToCheck{
    public int row;
    public int col;

    public SquaresToCheck(int row,int col) 
    {
        this.row = row;
        this.col = col;
    }
} 