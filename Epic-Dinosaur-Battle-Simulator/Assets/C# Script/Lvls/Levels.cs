using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLevel 
{
    //List<GameObject> enemieslist;
    int money;
    int troopsLimit;
    int enemyTroopsLimit;
    static int id=-1;
    bool isunlocked;
    List<ObjectToDisplay> objectOnScenes;
    public SceneLevel(int money, int troopslimit)
    {
        Money = money;
        Troopslimit = troopslimit;
        isunlocked = false;
        id++;
        objectOnScenes= CreateObjectForScene(id);

        //enemieslist = new List<GameObject>();
    }

    public int Money { get => money; set => money = value; }
    public int Troopslimit { get => troopsLimit; set => troopsLimit = value; }
    public static int Id { get => id;}
    public bool Isunlocked { get => isunlocked; set => isunlocked = value; }
    public int EnemyTroopsLimit { get => enemyTroopsLimit; set => enemyTroopsLimit = value; }

    //public List<GameObject> Enemieslist { set => enemieslist = value; }
    private List<ObjectToDisplay> CreateObjectForScene(int id) 
    {
        List<ObjectToDisplay> _lobject=new List<ObjectToDisplay>();
        if (id == 1) 
        {
            _lobject = new List<ObjectToDisplay> { new ObjectToDisplay(0, 0, 40, 0), new ObjectToDisplay(20, 0, 30, 0), new ObjectToDisplay(20, 0, 40, 1), new ObjectToDisplay(10, 0, 40, 0) };
      

        }
        if (id == 2)
        {
            _lobject = new List<ObjectToDisplay> { new ObjectToDisplay(0, 0, 40, 1), new ObjectToDisplay(20, 0, 30, 0), new ObjectToDisplay(20, 0, 40, 1), new ObjectToDisplay(10, 0, 40, 0) };

        }
        if (id != 0) { enemyTroopsLimit = _lobject.Count; }
        else if (id==0) { enemyTroopsLimit = 80;}
        return _lobject;
    }

    public void SetObjectToScene() 
    {
        Debug.Log(objectOnScenes.Count);
        foreach(var obj in objectOnScenes) 
        {
           
            GameObject gameObj = GameObject.Instantiate(GameManager.Instance.prefabGameObjects[obj.PrefabId]);
            gameObj.gameObject.transform.position =new Vector3(obj.XAxis,obj.YAxis, obj.ZAxis);
            gameObj.tag = "Enemy";
            Object.Destroy(gameObj.gameObject.GetComponent<MeleeFighter>());

            GameManager.Instance.enemyGameObjects.Add(gameObj);
            gameObj.SetActive(true);
        }
    }

}
