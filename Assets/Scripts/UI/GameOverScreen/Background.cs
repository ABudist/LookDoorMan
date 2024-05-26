using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameOverScreen
{
  public class Background : MonoBehaviour
  {
    [SerializeField] private float _duration;
    [SerializeField] private Color _blackColor;
    [SerializeField] private Color _startColor;
    private Image _image => GetComponent<Image>();
    
    public void ToBlack(Action callback)
    {
      _image.DOColor(_blackColor, _duration).onComplete += () => callback?.Invoke();
    }

    public void ToWhite(Action callback)
    {
      _image.DOColor(_startColor, _duration).onComplete += () => callback?.Invoke();
    }
  }
}