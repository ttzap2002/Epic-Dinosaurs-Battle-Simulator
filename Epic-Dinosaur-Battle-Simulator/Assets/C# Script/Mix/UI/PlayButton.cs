using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayButton : MonoBehaviour
{


    public void LetsPLay()
    {
        if (GameManager.Instance.blueGameObjects.Count > 0 && GameManager.Instance.enemyGameObjects.Count > 0)
        {
            CreateBattleManagerForBlue();
            CreateBattleManagerForEnemy();

            GameManager.Instance.IsRun = true;
            Destroy(GameObject.Find(("Scene Information")));
            Destroy(GameObject.Find(("Canvas")));
            Destroy(GameManager.Instance.UI);

            GameObject obj = GameObject.Find("Terrain");
            GameObject.Destroy(obj.gameObject.GetComponent<DraggableItem>());
        }
    }

    private void CreateBattleManagerForEnemy()
    {
        foreach (GameObject f in (GameManager.Instance.enemyGameObjects))
        {
            CreatureStats stats = f.GetComponent<CreatureStats>();
            FighterPlacement fPlacement = f.GetComponent<FighterPlacement>();

            if (stats.behaviourScript == ScriptType.MeleeFighter)
            {
                f.GetComponent<MeleeFighter>().IsActiveForBattle =true;

            }
            if (stats.behaviourScript == ScriptType.Spawner)
            {
                f.gameObject.GetComponent<SpawnerBehaviour>().IsReadyForFight=true;
            }
            if (stats.behaviourScript == ScriptType.EggLayer)
            {
                f.gameObject.GetComponent<LayEggsFighter>().IsActiveForBattle = true;
            }

            GameManager.Instance.battleManager.EnemyFighters[fPlacement.row, fPlacement.col].Add(fPlacement);
            
        }
    }

    private void CreateBattleManagerForBlue()
    {
        foreach (GameObject f in (GameManager.Instance.blueGameObjects))
        {
            CreatureStats stats = f.GetComponent<CreatureStats>();
            FighterPlacement fPlacement = f.GetComponent<FighterPlacement>();

            if (stats.behaviourScript == ScriptType.MeleeFighter)
            {
                f.GetComponent<MeleeFighter>().IsActiveForBattle = true;

            }
            if (stats.behaviourScript == ScriptType.Spawner)
            {
                f.gameObject.GetComponent<SpawnerBehaviour>().IsReadyForFight = true;
            }
            if (stats.behaviourScript == ScriptType.EggLayer)
            {
                f.gameObject.GetComponent<LayEggsFighter>().IsActiveForBattle = true;
            }
            GameManager.Instance.battleManager.BlueFighters[fPlacement.row, fPlacement.col].Add(fPlacement);
        }
    }
}
