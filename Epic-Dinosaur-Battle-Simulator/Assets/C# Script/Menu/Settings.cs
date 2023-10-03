using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider intenseSlider;
    [SerializeField] private GameObject menuBox;
    [SerializeField] private Sprite playingMusic;
    [SerializeField] private Sprite notPlayingMusic;
    [SerializeField] private Image musicButton;
    [SerializeField] private Slider intenseSliderOfSounds;
    [SerializeField] private Image soundsButton;
    //public MusicManager manager;
    // Start is called before the first frame update
    void Start()
    {
        intenseSlider.value = GameManager.Instance.dynamicData.musicIntense;
        if (GameManager.Instance.dynamicData.WantMusic)
        {
            intenseSlider.gameObject.SetActive(true);
            musicButton.sprite = playingMusic;
        }
        else
        {
            intenseSlider.gameObject.SetActive(false);
            musicButton.sprite = notPlayingMusic;
        }
        intenseSliderOfSounds.value = GameManager.Instance.dynamicData.SoundsIntense;
        if (GameManager.Instance.dynamicData.WantSounds)
        {
            intenseSliderOfSounds.gameObject.SetActive(true);
            soundsButton.sprite = playingMusic;
        }
        else
        {
            intenseSliderOfSounds.gameObject.SetActive(false);
            soundsButton.sprite = notPlayingMusic;
        }
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowSettings()
    {
        menuBox.SetActive(false);
        gameObject.SetActive(true);
    }

    public void HideSettings()
    {
        menuBox.SetActive(true);
        gameObject.SetActive(false);
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
        if (GameManager.Instance.dynamicData.WantMusic)
        {
            intenseSlider.gameObject.SetActive(true);
            musicButton.sprite = playingMusic;
        }
        else
        {
            intenseSlider.gameObject.SetActive(false);
            musicButton.sprite = notPlayingMusic;
        }
        //manager.PlayRandomMusic();
    }

    public void ReadIntenseOfMusic()
    {
        MusicManager.musicIntense = intenseSlider.value;
        GameManager.Instance.dynamicData.musicIntense = intenseSlider.value;
        GameManager.Instance.dynamicData.Save();
    }

    public void ReadIntenseOfSounds()
    {
        MusicManager.soundsIntense = intenseSliderOfSounds.value;
        GameManager.Instance.dynamicData.SoundsIntense = intenseSliderOfSounds.value;
        GameManager.Instance.dynamicData.Save();
    }

    public void TurnOnOffSounds()
    {
        GameManager.Instance.dynamicData.WantSounds= !GameManager.Instance.dynamicData.WantSounds;
        GameManager.Instance.dynamicData.Save();
        if (GameManager.Instance.dynamicData.WantSounds)
        {
            intenseSliderOfSounds.gameObject.SetActive(true);
            soundsButton.sprite = playingMusic;
        }
        else
        {
            intenseSliderOfSounds.gameObject.SetActive(false);
            soundsButton.sprite = notPlayingMusic;
        }
    }

    public void PlaySound(int idSound)
    {
        if(GameManager.Instance.dynamicData.WantSounds)
        {
            MusicManager.PlaySoundFromGameMenager(idSound);
        }
    }

    public void ImplementSoundsIntense()
    {
        MusicManager.StaticImplementationIntenseOfSounds();
    }

}
