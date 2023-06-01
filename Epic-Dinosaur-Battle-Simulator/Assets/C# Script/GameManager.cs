using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

using TMPro;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    private int i = 1;
    private bool isRun = false;
    public bool isContainRequireComponent = false;
    private bool isFirst = true;
    private static GameManager _instance;
    public GameObject mouse;
    public GameObject UI;
    public GameObject endOfBattle;
    public List<GameObject> prefabGameObjects = new List<GameObject>();
    public List<GameObject> enemyGameObjects = new List<GameObject>();
    public List<GameObject> blueGameObjects = new List<GameObject>();
    public bool isStarted = false;
    public AllLevelContainer levelContainer = new AllLevelContainer();
    public SceneLevel currentScene;
    public BattleManager battleManager;
    public GameObject draggable;
    public int idTileForBackFromShop = 0; //int, przeznaczony do cofania ze sklepu. 0- oznacza menu i jest domy�lne. Jednak jak wyjdziesz z lvlu do sklepu, to wr�ci ci� do wyboru lvlu. z sandboxu do wyboru mapy itp
    public int numberOfShopScreen = 0; //int przeznaczony do wyboru, kt�ry element sklepu jest widoczny (czy aktualnie przegl�dane s� dinozaury, mapy czy co). Numeracja: 0-dinozaury, 1-mapy, 2-pieni�dze, 99-brak
    //public int money = 10; // ilo�� posiadanej waluty przez gracza wykorzystywane do gry // nie aktualne. aktualnie kasa jest w dynamic data
    public DinoStats dinosaurStats; //klasa, posiadaj�ca pocz�tkowe statystyki ka�dego dinozaura
    public DynamicData dynamicData;
    public Dictionary<string, bool> canISetWarrior = new Dictionary<string, bool>(); //Zmienna booli kt�re decyduj� czy mo�na stawia� jednostk�. Jeden false blokuje t� mo�liwo��. Konkretne nazwy s� w starcie (pod awake)
    public static GameManager Instance { get { return _instance; } }

    public bool IsRun { get => isRun; set => isRun = value; }

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        
        if (isFirst) 
        {
            levelContainer.AddAllScene();
            dinosaurStats = new DinoStats();
            dynamicData = new DynamicData(new List<int>(){ 80, 1, 1, 1 }, new List<int>() { 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 }, new List<bool>() {true,true,false,false}, 25000);
            isFirst = false;
        }
         SceneManager.sceneLoaded += OnSceneLoaded; 

        DontDestroyOnLoad(gameObject);


    }

    // Start is called before the first frame update
    void Start()
    {
        canISetWarrior.Add("Joystick", true); //zmienna dla naci�ni�cia joysticka
        canISetWarrior.Add("Warrior", true); //zmienna dla naci�ni�cia paska z wyborem wojownik�w
        


    }
    /*
    // Update is called once per frame
    void Update()
    {
        
        if (isRun)
        {
            if (battleManager.EnemyFighters.Length == 0 || battleManager.BlueFighters.Length == 0)
            {
                endOfBattle.SetActive(true);
                var img = endOfBattle.GetComponentInChildren<Image>();
                TextMeshProUGUI pro = img.GetComponentInChildren<TextMeshProUGUI>();
                if(battleManager.EnemyFighters.Length.Equals(0)) 
                {
                    pro.text = "blue win";
                }
                else { pro.text = "red win"; }
                
                isRun = false;
                enemyGameObjects = null;
                blueGameObjects = null;
            }
        }

    }
    */
    public void CheckIfEndOfBattle()
    {
        bool isEnemyFighterContainAnyFighter = battleManager.IsEnemyFighterContainAnyFighter();
        if (!isEnemyFighterContainAnyFighter || !battleManager.IsBlueFighterContainAnyFighter()) 
        {
            endOfBattle.SetActive(true);
            var img = endOfBattle.GetComponentInChildren<Image>();
            TextMeshProUGUI pro = img.GetComponentInChildren<TextMeshProUGUI>();
            if (!isEnemyFighterContainAnyFighter)
            {
                pro.text = "blue win";
            }
            else { pro.text = "red win"; }

            isRun = false;
            enemyGameObjects = null;
            blueGameObjects = null;
            battleManager.DestroyAllObject();
        }
        
    }

    public void SetBattleManager()
    {
        List<FighterPlacement>[,] enemyFighters = new List<FighterPlacement>[10, 10];
        List<FighterPlacement>[,] blueFighters = new List<FighterPlacement>[10, 10];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                enemyFighters[i, j] = new List<FighterPlacement>();
                blueFighters[i, j] = new List<FighterPlacement>();
            }
        }
        battleManager = new BattleManager(enemyFighters, blueFighters);

    }
    
    public void GameResume()
    {
        Time.timeScale = 1f;
    }
    public void GamePause()
    {
        Time.timeScale = 0f;
    }

    public void AddScene(int id)
    {
        currentScene = levelContainer.LevelList[id];
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("kot");
        SetNecessaryComponent();
    }

    public void SetNecessaryComponent()
    {
        if (isContainRequireComponent)
        {
        
            draggable= GameObject.Find("Terrain");
            UI = GameObject.Find("UI");
            GameObject endobj = GameObject.FindGameObjectWithTag("EndOfBattle");
            endobj.SetActive(true);
            endOfBattle = endobj;
            mouse = GameObject.Find("MouseTarget");
            Canvas canvas = GameObject.Find("AllPrefab").GetComponent<Canvas>();
            prefabGameObjects = new List<GameObject>();
            SetBattleManager();
            Debug.Log(canvas.transform.childCount);
            for (int i = 0; i < canvas.transform.childCount; i++) // przechodzimy przez wszystkie dzieci Transform
            {
                Transform child = canvas.transform.GetChild(i);
                prefabGameObjects.Add(child.gameObject); // dodajemy komponent GameObject dziecka do listy
            }
            endOfBattle.SetActive(false);
            currentScene.SetObjectToScene();
        }
    }

    public void RefreshGameObjects() 
    {
        blueGameObjects = new List<GameObject>();
        enemyGameObjects = new List<GameObject>();
    }

    
}
