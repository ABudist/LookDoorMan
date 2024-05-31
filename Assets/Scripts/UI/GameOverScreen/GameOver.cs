using System;
using UI.BuyScreen;
using UI.SettingsScreen;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.GameOverScreen
{
  public class GameOver : MonoBehaviour
  {
    public event Action OnContinue;
    
    [SerializeField] private Background _background;
    [SerializeField] private Continue _continue;
    [SerializeField] private Buttons _buttons;
    [SerializeField] private Settings _settings;
    [SerializeField] private Buy _buy;
    
    public void Show()
    {
      _buttons.gameObject.SetActive(false);
      
      _background.ToBlack(null);
      
      _continue.gameObject.SetActive(true);
      _continue.Show(Continue, ShowButtons);
    }

    public void OpenSettings()
    {
      _settings.gameObject.SetActive(true);
      _settings.Open();
    }

    public void OpenSelector()
    {
      SceneManager.LoadScene("CharacterSelector");
    }

    public void OpenBuy()
    {
      _buy.gameObject.SetActive(true);
      _buy.Open();
    }

    private void Continue()
    {
      _continue.Hide(() =>
      {
        gameObject.SetActive(false);  
        OnContinue?.Invoke();
      });
    }

    private void ShowButtons()
    {
      _continue.Hide(() =>
      {
        _buttons.gameObject.SetActive(true);
        _buttons.Show();
      });
    }
  }
}