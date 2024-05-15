using UnityEngine;
using User;

namespace CharacterSelector
{
  public class Characters : MonoBehaviour
  {
    public static Characters Instance { get; private set; }

    public CharacterConfigSO[] CharactersConfigs => _characters;
    
    [SerializeField] private CharacterConfigSO[] _characters;

    private void Awake()
    {
      if (Instance != null)
      {
        Destroy(gameObject);
      }
      else
      {
        Instance = this;
        DontDestroyOnLoad(gameObject);
      }
    }

    public Player.Player GetSelectedPrefab()
    {
      int selectedCarId = UserCharacterData.CurrentId;
      
      for (int i = 0; i < _characters.Length; i++)
      {
        if (_characters[i].Id == selectedCarId)
        {
          return _characters[i].Prefab;
        }
      }

      return null;
    }
  }
}