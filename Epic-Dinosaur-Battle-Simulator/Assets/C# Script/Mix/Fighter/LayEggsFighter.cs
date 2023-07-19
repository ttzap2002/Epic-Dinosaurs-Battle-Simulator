using System;
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
    [SerializeField]protected Vector3 positionToReach;
    private bool isLaying;
    [SerializeField] private int range;
    protected FighterPlacement fighter;

    [SerializeField]private float timeForLayEgg;
    private float timer;

    public bool IsActiveForBattle { get => isActiveForBattle; set => isActiveForBattle = value; }

    protected void Start()
    {
        fighter = GetComponent<FighterPlacement>();

        positionToReach = GetPositionToMove();
    }

    protected virtual Vector3 GetPositionToMove()
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
  
        return new Vector3(x, fighter.YAxis, z);
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

    protected void Update()
    {
        if (GameManager.Instance.IsRun)
        {
            if (isActiveForBattle)
            {
                
                if (!isLaying)
                {
                    Move();
                    if (transform.position == positionToReach) { isLaying = true; }
                    if (isChangeOfSquare())
                    {
                        bool isLessThan40 = GameManager.Instance.blueGameObjects.Count +
                                    GameManager.Instance.enemyGameObjects.Count < 40;
                        SetReactForOpponents(isLessThan40);
                    }
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

        //GameObject obj = Instantiate(GameManager.Instance.prefabGameObjects[19]
        //);
        List<GameObject> poolList= GameManager.Instance.battleManager.poolingList[19];
        GameObject obj = poolList[poolList.Count - 1];
        poolList.Remove(obj);
        obj.SetActive(true);
        obj.tag = tag;
        obj.transform.position = transform.position;
        FighterPlacement f = obj.GetComponent<FighterPlacement>();
        if (tag == "Blue") { GameManager.Instance.battleManager.BlueFighters[fighter.row, fighter.col].Add(f); GameManager.Instance.blueGameObjects.Add(obj); }
        else { GameManager.Instance.battleManager.EnemyFighters[fighter.row, fighter.col].Add(f); GameManager.Instance.enemyGameObjects.Add(obj); }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, positionToReach, Time.deltaTime * fighter.stats.speed);

    }

    private void SetReactForOpponents(bool isLessThan40)
    {
        if (isLessThan40)
        {
            SetReact();
        }
        else
        {
            System.Random r = new System.Random();
            int complexity = GameManager.Instance.enemyGameObjects.Count
                * GameManager.Instance.blueGameObjects.Count * 2;
            double result = 800 / complexity;

            double probability = r.NextDouble();
            if (probability < result) { SetReact(); }

        }

    }

    private void SetReact()
    {
        if (tag == "Blue")
        {
            GameManager.Instance.battleManager.React(false, fighter);
        }
        else if (tag == "Enemy")
        {
            GameManager.Instance.battleManager.React(true, fighter);
        }
    }

}

