using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxChanger : MonoBehaviour
{
    /// <summary>
    /// Wymagane! Numer Panelu, który zostanie uruchomiony poprzez nacisniecie przycisku.
    /// </summary>
    public int nextScreen;
    /// <summary>
    /// Wymagane! Panel, ktory bedzie widoczny, w momencie nacisniecia tego przycisku
    /// </summary>
    public GameObject nextPanel;
    /// <summary>
    /// Nr panelu, ktory stanie sie niewidoczny po nacisnieciu tego przycisku
    /// </summary>
    public static int actualScreen;
    /// <summary>
    /// Panel, ktory stanie sie niewidoczny po nacisnieciu tego przycisku
    /// </summary>
    public static GameObject actualPanel;

    /// <summary>
    /// Ukrywa zbedne panele przed uzytkownikiem, a ten, ktory zostaje widoczny jest przypisywany do actualPanel
    /// </summary>
    void Start()
    {
        if (GameManager.Instance.numberOfShopScreen != nextScreen)
            nextPanel.SetActive(false);
        else
        {
            BoxChanger.actualPanel = nextPanel;
            actualScreen = nextScreen;
        }
    }

    /// <summary>
    /// Skrypt dla przyciskow do zmiany panelu z towarami sklepu
    /// </summary>
    public void ChengePanel()
    {
        if(BoxChanger.actualScreen != nextScreen)
        {
            nextPanel.SetActive(true);
            BoxChanger.actualPanel.SetActive(false);
            BoxChanger.actualPanel=nextPanel;
            actualScreen = nextScreen;
            GameManager.Instance.numberOfShopScreen = nextScreen;
        }
    }
}
