using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BattleInformation : MonoBehaviour
{
    public GameObject enemyMoney;
    public GameObject enemyTroops;
    public GameObject blueMoney;
    public GameObject blueTroops;

    int enemymoney=0;
    int bmoney=0;

    private TextMeshProUGUI enemyMoneyTxt;
    private TextMeshProUGUI enemyTroopsTxt;
    private TextMeshProUGUI blueMoneyTxt;
    private TextMeshProUGUI blueTroopsTxt;

    public void Start()
    {
        enemyMoneyTxt = enemyMoney.GetComponent<TextMeshProUGUI>();
        enemyTroopsTxt = enemyTroops.GetComponent<TextMeshProUGUI>();
        if (GameManager.Instance.enemyGameObjects != null && GameManager.Instance.enemyGameObjects != null) 
        {
            enemyTroopsTxt.text = $"{GameManager.Instance.enemyGameObjects.Count}/{GameManager.Instance.currentScene.EnemyTroopsLimit}";
            blueMoneyTxt = blueMoney.GetComponent<TextMeshProUGUI>();
            blueTroopsTxt = blueTroops.GetComponent<TextMeshProUGUI>();
            blueTroopsTxt.text = $"{GameManager.Instance.blueGameObjects.Count}/{GameManager.Instance.currentScene.Troopslimit}";

        }
        else 
        {
            enemyTroopsTxt.text = $"0/{GameManager.Instance.currentScene.EnemyTroopsLimit}";
            blueMoneyTxt = blueMoney.GetComponent<TextMeshProUGUI>();
            blueTroopsTxt = blueTroops.GetComponent<TextMeshProUGUI>();
            blueTroopsTxt.text = $"0/{GameManager.Instance.currentScene.Troopslimit}";
        }
        
        

    }

    public float[] GetMoney() 
    {
        float[] getMoney = new float[2];
        float.TryParse(blueMoneyTxt.text, out getMoney[0]);
        float.TryParse(enemyMoneyTxt.text, out getMoney[1]);
        return getMoney;
    }

    public void enemyMoneyUpdate(int money) 
    {

        enemyMoneyTxt.text = (enemymoney+=money).ToString();
    }

    public void enemyTroopsUpdate(bool isAdd)
    {
        //Debug.Log(enemyTroopsTxt.text);
        int i = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (isAdd)
        {
            enemyTroops.GetComponent<TextMeshProUGUI>().text = $"{i }/" +
             $"{GameManager.Instance.currentScene.Troopslimit}";
            
        }
        else 
        {
            enemyTroopsTxt.text = $"{i-1}/" +
            $"{GameManager.Instance.currentScene.Troopslimit}";
        }

    }

    public void blueMoneyUpdate(int money)
    {
        blueMoneyTxt.text = (bmoney += money).ToString();
    }

    public void blueTroopsUpdate(bool isAdd)
    {
        int a = GameObject.FindGameObjectsWithTag("Blue").Length;

        if (isAdd)
        {
            blueTroopsTxt.text = $"{a}/{GameManager.Instance.currentScene.Troopslimit}";
        }
        else
        {
            blueTroopsTxt.text = $"{a-1}/" +
            $"{GameManager.Instance.currentScene.Troopslimit}";
        }
    }
    public void RefreshMoney(int bluemoney, int enemymoney) 
    {
        blueTroopsTxt.text = $"{GameManager.Instance.blueGameObjects.Count}/{GameManager.Instance.currentScene.Troopslimit}";
        enemyTroopsTxt.text = $"{GameManager.Instance.enemyGameObjects.Count}/{GameManager.Instance.currentScene.EnemyTroopsLimit}";
        enemyMoneyTxt.text = $"{enemymoney}";
        blueMoneyTxt.text = $"{bluemoney}";
        this.enemymoney = enemymoney;
        bmoney = bluemoney;
    }


}
