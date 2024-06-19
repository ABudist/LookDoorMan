using System;
using CAS;
using UnityEngine;
using User;


public class CASAds : MonoBehaviour
{
  public static CASAds Instance { get; private set; }

  private IMediationManager _manager = null;
  private IAdView _banner;

  private Action _onSuccessCallback;

  private void Awake()
  {
    if (Instance == null)
      Instance = this;
    else
      Destroy(gameObject);

    DontDestroyOnLoad(this);
  }

  private void Start()
  {
    _manager = CreateAdManager();
  }

  public void ShowBanner(AdPosition position)
  {
    if (UserPurchasingData.IsNoADSPurchased())
    {
      return;
    }

    if (_manager != null)
    {
      _banner = _manager.GetAdView(AdSize.ThinBanner);

      _banner.position = position;
      _banner.SetActive(true);
    }
  }

  public void HideBanner()
  {
    if (_banner != null)
    {
      _banner.SetActive(false);
    }
  }

  public void ShowInterstitial()
  {
    if(!UserPurchasingData.IsNoADSPurchased())
      _manager?.ShowAd(AdType.Interstitial);
  }

  public bool IsRewardedReady() => _manager.IsReadyAd(AdType.Rewarded);

  public void ShowRewarded(Action onSuccess)
  {
    #if UNITY_EDITOR
    
    onSuccess?.Invoke();
    
    #else
    _onSuccessCallback = onSuccess;
    
    _manager.ShowAd(AdType.Rewarded);
    #endif
  }

  private IMediationManager CreateAdManager()
  {
    return MobileAds.BuildManager()
      .WithCompletionListener((config) =>
      {
        string initErrorOrNull = config.error;
        string userCountryISO2OrNull = config.countryCode;
        bool protectionApplied = config.isConsentRequired;
        IMediationManager manager = config.manager;

        manager.OnRewardedAdCompleted += RewardedCompleted;

        ShowBanner(AdPosition.BottomCenter);
      })
      .Build();
  }

  private void RewardedCompleted()
  {
    _onSuccessCallback?.Invoke();
  }
}