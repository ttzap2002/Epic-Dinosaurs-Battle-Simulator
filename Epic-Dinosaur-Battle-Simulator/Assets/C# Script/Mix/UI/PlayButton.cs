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
            
        }
        GameManager.Instance.SetBattleManager();
        Debug.Log("kotek");
        Debug.Log(GameManager.Instance.battleManager.BlueFighters.Count); 
        Debug.Log(GameManager.Instance.battleManager.EnemyFighters.Count);
        Debug.Log("piesek");
        Destroy(GameManager.Instance.draggableItem);
        Destroy(GameManager.Instance.Uiinformation);

      
    }
}
