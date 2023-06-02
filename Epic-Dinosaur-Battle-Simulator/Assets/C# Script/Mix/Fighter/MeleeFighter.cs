using Assets.C__Script;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Playables;

public class MeleeFighter : Fighter
{
    // Start is called before the first frame update

    public bool isFighting = false;
    int a = 1;
    [SerializeField]bool isActiveForBattle=false;
    protected CreatureStats myStats = null;
    private FighterPlacement fighter = null;
    private LongNeckFighting longNeckFighter = null;
    private float timer = 0.0f; // Zmienna do œledzenia czasu
    [SerializeField] protected float rotationSpeed = 5f;
    [SerializeField] private bool isResistForStunning;
    private float basicx;

    public bool IsActiveForBattle { get => isActiveForBattle; set => isActiveForBattle = value; }
    public bool IsResistForStunning { get => isResistForStunning; set => isResistForStunning = value; }

    private void Start()
    {
        //GameManager.Instance.Awake();

        basicx = transform.rotation.x;

        myStats = gameObject.GetComponent<CreatureStats>();
        fighter = gameObject.GetComponent<FighterPlacement>();
       
        //if (target is null) { GetFirstTarget(); }
        if (myStats.fightingScript == FightScript.LongNeck && longNeckFighter is null)
        {
            longNeckFighter = gameObject.GetComponent<LongNeckFighting>();

            longNeckFighter.onNoTargets += SetFightingStatusToFalse;
            longNeckFighter.IsReady=true;
        }
    }

    protected virtual void Update()
    {
        if (isActiveForBattle)
        {
            timer += Time.deltaTime;

            if (a % 3 == 0)
            {
                if (target != null && target != gameObject)
                {

                    if (isChangeOfSquare())
                    {
                        if (tag == "Blue")
                        {
                            GameManager.Instance.battleManager.React(false, gameObject, this);
                        }
                        else if (tag == "Enemy")
                        {
                            GameManager.Instance.battleManager.React(true, gameObject, this);
                        }
                    }
                    if (!isFighting)
                    {
                        Move(); if (Vector3.Distance(transform.position, target.transform.position) <= myStats.radius && Vector3.Distance(transform.position, target.transform.position) != 0) { isFighting = true; }
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, target.transform.position) > myStats.radius)
                        {
                            isFighting = false;
                        }
                        if (timer >= myStats.interval)
                        {
                            if (myStats.fightingScript == FightScript.Traditional)
                            {
                                Hit(target);
                            }
                            else
                            {
                                if (!longNeckFighter.HitAllEnemies(myStats.attack))
                                {
                                    isFighting = false;
                                }
                            }
                            timer = 0f;
                        }
                    }


                }
                else { GetFirstTarget(); }
            }


