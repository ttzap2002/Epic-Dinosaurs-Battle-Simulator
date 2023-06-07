using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FighterIdForButton :MonoBehaviour
{
    [SerializeField] private int fighterId;

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
