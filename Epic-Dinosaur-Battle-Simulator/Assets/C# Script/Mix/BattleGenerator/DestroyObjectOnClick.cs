using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DestroyObjectOnClick : MonoBehaviour
{
    public GameObject objectToDestroy;

   
    private void OnMouseDown()
    {
        if(objectToDestroy.gameObject.tag == "Enemy") 
        {
            GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().
                enemyTroopsUpdate(false);
            GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().
                enemyMoneyUpdate(-objectToDestroy.GetComponent<CreatureStats>().cost);
            GameManager.Instance.enemyGameObjects.Remove(objectToDestroy);
        }
        else 
        {
            GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().
               blueTroopsUpdate(false);
            GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().
                       blueMoneyUpdate(-objectToDestroy.GetComponent<CreatureStats>().cost);
            GameManager.Instance.blueGameObjects.Remove(objectToDestroy);

        }

        Destroy(objectToDestroy);   
    }
}
