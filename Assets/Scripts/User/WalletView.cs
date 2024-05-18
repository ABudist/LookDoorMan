using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace User
{
  public class WalletView : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private string _format;
    private Vector3 _origScale;
      
    private void Awake()
    {
      _format = _textMeshPro.text;
      _origScale = transform.localScale;
      
      UserWallet.OnChanged += UpdateView;
      
      ChangeText(UserWallet.CurrentValue);
    }

    private void OnDestroy()
    {
      UserWallet.OnChanged -= UpdateView;
    }

    private void UpdateView(int value)
    {
      ChangeText(value);

      DOTween.Sequence()
        .Append(transform.DOScale(_origScale * 1.2f, 0.2f))
        .Append(transform.DOScale(_origScale, 0.2f));
    }

    private void ChangeText(int value)
    {
      _textMeshPro.text = string.Format(_format, value.ToString());
    }
  }
}