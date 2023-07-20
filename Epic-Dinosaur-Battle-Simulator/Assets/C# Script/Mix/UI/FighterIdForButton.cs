using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class FighterIdForButton :MonoBehaviour
{
    [SerializeField] private int fighterId;
    [SerializeField] private GameObject padlock;
    [SerializeField] private GameObject TextBox;
    static public int idInText;
    private string text;

    private void Start()
    {
        if (GameManager.Instance.dynamicData.Dinosaurs[fighterId] > 0 && padlock != null)
        {
            padlock.SetActive(false);
        }
        else
        {
            //gameObject.SetActive(false);
        }
        text = $"attack: {ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[fighterId].attack, GameManager.Instance.dynamicData.Dinosaurs[fighterId] - 1)}\nHP: {ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[fighterId].hp, GameManager.Instance.dynamicData.Dinosaurs[fighterId] - 1)}\nspeed: {GameManager.Instance.dinosaurStats.Dinosaurs[fighterId].speed}\n{GameManager.Instance.dinosaurStats.Dinosaurs[fighterId].description}";
        TextBox.SetActive(false);
    }

    public void Click() 
    {
        if (!padlock.active)
        {
            /*
            GameObject draggableItemGO = new GameObject("DraggableItem"); // tworzenie nowego obiektu DraggableItem
            draggableItemGO.AddComponent<Collider>();
            DraggableItem draggableItem = draggableItemGO.AddComponent<DraggableItem>(); // dodanie komponentu DraggableItem
            draggableItem.fighterid = fighterId; // przypisanie fighterid
            GameManager.Instance.itemForDraggable = draggableItem;*/
            GameManager.Instance.draggable.GetComponent<DraggableItem>().Fighterid = fighterId;

            //Zmiana textu po nacisnieciu
            if (TextBox != null)
            {
                TextMeshProUGUI tmp = TextBox.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null)
                {
                    tmp.text = text;
                    if ((!TextBox.active) || (FighterIdForButton.idInText != fighterId))
                    {
                        TextBox.SetActive(true);
                        FighterIdForButton.idInText = fighterId;
                    }
                    else
                        TextBox.SetActive(false);
                }
                else
                {
                    Debug.LogWarning("Nie znaleziono komponentu TextMeshPro w dziecku obiektu.");
                }
            }
            else
            {
                Debug.LogWarning("Nie przypisano obiektu GameObject (TextBox) do skryptu.");
            }
        }
    }

    int ReturnerValue(int value, int power) => (int)(value * Math.Pow(1.1, power));
}
