using System;
using UnityEngine;

namespace UI.GameOverScreen
{
  public class GameOver : MonoBehaviour
  {
    public event Action OnContinue;
    
    [SerializeField] private Background _background;
    [SerializeField] private Continue _continue;
    [SerializeField] private Buttons _buttons;

    public void Show()
    {
      _buttons.gameObject.SetActive(false);
      
      _background.ToBlack(null);
      
      _continue.gameObject.SetActive(true);
      _continue.Show(Continue, ShowButtons);
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