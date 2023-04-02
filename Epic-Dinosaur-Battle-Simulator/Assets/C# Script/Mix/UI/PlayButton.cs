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


        GameManager.Instance.currentScene.SetObjectToScene();
        
        foreach (GameObject f in (GameManager.Instance.blueGameObjects.Concat( GameManager.Instance.enemyGameObjects))) 
        {
            f.gameObject.AddComponent<MeleeFighter>();
            
        }
     
        Debug.Log("Pies");
      
    }
}
