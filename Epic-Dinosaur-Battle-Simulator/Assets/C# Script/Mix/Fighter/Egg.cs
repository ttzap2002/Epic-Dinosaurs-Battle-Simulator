﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Egg: MonoBehaviour
{
    private float timer;
    private FighterPlacement myStats = null;
    private FighterPlacement fighter = null;
    private bool isReadyForFight = false;
    static int[] Probability = new int[20];

    private int i = 0;
    private bool isFirstCall;
    public bool IsReadyForFight { get => isReadyForFight; set => isReadyForFight = value; }

    void Start()
    {
        myStats = gameObject.GetComponent<FighterPlacement>();
        fighter = gameObject.GetComponent<FighterPlacement>();
        isFirstCall=true;
    }

    private void Update()
    {
        if (GameManager.Instance.IsRun)
        {
            int sumOfObject=GameManager.Instance.enemyGameObjects.Count()
                + GameManager.Instance.blueGameObjects.Count();
            if(timer > 0.1f && isFirstCall && sumOfObject<100) 
            {
                if (tag == "Blue")
                {
                    GameManager.Instance.battleManager.React(false, fighter);
                }
                else if (tag == "Enemy")
                {
                    GameManager.Instance.battleManager.React(true, fighter);
                }
                isFirstCall = false;
            }
            if (timer >= 5)
            {
                SetObject();
            }
            timer += Time.deltaTime;

        }
    }
    private void SetObject()
    {
        GameObject obj = Instantiate(GameManager.Instance.prefabGameObjects[RandomIdOfDino()]);
        obj.tag = tag;
        FighterPlacement c = obj.GetComponent<FighterPlacement>();
        obj.transform.position = new Vector3(transform.position.x,c.YAxis,transform.position.z);
        if (c.behaviourScript == ScriptType.MeleeFighter) { obj.GetComponent<MeleeFighter>().IsActiveForBattle = true; }
        else if (c.behaviourScript == ScriptType.Spawner) { obj.AddComponent<SpawnerBehaviour>(); }
        FighterPlacement f = obj.gameObject.GetComponent<FighterPlacement>();
        if (tag == "Blue") {
            GameManager.Instance.battleManager.BlueFighters[fighter.row, fighter.col].Add(f); 
            GameManager.Instance.blueGameObjects.Add(obj);
            GameManager.Instance.blueGameObjects.Remove(gameObject);
            GameManager.Instance.battleManager.BlueFighters[fighter.row, fighter.col].Remove(fighter);
        }
        else {
            GameManager.Instance.battleManager.EnemyFighters[fighter.row, fighter.col].Add(f); 
            GameManager.Instance.enemyGameObjects.Add(obj);
            GameManager.Instance.enemyGameObjects.Remove(gameObject);
            GameManager.Instance.battleManager.EnemyFighters[fighter.row, fighter.col].Remove(fighter);
        }
        obj.SetActive(true);
        f.CreateForSpawner();
        Destroy(gameObject);
    }

    int RandomIdOfDino()
    {
        if (tag == "Enemy" && GameManager.Instance.currentScene.Id != 0)
        {
            return ReturnIdOfObjectForNonSandboxEnemy();
        }
        else
        {
            return ReturnIdOfObjectInOtherSituation();
        }
    }

    private int ReturnIdOfObjectInOtherSituation()
    {
        int SumOfProbabilities = Egg.Probability.Sum();
        if (SumOfProbabilities == 0)
        {
            int id = 0;
            foreach (int item in GameManager.Instance.dynamicData.Dinosaurs)
            {
                if (item == 0 && id != 18 && id != 19)
                    Egg.Probability[id] = 1;
                else if (item == 1 && id != 18 && id != 19)
                    Egg.Probability[id] = 10;
                else if (id != 18 && id != 19)
                    Egg.Probability[id] = 10 + ((int)((item - 1) / 3));
                else
                    Egg.Probability[id] = 0;
                id++;
            }
            SumOfProbabilities = Egg.Probability.Sum();
        }
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, SumOfProbabilities+1);
        int returner = 0;
        while (randomNumber - Egg.Probability[returner] > 0)
        {
            randomNumber -= Egg.Probability[returner];
            returner++;
        }
        return returner;
    }

    private int ReturnIdOfObjectForNonSandboxEnemy()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(1, 67); // Wygeneruj liczbę od 1 do 66 (67 jest wyłączone)
        if (randomNumber <= 33)
        {
            if (randomNumber <= 18)
            {
                if (randomNumber <= 7)
                    return 0;
                else if (randomNumber <= 10)
                    return 1;
                else if (randomNumber == 11)
                    return 2;
                else
                    return 3;
            }
            else
            {
                if (randomNumber <= 21)
                    return 4;
                else if (randomNumber <= 22)
                    return 5;
                else if (randomNumber <= 29)
                    return 6;
                else if (randomNumber <= 32)
                    return 7;
                else
                    return 8;
            }
        }
        else
        {
            if (randomNumber <= 51)
            {
                if (randomNumber <= 40)
                    return 9;
                else if (randomNumber <= 43)
                    return 10;
                else if (randomNumber == 44)
                    return 11;
                else
                    return 12;
            }
            else
            {
                if (randomNumber <= 54)
                    return 13;
                else if (randomNumber == 55)
                    return 14;
                else if (randomNumber <= 62)
                    return 15;
                else if (randomNumber <= 65)
                    return 16;
                else
                    return 17;
            }
        }
    }
}

