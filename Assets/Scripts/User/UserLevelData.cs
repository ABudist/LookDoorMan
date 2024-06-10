using System;
using UnityEngine;

namespace User
{
  public static class UserLevelData
  {
    public static event Action<int> OnLevelChanged;
    public static int CurrentLevel => PlayerPrefs.GetInt(LEVEL_KEY);
    public static int DeathCount => PlayerPrefs.GetInt(DEATH_COUNT);
    public static int PlayPressedCount => PlayerPrefs.GetInt(PLAY_PRESSED_COUNT);
    
    public static bool NeedToNextLevel { get; set; }
    public static bool NeedStartWindow { get; set; } = true;

    private const string LEVEL_KEY = "level";
    private const string DEATH_COUNT = "death_count";
    private const string PLAY_PRESSED_COUNT = "pp_count";

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

    public static void AddDeathCount()
    {
      PlayerPrefs.SetInt(DEATH_COUNT, DeathCount + 1);
    }

    public static void PlayPressed()
    {
      PlayerPrefs.SetInt(PLAY_PRESSED_COUNT, PlayPressedCount + 1);
    }
  }
}