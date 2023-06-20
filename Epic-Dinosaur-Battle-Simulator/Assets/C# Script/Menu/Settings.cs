using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject settingsBox;
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

    }

    public void Statute()
    {

    }

    public void Store()
    {

    }

    public void FB()
    {

    }

    public void Instagram()
    {

    }

    public void TurnOnOffMusic()
    {

    }
}
