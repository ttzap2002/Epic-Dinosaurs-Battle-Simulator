using System;
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
                SetObject(BattleManager.RandomIdOfDino(tag));
                timer = 0;
            }
            timer += Time.deltaTime;

        }
    }
    private void SetObject(int index)
    {

        if (GameManager.Instance.battleManager.poolingList[index].Count > 0) 
        {
            TakeObjectFromList(index);
        }
        else 
        {
            GameObject obj = Instantiate(GameManager.Instance.prefabGameObjects[index]);
            obj.tag = tag;
            FighterPlacement f = obj.gameObject.GetComponent<FighterPlacement>();
            obj.transform.position = new Vector3(transform.position.x, f.YAxis, transform.position.z);
            if (f.behaviourScript == ScriptType.MeleeFighter) { obj.GetComponent<MeleeFighter>().IsActiveForBattle = true; }
            else if (f.behaviourScript == ScriptType.Spawner) { obj.AddComponent<SpawnerBehaviour>(); }

            if (tag == "Blue")
            {
                GameManager.Instance.battleManager.BlueFighters[fighter.row, fighter.col].Add(f);
                GameManager.Instance.blueGameObjects.Add(obj);
                GameManager.Instance.blueGameObjects.Remove(gameObject);
                GameManager.Instance.battleManager.BlueFighters[fighter.row, fighter.col].Remove(fighter);
            }
            else
            {
                GameManager.Instance.battleManager.EnemyFighters[fighter.row, fighter.col].Add(f);
                GameManager.Instance.enemyGameObjects.Add(obj);
                GameManager.Instance.enemyGameObjects.Remove(gameObject);
                GameManager.Instance.battleManager.EnemyFighters[fighter.row, fighter.col].Remove(fighter);
            }
            obj.SetActive(true);

            f.CreateForSpawner();
            f.InformDelegate(true);

        }

        //Destroy(gameObject);
        fighter.isAlive = false;
        fighter.RefreshStats(this.gameObject);
        this.gameObject.SetActive(false);
       


    }


    private void TakeObjectFromList(int index)
    {
        List<GameObject> poolList = GameManager.Instance.battleManager.poolingList[index];
        GameObject obj = poolList[poolList.Count - 1];
        poolList.Remove(obj);
        obj.SetActive(true);
        obj.tag = tag;
        obj.transform.position = transform.position;
        FighterPlacement f = obj.GetComponent<FighterPlacement>();
        if (tag == "Blue") { GameManager.Instance.battleManager.BlueFighters[fighter.row, fighter.col].Add(f); GameManager.Instance.blueGameObjects.Add(obj); }
        else
        {
            GameManager.Instance.battleManager.EnemyFighters[fighter.row, fighter.col].Add(f); GameManager.Instance.enemyGameObjects.Add(obj);
        }
        f.InformDelegate(true);
    }

}