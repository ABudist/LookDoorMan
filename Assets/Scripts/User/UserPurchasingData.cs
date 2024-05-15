using UnityEngine;

namespace User
{
  public static class UserPurchasingData
  {
    private const string NO_ADS_KEY = "No_ADS";

    public static bool IsNoADSPurchased() => PlayerPrefs.HasKey(NO_ADS_KEY);

    public static void SetNoADSPurchased() => PlayerPrefs.SetInt(NO_ADS_KEY, 1);
  }
}