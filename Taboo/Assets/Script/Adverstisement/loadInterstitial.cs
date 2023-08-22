using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class loadInterstitial : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public string androidAdUnityId;
    public string iosAdUnityId;

    string adUnitId;

    private void Awake()
    {
#if UNITY_IOS
        adUnitId = iosAdUnityId;

#elif UNITY_ANDROID
        adUnitId = androidAdUnityId;
#endif

    }

    public void LoadAd() {
        print("Loading interstitial!");
        Advertisement.Load(adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        print("Interstitial loaded!");
        ShowAd();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        print("Interstitial failed to load!");
    }

    public void ShowAd() {
        print("showing ad!");
        Advertisement.Show(adUnitId, this);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        print("interstitial clicked");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        print("interstitial shown complete");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        print("interstitial show failure");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        print("interstitial show start");
    }
}
