using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicData
{
    List<int> unlockLvls = new List<int>(); // 1- to odblokowany pierwszy lvl ale zablokowany 2 lvl, 0 oznacza zablokowany kontynent
    List<int> dinosaurs = new List<int>(); // 0-nie odblokowany dinozaur, 1- œwie¿o kupiony dunozaur, 2 - drugi lvl itd.
    List<bool> terrain = new List<bool>(); // odblokowane mapy dla sandboxu. true- gracz moze grac, false -nie moze
    int money; // jest to waluta in-game

    public DynamicData(List<int> unlockLvls, List<int> dinosaurs, List<bool> terrain, int money)
    {
        UnlockLvls = unlockLvls;
        Dinosaurs = dinosaurs;
        Terrain = terrain;
        Money = money;
    }

    // w przyszlosci mozna dodac skorki dinozaurów

    public List<int> UnlockLvls { get => unlockLvls; set => unlockLvls = value; }
    public List<int> Dinosaurs { get => dinosaurs; set => dinosaurs = value; }
    public List<bool> Terrain { get => terrain; set => terrain = value; }
    public int Money { get => money; set => money = value; }

    
}
