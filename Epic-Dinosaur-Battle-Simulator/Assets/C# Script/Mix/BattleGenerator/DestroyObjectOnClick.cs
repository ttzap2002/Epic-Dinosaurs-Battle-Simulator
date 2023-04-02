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
            GameManager.Instance.Uiinformation.GetComponent<BattleInformation>().
                enemyTroopsUpdate(false);
            GameManager.Instance.Uiinformation.GetComponent<BattleInformation>().
                enemyMoneyUpdate(-objectToDestroy.GetComponent<CreatureStats>().cost);
            GameManager.Instance.enemyGameObjects.Remove(objectToDestroy);

        }
        else 
        {
            GameManager.Instance.Uiinformation.GetComponent<BattleInformation>().
               blueTroopsUpdate(false);
            GameManager.Instance.Uiinformation.GetComponent<BattleInformation>().
                       blueMoneyUpdate(-objectToDestroy.GetComponent<CreatureStats>().cost);
            GameManager.Instance.blueGameObjects.Remove(objectToDestroy);



        }

        Destroy(objectToDestroy);   
    }
}
