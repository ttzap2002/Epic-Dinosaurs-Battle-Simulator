using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanging : MonoBehaviour
{
    public void Continents()
    {
        SceneManager.LoadScene(1);
    }
    public void Sandbox()
    {
        SceneManager.LoadScene(5);
    }
    public void Shop()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }

    public void LvlChoice()
    {
        SceneManager.LoadScene(2);
    }

    public void LevelBattleChoice()
    {
        SceneManager.LoadScene(3);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
