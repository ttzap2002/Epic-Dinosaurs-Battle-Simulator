using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int i=0;
    private static GameManager _instance;
  
    public bool isStarted=false;
    public MeleeFighter[] fighters=new MeleeFighter[100];
    
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
    }
    // Start is called before the first frame update
    void Start()
    {
        fighters = FindObjectsOfType<MeleeFighter>();
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

    public void AddToList(GameObject obj) 
    {
        fighters.AddRange(obj.GetComponentsInChildren<GameObject>());
    }
    public void SetAllObjectActive()
    {
   
        if (fighters == null) { Debug.Log("NULL"); }
        foreach (MeleeFighter fighter in fighters)
        {
            fighter.gameObject.SetActive(true);
        }
       
    }
    public void DoNothing() { }
}
