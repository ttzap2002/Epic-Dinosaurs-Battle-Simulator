using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public int i=0;
    private static GameManager _instance;
    public GameObject mouse;
    public GameObject Uiinformation;
    public List<GameObject> gameObjects = new List<GameObject>();
    public bool isStarted=false;
    public List<MeleeFighter> fighters=new List<MeleeFighter>();
    protected AllLevelContainer levelContainer= new AllLevelContainer();
    public SceneLevel currentScene;
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
        fighters = FindObjectsOfType<MeleeFighter>().ToListPooled();
        foreach(var f in fighters) { f.setdisactive(); }
    }
    // Update is called once per frame
    void Update()
    { 
        
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

    public void AddObject(MeleeFighter fighter)
    {
        fighters.Add(fighter);
    }
    public void SetAllObjectActive()
    {
       
   
        if (fighters == null) { Debug.Log("NULL"); }
        foreach (MeleeFighter fighter in fighters)
        {
            fighter.gameObject.SetActive(true);
        }
       
    }
    
}
