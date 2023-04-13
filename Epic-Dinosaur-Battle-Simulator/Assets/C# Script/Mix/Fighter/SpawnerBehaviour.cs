using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    private CreatureStats myStats = null;

    int fighterspawn=1;
    private int i = 0;
    void Start()
    {
        myStats= gameObject.GetComponent<CreatureStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (i % 200 == 0)
        {
            GameObject obj = Instantiate(GameManager.Instance.prefabGameObjects[fighterspawn]);

            obj.transform.position = transform.position;
            if (gameObject.tag == "Blue") { obj.tag = "Blue"; GameManager.Instance.battleManager.BlueFighters.Add(obj.GetComponent<FighterPlacement>()); }
            else { obj.tag = "Enemy";GameManager.Instance.battleManager.EnemyFighters.Add(obj.GetComponent<FighterPlacement>()); }
            obj.SetActive(true);
        }
        if(i% 100 == 0) 
        {
            myStats.hp -= 10;
        }
        if (myStats.hp <= 0) 
        {
            Destroy(gameObject);
            GameManager.Instance.battleManager.RemoveFromList(gameObject.GetComponent<FighterPlacement>());
        }
        i++;
    }
}
