using Firebase;
using Firebase.Analytics;
using UnityEngine;

namespace Analytic
{
  public class GameAnalytic : MonoBehaviour
  {
    public static GameAnalytic Instance { get; private set; }

    private bool _canUseAnalytics;

    void Awake()
    {
      if (Instance == null)
      {
        Instance = this;
        DontDestroyOnLoad(gameObject);
      }
      else
        Destroy(gameObject);

      FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
      {
        var dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available)
        {
          _canUseAnalytics = true;
        }
        else
        {
          Debug.LogError(System.String.Format(
            "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        }
      });
    }


    public void InAppPurchaseEvent()
    {
      if (!_canUseAnalytics)
        return;
      FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventPurchase);
    }


    public void InterstitialAd()
    {
      if (!_canUseAnalytics)
        return;

      FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAdImpression, new Parameter("Ad_Type", "Interstitial_Ad"));
    }

    public void RewardedAd()
    {
      if (!_canUseAnalytics)
        return;
      FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAdImpression, new Parameter("Ad_Type", "Rewarded_Ad"));
    }

    public void BannerAd()
    {
      if (!_canUseAnalytics)
        return;
      FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAdImpression, new Parameter("Ad_Type", "Banner_Ad"));
    }

    public void LogEvent(string eventName)
    {
      if (!_canUseAnalytics)
        return;
      FirebaseAnalytics.LogEvent(eventName);
    }

    public void LevelUp(int eventName)
    {
      if (!_canUseAnalytics)
        return;
      FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelUp, new Parameter(FirebaseAnalytics.ParameterLevel, eventName));
    }
  }
}