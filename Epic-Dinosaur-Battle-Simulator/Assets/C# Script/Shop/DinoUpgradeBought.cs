using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DinoUpgradeBought : MonoBehaviour
{
    public int indexOfDino;
    public GameObject price;
    private TextMeshProUGUI priceTxt;
    void Buy()
    {
        if (GameManager.Instance.dynamicData.Money >= ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].price, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino]))
        {
            priceTxt = price.GetComponent<TextMeshProUGUI>();
            GameManager.Instance.dynamicData.Money -= ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].price, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino]);
            priceTxt.text = ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].price, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino]).ToString();
        }
    }
    int ReturnerValue(int value, int power) => (int)(value * Math.Pow(1.1, power));
}
