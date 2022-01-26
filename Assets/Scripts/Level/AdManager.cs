using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{

    BannerView bannerAd;
    private InterstitialAd interstitial;
    RewardBasedVideoAd rewardAd;
    bool isRewarded = false;

    public static AdManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        MobileAds.Initialize(InitializationStatus => { });

        this.rewardAd = RewardBasedVideoAd.Instance;
        this.rewardAd.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
        this.rewardAd.OnAdClosed += this.HandleRewardBasedVideoClosed;

        this.RequestRewardBasedVideo();

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            this.RequestBanner();
        }

    }

    void Update()
    {
        if (isRewarded)
        {
            isRewarded = false;
            PlayerPrefs.SetInt("NumberOfCoins", PlayerPrefs.GetInt("NumberOfCoins", 0) + 100);
        }
    }

    private void RequestBanner()
    {
        string adUnitId = "secret";
        this.bannerAd = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerAd.LoadAd(this.CreateAdRequest());
    }

    public void RequestInterstitial()
    {
        string adUnitId = "secret";

        interstitial = new InterstitialAd(adUnitId);

        interstitial.OnAdClosed += HandleOnInterstitialAdClosed;

        interstitial.LoadAd(CreateAdRequest());
    }

    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

    public void HandleOnInterstitialAdClosed(object sender, EventArgs args)
    {
        interstitial = null;
    }

    public void RequestRewardBasedVideo()
    {
        string adUnitId = "secret";
        this.rewardAd.LoadAd(this.CreateAdRequest(), adUnitId);
    }

    public void ShowRewardBasedVideo()
    {
        if (this.rewardAd.IsLoaded())
        {
            this.rewardAd.Show();
        }
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        this.RequestRewardBasedVideo();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        Debug.Log(">>>>>>>rewarede");
        isRewarded = true;
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }
    

}
