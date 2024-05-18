using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
  public class Button : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
  {
    public event Action OnClick;

    [SerializeField] private float _scaleFactor;
    [SerializeField] private bool _sound = true;

    private Vector3 _origScale;

    private bool _downed;

    private void Start()
    {
      _origScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      _downed = true;

      transform.localScale = _origScale * _scaleFactor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      UpEffect();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      if (_downed)
      {
        UpEffect();

        if (_sound)
          SoundManager.SoundManager.Instance.PlayOneShot(SoundManager.SoundManager.Instance.Click);

        OnClick?.Invoke();
      }
    }

    private void UpEffect()
    {
      _downed = false;
      transform.localScale = _origScale;
    }
  }
}