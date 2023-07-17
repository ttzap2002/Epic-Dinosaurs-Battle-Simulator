using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using System.Threading.Tasks;
using Unity.VisualScripting;

public class IntelligentEggLayer : LayEggsFighter
{

    bool isFirst;
    private void Start()
    {
        isFirst = true;
        base.Start();
        
        
    }

    protected void Update()
    {
        base.Update();
    }

    protected override Vector3 GetPositionToMove()
    {
        Vector3 vector = new Vector3();
        float[] value = GameManager.Instance.battleManager.PowerValue;
        if (!isFirst) {

            vector = new Vector3(40, fighter.YAxis, 40);

        }
        else
        {
            if (tag == "Blue")
            {
                if (fighter.col == 4 || fighter.col == 3 || fighter.col == 0)
                {
                    return RandomizePosition(true);
                }
                else
                {
                    vector = transform.position;
                }
            }
            else 
            {
                if (fighter.col == 5 || fighter.col == 6 || fighter.col == 9)
                {
                    return RandomizePosition(false);
                }
                else
                {
                    vector = transform.position;
                }

            }

        }
        return vector;
    }

    private Vector3 RandomizePosition(bool isBlue)
    {
        Vector3 vector;
        System.Random r = new System.Random();
        if (isBlue)
        {
            if (fighter.col == 4 || fighter.col == 3)
            {
                vector = new Vector3((float)r.Next(fighter.row * 100000, fighter.row + 100000) / 10000,
                    fighter.YAxis, (float)r.Next(200000, 300000) / 10000);
            }
            else if (fighter.col == 0)
            {
                vector = new Vector3((float)r.Next(fighter.row * 100000, fighter.row + 100000) / 10000,
                    fighter.YAxis, (float)r.Next(1 * 100000, 200000) / 10000);
            }
            else
            {
                vector = transform.position;
            }
        }
        else 
        {
            vector=transform.position;
        }
        return vector;
    }

}

