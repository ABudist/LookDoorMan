using UnityEngine;

namespace CameraFollower
{
  public class CameraFollower : MonoBehaviour
  {
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed;
    
    private Transform _target;

    public void SetTarget(Transform target)
    {
      _target = target;
      transform.position = _target.position + _offset;
    }

    private void Update()
    {
      transform.position = Vector3.Lerp(transform.position, _target.position + _offset, Time.deltaTime * _speed);
    }
  }
}