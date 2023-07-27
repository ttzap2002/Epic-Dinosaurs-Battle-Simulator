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
            GameObject objScene = GameObject.Find(("Scene Information"));
            BattleInformation uiInfo = objScene.GetComponent<BattleInformation>();
            GameManager.Instance.battleManager.MoneySum=uiInfo.GetMoney();
            Destroy(objScene);
            Destroy(GameObject.Find(("Canvas")));
            (GameObject.Find("Buttons")).SetActive(false);
            Destroy(objScene);
            GameObject progressbar = GameObject.Find(("RedArmy"));
            progressbar.GetComponent<Image>().enabled = true;
            GameObject.Find(("ProgressBar")).GetComponent<Image>().enabled = true;
            GameManager.Instance.battleManager.SetDelegate(GameObject.Find(("RedArmy")));
            GameManager.Instance.battleManager.Delegate();
            GameObject obj = GameObject.Find("Terrain");
            GameObject.Destroy(obj.gameObject.GetComponent<DraggableItem>());
            GameManager.Instance.dynamicData.battlesWithoutAds++;
            GameManager.Instance.IsRun = true;
            GameManager.Instance.battleManager.SetCourutine();

        }
    }

    private void CreateBattleManagerForEnemy()
    {
        foreach (GameObject f in (GameManager.Instance.enemyGameObjects))
        {
            FighterPlacement stats = f.GetComponent<FighterPlacement>();
            FighterPlacement fPlacement = f.GetComponent<FighterPlacement>();

            if (stats.behaviourScript == ScriptType.MeleeFighter)
            {
                f.GetComponent<MeleeFighter>().IsActiveForBattle =true;

            }
            if (stats.behaviourScript == ScriptType.Spawner)
            {
                f.gameObject.GetComponent<SpawnerBehaviour>().IsReadyForFight=true;
                /*
                GameObject obj = Instantiate(GameManager.Instance.prefabGameObjects[19]);
                FighterPlacement eggFighter = obj.GetComponent<FighterPlacement>();
                obj.SetActive(false);
                eggFighter.isAlive = false;
                GameManager.Instance.battleManager.poolingList[19].Add(obj);*/
            }
            if (stats.behaviourScript == ScriptType.EggLayer)
            {
                f.gameObject.GetComponent<LayEggsFighter>().IsActiveForBattle = true;
                AddEggToPool();

            }

            GameManager.Instance.battleManager.EnemyFighters[fPlacement.row, fPlacement.col].Add(fPlacement);
            if (stats.index == 14) { GameManager.Instance.battleManager.KosmoceraptorsCount[1]++; }
        }
    }

    private static void AddEggToPool()
    {
        GameObject obj = Instantiate(GameManager.Instance.prefabGameObjects[19]);
        FighterPlacement eggFighter = obj.GetComponent<FighterPlacement>();
        obj.SetActive(false);
        eggFighter.isAlive = false;
        GameManager.Instance.battleManager.poolingList[19].Add(obj);
    }

    private void CreateBattleManagerForBlue()
    {
        foreach (GameObject f in (GameManager.Instance.blueGameObjects))
        {
            FighterPlacement stats = f.GetComponent<FighterPlacement>();
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
                //Tworze isntancje jajka i dodaje do zbiornika
                AddEggToPool();
            }
            GameManager.Instance.battleManager.BlueFighters[fPlacement.row, fPlacement.col].Add(fPlacement);
            if (stats.index == 14) { GameManager.Instance.battleManager.KosmoceraptorsCount[0]++; }

        }
    }
}
