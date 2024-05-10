using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
  public class BlackScreen : MonoBehaviour
  {
    [SerializeField] private float _duration;

    private Image _image => GetComponent<Image>();
    private Color _black = Color.black;
    private Color _clear = Color.clear;
    
    public void To(Action callback)
    {
      gameObject.SetActive(true);
      
      DOTween.Sequence()
        .Append(DOVirtual.Color(_clear, _black, _duration, color => _image.color = color))
        .OnComplete(() =>
        {
          callback?.Invoke();
        });
    }

    public void From(Action callback)
    {
      gameObject.SetActive(true);
      
      DOTween.Sequence()
        .Append(DOVirtual.Color(_black, _clear, _duration, color => _image.color = color))
        .OnComplete(() =>
        {
          gameObject.SetActive(false);
          callback?.Invoke();
        });
    }
  }
}