using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Levels()
    {
        
    }
    public void Sandbox()
    {
        SceneManager.LoadScene(1);
    }
    public void Shop()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }
}
