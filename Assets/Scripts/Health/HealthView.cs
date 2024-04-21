using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Health
{
  public class HealthView : MonoBehaviour
  {
    private Canvas _canvas => GetComponent<Canvas>();
    
    [SerializeField] private Health _health;
    [SerializeField] private Image _filled;
    [SerializeField] private float _animationSize;
    [SerializeField] private float _animationDuration;
    
    private Vector3 _origScale;
    
    private void Awake()
    {
      _origScale = transform.localScale;
      _canvas.worldCamera = Camera.main;
      _health.OnChanged += UpdateView;
    }

    private void OnDestroy()
    {
      _health.OnChanged -= UpdateView;
    }

    private void UpdateView()
    {
      DOTween.Sequence()
        .Append(transform.DOScale(_origScale * _animationSize, _animationDuration / 2))
        .Append(transform.DOScale(_origScale, _animationDuration / 2));
      
      _filled.fillAmount = _health.NormalizedValue;

      if (_health.NormalizedValue <= 0)
      {
        gameObject.SetActive(false);
      }
    }
  }
}