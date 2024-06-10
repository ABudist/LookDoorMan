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
    [SerializeField] private Transform _cross1;
    [SerializeField] private Transform _cross2;

    public void Show()
    {
      Vector3 origScale = _settings.transform.localScale;
      Vector3 crossScale = _cross1.transform.localScale;

      _cross1.localScale = _cross2.localScale = _settings.transform.localScale = _characterSelector.transform.localScale = _buy.transform.localScale = Vector3.zero;

      DOTween.Sequence()
        .Append(_settings.transform.DOScale(origScale, _showingDuration))
        .Join(_characterSelector.transform.DOScale(origScale, _showingDuration))
        .Join(_buy.transform.DOScale(origScale, _showingDuration))
        .Join(_cross1.DOScale(crossScale, _showingDuration))
        .Join(_cross2.DOScale(crossScale, _showingDuration))
        .SetEase(Ease.OutBack);
    }
  }
}