using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FighterIdForButton :MonoBehaviour
{
    [SerializeField] private int fighterId;
    [SerializeField] private GameObject padlock;

    private void Start()
    {
        if(GameManager.Instance.dynamicData.Dinosaurs[fighterId]> 0 && padlock != null)
        {
            padlock.SetActive(false);
        }
    }

    public void Click() 
    {
        /*
        GameObject draggableItemGO = new GameObject("DraggableItem"); // tworzenie nowego obiektu DraggableItem
        draggableItemGO.AddComponent<Collider>();
        DraggableItem draggableItem = draggableItemGO.AddComponent<DraggableItem>(); // dodanie komponentu DraggableItem
        draggableItem.fighterid = fighterId; // przypisanie fighterid
        GameManager.Instance.itemForDraggable = draggableItem;*/
        GameManager.Instance.draggable.GetComponent<DraggableItem>().Fighterid= fighterId;
    }
}
