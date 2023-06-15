using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    string _gameId;
    [SerializeField] bool _testMode = true;

    private void Awake()
    {
        if (Advertisement.isInitialized)
        {
            Debug.Log("Advertisement is Initialized");
            //LoadRewardedAd();
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                LoadBannerAd();
                OnBannerLoaded();
            }
            else if (GameManager.Instance.dynamicData.battlesWithoutAds >= 3)
                LoadInerstitialAd();
        }
        else
        {
            InitializeAds();
        }
    }
    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOSGameId : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        if (SceneManager.GetActiveScene().buildIndex == 0)
            LoadBannerAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void LoadInerstitialAd()
    {
        Advertisement.Load("Interstitial_Android", this);
        GameManager.Instance.dynamicData.battlesWithoutAds = 0;
        GameManager.Instance.dynamicData.Save();
    }

    public void LoadRewardedAd()
    {
        Advertisement.Load("Rewarded_Android", this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("OnUnityAdsAdLoaded");
        Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsShowFailure");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("OnUnityAdsShowStart");
        Time.timeScale = 0;
        Advertisement.Banner.Hide();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("OnUnityAdsShowClick");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete " + showCompletionState);
        if (placementId.Equals("Rewarded_Android") && UnityAdsShowCompletionState.COMPLETED.Equals(showCompletionState))
        {
            Debug.Log("rewared Player");
        }
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex != 4 && SceneManager.GetActiveScene().buildIndex != 5)
            Advertisement.Banner.Show("Banner_Android");
    }



    public void LoadBannerAd()
    {
        if (SceneManager.GetActiveScene().buildIndex != 4 && SceneManager.GetActiveScene().buildIndex != 5)
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            Advertisement.Banner.Load("Banner_Android",
                new BannerLoadOptions
                {
                    loadCallback = OnBannerLoaded,
                    errorCallback = OnBannerError
                }
                );
        }
    }

    void OnBannerLoaded()
    {
        if (SceneManager.GetActiveScene().buildIndex != 4 && SceneManager.GetActiveScene().buildIndex != 5)
            Advertisement.Banner.Show("Banner_Android");
    }

    void OnBannerError(string message)
    {

    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

}