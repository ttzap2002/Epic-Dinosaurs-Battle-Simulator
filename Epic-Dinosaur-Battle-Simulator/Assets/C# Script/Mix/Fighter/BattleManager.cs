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
    static int[] Probability = new int[20];
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



    public static int RandomIdOfDino(string tag)
    {
        if (tag == "Enemy" && GameManager.Instance.currentScene.Id != 0)
        {
            return ReturnIdOfObjectForNonSandboxEnemy();
        }
        else
        {
            return ReturnIdOfObjectInOtherSituation();
        }
    }

    private static int ReturnIdOfObjectInOtherSituation()
    {
        int SumOfProbabilities = BattleManager.Probability.Sum();
        if (SumOfProbabilities == 0)
        {
            int id = 0;
            foreach (int item in GameManager.Instance.dynamicData.Dinosaurs)
            {
                if (item == 0 && id != 18 && id != 19)
                    BattleManager.Probability[id] = 1;
                else if (item == 1 && id != 18 && id != 19)
                    BattleManager.Probability[id] = 10;
                else if (id != 18 && id != 19)
                    BattleManager.Probability[id] = 10 + ((int)((item - 1) / 3));
                else
                    BattleManager.Probability[id] = 0;
                id++;
            }
            SumOfProbabilities = BattleManager.Probability.Sum();
        }
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, SumOfProbabilities + 1);
        int returner = 0;
        while (randomNumber - BattleManager.Probability[returner] > 0)
        {
            randomNumber -= BattleManager.Probability[returner];
            returner++;
        }
        return returner;
    }

    private static int ReturnIdOfObjectForNonSandboxEnemy()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(1, 67); // Wygeneruj liczbê od 1 do 66 (67 jest wy³¹czone)
        if (randomNumber <= 33)
        {
            if (randomNumber <= 18)
            {
                if (randomNumber <= 7)
                    return 0;
                else if (randomNumber <= 10)
                    return 1;
                else if (randomNumber == 11)
                    return 2;
                else
                    return 3;
            }
            else
            {
                if (randomNumber <= 21)
                    return 4;
                else if (randomNumber <= 22)
                    return 5;
                else if (randomNumber <= 29)
                    return 6;
                else if (randomNumber <= 32)
                    return 7;
                else
                    return 8;
            }
        }
        else
        {
            if (randomNumber <= 51)
            {
                if (randomNumber <= 40)
                    return 9;
                else if (randomNumber <= 43)
                    return 10;
                else if (randomNumber == 44)
                    return 11;
                else
                    return 12;
            }
            else
            {
                if (randomNumber <= 54)
                    return 13;
                else if (randomNumber == 55)
                    return 14;
                else if (randomNumber <= 62)
                    return 15;
                else if (randomNumber <= 65)
                    return 16;
                else
                    return 17;
            }
        }
    }

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

    public void Clear() 
    {

        foreach (GameObject obj in GameManager.Instance.enemyGameObjects.Concat(GameManager.Instance.blueGameObjects)) 
        {
            obj.SetActive(false);
            poolingList[obj.GetComponent<FighterPlacement>().index].Add(obj);
        }
        BattleInformation b = GameManager.Instance.UI.GetComponentInChildren<BattleInformation>();
        b.RefreshMoney();

    }

    public void GameResume()
    {
        GameManager.Instance.IsRun = true;
    }
    public void GamePause()
    {
        GameManager.Instance.IsRun = false;
    }
}
