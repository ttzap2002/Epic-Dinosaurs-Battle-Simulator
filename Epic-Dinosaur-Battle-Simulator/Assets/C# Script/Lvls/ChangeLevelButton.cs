using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLevelButton : MonoBehaviour
{
    [SerializeField] bool isForRightButton;
    [SerializeField] Image img;
    // Start is called before the first frame update

    private void Start()
    {
        GameManager.Instance.changeLevel += ActiveImg;
        ActiveImg();
    }

    public void ActiveImg() 
    {
        img.enabled = true;
        if (isForRightButton)
        {
            if (CheckIfItAppropiateLvl())
            {
                img.enabled = false;
            }
        }
        else 
        {
            if (GameManager.Instance.currentScene.Id == 1) 
            {
                img.enabled = false;
            }

        }

    }

    private bool CheckIfItAppropiateLvl()
    {
        return GameManager.Instance.currentScene.Id + 1 > GameManager.Instance.dynamicData.UnlockLvls[GameManager.Instance.currentContinent]
            || GameManager.Instance.currentScene.Id == 80;
    }
}
