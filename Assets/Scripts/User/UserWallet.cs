using System;
using UnityEngine;

namespace User
{
  public static class UserWallet
  {
    public static event Action<int> OnChanged;
    public static int CurrentValue { get; private set; }
    
    private const string WALLET_VALUE_KEY = "wallet_value";

    static UserWallet()
    {
      CurrentValue = PlayerPrefs.GetInt(WALLET_VALUE_KEY);
      
      OnChanged += (int value) =>
      {
        PlayerPrefs.SetInt(WALLET_VALUE_KEY, value);
      };
    }
    
    public static void Add(int value)
    {
      CurrentValue += value;
      
      OnChanged?.Invoke(CurrentValue);
    }

    public static void Subtract(int value)
    {
      if (CurrentValue - value >= 0)
      {
        CurrentValue -= value;
      }
      
      OnChanged?.Invoke(CurrentValue);
    }
  }
}