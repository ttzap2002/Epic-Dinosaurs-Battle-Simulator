using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    private CreatureStats myStats = null;
    private FighterPlacement fighter = null;
    private bool isReadyForFight = false;

    int fighterspawn=0;
    private float timer = 0;
    int i = 0;

    public bool IsReadyForFight { get => isReadyForFight; set => isReadyForFight = value; }

    void Start()
    {
        myStats= gameObject.GetComponent<CreatureStats>();
        fighter= gameObject.GetComponent<FighterPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReadyForFight)
        {
            if (timer >= 5)
            {
                GameObject obj = Instantiate(GameManager.Instance.prefabGameObjects[fighterspawn]);
              
                obj.transform.position = transform.position;

                CreatureStats c = obj.GetComponent<CreatureStats>();
           
                if (c.behaviourScript == ScriptType.MeleeFighter) { obj.GetComponent<MeleeFighter>().IsActiveForBattle = true; }
                else if (c.behaviourScript == ScriptType.Spawner) { obj.AddComponent<SpawnerBehaviour>(); }
                FighterPlacement f = obj.gameObject.GetComponent<FighterPlacement>();
                f.CreateForSpawner();
                obj.SetActive(true);
                timer = 0;
                if (gameObject.tag == "Blue") { obj.tag = "Blue"; GameManager.Instance.battleManager.BlueFighters[fighter.row, fighter.col].Add(f); GameManager.Instance.blueGameObjects.Add(obj);
                    GameManager.Instance.battleManager.React(false, obj.gameObject);

                }
                else { obj.tag = "Enemy"; GameManager.Instance.battleManager.EnemyFighters[fighter.row, fighter.col].Add(f); GameManager.Instance.enemyGameObjects.Add(obj);
                    GameManager.Instance.battleManager.React(true, obj.gameObject);

                }
            }
            if (i%100 == 0)
            {
                myStats.hp -= 10;
            }
            if (myStats.hp <= 0)
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
