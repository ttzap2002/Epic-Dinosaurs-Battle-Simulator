using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TriceraptorsFighter:StunningFigter
{
    protected override void Stunning(GameObject myEnemy, CreatureStats m)
    {
        if (!isHitFirstTime)
        {
            base.IfStunningEnemy(myEnemy, m, stunningProbability);
            if (tag == "Blue")
            {
                m.hp -= myStats.attack * (float)Math.Log10(myStats.Speed) * (float)Math.Pow(1.1f, GameManager.Instance.battleManager.KosmoceraptorsCount[0]);
            }
            else 
            {
                m.hp -= myStats.attack * (float)Math.Log10(myStats.Speed) * (float)Math.Pow(1.1f, GameManager.Instance.battleManager.KosmoceraptorsCount[1]);

            }
            isHitFirstTime = true;
            myStats.Speed = BasicSpeed;
        }
        else
        {
            if (tag == "Blue")
            {
                m.hp -= myStats.attack * (float)Math.Pow(1.1f, GameManager.Instance.battleManager.KosmoceraptorsCount[0]);
            }
            else
            {
                m.hp -= myStats.attack * (float)Math.Pow(1.1f, GameManager.Instance.battleManager.KosmoceraptorsCount[1]);

            }
        }
    }
   

}

