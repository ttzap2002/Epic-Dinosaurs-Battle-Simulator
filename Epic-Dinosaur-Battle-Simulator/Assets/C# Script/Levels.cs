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
    int troopslimit;
    static int id=-1;
    bool isunlocked;
    
    public SceneLevel(int money, int troopslimit)
    {
        Money = money;
        Troopslimit = troopslimit;
        isunlocked = false;
        id++;
        //enemieslist = new List<GameObject>();
    }

    public int Money { get => money; set => money = value; }
    public int Troopslimit { get => troopslimit; set => troopslimit = value; }
    public static int Id { get => id;}
    public bool Isunlocked { get => isunlocked; set => isunlocked = value; }
    //public List<GameObject> Enemieslist { set => enemieslist = value; }

}
