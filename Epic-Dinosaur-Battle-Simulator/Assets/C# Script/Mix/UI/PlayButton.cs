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
        //GameManager.Instance.currentScene.SetObjectToScene();
        
        foreach (GameObject f in (GameManager.Instance.blueGameObjects.Concat( GameManager.Instance.enemyGameObjects))) 
        {
            CreatureStats stats= f.GetComponent<CreatureStats>();
            if (stats.behaviourScript == ScriptType.MeleeFighter)
            {
                f.gameObject.AddComponent<MeleeFighter>();
            }
            if (stats.behaviourScript == ScriptType.Spawner)
            {
                f.gameObject.AddComponent<SpawnerBehaviour>();
            }            
        }
        
        GameManager.Instance.SetBattleManager();
        GameManager.Instance.IsRun=true;

        Destroy(GameObject.Find(("Scene Information")));
        Destroy(GameObject.Find(("Canvas")));
        Destroy(GameManager.Instance.UI);
        GameObject obj = GameObject.Find("Terrain");
        GameObject.Destroy(obj.gameObject.GetComponent<DraggableItem>());
    }
}
