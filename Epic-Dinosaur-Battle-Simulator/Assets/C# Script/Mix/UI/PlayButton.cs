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
            f.gameObject.AddComponent<MeleeFighter>();
            Debug.Log(f);
            
        }
        
        GameManager.Instance.SetBattleManager();
        GameManager.Instance.IsRun=true;
        Debug.Log("kotek");
        Debug.Log(GameManager.Instance.battleManager.BlueFighters.Count); 
        Debug.Log(GameManager.Instance.battleManager.EnemyFighters.Count);
        Debug.Log("piesek");
        Destroy(GameObject.Find(("Scene Information")));
        Destroy(GameObject.Find(("Canvas")));
        Destroy(GameManager.Instance.draggableItem);
        Destroy(GameManager.Instance.Uiinformation);
    }
}
