using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FighterIdForButton :MonoBehaviour
{
    [SerializeField] private int fighterId;
    [SerializeField] private GameObject padlock;
    private string text;

    private void Start()
    {
        if(GameManager.Instance.dynamicData.Dinosaurs[fighterId]> 0 && padlock != null)
        {
            padlock.SetActive(false);
            text = $"attack: {ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[fighterId].attack, GameManager.Instance.dynamicData.Dinosaurs[fighterId] - 1)}\nHP: {ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[fighterId].hp, GameManager.Instance.dynamicData.Dinosaurs[fighterId] - 1)}\nspeed: {GameManager.Instance.dinosaurStats.Dinosaurs[fighterId].speed}\n{GameManager.Instance.dinosaurStats.Dinosaurs[fighterId].description}";
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

    int ReturnerValue(int value, int power) => (int)(value * Math.Pow(1.1, power));
}
