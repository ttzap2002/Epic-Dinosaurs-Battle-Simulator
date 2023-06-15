using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEditor;

[Serializable]
public class DynamicData
{
    List<int> unlockLvls = new List<int>(); // 1- to odblokowany pierwszy lvl ale zablokowany 2 lvl, 0 oznacza zablokowany kontynent
    List<int> dinosaurs = new List<int>(); // 0-nie odblokowany dinozaur, 1- œwie¿o kupiony dunozaur, 2 - drugi lvl itd.
    List<bool> terrain = new List<bool>(); // odblokowane mapy dla sandboxu. true- gracz moze grac, false -nie moze
    int money; // jest to waluta in-game
    public int battlesWithoutAds = 0; // ile bitew ostatnich nie mia³o wymuszonej reklamy (pomijalnej)

    private DynamicData() 
    {
        battlesWithoutAds = 0;
    }

    public DynamicData(List<int> unlockLvls, List<int> dinosaurs, List<bool> terrain, int money) : this()
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

    private void ZapiszBin(string nazwa)
    {
        FileStream fs = new FileStream($"{nazwa}.bin", FileMode.OpenOrCreate);
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(fs, this);
        }
        catch (SerializationException e)
        {
            Debug.Log("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }
    private static object OdczytajBin(string nazwa)
    {
        DynamicData zespol = new DynamicData(new List<int>() { 80, 1, 1, 1 }, new List<int>() { 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 }, new List<bool>() { true, true, false, false }, 25000);
        try
        {
            FileStream fs = new FileStream($"{nazwa}.bin", FileMode.Open); //zrobiæ na try, wrazie jakby pierwszy raz uruchamiano grê
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                zespol = (DynamicData)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Debug.Log("Loading failed");
            }
            finally
            {
                fs.Close();
            }
        }
        catch (FileNotFoundException ex)
        {
            zespol = new DynamicData(new List<int>() { 80, 1, 1, 1 }, new List<int>() { 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 }, new List<bool>() { true, true, false, false }, 25000);
        }
        return zespol;
    }

    public void Save()
    {
        ZapiszBin("sfdbs");
    }

    public static DynamicData Load(bool firstLoad)
    {
        DynamicData returner;
        if (firstLoad)
        {
            returner = (DynamicData)OdczytajBin("sfdbs");
            firstLoad = false;
        }
        else
        {
            returner = new DynamicData(new List<int>() { 80, 1, 1, 1 }, new List<int>() { 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 }, new List<bool>() { true, true, false, false }, 25000);
        }
        return returner;
    }

}
