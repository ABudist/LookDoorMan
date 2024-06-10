using System;
using CharacterSelector;
using UnityEngine;

namespace User
{
  public static class UserCharacterData
  {
    public static event Action OnChanged;

    public static int Cost = 100;
    
    private const string CHARACTER_ID_KEY = "Character Id";
    private const string CHARACTER_OPENED_KEY = "Character_opened_";

    public static int CurrentId
    {
      get => PlayerPrefs.GetInt(CHARACTER_ID_KEY);
      
      set
      {
        PlayerPrefs.SetInt(CHARACTER_ID_KEY, value); 
        OnChanged?.Invoke();
      }
    }

    static UserCharacterData()
    {
      int id = Characters.Instance.CharactersConfigs[0].Id;

      if (!PlayerPrefs.HasKey(CHARACTER_OPENED_KEY + id))
      {
        Open(id);
        CurrentId = id;
      }
    }
    
    public static bool IsOpened(int id) => PlayerPrefs.HasKey(CHARACTER_OPENED_KEY + id);

    public static void Open(int id) => PlayerPrefs.SetInt(CHARACTER_OPENED_KEY + id, 1);
  }
}