            //transform.position += moveDirection;
            a++;
        }
        else if(GameManager.Instance.IsRun && !isResistForStunning) 
        {
            if (timer < 5f) 
            {
                timer += Time.deltaTime;
            }
            else 
            {
                timer = 0;
                isActiveForBattle = true;
            }

        }
    }

    public void GetFirstTarget() {
        if (tag == "Blue") { target=FindNearestEnemy(transform.position); }
        else { target=FindNearestEnemy(transform.position);}}
  

    public void SetFightingStatusToFalse()
    {
        isFighting = false;
        Debug.Log("Done");
    }
    protected virtual void Hit(GameObject myEnemy) 
    {
        if (myEnemy == this.gameObject)
        {
            isFighting = false;
            GetFirstTarget();
        }
        else
        {
            if (myEnemy != null)
            {
                CreatureStats m = myEnemy.GetComponent<CreatureStats>();
                FighterPlacement f = myEnemy.GetComponent<FighterPlacement>();
                m.hp -= myStats.attack;
                if (m.hp <= 0) { 
                    isFighting = false;
                    GameManager.Instance.battleManager.RemoveFromList(f,f.row,f.col); 
                    Destroy(myEnemy); 
                    GameManager.Instance.CheckIfEndOfBattle();
                }
            }
        }
    }
 

    protected virtual void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * myStats.Speed);
        Vector3 directionToEnemy = (transform.position- target.transform.position).normalized;
        directionToEnemy = directionToEnemy.normalized;
        
        if (!myStats.IsObligatoryToRotate)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            //rotation.x = transform.rotation.x;
            //rotation.z = transform.rotation.z;
            transform.rotation = rotation;
        
        }
        /*
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            rotation.x = transform.rotation.x;
            rotation.y = -transform.rotation.y;
            rotation.z = transform.rotation.z;
            transform.rotation = rotation;
        }*/
        
    }

    private bool isChangeOfSquare()
    {
        if (fighter.CheckWhichSquare()[0]!=fighter.row || fighter.CheckWhichSquare()[1] != fighter.col)
        { 
            if (tag == "Blue") { GameManager.Instance.battleManager.BlueFighters[fighter.row, fighter.col].Remove(fighter); }
            else { GameManager.Instance.battleManager.EnemyFighters[fighter.row, fighter.col].Remove(fighter); }
            fighter.row = fighter.CheckWhichSquare()[0];
            fighter.col = fighter.CheckWhichSquare()[1];
            if (tag == "Blue") { GameManager.Instance.battleManager.BlueFighters[fighter.row, fighter.col].Add(fighter); }
            else { GameManager.Instance.battleManager.EnemyFighters[fighter.row, fighter.col].Add(fighter); }
            return true;
        }
        return false;
    }

    /*
    private GameObject FindNearestEnemy(string tag,Vector3 vectorOfMyObj)
    {
        float min = Mathf.Infinity;
        GameObject ObjectReturn = null;
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag(tag)) 
        {
         
            float distance = Vector3.Distance(obj.transform.position, vectorOfMyObj);
            if (distance < min)
            {
                min = distance;
                ObjectReturn = obj;
                if (min <= myStats.radius)
                {

                    isFighting = true;
                }
            }
          
        }
        return ObjectReturn;
    }
    */
    private GameObject FindNearestEnemy(Vector3 vectorOfMyObj)
    {
        float min = Mathf.Infinity;

        StructForTheBestObj ObjectToReturn = new StructForTheBestObj(null, false);
        ObjectToReturn = FindTarget(ObjectToReturn);

        /*
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag))
        {

            float distance = Vector3.Distance(obj.transform.position, vectorOfMyObj);
            if (distance < min)
            {
                min = distance;
                ObjectReturn = obj;
                if (min <= myStats.radius)
                {

                    isFighting = true;
                }
            }

        }
        */
        return ObjectToReturn.Obj;

    }

    private StructForTheBestObj FindTarget(StructForTheBestObj ObjectToReturn)
    {
        int iteration = 0;
        int range = 5;

        if (tag == "Blue")
        {
            while (ObjectToReturn.Obj == null && !ObjectToReturn.IsTrue)
            {
                ObjectToReturn = FindTheBestObject(fighter.row, fighter.col,
                    GameManager.Instance.battleManager.EnemyFighters, iteration, range);
                iteration++;
                range+= 10;
            }
        }
        else
        {
            while (ObjectToReturn.Obj == null && !ObjectToReturn.IsTrue)
            {
                ObjectToReturn = FindTheBestObject(fighter.row, fighter.col,
                    GameManager.Instance.battleManager.BlueFighters, iteration, range);
                iteration++;
                range += 10;

            }
          
        }

        return ObjectToReturn;
    }

    /// <summary>
    /// funkcja do znalezienia najblizszego obiektu. Patrzy po kwadratach ktory obiekt jest najblizszy
    /// 
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <param name="grid"></param>
    /// <param name="diff"></param>
    /// <returns></returns>
    private StructForTheBestObj FindTheBestObject(int row,int col, List<FighterPlacement>[,] grid,int iteration,int range)
    {
        
        StructForTheBestObj obj = new StructForTheBestObj(null,false);
        int[] rowColCheck = new int[4] {0,9,0,9 };
        float min = Mathf.Infinity;
        if (row - iteration >= 0) { rowColCheck[0]=row - iteration; }
        if (row + iteration < 10) { rowColCheck[1] = row + iteration; }
        if (col - iteration >= 0) { rowColCheck[2] = col - iteration; }
        if (col + iteration < 10) { rowColCheck[3] = col + iteration; }

        for (int i = rowColCheck[0]; i <= rowColCheck[1]; i++)
        {
            for (int j = rowColCheck[2]; j <= rowColCheck[3]; j++)
            {
                if((i> rowColCheck[0] && i < rowColCheck[1]) && (j > rowColCheck[2] && j < rowColCheck[3])) 
                {
                    continue;
                }
                if (grid[i, j] == null && grid[i, j].Count == 0 ) { continue; }

                foreach(var targetObj in grid[i, j]) 
                {
                    if (targetObj != null)
                    {
                        float distance = Vector3.Distance(targetObj.gameObject.transform.position, transform.position);
                        if (distance < min)
                        {
                            min = distance;
                            obj.Obj = targetObj.gameObject;
                            obj.IsTrue = true;
                            obj.Distance = distance;
                            /*
                            if (distance < range) 
                            {
                                obj.IsTrue = true;
                                obj.Distance = distance;
                            }
                            */
                        }
                    }
                }
              
            }
        }
        return obj;
    }
}



