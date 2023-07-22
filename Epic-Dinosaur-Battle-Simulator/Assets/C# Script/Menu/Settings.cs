using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject settingsBox;
    //public MusicManager manager;
    // Start is called before the first frame update
    void Start()
    {
        settingsBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowSettings()
    {
        settingsBox.SetActive(true);
    }

    public void HideSettings()
    {
        settingsBox.SetActive(false);
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://mistybytes.com/privacy-policy/");
    }

    public void Store()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=food.fight.survivors&hl=pl&gl=US");
        }
        else
        {
            Application.OpenURL("https://apps.apple.com/pl/app/pou/id575154654?l=pl");
        }
    }

    public void FB()
    {
        Application.OpenURL("https://m.facebook.com/profile.php?id=100089966326694&wtsid=rdr_0ki0O0r5dDiU3hAFh");
    }

    public void Instagram()
    {
        Application.OpenURL("https://www.instagram.com/foodfightsurvivors/");
    }

    public void TurnOnOffMusic()
    {
        GameManager.Instance.dynamicData.WantMusic = !GameManager.Instance.dynamicData.WantMusic;
        GameManager.Instance.dynamicData.Save();
        //manager.PlayRandomMusic();
    }
}
