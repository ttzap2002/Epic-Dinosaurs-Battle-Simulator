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

        Debug.Log("beginDrag");
        clone = Instantiate(gameObject);
        clone.transform.SetParent(canvas.transform,false);
        if (clone.GetComponent<DraggableItem>() == null)
        {
            clone.AddComponent<DraggableItem>(); 
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("drag");
        transform.position = Input.mousePosition;
      
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End");

        GameObject obj = Instantiate(GameManager.Instance.gameObjects[fighterid]);
        obj.SetActive(true);
        int cost = obj.GetComponent<CreatureStats>().cost;
   
        Destroy(obj.gameObject.GetComponent<MeleeFighter>());
        obj.transform.position = GameManager.Instance.mouse.transform.position;

        GameManager.Instance.gameObjects.Add(obj);
        if (obj.transform.position.z > 50f) 
        { obj.tag = "Blue";
           GameManager.Instance.Uiinformation.GetComponent<BattleInformation>().blueTroopsUpdate(true);
           GameManager.Instance.Uiinformation.GetComponent<BattleInformation>().blueMoneyUpdate(cost);

        }
        else { obj.tag = "Enemy";
           GameManager.Instance.Uiinformation.GetComponent<BattleInformation>().enemyTroopsUpdate(true);
           GameManager.Instance.Uiinformation.GetComponent<BattleInformation>().enemyMoneyUpdate(cost);
        }

        Destroy(gameObject);
    }
}
