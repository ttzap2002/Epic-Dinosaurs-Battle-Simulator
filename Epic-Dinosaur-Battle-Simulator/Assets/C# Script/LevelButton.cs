using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] int SceneId;

    public void SetScene() 
    {
        GameManager.Instance.currentScene = GameManager.Instance.levelContainer.LevelList[SceneId];
    }
}
