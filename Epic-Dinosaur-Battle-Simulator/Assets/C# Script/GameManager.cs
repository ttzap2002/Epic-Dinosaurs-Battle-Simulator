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
    /// Zmienna odpowiadaj�ca za to czy bitwa si� rozpocze�a
    /// </summary>
    [SerializeField] private bool isRun = false;

    public bool isContainRequireComponent = false;
    /// <summary>
    /// isFirst zmienna do sprawdzenia czy GameManager zosta� juz wywo�any
    /// </summary>
    /// 
    private static bool isFirst=true;
    /// <summary>
    /// Instancja gameManager
    /// </summary>
    private static GameManager _instance;

    public GameObject mouse;
    public GameObject UI;
    /// <summary>
    /// Ui Odpowiadaj�ce za zako�czenie bitwy
    /// </summary>
    public GameObject endOfBattle;
    /// <summary>
    /// lista przechowuj�ca wszyskie prefaby (wszystkich fighter�w), potrzebna do stawiania nowych jednostek
    /// </summary>
    public List<GameObject> prefabGameObjects = new List<GameObject>();
    /// <summary>
    /// lista przechowuj�ca wszystkie obiekty gracza enemy
    /// </summary>
    public List<GameObject> enemyGameObjects = new List<GameObject>();
    /// <summary>
    /// lista przechowuj�ca wszystkie obiekty gracza blue
    /// </summary>
    public List<GameObject> blueGameObjects = new List<GameObject>();

    public List<GameObject> obstaclesObjects= new List<GameObject>();
    public bool isStarted = false;
    /// <summary>
    /// Klasa przechowuj�ca wszystkie lvle, wraz z odpowiedni� specyfikacj� dla ka�dego lvl-u w tym sandbox
    /// </summary>
    public AllLevelContainer levelContainer = new AllLevelContainer();

    public AllMapContainer mapContainer= new AllMapContainer();

    public Map currentMap;
    /// <summary>
    /// Obecna level jaki jest 
    /// </summary>
    public SceneLevel currentScene;
    /// <summary>
    /// klasa odpowiadaj�ca za zarz�dzanie bitw�
    /// </summary>
    public BattleManager battleManager;
    /// <summary>
    /// GameObject w unity do dodawawania/usuwania obiekt�w
    /// </summary>
    public GameObject draggable;
    public int idTileForBackFromShop = 0; //int, przeznaczony do cofania ze sklepu. 0- oznacza menu i jest domy�lne. Jednak jak wyjdziesz z lvlu do sklepu, to wr�ci ci� do wyboru lvlu. z sandboxu do wyboru mapy itp
    public int numberOfShopScreen = 0; //int przeznaczony do wyboru, kt�ry element sklepu jest widoczny (czy aktualnie przegl�dane s� dinozaury, mapy czy co). Numeracja: 0-dinozaury, 1-mapy, 2-pieni�dze, 99-brak
    //public int money = 10; // ilo�� posiadanej waluty przez gracza wykorzystywane do gry // nie aktualne. aktualnie kasa jest w dynamic data
   
    public DinoStats dinosaurStats; //klasa, posiadaj�ca pocz�tkowe statystyki ka�dego dinozaura
    /// <summary>
    /// przechowuje zmienne dynamiczne, konieczne do dobrego zarz�dzania gr�
    /// </summary>
    public DynamicData dynamicData;
    public Dictionary<string, bool> canISetWarrior = new Dictionary<string, bool>(); //Zmienna booli kt�re decyduj� czy mo�na stawia� jednostk�. Jeden false blokuje t� mo�liwo��. Konkretne nazwy s� w starcie (pod awake)
    public int salaryForBattle; //zmienna posiaajaca info o zdobytych pieniazkach z bitwy

    public static GameManager Instance { get { return _instance; } }

    public bool IsRun { get => isRun; set => isRun = value; }

    public void Awake()
    {
        
        if (_instance != null && _instance!=this)
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
    /// <summary>
    /// Funkcja sprawdzaj�ca czy bitwa powinna si� zako�czy�
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
    /// Funkcja tworz�ca battleManager, ustawia niezbedne pola 
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
        battleManager.Initialize(enemyFighters, blueFighters);

    }

    

    /// <summary>
    /// Funkcja kt�ra zmienia obecn� scene przydatna w szczeg�lno�ci do przycisk�w przy lvl
    /// </summary>
    /// <param name="id"></param>
    public void AddScene(int id)
    {
        currentScene = levelContainer.LevelList[id];
    }

    public void ChangeMap(int id)
    {
        currentMap = mapContainer.MapList[id];
    }

    /// <summary>
    /// Funckcja wywo�ywana przy zmianie scen
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
            Canvas obstaclesCanvas = GameObject.Find("Obstacles").GetComponent<Canvas>();
            this.battleManager  = GameObject.Find("BattleManager").GetComponent<BattleManager>();
            prefabGameObjects = new List<GameObject>();
            obstaclesObjects= new List<GameObject>();
            SetBattleManager();
            UnityEngine.Debug.Log(canvas.transform.childCount);
            for (int i = 0; i < canvas.transform.childCount; i++) // przechodzimy przez wszystkie dzieci Transform
            {
                Transform child = canvas.transform.GetChild(i);
                prefabGameObjects.Add(child.gameObject); // dodajemy komponent GameObject dziecka do listy
            }
            for (int i = 0; i < obstaclesCanvas.transform.childCount; i++) // przechodzimy przez wszystkie dzieci Transform
            {
                Transform child = obstaclesCanvas.transform.GetChild(i);
                obstaclesObjects.Add(child.gameObject); // dodajemy komponent GameObject dziecka do listy
            }


            endOfBattle.SetActive(false);
            currentScene.SetObjectToScene();
            currentMap.SetObjectToScene();
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
