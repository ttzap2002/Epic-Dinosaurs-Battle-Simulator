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
    static List<int> StartLvls = new List<int>() { 2, 1, 1, 1 }; //Poziomy ktore sa odblokowane na start (stworzone wylacznie w celu zmiany jednej listy zamiast kilku)
    public bool wantMusic = true;
    public float musicIntense = 0.15f;
    public List<bool> isShowTutorialOnScene;

protected DynamicData()
    {
        battlesWithoutAds = 0;
        WantMusic = true;
        isShowTutorialOnScene = new List<bool> { false, false, false, false };
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
    public bool WantMusic { get => wantMusic; set => wantMusic = value; }

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
        object zespol;// = new DynamicData(DynamicData.StartLvls, new List<int>() { 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 }, new List<bool>() { true, false, true, false }, 25000);
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
                zespol = false;
            }
            catch (InvalidCastException f)
            {
                zespol = false;
            }
            finally
            {
                fs.Close();
            }
        }
        catch (FileNotFoundException ex)
        {
            return false;
        }
        return zespol;
    }

    public virtual void Save()
    {
        ZapiszBin("sfdbs");
    }

    public static DynamicData Load()
    {
        object returner;
        returner = OdczytajBin("sfdbs");
        if (returner.GetType() != typeof(DynamicData))
        {
            returner = new DynamicData(DynamicData.StartLvls, new List<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, new List<bool>() { true, false, true, false }, 25000);
        }
        return (DynamicData)returner;
    }

    //Lista dinozaurow
    /*
     * 0 - Allosaur
     * 1 - Diplodocus
     * 2 - Stegoceras
     * 3 - Triceratops
     * 4 - T-rex
     */
}


[Serializable]
public class DynamicData2 : DynamicData
{
    static List<int> StartLvls = new List<int>() { 2, 1, 1, 1 }; //Poziomy ktore sa odblokowane na start (stworzone wylacznie w celu zmiany jednej listy zamiast kilku)
    private bool itWorks;
    private bool wantSounds;
    private float soundsIntense;
    public bool ItWorks { get => itWorks; set => itWorks = value; }
    public bool WantSounds { get => wantSounds; set => wantSounds = value; }
    public float SoundsIntense { get => soundsIntense; set => soundsIntense = value; }

    protected DynamicData2()
    {
        battlesWithoutAds = 0;
        WantMusic = true;
        WantSounds = true;
        SoundsIntense = 0.15f;
    }

    public DynamicData2(List<int> unlockLvls, List<int> dinosaurs, List<bool> terrain, int money, bool itWorks) : this()
    {
        UnlockLvls = unlockLvls;
        Dinosaurs = dinosaurs;
        Terrain = terrain;
        Money = money;
        ItWorks = itWorks;
    }

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
        object zespol;// = new DynamicData2(DynamicData2.StartLvls, new List<int>() { 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 }, new List<bool>() { true, false, true, false }, 25000, true);
        try
        {
            FileStream fs = new FileStream($"{nazwa}.bin", FileMode.Open); //zrobiæ na try, wrazie jakby pierwszy raz uruchamiano grê
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                zespol = (DynamicData2)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Debug.Log("Loading failed");
                zespol = false;
            }
            catch (InvalidCastException f)
            {
                zespol = false;
            }
            finally
            {
                fs.Close();
            }
        }
        catch (FileNotFoundException ex)
        {
            return false;
        }
        return zespol;
    }

    public override void Save()
    {
        ZapiszBin("sfdbs");
    }

    public static DynamicData2 Load()
    {
        object returner;
        returner = OdczytajBin("sfdbs");
        if (returner.GetType() != typeof(DynamicData2))
        {
            DynamicData returner2 = DynamicData.Load();
            DynamicData2 cosiek = new DynamicData2(returner2.UnlockLvls, returner2.Dinosaurs, returner2.Terrain, returner2.Money, true);
            cosiek.battlesWithoutAds = returner2.battlesWithoutAds;
            cosiek.wantMusic = returner2.wantMusic;
            cosiek.musicIntense = returner2.musicIntense;
            cosiek.isShowTutorialOnScene = returner2.isShowTutorialOnScene;
            return cosiek;
        }
        return (DynamicData2)returner;
    }
}