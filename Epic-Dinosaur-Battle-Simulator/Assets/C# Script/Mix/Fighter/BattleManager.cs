using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Purchasing;

public class BattleManager :MonoBehaviour
{
    //List<FighterPlacement> enemyFighters;
    //List<FighterPlacement> blueFighters;

    List<FighterPlacement>[,] enemyFighters= new List<FighterPlacement>[10,10];
    List<FighterPlacement>[,] blueFighters = new List<FighterPlacement>[10, 10];
    int[] kosmoceraptorsCount=new int[2] { 0,0 };
    /// <summary>
    /// zmienna potrzebna do ogarniecia stosunku mojewojska kontra wroga
    /// </summary>
    float[] moneySum = new float[2] { 0f,0f};
    private float timer = 0f;
    /// <summary>
    /// zmienna która okreœla moc gêstoœci centrum w rozumieniu takim ze im wieksze tym d³u¿ej bitwa bedzie trwaæ
    /// </summary>
    private float[] powerValue;

    private float[] parameter=new float[16]
    { -1.5f,-2.0f, -2.0f,-1.5f,-0.25f,-0.5f,-0.5f,-0.25f,
      0.25f,0.5f, 0.5f,0.25f,1.5f,2.0f,2.0f,1.5f
    };

    public List<GameObject>[] poolingList=new List<GameObject>[20];

    public delegate void changeProgressBar();
    public changeProgressBar Delegate;
    int i = 0;

    public void Initialize(List<FighterPlacement>[,] enemyFighters, List<FighterPlacement>[,] blueFighters)
    {
        this.enemyFighters = enemyFighters;
        this.blueFighters = blueFighters;
        for(int i=0;i<20;i++) 
        {
            poolingList[i]=new List<GameObject>(); 
        }
        PowerValue = new float[2] { 0, 0 };
       

    }

    public void SetDelegate(GameObject obj) 
    {
        Delegate=obj.GetComponent<ProgressBar>().ChangeProgressBar;
    }
    public List<FighterPlacement>[,] EnemyFighters { get => enemyFighters; set => enemyFighters = value; }
    public List<FighterPlacement>[,] BlueFighters { get => blueFighters; set => blueFighters = value; }
    public int[] KosmoceraptorsCount { get => kosmoceraptorsCount; set => kosmoceraptorsCount = value; }
    public float[] MoneySum { get => moneySum; set => moneySum = value; }
    public float[] PowerValue { get => powerValue; set => powerValue = value; }

    //przestarza³a wersja
    /*
    public void RemoveFromList(FighterPlacement g) 
    {
        if (g.tag == "Blue")
        {
            BlueFighters.Remove(g);
        }
        else { EnemyFighters.Remove(g); }
    }
    */
    public void React(bool isReactForBlue,FighterPlacement obj) 
    {
        if (obj != null)
        {

            if (isReactForBlue)
            {
                foreach (var listOfFighter in blueFighters)
                {
                    foreach (var fighter in listOfFighter)
                    {
                        fighter.TryChangeTarget(obj);
                    }
                }
            }
            else
            {
                foreach (var listOfFighter in EnemyFighters)
                {
                    foreach (var fighter in listOfFighter)
                    {
                        fighter.TryChangeTarget(obj);

                    }
                }
            }
        }
    }

    public void RemoveFromList(FighterPlacement g,int row,int col)
    {
        
        if (g.tag == "Blue")
        {
            for (int i = BlueFighters[row, col].Count-1; i >= 0; i--)
            {
                if (BlueFighters[row, col][i] == g)
                {
                    BlueFighters[row, col].RemoveAt(i);
                }
            }
           
        }
        else
        {
            for (int i = EnemyFighters[row, col].Count - 1; i >= 0; i--)
            {
                if (EnemyFighters[row, col][i] == g)
                {
                    EnemyFighters[row, col].RemoveAt(i);
                }
            }
        }
    }

    public void MakeMoney() 
    {

    }

    public bool IsEnemyFighterContainAnyFighter() 
    {
       foreach(var list in EnemyFighters) 
       {
            if (list.Count > 0) 
            {
                return true;
            }
       }
       return false;
    }

    public bool IsBlueFighterContainAnyFighter()
    {
        foreach (var list in BlueFighters)
        {
            if (list.Count > 0)
            {
                return true;
            }
        }
        return false;
    }

    public void DestroyAllObject() 
    {

       foreach(var list in EnemyFighters) 
       {
            for(int i = list.Count - 1; i >= 0; i--) 
            {
                FighterPlacement c = list[i].gameObject.GetComponent<FighterPlacement>();
                if (c.behaviourScript == ScriptType.MeleeFighter) 
                {
                    list[i].gameObject.GetComponent<MeleeFighter>().IsActiveForBattle = false;

                }
                else 
                {
                    list[i].gameObject.GetComponent<SpawnerBehaviour>().IsReadyForFight = false;

                }
            }
       }
       foreach (var list in BlueFighters)
       {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                FighterPlacement c = list[i].gameObject.GetComponent<FighterPlacement>();
                if (c.behaviourScript == ScriptType.MeleeFighter)
                {
                    list[i].gameObject.GetComponent<MeleeFighter>().IsActiveForBattle = false;

                }
                else
                {
                    list[i].gameObject.GetComponent<SpawnerBehaviour>().IsReadyForFight = false;

                }
            }
        }

    }
    public void SetCourutine() 
    {
        StartCoroutine(UpdateCoroutine());
    }
    private void Start()
    {
        StartCoroutine(UpdateCoroutine());

    }
    private IEnumerator UpdateCoroutine()
    {
      

        while (GameManager.Instance.IsRun)
        {
           
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            MaKeValueForIntelligentLayerFighter();
            stopwatch.Stop();
            long ticks = stopwatch.ElapsedTicks;
            double timeInMicroseconds = (double)ticks / Stopwatch.Frequency * 1000000;
            UnityEngine.Debug.Log($"{PowerValue[0]}  i {PowerValue[1]} czas wykonywania {timeInMicroseconds}");
            yield return new WaitForSeconds(3.0f);
        }
        yield return null;  // czekaj na nastêpny krok, jeœli GameManager.Instance.IsRun jest fa³szywy
        
    }



    /*mo¿liwe modele
     * value=x/y*(x+y)+Suma z aij*zij
     * przy czym x to liczba naszych jednostek w centralnym kwadracie
       gdzie aij to parametry dla poszczegolnego kwadratu a zij to ilosc jednostek na poszczegolnym kwadracie

     * 
    */





    private void MaKeValueForIntelligentLayerFighter()
    {
        float enemyResult = 0;
        int enemyCount = 0;
        float blueResult = 0;
        int blueCount = 0;
        int k = 0;
        for (int i = 3; i < 7; i++)
        {
            for (int j = 3; j < 7; j++)
            {
                enemyResult += enemyFighters[i, j].Count * parameter[k];
                enemyCount += enemyFighters[i, j].Count;
                blueResult += blueFighters[i, j].Count * parameter[k] * -1;
                blueCount += blueFighters[i, j].Count;
                k++;
            }
        }
        try
        {
            enemyResult = enemyResult + enemyCount * (enemyCount + blueCount) / blueCount;
        }
        catch 
        {
            enemyResult = 1000;
        }
        try
        {
            blueResult = blueResult + blueCount * (enemyCount + blueCount) / enemyCount;
        
        }
        catch 
        {
            blueResult= 1000;
        }

        PowerValue[0] = blueResult;
        PowerValue[1] = enemyResult;
    }



}
