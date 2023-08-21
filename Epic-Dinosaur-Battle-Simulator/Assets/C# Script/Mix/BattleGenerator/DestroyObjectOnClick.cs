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

        if (!CheckCondition())
        {
            try
            {
                if (gameObject.tag == "Enemy")
                {
                    GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().
                        enemyTroopsUpdate(false);
                    GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().
                        enemyMoneyUpdate(-gameObject.GetComponent<FighterPlacement>().Price);
                    GameManager.Instance.enemyGameObjects.Remove(gameObject);
                }
                else
                {
                    GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().
                       blueTroopsUpdate(false);
                    GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().
                               blueMoneyUpdate(-gameObject.GetComponent<FighterPlacement>().Price);
                    GameManager.Instance.blueGameObjects.Remove(gameObject);

                }

                Destroy(gameObject);
            }
            catch 
            {
                //DoNothing
            }
        }
    }

    private bool CheckCondition()
    {
        return gameObject.tag == "Enemy"
                    && GameManager.Instance.currentScene.Id > 0;
    }
}
