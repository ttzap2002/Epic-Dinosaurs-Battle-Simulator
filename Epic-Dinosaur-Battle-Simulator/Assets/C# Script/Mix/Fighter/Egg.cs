using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Egg: MonoBehaviour
{
    private float timer;
    private CreatureStats myStats = null;
    private FighterPlacement fighter = null;
    private bool isReadyForFight = false;

    private int i = 0;

    public bool IsReadyForFight { get => isReadyForFight; set => isReadyForFight = value; }

    void Start()
    {
        myStats = gameObject.GetComponent<CreatureStats>();
        fighter = gameObject.GetComponent<FighterPlacement>();
    }

    private void Update()
    {
        if(GameManager.Instance.IsRun) 
        {
            if (timer >= 5)
            {
                SetObject();
            }
        }
      
        timer += Time.deltaTime;
    }

    private void SetObject()
    {
        GameObject obj = Instantiate(GameManager.Instance.prefabGameObjects[0]);
        obj.tag = tag;
        
        CreatureStats c = obj.GetComponent<CreatureStats>();
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
}

