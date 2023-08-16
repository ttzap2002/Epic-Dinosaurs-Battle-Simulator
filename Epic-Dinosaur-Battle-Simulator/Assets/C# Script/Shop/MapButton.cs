using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MapButton : MonoBehaviour
{
    public int mapID = 99;
    public GameObject button;
    public GameObject price;
    public GameObject money;
    private int cost;
    private TextMeshProUGUI priceTxt;
    private TextMeshProUGUI moneyTxt;
    // Start is called before the first frame update
    void Start()
    {
        priceTxt = price.GetComponent<TextMeshProUGUI>();
        cost = int.Parse(priceTxt.text);
        Debug.Log(cost);
        /*if (!GameManager.Instance.dynamicData.Terrain[mapID])
            button.GetComponent<Renderer>().material.color = Color.red;
        else
            Destroy(button);*/
        if (GameManager.Instance.dynamicData.Terrain[mapID])
            Destroy(button);
    }

    public void Buy()
    {
        if(GameManager.Instance.dynamicData.Money >= cost)
        {
            GameManager.Instance.dynamicData.Money -= cost;
            GameManager.Instance.dynamicData.Terrain[mapID] = true;
            moneyTxt = money.GetComponent<TextMeshProUGUI>();
            moneyTxt.text = GameManager.Instance.dynamicData.Money.ToString();
            Destroy(button);
            GameManager.Instance.dynamicData.Save();
        }
    }

}
