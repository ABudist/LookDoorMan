using System;
using DG.Tweening;
using UnityEngine;

namespace UI.GameOverScreen
{
  public class Buttons : MonoBehaviour
  {
    public event Action OnSettingsClicked;
    public event Action OnCharactersClicked;
    public event Action OnBuyClicked;

    [SerializeField] private float _showingDuration;
    [SerializeField] private Button _settings;
    [SerializeField] private Button _characterSelector;
    [SerializeField] private Button _buy;

    public void Show()
    {
      Vector3 origScale = _settings.transform.localScale;

      _settings.transform.localScale = _characterSelector.transform.localScale = _buy.transform.localScale = Vector3.zero;

      DOTween.Sequence()
        .Append(_settings.transform.DOScale(origScale, _showingDuration))
        .Join(_characterSelector.transform.DOScale(origScale, _showingDuration))
        .Join(_buy.transform.DOScale(origScale, _showingDuration))
        .SetEase(Ease.OutBack);
    }
  }
}