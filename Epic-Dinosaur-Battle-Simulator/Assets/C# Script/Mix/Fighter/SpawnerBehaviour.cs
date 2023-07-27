using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    private FighterPlacement fighter = null;
    private bool isReadyForFight = false;

    int fighterspawn=0;
    private float timer = 0;
    int i = 0;

    public bool IsReadyForFight { get => isReadyForFight; set => isReadyForFight = value; }

    void Start()
    {
      
        fighter= gameObject.GetComponent<FighterPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsRun)
        {
            if (isReadyForFight)
            {
                if (timer >= 5)
                {
                    SetObject(BattleManager.RandomIdOfDino(tag));
                    timer = 0;
                }
                if (i % 400 == 0)
                {
                    fighter.Hp -= 10;
                }
                if (fighter.Hp <= 0)
                {
                    if (tag == "Blue")
                    {
                        GameManager.Instance.blueGameObjects.Remove(gameObject);
                    }
                    else
                    {
                        GameManager.Instance.enemyGameObjects.Remove(gameObject);
                    }

                    GameManager.Instance.battleManager.RemoveFromList(gameObject.GetComponent<FighterPlacement>(), fighter.row, fighter.col);
                    Destroy(gameObject);
                }
                timer += Time.deltaTime;
                i++;
            }
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
