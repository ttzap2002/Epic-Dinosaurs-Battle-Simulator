using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanging : MonoBehaviour
{
    public void Continents()
    {

        GameManager.Instance.isContainRequireComponent = false;
        SceneManager.LoadScene(1);
    }
    public void Sandbox()
    {
        GameManager.Instance.isContainRequireComponent = true;
        SceneManager.LoadScene(5);
        GameManager.Instance.currentScene = GameManager.Instance.levelContainer.LevelList[0];
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
        GameManager.Instance.isContainRequireComponent= false;
        SceneManager.LoadScene(2);
    }

    public void LevelBattleChoice()
    {
        SceneManager.LoadScene(3);

    }

    public void MainMenu()
    {
        GameManager.Instance.isContainRequireComponent = false;

        SceneManager.LoadScene(0);
        GameManager.Instance.Awake();
    }
}
