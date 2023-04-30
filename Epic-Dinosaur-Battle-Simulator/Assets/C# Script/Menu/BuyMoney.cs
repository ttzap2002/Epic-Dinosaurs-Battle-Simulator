using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyMoney : MonoBehaviour
{
    public GameObject money;
    public GameObject gettingMoney;
    private TextMeshProUGUI moneyTxt;
    private TextMeshProUGUI gettingmoneyTxt;
    // Start is called before the first frame update
    public void Buy()
    {
        if (SuccesfullTransaction())
        {
            gettingmoneyTxt = gettingMoney.GetComponent<TextMeshProUGUI>();
            GameManager.Instance.dynamicData.Money += int.Parse(gettingmoneyTxt.text);
            moneyTxt = money.GetComponent<TextMeshProUGUI>();
            moneyTxt.text = GameManager.Instance.dynamicData.Money.ToString();
        }
    }

    /// <summary>
    /// Not implemented. There have to be paymant
    /// </summary>
    /// <returns></returns>
    bool SuccesfullTransaction()
    {
        return true;
    }
}
