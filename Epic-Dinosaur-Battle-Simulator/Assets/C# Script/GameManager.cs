using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

using TMPro;
using UnityEngine.UI;

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
    public int idTileForBackFromShop = 0; //int, przeznaczony do cofania ze sklepu. 0- oznacza menu i jest domyœlne. Jednak jak wyjdziesz z lvlu do sklepu, to wróci ciê do wyboru lvlu. z sandboxu do wyboru mapy itp
    public int numberOfShopScreen = 0; //int przeznaczony do wyboru, który element sklepu jest widoczny (czy aktualnie przegl¹dane s¹ dinozaury, mapy czy co). Numeracja: 0-dinozaury, 1-mapy, 2-pieni¹dze, 99-brak
    public int money = 10; // iloœæ posiadanej waluty przez gracza wykorzystywane do gry

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
            isFirst = false;
        }
         SceneManager.sceneLoaded += OnSceneLoaded; 

        DontDestroyOnLoad(gameObject);


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (isRun)
        {
            if (battleManager.EnemyFighters.Count == 0 || battleManager.BlueFighters.Count == 0)
            {
                endOfBattle.SetActive(true);
                var img = endOfBattle.GetComponentInChildren<Image>();
                TextMeshProUGUI pro = img.GetComponentInChildren<TextMeshProUGUI>();
                if(battleManager.EnemyFighters.Count == 0) 
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

    public void SetBattleManager()
    {
        List<FighterPlacement> enemyFighters = enemyGameObjects.Select(x => x.GetComponent<FighterPlacement>()).ToList();
        List<FighterPlacement> blueFighters = blueGameObjects.Select(x => x.GetComponent<FighterPlacement>()).ToList();
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
        Debug.Log("kot");
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
