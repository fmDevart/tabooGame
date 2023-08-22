using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class loadBanner : MonoBehaviour
{
    public string androidAdUnityId;
    public string iosAdUnityId;

    string adUnitId;

    BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    private void Start()
    {
#if UNITY_IOS
        adUnitId = iosAdUnityId;

#elif UNITY_ANDROID
        adUnitId = androidAdUnityId;
#endif
        Advertisement.Banner.SetPosition(bannerPosition);

    }

    public void LoadBanner() { 
        BannerLoadOptions options = new BannerLoadOptions {
            loadCallback=OnBannerLoaded,
            errorCallback=OnBannerLoadError
        };
        Advertisement.Banner.Load(adUnitId, options);
    }
    void OnBannerLoaded() {

    }
    void OnBannerLoadError(string error) {
    }

    public void ShowBanner()
    {
        BannerOptions options = new BannerOptions
        {
            showCallback = OnBannerShown,
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden
            
        };
        Advertisement.Banner.Show(adUnitId, options);
    }

    void OnBannerShown() { }
    void OnBannerClicked() { }
    void OnBannerHidden() { }

    public void HideBannerAd() {
        Advertisement.Banner.Hide();
    }
}
