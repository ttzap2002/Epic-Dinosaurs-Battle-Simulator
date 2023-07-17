using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FighterPlacement: MonoBehaviour
{
    
    public int row;
    public int col;
    public FighterPlacement target;
    public bool isAlive;
    public OneDinoStat stats;
    public int index = 0;
    public float radius = 5f;
    public float interval = 5.0f; 
    [SerializeField] private bool haveTailAttack;
    [SerializeField] private float yAxis;
    [SerializeField] private bool isObligatoryToRotate;

    public ScriptType behaviourScript;
    public FightScript fightingScript;

    public float YAxis { get => yAxis; set => yAxis = value; }
    public bool IsObligatoryToRotate { get => isObligatoryToRotate; set => isObligatoryToRotate = value; }
    public bool HaveTailAttack { get => haveTailAttack; set => haveTailAttack = value; }
    void Start()
    {
        row = CheckWhichSquare()[0];
        col = CheckWhichSquare()[1];
        stats = GameManager.Instance.dinosaurStats.Dinosaurs[index];
    }
    public void CreateForSpawner() 
    {
        row = CheckWhichSquare()[0];
        col = CheckWhichSquare()[1];
        stats = GameManager.Instance.dinosaurStats.Dinosaurs[index];
    } 
    public int[] CheckWhichSquare() 
    {
        int[] list = new int[2];
        list[0] = (int)(transform.position.x / 10);
        list[1] = (int)(transform.position.z / 10);
        return list;
    }

    public void TryChangeTarget(FighterPlacement obj)
    {
        if (target!=null)
        {
            if (Vector3.Distance(transform.position, obj.transform.position) < Vector3.Distance(transform.position, target.transform.position))
            {
                target = obj;
            }
        }
  
    }
    

    public void UpgradeStatLevel(int level)
    {
        stats.attack *= (int)Math.Pow(1.1, level);
        stats.speed += (int)Math.Log(1.1*level);
        stats.hp *= (int)Math.Pow(1.1, level);

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