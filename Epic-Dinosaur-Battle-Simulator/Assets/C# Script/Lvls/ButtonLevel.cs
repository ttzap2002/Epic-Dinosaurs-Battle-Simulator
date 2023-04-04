using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLevel 
{
    [SerializeField] int idSceneLevel;

    public void LevelBattleChoice()
    {
        SceneManager.LoadScene(3);
        GameManager.Instance.AddScene(idSceneLevel);
    }
}
