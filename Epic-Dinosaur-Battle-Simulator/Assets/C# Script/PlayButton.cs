using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{

    private MeleeFighter[] fighter;
    public void LetsPLay()
    {

        //GameManager.Instance.SetAllObjectActive();
        fighter = GameManager.Instance.fighters;
        foreach(MeleeFighter f in fighter) 
        {
            f.gameObject.SetActive(true);
        }
        Debug.Log("Pies");
      
    }
}
