using System;
using UnityEngine;

namespace User
{
  public static class UserLevelData
  {
    public static event Action<int> OnLevelChanged;
    public static int CurrentLevel => PlayerPrefs.GetInt(LEVEL_KEY);
    
    public static bool NeedToNextLevel { get; set; }

    private const string LEVEL_KEY = "level";

    static UserLevelData()
    {
      NeedToNextLevel = false;
      
      if (!PlayerPrefs.HasKey(LEVEL_KEY))
      {
        NextLevel();
      }
    }
    
    public static void NextLevel()
    {
      Debug.Log("ADD");
      NeedToNextLevel = false;
      
      PlayerPrefs.SetInt(LEVEL_KEY, CurrentLevel + 1);
      
      OnLevelChanged?.Invoke(CurrentLevel);
    }
  }
}