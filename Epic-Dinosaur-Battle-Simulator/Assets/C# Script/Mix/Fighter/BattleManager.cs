using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    List<FighterPlacement> enemyFighters;
    List<FighterPlacement> blueFighters;
    int i = 0;
    public BattleManager(List<FighterPlacement> enemyFighters, List<FighterPlacement> blueFighters)
    {
        EnemyFighters = enemyFighters;
        BlueFighters = blueFighters;
    }

    public List<FighterPlacement> EnemyFighters { get => enemyFighters; set => enemyFighters = value; }
    public List<FighterPlacement> BlueFighters { get => blueFighters; set => blueFighters = value; }

    public void RemoveFromList(FighterPlacement g) 
    {
        if (g.tag == "Blue")
        {
            BlueFighters.Remove(g);
        }
        else { EnemyFighters.Remove(g); }
    }

    public void React(bool isReactForBlue,GameObject obj,MeleeFighter m) 
    {
        
            if (isReactForBlue)
            {
                foreach (var fighter in blueFighters)
                {
                    fighter.TryChangeTarget(obj, m);
                }
            }
            else
            {
                foreach (var fighter in enemyFighters)
                {
                    fighter.TryChangeTarget(obj, m);
                    Debug.Log("pies");
                }
            }
        }
   
        
    
}
