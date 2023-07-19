using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using System.Threading.Tasks;
using Unity.VisualScripting;

public class IntelligentEggLayer : LayEggsFighter
{

    bool isFirstCall;
    private void Start()
    {
        isFirstCall = true;
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
        if (!isFirstCall)
        {
           vector = IfGoOrStay(CheckWhatDecisionMake(value));
        }
        else
        {
            
            if (fighter.col == 0 || fighter.col == 3 || fighter.col == 4 ||
                fighter.col == 5 || fighter.col == 6 || fighter.col == 9)
            {
                isFirstCall = false;
                return RandomizePosition();
            }
            else
            {
                vector = transform.position;
            }
            isFirstCall = false;

        }
        return vector;
    }

    private Vector3 IfGoOrStay(int[] check)
    {
        System.Random randomZ = new System.Random();
        float zAxis = randomZ.Next(1000*check[0],1000* check[1])/1000;

        Vector3 vector = new Vector3(transform.position.x, fighter.YAxis, zAxis);
        return vector;
    }

    private int[] CheckWhatDecisionMake(float[] value)
    {
        int[] result;
        if (tag == "Blue")
        { 
            if (value[0] < 10)
            {
                result = new int[2] { 95, 100 };
            }
            else if (value[0] < 20 && value[0] < value[1])
            {
                result = new int[2] { 90, 95};
            }
            else if (value[0] < 20 && value[0] > value[1])
            {
                result = new int[2] { 85, 90 };
            }
            else if (value[0] >= 20 && value[0] <= 20) 
            {
                result = new int[2] { 80, 85 };
            }
            else if (value[0] > 30 && value[1] > value[0])
            {
                result = new int[2] { 75, 80 };
            }
            else 
            {
                result = new int[2] { 70, 75 };
            }
        }
        else 
        {
            if (value[1] < 10)
            {
                result = new int[2] { 0, 5 };
            }
            else if (value[1] < 20 && value[1] < value[0])
            {
                result = new int[2] { 5, 10 };
            }
            else if (value[1] < 20 && value[1] > value[0])
            {
                result = new int[2] { 10, 15 };
            }
            else if (value[1] >= 20 && value[1] <= 20)
            {
                result = new int[2] { 15, 20 };
            }
            else if (value[1] > 30 && value[0] > value[1])
            {
                result = new int[2] { 20, 25 };
            }
            else
            {
                result = new int[2] { 25, 30 };
            }
        }
        return result;
    }

    private Vector3 RandomizePosition()
    {
        Vector3 vector=new Vector3();
        System.Random r = new System.Random();
        
        if (fighter.col == 5 || fighter.col == 6)
        {
            vector = new Vector3((float)r.Next(fighter.row * 100000, fighter.row* 100000 + 100000) / 10000,
                fighter.YAxis, (float)r.Next(600000, 700000) / 10000);
        }
        else if (fighter.col == 3 || fighter.col == 4)
        {
            vector = new Vector3((float)r.Next(fighter.row * 100000, fighter.row * 100000 + 100000) / 10000,
                fighter.YAxis, (float)r.Next(200000, 300000) / 10000);
        }
        else if (fighter.col == 9)
        {
            vector = new Vector3((float)r.Next(fighter.row * 100000, fighter.row* 100000 + 100000) / 10000,
                fighter.YAxis, (float)r.Next(800000, 900000) / 10000);
        }
        else if (fighter.col == 0)
        {
            vector = new Vector3((float)r.Next(fighter.row * 100000, fighter.row * 100000 + 100000) / 10000,
                fighter.YAxis, (float)r.Next(100000, 200000) / 10000);
        }
        return vector;
    }

}

