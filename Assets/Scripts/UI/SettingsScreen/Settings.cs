using DG.Tweening;
using UnityEngine;

namespace UI.SettingsScreen
{
  public class Settings : MonoBehaviour
  {
    [SerializeField] private Transform _window;

    private Vector3 _origScale;

    private bool _isInitialized;
    private bool _active;

    public void Open()
    {
      _active = false;

      gameObject.SetActive(true);

      if (!_isInitialized)
      {
        _origScale = _window.localScale;
        _isInitialized = true;
      }

      _window.transform.localScale = Vector3.zero;

      DOTween.Sequence()
        .Append(_window.DOScale(_origScale*1.1f, 0.3f))
        .Append(_window.DOScale(_origScale, 0.05f))
        .OnComplete(() => _active = true);
    }

    public void Close()
    {
      _active = false;

      DOTween.Sequence()
        .Append(_window.DOScale(Vector3.zero, 0.2f))
        .OnComplete(() => { gameObject.SetActive(false); });
    }

    public void OpenMoreApps()
    {
      if (!_active)
        return;
    }

    public void RateUs()
    {
      if (!_active)
        return;
    }

    public void Restore()
    {
      if (!_active)
        return;
    }
  }
}