using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int fighterid;
    public GameObject border;
    private GameObject clone;
    public Camera mainCamera;
    public GameObject canvas;
    public GameObject Uiinformation;

    public void OnBeginDrag(PointerEventData eventData)
    {


        clone = Instantiate(gameObject);
        clone.transform.SetParent(canvas.transform,false);
        if (clone.GetComponent<DraggableItem>() == null)
        {
            clone.AddComponent<DraggableItem>(); 
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    
        SetInstanceOfObject();

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        SetInstanceOfObject();
        //Destroy(gameObject);
    }

    private void SetInstanceOfObject() 
    {
        GameObject obj = Instantiate(GameManager.Instance.prefabGameObjects[fighterid]);
        obj.SetActive(true);
        int cost = obj.GetComponent<CreatureStats>().cost;
        Destroy(obj.gameObject.GetComponent<MeleeFighter>());
        obj.transform.position = GameManager.Instance.mouse.transform.position;
      
        if (obj.transform.position.z > 50f)
        {
            obj.tag = "Blue";
          
            if (GameManager.Instance.blueGameObjects.Count >= GameManager.Instance.currentScene.Troopslimit) { Destroy(obj); }
            else {
                GameManager.Instance.Uiinformation.GetComponent<BattleInformation>().blueTroopsUpdate(true);
                GameManager.Instance.Uiinformation.GetComponent<BattleInformation>().blueMoneyUpdate(cost);
                GameManager.Instance.blueGameObjects.Add(obj); }
     
        }
        else
        {
            obj.tag = "Enemy";
            
            if (GameManager.Instance.enemyGameObjects.Count >= GameManager.Instance.currentScene.EnemyTroopsLimit) { Destroy(obj); }
            else {
                GameManager.Instance.Uiinformation.GetComponent<BattleInformation>().enemyTroopsUpdate(true);
                GameManager.Instance.Uiinformation.GetComponent<BattleInformation>().enemyMoneyUpdate(cost);
                GameManager.Instance.enemyGameObjects.Add(obj);
            }
        }
        
    }


}
