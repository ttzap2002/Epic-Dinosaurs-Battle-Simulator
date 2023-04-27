using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DinoStatsForShop : MonoBehaviour
{
    public int indexOfDino;
    public GameObject attack;
    public GameObject hp;
    public GameObject speed;
    public GameObject price;

    private TextMeshProUGUI attackTxt;
    private TextMeshProUGUI hpTxt;
    private TextMeshProUGUI speedTxt;
    private TextMeshProUGUI priceTxt;

    // Start is called before the first frame update
    void Start()
    {
        attackTxt = attack.GetComponent<TextMeshProUGUI>();
        hpTxt = hp.GetComponent<TextMeshProUGUI>();
        speedTxt = speed.GetComponent<TextMeshProUGUI>();
        priceTxt = price.GetComponent<TextMeshProUGUI>();
        if (GameManager.Instance.dynamicData.Dinosaurs[indexOfDino] != 0)
        {
            attackTxt.text = $"{ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].attack, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino] - 1)} (+{ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].attack, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino]) - ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].attack, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino] - 1)})";
            hpTxt.text = $"{ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].hp, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino] - 1)} (+{ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].hp, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino]) - ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].hp, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino] - 1)})";
            speedTxt.text = $"{ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].speed, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino]-1)} (+{ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].speed, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino])- ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].speed, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino] - 1)})";
            priceTxt.text = ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].price, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino]).ToString();
        }
        else
        {
            attackTxt.text = $"{ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].attack, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino])}";
            hpTxt.text = ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].hp, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino]).ToString();
            speedTxt.text = ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].speed, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino]).ToString();
            priceTxt.text = ReturnerValue(GameManager.Instance.dinosaurStats.Dinosaurs[indexOfDino].price, GameManager.Instance.dynamicData.Dinosaurs[indexOfDino]).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int ReturnerValue(int value, int power) => (int)(value * Math.Pow(1.1, power));
}
