using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class loadRewarded : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
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

    public void LoadAd()
    {
        print("Loading rewarded!");
        Advertisement.Load(adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if(placementId.Equals(adUnitId)){
            print("Rewarded loaded!");
            ShowAd();

        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        print("Rewarded failed to load!");
    }

    public void ShowAd()
    {
        print("showing ad!");
        Advertisement.Show(adUnitId, this);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        print("Rewarded clicked");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
       
        if (placementId.Equals(adUnitId) && showCompletionState == UnityAdsShowCompletionState.COMPLETED) {
            print("Rewarded show complete, Distribute the rewards");
        }
        
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        print("Rewarded show failure");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        print("Rewarded show start");
    }
}
