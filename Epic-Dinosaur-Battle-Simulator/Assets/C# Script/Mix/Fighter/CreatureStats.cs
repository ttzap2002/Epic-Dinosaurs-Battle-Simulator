using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStats : MonoBehaviour
{
    public int index = 0;
    public int cost = 0;
    public float speed = 25f;
    public float attack = 20f;
    public float radius = 5f;
    public float hp = 100f;
    public float interval = 5.0f; // Czas w sekundach miêdzy wywo³aniami akcji
    public ScriptType behaviourScript;
    public FightScript fightingScript;
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