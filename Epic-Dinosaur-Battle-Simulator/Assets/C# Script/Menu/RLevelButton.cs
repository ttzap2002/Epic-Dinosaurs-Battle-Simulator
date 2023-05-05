using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RLevelButton : MonoBehaviour
{
    [SerializeField] int SceneId;
    public int continent;
    public GameObject padlock;
    public GameObject textBox;
    private TextMeshProUGUI idLevelTxt;

    private void Start()
    {
        idLevelTxt = textBox.GetComponent<TextMeshProUGUI>();
        idLevelTxt.text = SceneId.ToString();
        if(padlock != null && GameManager.Instance.dynamicData.UnlockLvls[continent] >= SceneId)
            Destroy(padlock);
    }

    public void LevelBattleChoice()
    {
        if (GameManager.Instance.dynamicData.UnlockLvls[continent] >= SceneId)
        {
            GameManager.Instance.isContainRequireComponent = true;
            GameManager.Instance.AddScene(SceneId);
            SceneManager.LoadScene("LVL");
        }
    }

}
