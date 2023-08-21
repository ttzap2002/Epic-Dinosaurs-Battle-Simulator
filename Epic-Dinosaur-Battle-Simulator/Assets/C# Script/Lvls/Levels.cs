using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLevel 
{
    //List<GameObject> enemieslist;
    int moneyLimit;
    int troopsLimit;
    int enemyTroopsLimit;
    int id;
    int enemyMoney;
    List<ObjectToDisplay> objectOnScenes;
    public SceneLevel(int money, int troopslimit,int id)
    {
        moneyLimit = money;
        Troopslimit = troopslimit;
        this.id=id;
        objectOnScenes= CreateObjectForScene(id);
        enemyMoney = returnEnemyMoney();
        //enemieslist = new List<GameObject>();
    }

  

    public int MoneyLimit { get => moneyLimit; set => moneyLimit = value; }
    public int Troopslimit { get => troopsLimit; set => troopsLimit = value; }
    public int Id { get => id;}
    public int EnemyTroopsLimit { get => enemyTroopsLimit; set => enemyTroopsLimit = value; }
    public int EnemyMoney { get => enemyMoney; set => enemyMoney = value; }

    //public List<GameObject> Enemieslist { set => enemieslist = value; }
    private List<ObjectToDisplay> CreateObjectForScene(int id) 
    {
        List<ObjectToDisplay> _lobject=new List<ObjectToDisplay>();
        if (id == 1) 
        {
            _lobject = new List<ObjectToDisplay> { new ObjectToDisplay(0, 0, 40, 0,1), new ObjectToDisplay(20, 0, 30, 0,1), new ObjectToDisplay(20, 0, 40, 1,1), new ObjectToDisplay(10, 0, 40, 0, 1) };
      
        }
        if (id == 2)
        {
            _lobject = new List<ObjectToDisplay> { new ObjectToDisplay(0, 0, 40, 1, 1), new ObjectToDisplay(20, 0, 30, 0, 1), new ObjectToDisplay(20, 0, 40, 1, 1), new ObjectToDisplay(10, 0, 40, 0, 1) };
        }
        if (id == 3)
        {
            _lobject = new List<ObjectToDisplay> { new ObjectToDisplay(0, 0, 40, 1, 1), new ObjectToDisplay(40, 0, 30, 0, 1), new ObjectToDisplay(20, 0, 40, 2, 1 ), new ObjectToDisplay(50, 0, 40, 3, 1),
            new ObjectToDisplay(10, 0, 30, 1, 1), new ObjectToDisplay(20, 0, 45, 2, 1), new ObjectToDisplay(30, 0, 40, 1, 1), new ObjectToDisplay(40, 0, 30, 0, 1),
            new ObjectToDisplay(4, 0, 36, 2, 1), new ObjectToDisplay(20, 0, 20, 1, 1), new ObjectToDisplay(13, 0, 47, 0, 1), new ObjectToDisplay(31, 0, 37, 3, 1),
            new ObjectToDisplay(10, 0, 30, 1, 1), new ObjectToDisplay(25, 0, 15.5f, 2, 1), new ObjectToDisplay(31, 0, 36, 2, 1), new ObjectToDisplay(10, 0, 0, 2, 1)
            };
        }
        if (id != 0) { enemyTroopsLimit = _lobject.Count; }
        else if (id==0) { enemyTroopsLimit = 80;}
        return _lobject;
    }

    private int returnEnemyMoney() => objectOnScenes.Select(x=>x.returnMoneyForThisUnit()).Sum();
    
    public void SetObjectToScene() 
    {
        Debug.Log(objectOnScenes.Count);
        foreach(var obj in objectOnScenes) 
        {
            GameObject gameObj = GameObject.Instantiate(GameManager.Instance.prefabGameObjects[obj.PrefabId]);
            gameObj.gameObject.transform.position =new Vector3(obj.XAxis,obj.YAxis, obj.ZAxis);
            gameObj.tag = "Enemy";
            //Object.Destroy(gameObj.gameObject.GetComponent<DestroyObjectOnClick>());
            gameObj.gameObject.GetComponent<FighterPlacement>().UpgradeStatLevel(obj.Lvl-1);
            GameManager.Instance.enemyGameObjects.Add(gameObj);
            gameObj.SetActive(true);
        }
    }

}
