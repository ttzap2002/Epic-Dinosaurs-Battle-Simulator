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
    [SerializeField] private TextMeshProUGUI attack, hp, speed, money;
    static public int idInText;

    private void Start()
    {
        if (GameManager.Instance.dynamicData.Dinosaurs[fighterId] > 0 && padlock != null)
        {
            padlock.SetActive(false);
        }
        TextBox.SetActive(false);
    }

    public void Click() 
    {
        if (!padlock.active)
        {
            GameManager.Instance.draggable.GetComponent<DraggableItem>().Fighterid = fighterId;

            //Zmiana textu po nacisnieciu
            if (TextBox != null)
            {
                if (attack != null)
                {
                    attack.text = $"{ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[fighterId].attack, GameManager.Instance.dynamicData.Dinosaurs[fighterId] - 1)}";
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
                    Debug.LogWarning("Nie znaleziono komponentu TextMeshPro attack.");
                }
                if (hp != null)
                {
                    hp.text = $"{ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[fighterId].hp, GameManager.Instance.dynamicData.Dinosaurs[fighterId] - 1)}";
                }
                else
                {
                    Debug.LogWarning("Nie znaleziono komponentu TextMeshPro hp.");
                }
                if (speed != null)
                {
                    speed.text = $"{GameManager.Instance.dinosaurStats.Dinosaurs[fighterId].speed}";
                }
                else
                {
                    Debug.LogWarning("Nie znaleziono komponentu TextMeshPro speed.");
                }
                if (money != null)
                {
                    money.text = $"{GameManager.Instance.dinosaurStats.Dinosaurs[fighterId].price}";
                }
                else
                {
                    Debug.LogWarning("Nie znaleziono komponentu TextMeshPro money.");
                }
            }
            else
            {
                Debug.LogWarning("Nie przypisano obiektu GameObject (TextBox) do skryptu.");
            }
        }
    }

    int ReturnerValue(int value, int power) => (int)(value * Math.Pow(1.1, power));

    public void HideTextBox()
    {
        TextBox.SetActive(false);
    }

}
