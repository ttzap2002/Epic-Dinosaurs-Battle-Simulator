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
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

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
    private static bool isFirst = true;
    /// <summary>
    /// Instancja gameManager
    /// </summary>
    private static GameManager _instance;

    public MousePosition mouse;
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

    public List<GameObject> obstaclesObjects = new List<GameObject>();
    public bool isStarted = false;
    /// <summary>
    /// Klasa przechowuj¹ca wszystkie lvle, wraz z odpowiedni¹ specyfikacj¹ dla ka¿dego lvl-u w tym sandbox
    /// </summary>
    public AllLevelContainer levelContainer = new AllLevelContainer();

    public AllMapContainer mapContainer = new AllMapContainer();

    public Map currentMap;
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

    public DinoStats dinosaurStats; //klasa, posiadaj¹ca pocz¹tkowe statystyki ka¿dego dinozaura
    /// <summary>
    /// przechowuje zmienne dynamiczne, konieczne do dobrego zarz¹dzania gr¹
    /// </summary>
    public DynamicData2 dynamicData;
    public Dictionary<string, bool> canISetWarrior = new Dictionary<string, bool>(); //Zmienna booli które decyduj¹ czy mo¿na stawiaæ jednostkê. Jeden false blokuje t¹ mo¿liwoœæ. Konkretne nazwy s¹ w starcie (pod awake)
    public int salaryForBattle; //zmienna posiaajaca info o zdobytych pieniazkach z bitwy

    public List<ObjectToDisplay> temporaryObjectsToDisplay = new List<ObjectToDisplay>();

    public double f = 0f;

    public LevelReminder objectPositions;

    public short currentContinent;

    public Stopwatch time = new Stopwatch();

    public Action changeLevel;

    public Tutorial tutorial;

    //static List<Material> backgroundMapsForArenas;

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
            currentContinent = 0;
            dinosaurStats = new DinoStats();
            levelContainer.AddAllScene();
            currentMap = mapContainer.MapList[0];
            //dynamicData = new DynamicData(new List<int>(){ 80, 1, 1, 1 }, new List<int>() { 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 }, new List<bool>() {true,true,false,false}, 25000);
            dynamicData = DynamicData2.Load();
            dynamicData.Save();
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
            GetMoneyForLevel(!FinishGame(isEnemyFighterContainAnyFighter));
            time.Stop();
            UnityEngine.Debug.Log($"{f / time.Elapsed.TotalSeconds}");
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
        GameObject pause = GameObject.Find("PauseButton");
        pause.SetActive(false);
        GameObject blue = GameObject.Find("BlueWon");
        GameObject red = GameObject.Find("RedWon");
        if (!isEnemyFighterContainAnyFighter)
        {
            blue.GetComponent<UnityEngine.UI.Image>().enabled = true;
            red.GetComponent<UnityEngine.UI.Image>().enabled = false;
        }
        else
        {
            blue.GetComponent<UnityEngine.UI.Image>().enabled = false;
            red.GetComponent<UnityEngine.UI.Image>().enabled = true;
        }

        isRun = false;


        if (currentScene.Id > 0 && !isEnemyFighterContainAnyFighter
      && currentScene.Id == dynamicData.UnlockLvls[currentContinent])
        {
            dynamicData.UnlockLvls[currentContinent]++;
            dynamicData.Save();
        }

        if(currentScene.Id > 0)
        {
            Transform childTransform = endOfBattle.transform.Find("EndView");
            Transform childTransformLeft = childTransform.transform.Find("Left");
            Transform childTransformRight = childTransform.transform.Find("Right");
            childTransformLeft.gameObject.SetActive(true);
            childTransformRight.gameObject.SetActive(true);

            if (currentScene.Id == 1)
            {
                childTransformLeft.gameObject.SetActive(false);
            }

            if (currentScene.Id == 80 || currentScene.Id + 1 > dynamicData.UnlockLvls[currentContinent])
            {
                childTransformRight.gameObject.SetActive(false);
            }
        }

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
        battleManager.Initialize(enemyFighters, blueFighters);

    }



    /// <summary>
    /// Funkcja która zmienia obecn¹ scene przydatna w szczególnoœci do przycisków przy lvl
    /// </summary>
    /// <param name="id"></param>
    public void AddScene(int id)
    {
        currentScene = levelContainer.LevelList[currentContinent][id];
    }

    public void ChangeMap(int id)
    {
        currentMap = mapContainer.MapList[id];
    }

    private void ChangeBattleInformation(int bluemoney, int enemymoney)
    {
        BattleInformation b = UI.GetComponentInChildren<BattleInformation>();
        b.RefreshMoney(bluemoney, enemymoney);
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
            mouse = GameObject.Find("MouseTarget").GetComponent<MousePosition>();
            Canvas canvas = GameObject.Find("AllPrefab").GetComponent<Canvas>();
            Canvas obstaclesCanvas = GameObject.Find("Obstacles").GetComponent<Canvas>();
            this.battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
            prefabGameObjects = new List<GameObject>();
            obstaclesObjects = new List<GameObject>();
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

            changeLevel = null; 
            endOfBattle.SetActive(false);
            currentScene.SetObjectToScene();
            currentMap.SetObjectToScene();
           
            GameObject Skysphere = GameObject.Find("Skysphere");
            switch (currentMap.mapBackground)
            {
                case(Background.DinoDesert):
                    Skysphere.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Images/FromMB/ArenasPictures/DinoDesert/Materials/Dino_Desert_caosc");
                    break;
                case (Background.SandStorm):
                    Skysphere.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Images/FromMB/ArenasPictures/DinoDesert/Materials/Dino_Desert_caosc");
                    break;
                case (Background.ForestFrenzy):
                    Skysphere.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Images/FromMB/ArenasPictures/ForestFrenzy/Materials/ForestFrenzy");
                    break;
                case (Background.JurrasicJungle):
                    Skysphere.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Images/FromMB/ArenasPictures/DinoDesert/Materials/Dino_Desert_caosc");
                    break;

            }

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


public class LevelReminder
{
    List<ObjectToDisplay> temporaryObjectsToDisplayForBlue;
    List<ObjectToDisplay> temporaryObjectsToDisplayForEnemy;
    readonly int blueMoney;
    readonly int redMoney;
    int blueTroops;
    int redTroops;

    public LevelReminder(List<ObjectToDisplay> temporaryObjectsToDisplayForBlue, List<ObjectToDisplay> temporaryObjectsToDisplayForEnemy, int blueMoney, int redMoney)
    {
        this.temporaryObjectsToDisplayForBlue = temporaryObjectsToDisplayForBlue;
        this.temporaryObjectsToDisplayForEnemy = temporaryObjectsToDisplayForEnemy;
        this.blueMoney = blueMoney;
        this.redMoney = redMoney;
    }

    public List<ObjectToDisplay> TemporaryObjectsToDisplayForBlue { get => temporaryObjectsToDisplayForBlue; set => temporaryObjectsToDisplayForBlue = value; }
    public List<ObjectToDisplay> TemporaryObjectsToDisplayForEnemy { get => temporaryObjectsToDisplayForEnemy; set => temporaryObjectsToDisplayForEnemy = value; }

    public int BlueMoney => blueMoney;

    public int RedMoney => redMoney;
}