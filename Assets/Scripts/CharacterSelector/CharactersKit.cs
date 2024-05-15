using System.Collections.Generic;
using UnityEngine;

namespace CharacterSelector
{
  public class CharactersKit : MonoBehaviour
  {
    public List<SelectCharacterButton> Buttons => _buttons;

    [SerializeField] private Characters _characters;
    [SerializeField] private CharacterSelector _characterSelector;
    [SerializeField] private SelectCharacterButton _selectCharacterButtonPrefab;
    [SerializeField] private Transform _parentForButtons;

    private List<SelectCharacterButton> _buttons = new List<SelectCharacterButton>(9);
    
    public void SpawnButtons(Rect cutZone)
    {
      for (int i = 0; i < _characters.CharactersConfigs.Length; i++)
      {
        SelectCharacterButton selectCharacterButton = Instantiate(_selectCharacterButtonPrefab, _parentForButtons);
        selectCharacterButton.Construct(_characters.CharactersConfigs[i], cutZone, _characterSelector);
        
        _buttons.Add(selectCharacterButton);
      }
    }

    public bool HasId(int carId)
    {
      foreach (CharacterConfigSO car in _characters.CharactersConfigs)
      {
        if (car.Id == carId)
          return true;
      }

      return false;
    }
  }
}