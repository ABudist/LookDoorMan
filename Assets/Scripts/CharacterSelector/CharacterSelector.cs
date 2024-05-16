using System;
using System.Collections.Generic;
using CarSelector;
using UI;
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
    [SerializeField] private BuyPanel _buyPanelPrefab;
    [SerializeField] private Transform _parentForBuyPanel;
    [SerializeField] private ParticleSystem _particleSystem;

    private const int PRICE = 0;
    
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
        return;
        Transform panel = Instantiate(_buyPanelPrefab, _parentForBuyPanel).transform;
        panel.GetChild(1).transform.localScale = Vector3.one;
        panel.transform.localPosition = new Vector3(0, 0, -1500);

        var canvas = panel.gameObject.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = 1;
        
        panel.gameObject.AddComponent<GraphicRaycaster>();

      }
    }

    public void OpenNextClosedCar()
    {
      if (!_active)
        return;

      if (UserWallet.CurrentValue >= PRICE)
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
          UserWallet.Subtract(PRICE);
          
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