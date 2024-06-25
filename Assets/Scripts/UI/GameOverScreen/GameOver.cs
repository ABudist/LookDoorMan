using System;
using DG.Tweening;
using UI.BuyScreen;
using UI.SettingsScreen;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.GameOverScreen
{
  public class GameOver : MonoBehaviour
  {
    public event Action OnPlayClicked;

    [SerializeField] private Background _background;
    [SerializeField] private Buttons _buttons;
    [SerializeField] private Settings _settings;
    [SerializeField] private Buy _buy;
    [SerializeField] private Transform _playButton;
    [SerializeField] private Transform _looseText;
    [SerializeField] private GameObject _levelComplete;

    private Sequence _sequence;

    public void ShowStart(bool showLevelCompete)
    {
      if (_sequence != null)
      {
        _sequence.Kill();
      }
      
      _buttons.gameObject.SetActive(false);
      _playButton.gameObject.SetActive(false);
      _looseText.gameObject.SetActive(false);
      _buttons.gameObject.SetActive(false);

      _background.ToBlack(() =>
      {
        _buttons.gameObject.SetActive(true);
        _buttons.Show();
        _levelComplete.SetActive(showLevelCompete);
        ShowPlayText();
      });
    }

    public void ShowEnd()
    {
      if (_sequence != null)
      {
        _sequence.Kill();
      }
      
      _buttons.gameObject.SetActive(false);
      _playButton.gameObject.SetActive(false);
      _looseText.gameObject.SetActive(false);
      _buttons.gameObject.SetActive(false);

      _background.ToBlack(() =>
      {
        _buttons.gameObject.SetActive(true);
        _buttons.Show();
        ShowEndTextAndPlay();
      });
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

    public void PlayGame()
    {
      SoundManager.SoundManager.Instance.PlayOneShot(SoundManager.SoundManager.Instance.Click);

      gameObject.SetActive(false);

      OnPlayClicked?.Invoke();
    }

    private void ShowPlayText()
    {
      _playButton.gameObject.SetActive(true);

      Vector3 playScale = _playButton.localScale;
      _playButton.localScale = Vector3.zero;

      DOTween.Sequence()
        .AppendInterval(0.5f)
        .Append(_playButton.DOScale(playScale, 0.5f))
        .onComplete += () =>
      {
        _sequence = DOTween.Sequence()
          .Append(_playButton.DOScale(playScale * 1.2f, 0.6f))
          .Append(_playButton.DOScale(playScale, 0.6f))
          .SetLoops(-1);
      };
    }

    private void ShowEndTextAndPlay()
    {
      _playButton.gameObject.SetActive(true);
      _looseText.gameObject.SetActive(true);

      Vector3 playScale = _playButton.localScale;
      Vector3 endScale = _looseText.localScale;

      _playButton.localScale = Vector3.zero;
      _looseText.localScale = Vector3.zero;

      DOTween.Sequence()
        .AppendInterval(0.8f)
        .Append(_looseText.DOScale(endScale * 2f, 0))
        .Append(_looseText.DOScale(endScale, 0.2f))
        .AppendInterval(1f)
        .Append(_playButton.DOScale(playScale, 0.5f))
        .onComplete += () =>
      {
        _sequence = DOTween.Sequence()
          .Append(_playButton.DOScale(playScale * 1.2f, 0.6f))
          .Append(_playButton.DOScale(playScale, 0.6f))
          .SetLoops(-1);
      };
    }
  }
}