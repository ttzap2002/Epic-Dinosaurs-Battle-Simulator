using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxChanger : MonoBehaviour
{
    /// <summary>
    /// Numer Panelu, do którego dany przycisk zosta³ do³¹czony. domyslnie jest 99 co oznacza, ze nie ma takiego panelu
    /// </summary>
    public int shopScreen=99;
    /// <summary>
    /// panel, do ktorego zostal dolaczony dany przycisk. powinien to byc element, ktory posiada wszystkie elementy graficzne (takze te z dzieciakami)
    /// </summary>
    public GameObject shopPanel;
    /// <summary>
    /// Wymagane! Numer Panelu, który zostanie uruchomiony poprzez nacisniecie przycisku.
    /// </summary>
    public int nextScreen;
    /// <summary>
    /// Wymagane! Panel, ktory bedzie widoczny, w momencie nacisniecia tego przycisku
    /// </summary>
    public GameObject nextPanel;
    
    /// <summary>
    /// Wymaga jednego uzupelnienia shopPanelu. Ukrywa zbedne panele przed uzytkownikiem
    /// </summary>
    void Start()
    {
        if(GameManager.Instance.numberOfShopScreen !=shopScreen && shopScreen!=99)
            shopPanel.SetActive(false);
    }

    /// <summary>
    /// Skrypt dla przyciskow do zmiany panelu z towarami sklepu
    /// </summary>
    public void ChengePanel()
    {
        if(shopPanel!=nextPanel)
        {
            nextPanel.SetActive(true);
            shopPanel.SetActive(false);
            GameManager.Instance.numberOfShopScreen = nextScreen;
        }
    }
}
