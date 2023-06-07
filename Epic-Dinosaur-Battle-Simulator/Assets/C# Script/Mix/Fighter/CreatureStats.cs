using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStats : MonoBehaviour
{
    public int index = 0;
    public int cost = 0;
    [SerializeField]private float speed = 25f;
    public float attack = 20f;
    public float radius = 5f;
    public float hp = 100f;
    public float interval = 5.0f; // Czas w sekundach miêdzy wywo³aniami akcji
    [SerializeField] private bool haveTailAttack;
    [SerializeField] private float yAxis;
    [SerializeField] private bool isObligatoryToRotate;

    public ScriptType behaviourScript;
    public FightScript fightingScript;

    public float YAxis { get => yAxis; set => yAxis = value; }
    public bool IsObligatoryToRotate { get => isObligatoryToRotate; set => isObligatoryToRotate = value; }
    public float Speed { get => speed; set => speed = value; }
    public bool HaveTailAttack { get => haveTailAttack; set => haveTailAttack = value; }

    public void UpgradeStatLevel(int level)
    {
        attack *= (float)Math.Pow(1.1, level);
        speed *= (float)Math.Pow(1.1, level);
        hp *= (float)Math.Pow(1.1, level);
    }
}


public enum ScriptType 
{
    MeleeFighter,
    Spawner
}


public enum FightScript 
{
    Traditional,//zwykle uderzenie
    LongNeck

}