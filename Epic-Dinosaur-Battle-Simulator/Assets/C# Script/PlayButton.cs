using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{


    public void LetsPLay()
    {

        //GameManager.Instance.SetAllObjectActive();
        

        foreach (GameObject f in GameManager.Instance.gameObjects) 
        {
            f.gameObject.AddComponent<MeleeFighter>();
            
        }
     
        Debug.Log("Pies");
      
    }
}
