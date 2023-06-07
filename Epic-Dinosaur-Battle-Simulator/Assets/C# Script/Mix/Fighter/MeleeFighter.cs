using Assets.C__Script;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
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
    private float timer = 0.0f; // Zmienna do �ledzenia czasu
    Func<List<GameObject>> neckAttack;
    Func<List<GameObject>> tailAttack;

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
            neckAttack = gameObject.GetComponent<LongNeckFighting>().ObjectsToHit;
        }
        if (myStats.HaveTailAttack) 
        {
            tailAttack = gameObject.GetComponent<TailAttacKFighter>().ObjectsToHitByTail;
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
                            GameManager.Instance.battleManager.React(false, gameObject);
                        }
                        else if (tag == "Enemy")
                        {
                            GameManager.Instance.battleManager.React(true, gameObject);
                        }
                    }
                    if (!isFighting)
                    {
                        Move(); 
                        if (Vector3.Distance(transform.position, target.transform.position) <= myStats.radius)
                        { isFighting = true; }
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
                            else if (myStats.fightingScript == FightScript.LongNeck && !myStats.HaveTailAttack)
                            {
                                HitAllObjects(neckAttack.Invoke());   
                            }
                            else 
                            {
                                HitAllObjects(neckAttack.Invoke());
                                HitAllObjects(tailAttack.Invoke());
                            }
                            timer = 0f;
                        }
                    }


                }
                else { GetTarget(); }
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

    public void GetTarget() {
        if (tag == "Blue") { target=FindNearestEnemy(GameManager.Instance.battleManager.EnemyFighters); }
        else { target=FindNearestEnemy(GameManager.Instance.battleManager.BlueFighters);}}
  

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
            GetTarget();
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
                    GameObject.Destroy(myEnemy); 
                    GameManager.Instance.CheckIfEndOfBattle();
                }
            }
        }
    }

    private void HitAllObjects(List<GameObject> ObjectsToHit) 
    {
        foreach(GameObject obj in ObjectsToHit)
        { 
            CreatureStats m = obj.GetComponent<CreatureStats>();
            FighterPlacement f = obj.GetComponent<FighterPlacement>();
            m.hp -= myStats.attack;
            if (m.hp <= 0)
            {
                isFighting = false;
                GameManager.Instance.battleManager.RemoveFromList(f, f.row, f.col);
                GameObject.Destroy(obj);
                GameManager.Instance.CheckIfEndOfBattle();
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
        int[] result = fighter.CheckWhichSquare();
        if (result[0]!=fighter.row || result[1] != fighter.col)
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
    /*
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
        
        return ObjectToReturn.Obj;

    }

    private StructForTheBestObj FindTarget(StructForTheBestObj ObjectToReturn)
    {
        int iteration = 0;

        if (tag == "Blue")
        {
            while (ObjectToReturn.Obj == null && !ObjectToReturn.IsTrue)
            {
                ObjectToReturn = FindTheBestObject(fighter.row, fighter.col,
                    GameManager.Instance.battleManager.EnemyFighters, iteration);
                iteration++;
            }
        }
        else
        {
            while (ObjectToReturn.Obj == null && !ObjectToReturn.IsTrue)
            {
                ObjectToReturn = FindTheBestObject(fighter.row, fighter.col,
                    GameManager.Instance.battleManager.BlueFighters, iteration);
                iteration++;
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
    private StructForTheBestObj FindTheBestObject(int row,int col, List<FighterPlacement>[,] grid,int iteration)
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
                        }
                    }
                }
              
            }
        }
        return obj;
    }
    */
    private GameObject FindNearestEnemy(List<FighterPlacement>[,] grid)
    {
        int maxDistance = Mathf.Max(grid.GetLength(0), grid.GetLength(1));
        for (int distance = 0; distance <= maxDistance; distance++)
        {
            GameObject enemy = FindEnemyInDistance(grid, distance);
            if (enemy != null)
            {
                return enemy;
            }
        }
        return null;
    }

    private GameObject FindEnemyInDistance(List<FighterPlacement>[,] grid, int distance)
    {
        for (int i = -distance; i <= distance; i++)
        {
            for (int j = -distance; j <= distance; j++)
            {
                if (i != -distance && i != distance && j != -distance && j != distance)
                {
                    continue;  // Skip nodes that are not at the edge of the square
                }
                int newRow = fighter.row + i;
                int newCol = fighter.col + j;
                if (newRow >= 0 && newRow < grid.GetLength(0) && newCol >= 0 && newCol < grid.GetLength(1))
                {
                    if (grid[newRow, newCol] != null && grid[newRow, newCol].Count > 0)
                    {
                        return grid[newRow, newCol][0].gameObject;  // Assumes we're interested in any enemy at this position
                    }
                }
            }
        }
        return null;
    }

}



