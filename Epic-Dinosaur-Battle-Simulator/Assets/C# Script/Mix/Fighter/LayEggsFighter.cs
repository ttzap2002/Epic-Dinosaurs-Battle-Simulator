﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;

public class LayEggsFighter :MonoBehaviour
{
    private bool isActiveForBattle;
    [SerializeField]private Vector3 positionToReach;
    private bool isLaying;
    [SerializeField] private int range;
    FighterPlacement fighter;
    CreatureStats myStats;
    [SerializeField]private float timeForLayEgg;
    private float timer;

    public bool IsActiveForBattle { get => isActiveForBattle; set => isActiveForBattle = value; }

    private void Start()
    {
        fighter = GetComponent<FighterPlacement>();
        myStats = GetComponent<CreatureStats>();
        positionToReach = GetPositionToMove();
    }

    private Vector3 GetPositionToMove()
    {
        bool checker = false;
        float x = 0f; 
        float z=0f;
        while (!checker) 
        {
            System.Random r = new System.Random();
            x = (float)r.Next(((int)transform.position.x-range)*10000, ((int)transform.position.x + range)* 10000)/10000;
            z = (float)r.Next(((int)transform.position.z - range) * 10000, ((int)transform.position.z + range) * 10000) / 10000;
            if (x > 100 || x < 0) { continue; }
            if (z > 100 || z < 0) { continue; }
            checker = CheckIfInCircle(x,z);
        }
  
        return new Vector3(x, myStats.YAxis, z);
    }

    private bool CheckIfInCircle(float x,float z) 
    {
        float a = transform.position.x-x;
        float b = transform.position.z-z;
        if (a*a+b*b<=range*range) 
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (isActiveForBattle) 
        {
            if (isChangeOfSquare())
            {
                if (tag == "Blue")
                {
                    GameManager.Instance.battleManager.React(false, gameObject);
                }
                else if (tag == "Enemy")
                {
                    GameManager.Instance.battleManager.React(true, gameObject);
                }
            }
            if (!isLaying) 
            {
                Move();
                if (transform.position == positionToReach) { isLaying = true; }
            }
            else 
            {
                timer += Time.deltaTime;
                if (timer >= timeForLayEgg)
                {
                    if (GameManager.Instance.IsRun) 
                    {
                        SetEgg();
                    }
                  
                    timer = 0;
                    isLaying = false;
                    positionToReach = GetPositionToMove();
                }
            }

        }
    }

    private bool isChangeOfSquare()
    {
        int[] result = fighter.CheckWhichSquare();
        if (result[0] != fighter.row || result[1] != fighter.col)
        {
            if (tag == "Blue") { GameManager.Instance.battleManager.BlueFighters[fighter.row, fighter.col].Remove(fighter); }
            else { GameManager.Instance.battleManager.EnemyFighters[fighter.row, fighter.col].Remove(fighter); }
            fighter.row = result[0];
            fighter.col = result[1];
            if (tag == "Blue") { GameManager.Instance.battleManager.BlueFighters[fighter.row, fighter.col].Add(fighter); }
            else { GameManager.Instance.battleManager.EnemyFighters[fighter.row, fighter.col].Add(fighter); }
            return true;
        }
        return false;
    }

    private void SetEgg()
    {
        GameObject obj = Instantiate(GameManager.Instance.prefabGameObjects[6]);
        obj.SetActive(true);
        obj.tag = tag;
        obj.transform.position = transform.position;
        FighterPlacement f = obj.GetComponent<FighterPlacement>();
        if (tag == "Blue") { GameManager.Instance.battleManager.BlueFighters[fighter.row, fighter.col].Add(f); GameManager.Instance.blueGameObjects.Add(obj); }
        else { GameManager.Instance.battleManager.EnemyFighters[fighter.row, fighter.col].Add(f); GameManager.Instance.enemyGameObjects.Add(obj); }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, positionToReach, Time.deltaTime * myStats.Speed);

    }
}
