using System;
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
                    fighter.stats.speed += 1;
                }
            }
        }
       
    }


   
    protected override void Hit(FighterPlacement myEnemy)
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

                Stunning(myEnemy);
                if (fighter.stats.hp <= 0)
                {
                    isFighting = false;
                    myEnemy.Destroyme();
                }
            }
        }

      
    }
    protected virtual void Stunning(FighterPlacement myenemy)
    {
        if (!isHitFirstTime)
        {
            IfStunningEnemy(myenemy,stunningProbability);
            myenemy.stats.hp -= fighter.stats.attack * (int)Math.Log10(fighter.stats.speed);
            isHitFirstTime = true;
            fighter.stats.speed = GameManager.Instance.dinosaurStats.Dinosaurs[fighter.index].speed;
        }
        else
        {
            IfStunningEnemy( myenemy, stunningProbAfterHit);
            myenemy.stats.hp -= fighter.stats.attack;
        }
    }
    protected void IfStunningEnemy(FighterPlacement myEnemy,float probability)
    {
        System.Random r = new System.Random();
        float result = (float)r.Next(0, 10000) / 10000;
        if (result <= probability)
        {
            if (myEnemy.behaviourScript != ScriptType.Spawner)
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

