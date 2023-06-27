﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


public class StunningFigter: MeleeFighter
{

    [SerializeField] protected float stunningProbability;
    [SerializeField] private float stunningProbAfterHit;
    protected bool isHitFirstTime=false;
    [SerializeField] protected float BasicSpeed;


    private void Start()
    {
        base.Start();
    }


    protected override void Update()
    {
        if (GameManager.Instance.IsRun)
        {
            base.Update();
            if (IsActiveForBattle)
            {
                if (!isHitFirstTime)
                {
                    myStats.Speed += 0.1f;
                }
            }
        }
       
    }


   
    protected override void Hit(GameObject myEnemy)
    {
        if (myEnemy == this.gameObject)
        {
            isFighting = false;
            GetTarget();
        }
        else
        {
            if (myEnemy != null)
            {

                CreatureStats m = myEnemy.GetComponent<CreatureStats>();
                FighterPlacement f = myEnemy.GetComponent<FighterPlacement>();
                Stunning(myEnemy, m);
                if (m.hp <= 0)
                {
                    isFighting = false;
                    DestroyEnemy(myEnemy, f);
                }
            }
        }

      
    }
    protected virtual void Stunning(GameObject myEnemy, CreatureStats m)
    {
        if (!isHitFirstTime)
        {
            IfStunningEnemy(myEnemy, m, stunningProbability);
            m.hp -= myStats.attack * (float)Math.Log10(myStats.Speed);
            isHitFirstTime = true;
            myStats.Speed = BasicSpeed;
        }
        else
        {
            IfStunningEnemy(myEnemy, m, stunningProbAfterHit);
            m.hp -= myStats.attack;
        }
    }
    protected void IfStunningEnemy(GameObject myEnemy, CreatureStats m,float probability)
    {
        System.Random r = new System.Random();
        float result = (float)r.Next(0, 10000) / 10000;
        if (result <= probability)
        {
            if (m.behaviourScript != ScriptType.Spawner)
            {
                MeleeFighter fighter = myEnemy.GetComponent<MeleeFighter>();
                if (!fighter.IsResistForStunning) 
                {
                    fighter.IsActiveForBattle = false;
                }
            }
        }
    }
}

