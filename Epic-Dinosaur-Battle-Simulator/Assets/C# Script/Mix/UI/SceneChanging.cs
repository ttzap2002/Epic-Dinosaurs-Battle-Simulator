using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanging : MonoBehaviour
{
    public int idForShop = 0;
    void Awake()
    {
        if(idForShop != 7 && GameManager.Instance != null)
        {
            GameManager.Instance.idTileForBackFromShop = idForShop;
        }
    }

    public void Continents()
    {
        GameManager.Instance.isContainRequireComponent = false;
        SceneManager.LoadScene(1);
    }
    public void Sandbox(bool isRefresh=true)
    {
        GameManager.Instance.isContainRequireComponent = true;
        GameManager.Instance.AddScene(0);
        GameManager.Instance.RefreshGameObjects();
        SceneManager.LoadScene(5);

        
    
    }

    public void SetMapOne()
    {
        GameManager.Instance.currentMap = GameManager.Instance.mapContainer.MapList[0];
    }
    public void SetMapSecond()
    {
        GameManager.Instance.currentMap = GameManager.Instance.mapContainer.MapList[1];
    }
    public void SetMapthird()
    {
        GameManager.Instance.currentMap = GameManager.Instance.mapContainer.MapList[2];
    }
    public void SetMapfourth()
    {
        GameManager.Instance.currentMap = GameManager.Instance.mapContainer.MapList[3];
    }


    public void Shop()
    {
        SceneManager.LoadScene(7);
    }
    public void Exit()
    {
        Application.Quit();
        //Debug.Log("Zamykam Apke");
    }

    public void LvlChoice()
    {
        GameManager.Instance.isContainRequireComponent= false;
        GameManager.Instance.RefreshGameObjects();
        SceneManager.LoadScene(2);
    }

    public void LevelBattleChoice()
    {
        SceneManager.LoadScene(3);
    }

    public void MainMenu()
    {
        GameManager.Instance.isContainRequireComponent = false;
        GameManager.Instance.RefreshGameObjects();
        SceneManager.LoadScene(0);
    }

    public void MapSelector()
    {
        GameManager.Instance.isContainRequireComponent = false;
        GameManager.Instance.RefreshGameObjects();
        SceneManager.LoadScene(6);
    }


}
