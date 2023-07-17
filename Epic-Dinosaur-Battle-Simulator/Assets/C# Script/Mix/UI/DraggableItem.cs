using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour //IDragHandler, IEndDragHandler,IBeginDragHandler
{
    [SerializeField]private int fighterid=0;
    private bool isDragging = false;

    public int Fighterid { get => fighterid; set => fighterid = value; }

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
        if(GameManager.Instance.blueGameObjects==null || GameManager.Instance.enemyGameObjects == null) 
        {
            return true;
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
        GameObject obj = Instantiate(GameManager.Instance.prefabGameObjects[Fighterid]);
        FighterPlacement creature = obj.GetComponent<FighterPlacement>();
        creature.CreateForSpawner();
        creature.UpgradeStatLevel(GameManager.Instance.dynamicData.Dinosaurs[Fighterid]-1);
        obj.SetActive(true);
        int cost = creature.stats.price;

        obj.transform.position = new Vector3(GameManager.Instance.mouse.transform.position.x,
            creature.YAxis, GameManager.Instance.mouse.transform.position.z); 
      
        if (obj.transform.position.z > 50f)
        {
            SetObject("Blue", obj, cost,creature);
        }
        else
        {
            SetObject("Enemy", obj, cost,creature);
        }
    }
    private void SetObject(string type,GameObject obj,int cost,FighterPlacement creature) 
    {
        obj.tag= type;

        if (type == "Blue")
        {
            if (GameManager.Instance.blueGameObjects.Count >= GameManager.Instance.currentScene.Troopslimit) { Destroy(obj); }
            else
            {

                GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().blueTroopsUpdate(true);
                GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().blueMoneyUpdate(cost);
                GameManager.Instance.blueGameObjects.Add(obj);
                if (creature.IsObligatoryToRotate)
                {
                    obj.transform.rotation = new Quaternion(obj.transform.rotation.x, 180, obj.transform.rotation.z, obj.transform.rotation.w);

                }
            }
        }
        else 
        {
            if (GameManager.Instance.enemyGameObjects.Count >= GameManager.Instance.currentScene.EnemyTroopsLimit) { Destroy(obj); }
            else
            {
                GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().enemyTroopsUpdate(true);
                GameManager.Instance.UI.GetComponentInChildren<BattleInformation>().enemyMoneyUpdate(cost);
                GameManager.Instance.enemyGameObjects.Add(obj);
                if (!creature.IsObligatoryToRotate) 
                {
                    obj.transform.rotation = new Quaternion(obj.transform.rotation.x, 180, obj.transform.rotation.z, obj.transform.rotation.w);

                }
            }
        }

    }


}
