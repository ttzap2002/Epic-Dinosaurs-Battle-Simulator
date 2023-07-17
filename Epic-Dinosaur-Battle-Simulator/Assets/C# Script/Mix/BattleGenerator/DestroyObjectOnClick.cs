using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestroyObjectOnClick : MonoBehaviour
{



    private void Start()
    {
        Debug.Log(gameObject.transform.position);
    }
    private void OnMouseDown()
    {
        if(gameObject.tag == "Enemy") 
        {
            GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().
                enemyTroopsUpdate(false);
            GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().
                enemyMoneyUpdate(-gameObject.GetComponent<FighterPlacement>().stats.price);
            GameManager.Instance.enemyGameObjects.Remove(gameObject);
        }
        else 
        {
            GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().
               blueTroopsUpdate(false);
            GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().
                       blueMoneyUpdate(-gameObject.GetComponent<FighterPlacement>().stats.price);
            GameManager.Instance.blueGameObjects.Remove(gameObject);

        }

        Destroy(gameObject);   
    }
}
