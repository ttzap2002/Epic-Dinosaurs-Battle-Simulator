using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

using TMPro;
using UnityEngine.UI;
using System;
using UnityEditorInternal;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    private int i = 1;
    /// <summary>
    /// Zmienna odpowiadaj¹ca za to czy bitwa siê rozpocze³a
    /// </summary>
    [SerializeField] private bool isRun = false;

    public bool isContainRequireComponent = false;
    /// <summary>
    /// isFirst zmienna do sprawdzenia czy GameManager zosta³ juz wywo³any
    /// </summary>
    /// 
    [SerializeField]
    private bool isFirst;
    /// <summary>
    /// Instancja gameManager
    /// </summary>
    private static GameManager _instance;

    public GameObject mouse;
    public GameObject UI;
    /// <summary>
    /// Ui Odpowiadaj¹ce za zakoñczenie bitwy
    /// </summary>
    public GameObject endOfBattle;
    /// <summary>
    /// lista przechowuj¹ca wszyskie prefaby (wszystkich fighterów), potrzebna do stawiania nowych jednostek
    /// </summary>
    public List<GameObject> prefabGameObjects = new List<GameObject>();
    /// <summary>
    /// lista przechowuj¹ca wszystkie obiekty gracza enemy
    /// </summary>
    public List<GameObject> enemyGameObjects = new List<GameObject>();
    /// <summary>
    /// lista przechowuj¹ca wszystkie obiekty gracza blue
    /// </summary>
    public List<GameObject> blueGameObjects = new List<GameObject>();
    public bool isStarted = false;
    /// <summary>
    /// Klasa przechowuj¹ca wszystkie lvle, wraz z odpowiedni¹ specyfikacj¹ dla ka¿dego lvl-u w tym sandbox
    /// </summary>
    public AllLevelContainer levelContainer = new AllLevelContainer();
    /// <summary>
    /// Obecna level jaki jest 
    /// </summary>
    public SceneLevel currentScene;
    /// <summary>
    /// klasa odpowiadaj¹ca za zarz¹dzanie bitw¹
    /// </summary>
    public BattleManager battleManager;
    /// <summary>
    /// GameObject w unity do dodawawania/usuwania obiektów
    /// </summary>
    public GameObject draggable;
    public int idTileForBackFromShop = 0; //int, przeznaczony do cofania ze sklepu. 0- oznacza menu i jest domyœlne. Jednak jak wyjdziesz z lvlu do sklepu, to wróci ciê do wyboru lvlu. z sandboxu do wyboru mapy itp
    public int numberOfShopScreen = 0; //int przeznaczony do wyboru, który element sklepu jest widoczny (czy aktualnie przegl¹dane s¹ dinozaury, mapy czy co). Numeracja: 0-dinozaury, 1-mapy, 2-pieni¹dze, 99-brak
    //public int money = 10; // iloœæ posiadanej waluty przez gracza wykorzystywane do gry // nie aktualne. aktualnie kasa jest w dynamic data
    public DinoStats dinosaurStats; //klasa, posiadaj¹ca pocz¹tkowe statystyki ka¿dego dinozaura
    /// <summary>
    /// przechowuje zmienne dynamiczne, konieczne do dobrego zarz¹dzania gr¹
    /// </summary>
    public DynamicData dynamicData;
    public Dictionary<string, bool> canISetWarrior = new Dictionary<string, bool>(); //Zmienna booli które decyduj¹ czy mo¿na stawiaæ jednostkê. Jeden false blokuje t¹ mo¿liwoœæ. Konkretne nazwy s¹ w starcie (pod awake)
    public int salaryForBattle; //zmienna posiaajaca info o zdobytych pieniazkach z bitwy

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
            //dynamicData = new DynamicData(new List<int>(){ 80, 1, 1, 1 }, new List<int>() { 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 }, new List<bool>() {true,true,false,false}, 25000);
            dynamicData = DynamicData.Load(isFirst);
            isFirst = false;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;

        DontDestroyOnLoad(gameObject);


    }

    // Start is called before the first frame update
    void Start()
    {
        canISetWarrior.Add("Joystick", true); //zmienna dla naciœniêcia joysticka
        canISetWarrior.Add("Warrior", true); //zmienna dla naciœniêcia paska z wyborem wojowników

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
    /// <summary>
    /// Funkcja sprawdzaj¹ca czy bitwa powinna siê zakoñczyæ
    /// </summary>
    public void CheckIfEndOfBattle()
    {
        bool isEnemyFighterContainAnyFighter = IsEnemyContainAnyFighter();
        if (!isEnemyFighterContainAnyFighter || !IsBlueContainAnyFighter())
        {
            GetMoneyForLevel(FinishGame(isEnemyFighterContainAnyFighter));
        }

    }


    private bool IsEnemyContainAnyFighter()
    {
        return enemyGameObjects.Count > 0;
    }

    private bool IsBlueContainAnyFighter()
    {
        return blueGameObjects.Count > 0;
    }

    public bool FinishGame(bool isEnemyFighterContainAnyFighter)
    {
        endOfBattle.SetActive(true);
        var img = endOfBattle.GetComponentInChildren<Image>();
        TextMeshProUGUI pro = img.GetComponentInChildren<TextMeshProUGUI>();
        if (!isEnemyFighterContainAnyFighter)
        {
            pro.text = "Player Blue is winner";
            pro.color = Color.blue;
        }
        else
        {
            pro.text = "Player Red is winner";
            pro.color = Color.red;
        }

        isRun = false;
        enemyGameObjects = null;
        blueGameObjects = null;
        //battleManager.DestroyAllObject();
        return isEnemyFighterContainAnyFighter;
    }

    /// <summary>
    /// Przydzielanie pieniedzy po bitwie
    /// </summary>
    /// <param name="isWin">Zmeinna, czy gracz wygral level</param>
    private void GetMoneyForLevel(bool isWin)
    {
        salaryForBattle = currentScene.Id;
        if (salaryForBattle == 0)
        {
            System.Random random = new System.Random();
            salaryForBattle = random.Next(70, 141);
        }
        else if (isWin)
        {
            salaryForBattle = salaryForBattle % 80;
            System.Random random = new System.Random();
            salaryForBattle = random.Next(100 + salaryForBattle, (salaryForBattle * 3) + 101);
        }
        else
        {
            salaryForBattle = salaryForBattle % 80;
            System.Random random = new System.Random();
            salaryForBattle = random.Next(50 + salaryForBattle, (salaryForBattle * 2) + 51);
        }
        dynamicData.Money += salaryForBattle;
        UnityEngine.Debug.Log(salaryForBattle.ToString());
        Instance.dynamicData.Save();
        TextMeshProUGUI moneyInfoTxt = GameObject.Find("Money").GetComponent<TextMeshProUGUI>();
        moneyInfoTxt.text = $"+{salaryForBattle.ToString()}";
    }

    /// <summary>
    /// Funkcja tworz¹ca battleManager, ustawia niezbedne pola 
    /// </summary>
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
        isRun = true;
    }
    public void GamePause()
    {
        isRun = false;


    }

    /// <summary>
    /// Funkcja która zmienia obecn¹ scene przydatna w szczególnoœci do przycisków przy lvl
    /// </summary>
    /// <param name="id"></param>
    public void AddScene(int id)
    {
        currentScene = levelContainer.LevelList[id];
    }

    /// <summary>
    /// Funckcja wywo³ywana przy zmianie scen
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetNecessaryComponent();
    }

    /// <summary>
    /// Ustawia konieczne komponenty dla GameManagera (przy zmianie scen gameManager traci referencje)
    /// </summary>
    public void SetNecessaryComponent()
    {
        if (isContainRequireComponent)
        {

            draggable = GameObject.Find("Terrain");
            UI = GameObject.Find("UI");
            GameObject endobj = GameObject.FindGameObjectWithTag("EndOfBattle");
            endobj.SetActive(true);
            endOfBattle = endobj;
            mouse = GameObject.Find("MouseTarget");
            Canvas canvas = GameObject.Find("AllPrefab").GetComponent<Canvas>();
            prefabGameObjects = new List<GameObject>();
            SetBattleManager();
            UnityEngine.Debug.Log(canvas.transform.childCount);
            for (int i = 0; i < canvas.transform.childCount; i++) // przechodzimy przez wszystkie dzieci Transform
            {
                Transform child = canvas.transform.GetChild(i);
                prefabGameObjects.Add(child.gameObject); // dodajemy komponent GameObject dziecka do listy
            }
            endOfBattle.SetActive(false);
            currentScene.SetObjectToScene();
        }
    }

    /// <summary>
    /// Ustawia obiekty gracza blue i enemy na nowe puste listy
    /// </summary>
    public void RefreshGameObjects()
    {
        blueGameObjects = new List<GameObject>();
        enemyGameObjects = new List<GameObject>();
    }


}
