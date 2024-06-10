using System;
using System.Collections.Generic;
using CarSelector;
using UI;
using UI.BuyScreen;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using User;
using Random = UnityEngine.Random;

namespace CharacterSelector
{
  public class CharacterSelector : MonoBehaviour
  {
    [SerializeField] private Podium _podium;
    [SerializeField] private CharacterSelectPanel characterSelectPanel;
    [SerializeField] private OpenCarAnimation _openCarAnimation;
    [SerializeField] private CharactersKit[] _kits;
    [SerializeField] private Buy _buyPanel;
    [SerializeField] private Transform _parentForBuyPanel;
    [SerializeField] private ParticleSystem _particleSystem;
      
    private SelectCharacterButton _currentSelected;
    private bool _active = true;
    private bool _start = true;

    private const int CAPACITY_IN_KIT = 9;

    private void Start()
    {
      Application.targetFrameRate = 50;
    }

    public void TrySelect(SelectCharacterButton button)
    {
      if (!_active)
        return;

      if (UserCharacterData.IsOpened(button.CurrentConfig.Id))
      {
        SelectCharacter(button, !_start);
        _start = false;
      }
      else
      {
        _buyPanel.gameObject.SetActive(true);
        _buyPanel.Open();
      }
    }

    public void OpenNextClosedCar()
    {
      if (!_active)
        return;

      if (UserWallet.CurrentValue >= UserCharacterData.Cost)
      {
        CharactersKit kitWithClosedCharacters = GetKitWithClosedCars();

        if (kitWithClosedCharacters == null)
        {
          return;
        }

        _active = false;
        characterSelectPanel.SetBlocked(true);

        characterSelectPanel.ShowKit(kitWithClosedCharacters);

        List<SelectCharacterButton> closedCarsButtons = FindClosedCars(kitWithClosedCharacters);

        SelectCharacterButton btnToOpenCharacter = closedCarsButtons[Random.Range(0, closedCarsButtons.Count)];

        _openCarAnimation.StartAnimation(closedCarsButtons, btnToOpenCharacter, () =>
        {
          UserWallet.Subtract(UserCharacterData.Cost);
          
          UserCharacterData.Open(btnToOpenCharacter.CurrentConfig.Id);
          SelectCharacter(btnToOpenCharacter, false);
          _active = true;
          characterSelectPanel.SetBlocked(false);
        });
      }
    }

    public void SelectCurrentCharacter()
    {
      SceneManager.LoadScene("Game");
    }

    private void SelectCharacter(SelectCharacterButton button, bool withEffect)
    {
      UserCharacterData.CurrentId = button.CurrentConfig.Id;

      if (_currentSelected != null)
        _currentSelected.UpdateView();

      button.UpdateView();

      _currentSelected = button;
      _podium.SetCharacter(button.CurrentConfig);
      
      if(withEffect)
        _particleSystem.Play();
    }

    private CharactersKit GetKitWithClosedCars()
    {
      List<CharactersKit> kitsWithClosedCars = new List<CharactersKit>(3);

      for (int i = 0; i < _kits.Length; i++)
      {
        List<SelectCharacterButton> buttons = _kits[i].Buttons;

        foreach (SelectCharacterButton selectCarButton in buttons)
        {
          if (!UserCharacterData.IsOpened(selectCarButton.CurrentConfig.Id))
          {
            kitsWithClosedCars.Add(_kits[i]);
            break;
          }
        }
      }

      if (kitsWithClosedCars.Count == 0)
        return null;

      return kitsWithClosedCars[Random.Range(0, kitsWithClosedCars.Count)];
    }

    private List<SelectCharacterButton> FindClosedCars(CharactersKit kit)
    {
      List<SelectCharacterButton> closed = new List<SelectCharacterButton>(CAPACITY_IN_KIT);

      foreach (SelectCharacterButton selectCarButton in kit.Buttons)
      {
        if (!UserCharacterData.IsOpened(selectCarButton.CurrentConfig.Id))
        {
          closed.Add(selectCarButton);
        }
      }

      return closed;
    }
  }
}