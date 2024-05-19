using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using User;

namespace UI
{
  public class LevelView : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private Vector3 _origScale;

    private void Start()
    {
      _origScale = _textMeshPro.transform.localScale;
    }

    private void OnEnable()
    {
      UserLevelData.OnLevelChanged += UpdateViewWithAnim;
      
      UpdateView(UserLevelData.CurrentLevel);
    }

    private void OnDisable()
    {
      UserLevelData.OnLevelChanged -= UpdateViewWithAnim;
    }

    private void UpdateView(int level)
    {
      _textMeshPro.text = $"LEVEL {level}";
    }

    private void UpdateViewWithAnim(int level)
    {
      DOTween.Sequence()
        .Append(_textMeshPro.transform.DOScale(_origScale * 2f, 0.3f)).OnComplete(() =>
        {
          UpdateView(level);
          _textMeshPro.transform.DOScale(_origScale, 0.3f);
        });
    }
  }
}