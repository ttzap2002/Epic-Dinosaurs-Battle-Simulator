using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Services.Analytics.Internal;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Purchasing;
using static UnityEngine.GraphicsBuffer;

public class FighterPlacement: MonoBehaviour
{
    
    public int row;
    public int col;
    public FighterPlacement target;
    public bool isAlive;
  
    public int index = 0;
    public float radius = 5f;
    public float interval = 5.0f;
    private float speed;
    private int attack;
    private int hp;
    private int price;
    [SerializeField] private bool haveTailAttack;
    [SerializeField] private float yAxis;
    [SerializeField] private bool isObligatoryToRotate;
    [SerializeField] private NavMeshAgent agent;

    public ScriptType behaviourScript;
    public FightScript fightingScript;

    public float YAxis { get => yAxis; set => yAxis = value; }
    public bool IsObligatoryToRotate { get => isObligatoryToRotate; set => isObligatoryToRotate = value; }
    public bool HaveTailAttack { get => haveTailAttack; set => haveTailAttack = value; }
    public NavMeshAgent Agent { get => agent; set => agent = value; }
    public int Hp { get => hp; set => hp = value; }
    public float Speed { get => speed; set => speed = value; }
    public int Attack { get => attack; set => attack = value; }
    public int Price { get => price; set => price = value; }

    void Start()
    {
        row = CheckWhichSquare()[0];
        col = CheckWhichSquare()[1];
        OneDinoStat stats = GameManager.Instance.dinosaurStats.Dinosaurs[index];
        agent = gameObject.GetComponent<NavMeshAgent>();
        if (Agent != null)
        {
            agent.Warp(transform.position);
        }
        hp = stats.hp;
        Debug.Log(stats.speed);
        speed = stats.speed;
        attack = stats.attack;
        price = stats.price;
        agent.speed = stats.speed;
    }
    public void CreateForSpawner() 
    {
        row = CheckWhichSquare()[0];
        col = CheckWhichSquare()[1];
        OneDinoStat stats = GameManager.Instance.dinosaurStats.Dinosaurs[index];
        agent = gameObject.GetComponent<NavMeshAgent>();
        if (Agent != null)
        {
            agent.Warp(transform.position);
        }
        speed = stats.speed;
        attack = stats.attack;
        price = stats.price;
        agent.speed = stats.speed;
    } 
    public int[] CheckWhichSquare() 
    {
        int[] list = new int[2];
        list[0] = (int)(transform.position.x / 10);
        list[1] = (int)(transform.position.z / 10);
        return list;
    }


    public void Destroyme() 
    {
        if (isAlive)
        {
            GameManager.Instance.battleManager.RemoveFromList(this, row, col);
            if (tag == "Blue")
            {
                GameManager.Instance.blueGameObjects.Remove(gameObject);
            }
            else
            {
                GameManager.Instance.enemyGameObjects.Remove(gameObject);

            }
          

            InformDelegate(false);

            RefreshStats(gameObject);

            gameObject.SetActive(false);

            GameManager.Instance.CheckIfEndOfBattle();
        }
    }

    public void InformDelegate(bool isAdd)
    {

        if (tag == "Blue")
        {
            if (isAdd)
            {
                GameManager.Instance.battleManager.MoneySum[0] +=
                    GameManager.Instance.dinosaurStats.Dinosaurs[index].price;
            }
            else 
            {
                GameManager.Instance.battleManager.MoneySum[0] -=
                    GameManager.Instance.dinosaurStats.Dinosaurs[index].price;
            }
        }
        else {
            if (isAdd)
            {
                GameManager.Instance.battleManager.MoneySum[1] +=
                    GameManager.Instance.dinosaurStats.Dinosaurs[index].price;
            }
            else
            {
                GameManager.Instance.battleManager.MoneySum[1] -=
                    GameManager.Instance.dinosaurStats.Dinosaurs[index].price;

            }        
        }
        GameManager.Instance.battleManager.Delegate();
    }

    public void RefreshStats(GameObject obj) 
    {
        isAlive = false;
        hp = GameManager.Instance.dinosaurStats.Dinosaurs[index].hp;
        speed = GameManager.Instance.dinosaurStats.Dinosaurs[index].speed;
        
        GameManager.Instance.battleManager.poolingList[index].Add(obj);
    }
    public void TryChangeTarget(FighterPlacement obj)
    {
        if (target!=null)
        {
            if (Vector3.Distance(transform.position, obj.transform.position) < Vector3.Distance(transform.position, target.transform.position))
            {
                target = obj;
                Agent.SetDestination(obj.transform.position);
            }
        }
  
    }
    
    public void ChangeDestination(Vector3 vector) 
    {
        Agent.SetDestination(vector);
    }
    public void UpgradeStatLevel(int level)
    {
        if (level > 0) 
        {
            attack *= (int)Math.Pow(1.1, level);
            speed += (int)Math.Log(1.1 * level);
            hp *= (int)Math.Pow(1.1, level);
        }
  

    }
}


public enum ScriptType
{
    MeleeFighter,
    Spawner,
    EggLayer,
    Egg
}


public enum FightScript
{
    Traditional,//zwykle uderzenie
    LongNeck

}