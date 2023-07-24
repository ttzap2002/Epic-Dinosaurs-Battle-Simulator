using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TriceraptorsFighter:StunningFigter
{
    protected override void Stunning(FighterPlacement myEnemy)
    {
        if (!isHitFirstTime)
        {
            base.IfStunningEnemy(myEnemy,stunningProbability);
            if (tag == "Blue")
            {
                myEnemy.Hp -= fighter.Attack * (int)Math.Log10(fighter.Speed) * (int)Math.Pow(1.1f, GameManager.Instance.battleManager.KosmoceraptorsCount[0]);
            }
            else 
            {
                myEnemy.Hp -= fighter.Attack * (int)Math.Log10(fighter.Speed) * (int)Math.Pow(1.1f, GameManager.Instance.battleManager.KosmoceraptorsCount[1]);

            }
            isHitFirstTime = true;
            fighter.Speed = GameManager.Instance.dinosaurStats.Dinosaurs[fighter.index].speed;
        }
        else
        {
            if (tag == "Blue")
            {
                myEnemy.Hp -= fighter.Attack * (int)Math.Pow(1.1f, GameManager.Instance.battleManager.KosmoceraptorsCount[0]);
            }
            else
            {
                myEnemy.Hp -= fighter.Attack * (int)Math.Pow(1.1f, GameManager.Instance.battleManager.KosmoceraptorsCount[1]);

            }
        }
    }
   

}

