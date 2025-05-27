using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class InterstitialAdManager : MonoBehaviour
{
    public static InterstitialAdManager Instance { get; private set; }

    private InterstitialAd interstitialAd;

#if UNITY_ANDROID
    private string adUnitId = "ca-app-pub-3940256099942544/1033173712"; // 테스트 ID
#elif UNITY_IPHONE
    private string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
    private string adUnitId = "unexpected_platform";
#endif

    private void Awake()
    {
        // Singleton 유지
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        MobileAds.Initialize(initStatus => LoadInterstitialAd());
    }

    public void LoadInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        AdRequest request = new AdRequest();
        InterstitialAd.Load(adUnitId, request, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogWarning("Interstitial ad failed to load: " + error);
                return;
            }

            interstitialAd = ad;
            RegisterEventHandlers(interstitialAd);
            Debug.Log("Interstitial ad loaded successfully.");
        });
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
        else
        {
            Debug.LogWarning("Interstitial ad is not ready yet.");
            LoadInterstitialAd(); // 예비 로드
        }
    }

    private void RegisterEventHandlers(InterstitialAd ad)
    {
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Ad failed to show full screen content: " + error);
            LoadInterstitialAd(); // 실패 시 다시 로딩
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad closed.");
            LoadInterstitialAd(); // 닫힌 후 다시 로딩
        };

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad opened.");
        };

        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad impression recorded.");
        };

        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad clicked.");
        };
    }
}
