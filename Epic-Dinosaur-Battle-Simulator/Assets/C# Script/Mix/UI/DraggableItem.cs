using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour //IDragHandler, IEndDragHandler,IBeginDragHandler
{
    public int fighterid;
    private bool isDragging = false;


    private void Update()
    {
        
        if (isDragging)
        {
            if (isPossible())
            {
                SetInstanceOfObject();
            }
        }
    }

    public void OnMouseDown()
    {
        isDragging = true;
    }

    public void OnMouseUp()
    {
        isDragging = false;
    }


    private bool isPossible() 
    {
        Vector3 vector = GameManager.Instance.mouse.transform.position;
        if (EventSystem.current.IsPointerOverGameObject()) 
        {
            return false;
        }
        foreach (GameObject g in GameManager.Instance.blueGameObjects.Concat(GameManager.Instance.enemyGameObjects)) 
        {
            if (Vector3.Distance(vector, g.transform.position) < 4) 
            {
                return false;
            }
        }
        return true;
    }

    /*
public GameObject border;
private GameObject clone;
public Camera mainCamera;
public GameObject canvas;
public GameObject Uiinformation;
*/



    public void OnBeginDrag(PointerEventData eventData)
    {
        /*
        clone = Instantiate(gameObject);
        clone.transform.SetParent(canvas.transform,false);
        if (clone.GetComponent<DraggableItem>() == null)
        {
            clone.AddComponent<DraggableItem>(); 
        }*/
    }
    /*
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        SetInstanceOfObject();
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        SetInstanceOfObject();
        //Destroy(gameObject);
    }


    */

    private void SetInstanceOfObject() 
    {
        GameObject obj = Instantiate(GameManager.Instance.prefabGameObjects[fighterid]);
        CreatureStats creature = obj.GetComponent<CreatureStats>();
        creature.UpgradeStatLevel(GameManager.Instance.dynamicData.Dinosaurs[fighterid]-1);
        obj.SetActive(true);
        int cost = creature.cost;

        Destroy(obj.gameObject.GetComponent<MeleeFighter>());
        obj.transform.position = new Vector3(GameManager.Instance.mouse.transform.position.x,
            creature.YAxis, GameManager.Instance.mouse.transform.position.z); 
      
        if (obj.transform.position.z > 50f)
        {
            obj.tag = "Blue";
          
            if (GameManager.Instance.blueGameObjects.Count >= GameManager.Instance.currentScene.Troopslimit) { Destroy(obj); }
            else {
                GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().blueTroopsUpdate(true);
                GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().blueMoneyUpdate(cost);
                GameManager.Instance.blueGameObjects.Add(obj); 
            }
     
        }
        else
        {
            obj.tag = "Enemy";
            
            if (GameManager.Instance.enemyGameObjects.Count >= GameManager.Instance.currentScene.EnemyTroopsLimit) { Destroy(obj); }
            else {
                GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().enemyTroopsUpdate(true);
                GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().enemyMoneyUpdate(cost);
                GameManager.Instance.enemyGameObjects.Add(obj);
            }
        }
        
    }


}
