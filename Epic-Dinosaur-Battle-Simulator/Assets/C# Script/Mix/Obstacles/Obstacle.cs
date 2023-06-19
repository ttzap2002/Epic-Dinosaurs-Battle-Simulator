using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle: MonoBehaviour
{
    private ListToCheck[] list;
    private int range;


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

    private void CheckIfAnyTroopInSquareHasToChangeSquare(ListToCheck item)
    {
        foreach (var k in GameManager.Instance.battleManager.EnemyFighters[item.row, item.col])
        {
            if (Vector3.Distance(transform.position, k.transform.position) <= 5)
            {
                //zrob cos
            }
        }

        foreach (var k in GameManager.Instance.battleManager.BlueFighters[item.row, item.col])
        {
            if (Vector3.Distance(transform.position, k.transform.position) <= 5)
            {
                //zrob cos
            }
        }
    }
}

public struct ListToCheck{
    public int row;
    public int col;
} 