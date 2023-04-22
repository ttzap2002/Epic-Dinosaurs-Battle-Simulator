using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackShop : MonoBehaviour
{
    public void BackFromShop()
    {
        SceneManager.LoadScene(GameManager.Instance.idTileForBackFromShop);
    }
}
