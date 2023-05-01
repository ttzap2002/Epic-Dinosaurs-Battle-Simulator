using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class ArrowsForScroll : MonoBehaviour
{
    public GameObject scroll;
    public int continent;
    private float distance;

    private void Start()
    {
        distance = (float)(Screen.width * 0.09); // z obu boków ucinamy po 5% czyli zostaje nam do dyspozycji 90% ekranu. Do tego mamy wyœwietlane ju¿ 50% przycisków, wiêc poruszamy siê w spektrum tylko (1/2 * 20 =) 10 przycisków. z tego wychodzi 0.9 / 10 = 0.09
        Vector3 newPosition = scroll.transform.position;
        newPosition.x -= distance * (int)((GameManager.Instance.dynamicData.UnlockLvls[continent]-1)/4);
        scroll.transform.position = newPosition;
        Debug.Log(distance.ToString());
        Debug.Log(Screen.width.ToString());
    }

    public void Right()
    {
        Vector3 newPosition = scroll.transform.position;
        newPosition.x -= distance;
        scroll.transform.position = newPosition;
    }

    public void Left() 
    {
        Vector3 newPosition = scroll.transform.position;
        newPosition.x += distance;
        scroll.transform.position = newPosition;
    }

    public void Next()
    {
        GameManager.Instance.isContainRequireComponent = true;
        GameManager.Instance.AddScene(GameManager.Instance.dynamicData.UnlockLvls[continent]);
        SceneManager.LoadScene("LVL");
    }

}
