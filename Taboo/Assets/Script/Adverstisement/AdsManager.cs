using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    public string androidGameId;
    public string iosGameId;

    public bool isTestingMode = false;
    string gameId;

    void Awake() {
        InitializeAds();
    }

    public void InitializeAds() {

//richiamiamo stati del dispositivo quindi se siamo su unity_ios allora l'id sarà quello di IO ecc
#if UNITY_IOS
        gameId = iosGameId;

#elif UNITY_ANDROID
        gameId = androidGameId;

        //questo è di testing, se in caso si fa partire come unity editor diamo un id a caso
#elif UNITY_EDITOR
        gameId = androidGameId;
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported) {
            Advertisement.Initialize(gameId, isTestingMode, this);
        }

    }

    public void OnInitializationComplete()
    {
        print("Ads inititialized!");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        print("Failed to initialize!");
    }
}
