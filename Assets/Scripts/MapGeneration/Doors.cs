using DG.Tweening;
using UnityEngine;

namespace MapGeneration
{
  public class Doors : MonoBehaviour
  {
    [SerializeField] private Transform _left;
    [SerializeField] private Transform _right;

    [SerializeField] private Vector3 _leftOpenedPos;
    [SerializeField] private Vector3 _rightOpenedPos;
    [SerializeField] private float _duration;

    private Vector3 _leftClosedPos;
    private Vector3 _rightClosedPos;

    private void Start()
    {
      _leftClosedPos = _left.localPosition;
      _rightClosedPos = _right.localPosition;
    }

    public void Open()
    {
      SoundManager.SoundManager.Instance.PlayOneShot(SoundManager.SoundManager.Instance.Door);
      
      DOTween.Sequence()
        .Append(_left.DOLocalMove(_leftOpenedPos, _duration))
        .Join(_right.DOLocalMove(_rightOpenedPos, _duration));
    }

    public void Close()
    {
      SoundManager.SoundManager.Instance.PlayOneShot(SoundManager.SoundManager.Instance.Door);

      DOTween.Sequence()
        .Append(_left.DOLocalMove(_leftClosedPos, _duration))
        .Join(_right.DOLocalMove(_rightClosedPos, _duration));
    }
  }
}