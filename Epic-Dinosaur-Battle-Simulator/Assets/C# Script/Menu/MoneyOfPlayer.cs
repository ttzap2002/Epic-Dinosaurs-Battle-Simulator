using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyOfPlayer : MonoBehaviour
{
    public GameObject textBox; // Zmieniane pole z tekstem na wartoœæ posiadanej gotówki
    private TextMeshProUGUI moneyTxt;
    // Start is called before the first frame update
    void Start()
    {
        moneyTxt = textBox.GetComponent<TextMeshProUGUI>();
        moneyTxt.text = GameManager.Instance.dynamicData.Money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        /*moneyTxt = textBox.GetComponent<TextMeshProUGUI>();
        moneyTxt.text = GameManager.Instance.money.ToString();*/
    }
}
