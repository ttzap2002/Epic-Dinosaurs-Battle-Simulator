using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    Image img;
    private void Start()
    {
        img=GetComponent<Image>();  
    }
    public void ChangeProgressBar()
    {
        float[] value = GameManager.Instance.battleManager.MoneySum;
        img.fillAmount = value[0] / (value[0] + value[1]);
    }
}
