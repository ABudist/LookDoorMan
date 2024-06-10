using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BuyScreen
{
  public class Buy : MonoBehaviour
  {
    [SerializeField] private Transform _window;
    [SerializeField] private Text _noAdsPrice;
    [SerializeField] private Text _fullPrice;

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

      _noAdsPrice.text = Purchaser.Instance.GetNoAdsPrice();
      _fullPrice.text = Purchaser.Instance.GetFullPrice();
    }

    public void Close()
    {
      _active = false;

      DOTween.Sequence()
        .Append(_window.DOScale(Vector3.zero, 0.2f))
        .OnComplete(() => { gameObject.SetActive(false); });
    }
      
    
    public void BuyNoAds()
    {
      Purchaser.Instance.BuyNoAds();
    }

    public void BuyFull()
    {
      Purchaser.Instance.BuyFull();
    }

    public void Restore()
    {
      Purchaser.Instance.RestorePurchases();
    }
  }
}