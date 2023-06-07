using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //List<FighterPlacement> enemyFighters;
    //List<FighterPlacement> blueFighters;

    List<FighterPlacement>[,] enemyFighters= new List<FighterPlacement>[10,10];
    List<FighterPlacement>[,] blueFighters = new List<FighterPlacement>[10, 10];

    int i = 0;
    
    public BattleManager(List<FighterPlacement>[,] enemyFighters, List<FighterPlacement>[,] blueFighters)
    {
        EnemyFighters = enemyFighters;
        BlueFighters = blueFighters;
    }
    
   



    public List<FighterPlacement>[,] EnemyFighters { get => enemyFighters; set => enemyFighters = value; }
    public List<FighterPlacement>[,] BlueFighters { get => blueFighters; set => blueFighters = value; }

    //przestarza³a wersja
    /*
    public void RemoveFromList(FighterPlacement g) 
    {
        if (g.tag == "Blue")
        {
            BlueFighters.Remove(g);
        }
        else { EnemyFighters.Remove(g); }
    }
    */
    public void React(bool isReactForBlue,GameObject obj) 
    {
        if (obj != null)
        {

            if (isReactForBlue)
            {
                foreach (var listOfFighter in blueFighters)
                {
                    foreach (var fighter in listOfFighter)
                    {
                        fighter.TryChangeTarget(obj);
                    }
                }
            }
            else
            {
                foreach (var listOfFighter in EnemyFighters)
                {
                    foreach (var fighter in listOfFighter)
                    {
                        fighter.TryChangeTarget(obj);

                    }
                }
            }
        }
    }

    public void RemoveFromList(FighterPlacement g,int row,int col)
    {
        
        if (g.tag == "Blue")
        {
            for (int i = BlueFighters[row, col].Count-1; i >= 0; i--)
            {
                if (BlueFighters[row, col][i] == g)
                {
                    BlueFighters[row, col].RemoveAt(i);
                }
            }
           
        }
        else
        {
            for (int i = EnemyFighters[row, col].Count - 1; i >= 0; i--)
            {
                if (EnemyFighters[row, col][i] == g)
                {
                    EnemyFighters[row, col].RemoveAt(i);
                }
            }
        }
    }


    public bool IsEnemyFighterContainAnyFighter() 
    {
       foreach(var list in EnemyFighters) 
       {
            if (list.Count > 0) 
            {
                return true;
            }
       }
       return false;
    }

    public bool IsBlueFighterContainAnyFighter()
    {
        foreach (var list in BlueFighters)
        {
            if (list.Count > 0)
            {
                return true;
            }
        }
        return false;
    }

    public void DestroyAllObject() 
    {

       foreach(var list in EnemyFighters) 
       {
            for(int i = list.Count - 1; i >= 0; i--) 
            {
                CreatureStats c = list[i].gameObject.GetComponent<CreatureStats>();
                if (c.behaviourScript == ScriptType.MeleeFighter) 
                {
                    list[i].gameObject.GetComponent<MeleeFighter>().IsActiveForBattle = false;

                }
                else 
                {
                    list[i].gameObject.GetComponent<SpawnerBehaviour>().IsReadyForFight = false;

                }
            }
       }
       foreach (var list in BlueFighters)
       {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                CreatureStats c = list[i].gameObject.GetComponent<CreatureStats>();
                if (c.behaviourScript == ScriptType.MeleeFighter)
                {
                    list[i].gameObject.GetComponent<MeleeFighter>().IsActiveForBattle = false;

                }
                else
                {
                    list[i].gameObject.GetComponent<SpawnerBehaviour>().IsReadyForFight = false;

                }
            }
        }

    }

}
