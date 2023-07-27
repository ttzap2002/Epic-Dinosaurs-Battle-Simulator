using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        pauseButton.SetActive(false);
    }

    public void ShowPausedUI()
    {
        gameObject.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseButton.SetActive(true);
        gameObject.SetActive(false);
    }

    public void StartGame()
    {
        pauseButton.SetActive(true);
    }
}
