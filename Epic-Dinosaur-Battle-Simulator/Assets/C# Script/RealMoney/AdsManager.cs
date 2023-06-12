using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public AdsInitializer ads = new AdsInitializer();
    InterstitialAdExample skipAd = new InterstitialAdExample();
    RewardedAdsButton bonusAd = new RewardedAdsButton();
    BannerAdExample bannerAd = new BannerAdExample();
    // Start is called before the first frame update
    void Start()
    {
        ads._androidGameId = "5311809";
        ads._iOSGameId = "5311808";
    }

    public void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
