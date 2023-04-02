using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    List<WhichSquare> enemyFighters;
    List<WhichSquare> blueFighters;

    public BattleManager(List<WhichSquare> enemyFighters, List<WhichSquare> blueFighters)
    {
        EnemyFighters = enemyFighters;
        BlueFighters = blueFighters;
    }

    public List<WhichSquare> EnemyFighters { get => enemyFighters; set => enemyFighters = value; }
    public List<WhichSquare> BlueFighters { get => blueFighters; set => blueFighters = value; }


    public void React(bool isReactForBlue,GameObject obj) 
    {
        if(isReactForBlue) 
        {
            foreach(var fighter in blueFighters) 
            {
                fighter.TryChangeTarget(obj);
            }
        }
        else 
        {
            foreach(var fighter in enemyFighters) 
            {
                fighter.TryChangeTarget(obj);
            }
        }
    }
}
