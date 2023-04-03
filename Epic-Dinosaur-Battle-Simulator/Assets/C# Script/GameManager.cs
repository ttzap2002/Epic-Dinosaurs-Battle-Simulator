using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private int i=0;
    private static GameManager _instance;
    public GameObject mouse;
    public GameObject Uiinformation;
    public GameObject draggableItem;
    public List<GameObject> prefabGameObjects = new List<GameObject>();
    public List<GameObject> enemyGameObjects = new List<GameObject>();
    public List<GameObject> blueGameObjects = new List<GameObject>();
    public bool isStarted=false;
    public AllLevelContainer levelContainer= new AllLevelContainer();
    public SceneLevel currentScene;
    public BattleManager battleManager;
    public static GameManager Instance { get { return _instance; } }


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
        levelContainer.AddAllScene();
        currentScene = levelContainer.LevelList[0];

    }
    // Start is called before the first frame update
    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    { 
        
    }

    public void SetBattleManager() 
    {
        List<FighterPlacement> enemyFighters = enemyGameObjects.Select(x => x.GetComponent<FighterPlacement>()).ToList();
        List<FighterPlacement> blueFighters = blueGameObjects.Select(x => x.GetComponent<FighterPlacement>()).ToList();
        battleManager=new BattleManager(enemyFighters, blueFighters);

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

    
}
