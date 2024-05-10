using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Player
{
  public class PlayerSpawnEffect : MonoBehaviour
  {
    [SerializeField] private GameObject _effect;

    public void Show()
    {
      StartCoroutine(Anim());
    }

    private IEnumerator Anim()
    {
      Vector3 origScale = transform.localScale;
      transform.localScale = Vector3.zero;

      yield return DOTween.Sequence()
        .AppendInterval(1.5f).WaitForCompletion();

      GameObject effect = Instantiate(_effect);
      effect.transform.position = transform.position;

      DOTween.Sequence()
        .Append(transform.DOScale(origScale, 1f));
    }
  }
}