using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
  public class AnchorMover : MonoBehaviour
  {
    [SerializeField] private bool _moveOnStart;
    [SerializeField] private Vector2 _target;
    [SerializeField] private float _duration;

    private RectTransform _transform => GetComponent<RectTransform>();
      
    private void Start()
    {
      if (_moveOnStart)
      {
        Move();
      }
    }

    public void Move()
    {
      _transform.DOAnchorPosX(_target.x, _duration);
    }
  }
}