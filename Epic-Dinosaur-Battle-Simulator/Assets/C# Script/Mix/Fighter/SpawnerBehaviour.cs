using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    private CreatureStats myStats = null;
    private FighterPlacement fighter = null;

    int fighterspawn=1;
    private int i = 0;
    void Start()
    {
        myStats= gameObject.GetComponent<CreatureStats>();
        fighter= gameObject.GetComponent<FighterPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (i % 200 == 0)
        {
            GameObject obj = Instantiate(GameManager.Instance.prefabGameObjects[fighterspawn]);

            obj.transform.position = transform.position;
            if (gameObject.tag == "Blue") { obj.tag = "Blue"; GameManager.Instance.battleManager.BlueFighters[fighter.row,fighter.col].Add(obj.GetComponent<FighterPlacement>()); }
            else { obj.tag = "Enemy";GameManager.Instance.battleManager.EnemyFighters[fighter.row, fighter.col].Add(obj.GetComponent<FighterPlacement>()); }
            CreatureStats c = obj.GetComponent<CreatureStats>();
            if (c.behaviourScript == ScriptType.MeleeFighter) { obj.AddComponent<MeleeFighter>(); }
            else if (c.behaviourScript == ScriptType.Spawner) { obj.AddComponent<SpawnerBehaviour>(); }
            obj.SetActive(true);
        }
        if(i% 100 == 0) 
        {
            myStats.hp -= 10;
        }
        if (myStats.hp <= 0) 
        {
            Destroy(gameObject);
            GameManager.Instance.battleManager.RemoveFromList(gameObject.GetComponent<FighterPlacement>(),fighter.row,fighter.col);
        }
        i++;
    }
}
