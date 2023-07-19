using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class MeleeFighter : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// zmienna odpowiadaj¹ca za to czy fighter jest w stanie walki
    /// </summary>
    public bool isFighting = false;
    int a = 1;
    /// <summary>
    /// zmienna odpowiadaj¹ca za to czy fighter jest gotowy do bitwy (false na poczatku i kiedy jest oszolomiony)
    /// </summary>
    [SerializeField] bool isActiveForBattle = false;

    protected FighterPlacement fighter = null;
    protected LongNeckFighting longNeckFighter = null;
    [SerializeField]
    protected bool ifEatEggImmediately;
    private float timer = 0.0f; // Zmienna do œledzenia czasu
    /// <summary>
    /// delegat przechowuj¹cy funkcje do atakowania szyj¹
    /// </summary>
    Func<List<GameObject>> neckAttack;
    /// <summary>
    /// delegat przechowuj¹cy funkcje do atakowania ogonem
    /// </summary>
    Func<List<GameObject>> tailAttack;

    /// <summary>
    /// zmienna do szybkosci rotacji
    /// </summary>
    [SerializeField] protected float rotationSpeed = 5f;
    /// <summary>
    /// zmienna bool sprawdza czy obiekt jest odporny na oszo³omienie
    /// </summary>
    [SerializeField] private bool isResistForStunning;
    private float basicx;
 
    public bool IsActiveForBattle { get => isActiveForBattle; set => isActiveForBattle = value; }
    public bool IsResistForStunning { get => isResistForStunning; set => isResistForStunning = value; }


    private void OnTriggerEnter(Collider other)
    {
        if (!isFighting)
        {
            Rigidbody otherRb = other.gameObject.GetComponent<Rigidbody>();
            if (otherRb != null)
            {
                Vector3 pushDirection = other.transform.position - transform.position;
                Vector3 newDirection = Quaternion.Euler(0, 90, 0) * pushDirection; // Rotate push direction by 90 degrees
                otherRb.AddForce(newDirection.normalized * 0.5f, ForceMode.Impulse);
            }
        }
    }
    protected void Start()
    {
        //GameManager.Instance.Awake();
        
        basicx = transform.rotation.x;

        fighter = gameObject.GetComponent<FighterPlacement>();

        //if (fighter.target is null) { GetFirstTarget(); }
        if (fighter.fightingScript == FightScript.LongNeck && longNeckFighter is null)
        {
            neckAttack = gameObject.GetComponent<LongNeckFighting>().ObjectsToHit;
        }
        if (fighter.HaveTailAttack)
        {
            tailAttack = gameObject.GetComponent<TailAttacKFighter>().ObjectsToHitByTail;
        }
       
    }

    protected virtual void Update()
    {
        UnityEngine.Profiling.Profiler.BeginSample("MeleeFighter");
        if (GameManager.Instance.IsRun && fighter.isAlive)
        {
            if (isActiveForBattle)
            {
                timer += Time.deltaTime;

                if (a % 3 == 0)
                {
                    if (fighter.target != null && fighter.target != fighter 
                        && fighter.target.isAlive)
                    {
                        if (!isFighting)
                        {
                            if (isChangeOfSquare())
                            {
                                bool isLessThan40 = GameManager.Instance.blueGameObjects.Count +
                                    GameManager.Instance.enemyGameObjects.Count < 40 ;
                                SetReactForOpponents(isLessThan40);
                              
                            }
                            if (Vector3.Distance(transform.position, fighter.target.transform.position) <= fighter.radius)
                            {
                                fighter.Agent.speed = 0;
                                isFighting = true;
                            }

                        }
                        else
                        {

                            if (Vector3.Distance(transform.position, fighter.target.transform.position) > fighter.radius)
                            {
                                isFighting = false;
                            }
                            if (timer >= fighter.interval)
                            {
                                if (fighter.fightingScript == FightScript.Traditional)
                                {
                                    Hit(fighter.target);
                                }
                                else if (fighter.fightingScript == FightScript.LongNeck && !fighter.HaveTailAttack)
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
                        Rotate();

                    }
                    else { GetTarget(); }
                }


                //transform.position += moveDirection;
                a++;
            }
            else if (!isResistForStunning)
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
        UnityEngine.Profiling.Profiler.EndSample();
    }

    public void GetTarget() {
        if (tag == "Blue") { fighter.target = FindNearestEnemy(GameManager.Instance.battleManager.EnemyFighters); }
        else { fighter.target = FindNearestEnemy(GameManager.Instance.battleManager.BlueFighters); }
        fighter.ChangeDestination(fighter.target.transform.position);
        fighter.Agent.speed = fighter.stats.speed;
    }


    public void SetFightingStatusToFalse()
    {
        isFighting = false;
        Debug.Log("Done");
    }
    protected virtual void Hit(FighterPlacement myEnemy)
    {
        if (myEnemy == fighter)
        {
            isFighting = false;
            GetTarget();
        }
        else
        {
            if (myEnemy != null)
            {
                FighterPlacement f = myEnemy.GetComponent<FighterPlacement>();
                if (f.behaviourScript == ScriptType.Egg)
                {
                    if (ifEatEggImmediately)
                    {
                        isFighting = false;
                        f.Destroyme();
                    }
                }
                f.stats.hp -= fighter.stats.attack;
                if (f.stats.hp <= 0)
                {
                    isFighting = false;
                    f.Destroyme();
                }
            }
        }
    }

    

    private void HitAllObjects(List<GameObject> ObjectsToHit)
    {
        foreach (GameObject obj in ObjectsToHit)
        {
            FighterPlacement f = obj.GetComponent<FighterPlacement>();
            f.stats.hp -= fighter.stats.attack;
            if (f.stats.hp <= 0)
            {
                isFighting = false;
                f.Destroyme();
            }
        }
    }

    /*
    protected virtual void Move()
    {
   
        agent.SetDestination(fighter.target.transform.position);

    }
*/
    protected void Rotate()
    {
        Vector3 directionToEnemy = (transform.position - fighter.target.transform.position).normalized;
        directionToEnemy = directionToEnemy.normalized;

        if (!fighter.IsObligatoryToRotate)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            //rotation.x = transform.rotation.x;
            //rotation.z = transform.rotation.z;
            transform.rotation = rotation;

        }

        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            rotation.x = transform.rotation.x;
            rotation.y = -transform.rotation.y;
            rotation.z = transform.rotation.z;
            transform.rotation = rotation;
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
    private void SetReactForOpponents (bool isLessThan40)
    {
        if (isLessThan40)
        {
            SetReact();
        }
        else { 
            System.Random r = new System.Random();
            int complexity = GameManager.Instance.enemyGameObjects.Count
                * GameManager.Instance.blueGameObjects.Count*2;
            double result = 800 / complexity;

            double probability = r.NextDouble();
            if(probability < result) {SetReact(); }
            
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
                if (min <= fighter.stats.radius)
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
                if (min <= fighter.stats.radius)
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
    private FighterPlacement FindNearestEnemy(List<FighterPlacement>[,] grid)
    {
        int maxDistance = Mathf.Max(grid.GetLength(0), grid.GetLength(1));
        FighterPlacement nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        for (int distance = 0; distance <= maxDistance; distance++)
        {
            FighterPlacement enemy = FindEnemyInDistance(grid, distance);
            if (enemy != null)
            {
                float currentDistance = Vector3.Distance(this.transform.position, enemy.transform.position);
                if (currentDistance < nearestDistance)
                {
                    nearestDistance = currentDistance;
                    nearestEnemy = enemy;
                }
            }
        }
        return nearestEnemy;
    }

    private FighterPlacement FindEnemyInDistance(List<FighterPlacement>[,] grid, int distance)
    {
        FighterPlacement nearestEnemy = null;
        float nearestDistance = float.MaxValue;

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
                        foreach (FighterPlacement fighterPlacement in grid[newRow, newCol])
                        {
                            float currentDistance = Vector3.Distance(this.transform.position, fighterPlacement.gameObject.transform.position);
                            if (currentDistance < nearestDistance)
                            {
                                nearestDistance = currentDistance;
                                nearestEnemy = fighterPlacement; 
                            }
                        }
                    }
                }
            }
        }
        return nearestEnemy;
    }
    private List<GameObject> FindEnemiesInRadius(float radius)
    {
        List<GameObject> enemiesInRadius = new List<GameObject>();

        // Pobieramy wszystkie kolizje w promieniu
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        // Sprawdzamy ka¿d¹ kolizjê
        foreach (var hit in hits)
        {
            // Sprawdzamy, czy kolizja nale¿y do wroga (na przyk³ad sprawdzaj¹c tag)
            if (hit.gameObject.CompareTag("Enemy"))
            {
                enemiesInRadius.Add(hit.gameObject);
            }
        }

        return enemiesInRadius;
    }

}



