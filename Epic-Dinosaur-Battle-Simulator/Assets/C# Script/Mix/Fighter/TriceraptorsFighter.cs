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
                myEnemy.stats.hp -= myEnemy.stats.attack * (int)Math.Log10(myEnemy.stats.speed) * (int)Math.Pow(1.1f, GameManager.Instance.battleManager.KosmoceraptorsCount[0]);
            }
            else 
            {
                myEnemy.stats.hp -= myEnemy.stats.attack * (int)Math.Log10(myEnemy.stats.speed) * (int)Math.Pow(1.1f, GameManager.Instance.battleManager.KosmoceraptorsCount[1]);

            }
            isHitFirstTime = true;
            fighter.stats.speed = GameManager.Instance.dinosaurStats.Dinosaurs[fighter.index].speed;
        }
        else
        {
            if (tag == "Blue")
            {
                myEnemy.stats.hp -= myEnemy.stats.attack * (int)Math.Pow(1.1f, GameManager.Instance.battleManager.KosmoceraptorsCount[0]);
            }
            else
            {
                myEnemy.stats.hp -= myEnemy.stats.attack * (int)Math.Pow(1.1f, GameManager.Instance.battleManager.KosmoceraptorsCount[1]);

            }
        }
    }
   

}

