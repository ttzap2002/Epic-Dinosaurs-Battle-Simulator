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
                    fighter.Speed += 0.1f;
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
                if (fighter.Price <= 0)
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
            myenemy.Hp -= fighter.Hp * (int)Math.Log10(fighter.Price);
            isHitFirstTime = true;
            fighter.Speed = GameManager.Instance.dinosaurStats.Dinosaurs[fighter.index].speed;
        }
        else
        {
            IfStunningEnemy( myenemy, stunningProbAfterHit);
            myenemy.Hp -= fighter.Attack;
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

