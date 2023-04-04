using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField] int SceneId;


    public void LevelBattleChoice()
    {
        GameManager.Instance.isContainRequireComponent = true;
        GameManager.Instance.AddScene(SceneId);
        SceneManager.LoadScene("LVL");


    }




}
