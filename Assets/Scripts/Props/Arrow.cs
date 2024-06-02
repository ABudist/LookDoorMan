using System;
using DG.Tweening;
using UnityEngine;

namespace Props
{
  public class Arrow : MonoBehaviour
  {
    private void Start()
    {
      Vector3 startPos = transform.localPosition;
      Vector3 endPos = transform.localPosition - transform.InverseTransformDirection(transform.forward);

      DOTween.Sequence()
        .Append(transform.DOLocalMove(endPos, 0.5f))
        .Append(transform.DOLocalMove(startPos, 0.5f))
        .SetLoops(-1);
    }
  }
}