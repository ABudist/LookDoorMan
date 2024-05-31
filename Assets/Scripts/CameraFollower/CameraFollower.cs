using UnityEngine;

namespace CameraFollower
{
  public class CameraFollower : MonoBehaviour
  {
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed;

    private Vector3 _minPos;
    private Vector3 _maxPos;
    private float _factorX = 0.9f;
    private float _factorZ = 0.8f;
    
    private Transform _target;

    public void SetTarget(Transform target)
    {
      _target = target;
      transform.position = _target.position + _offset;
    }

    public void SetBorders(Transform floor)
    {
      _minPos = new Vector3(floor.localScale.x / -2 * _factorX, 0, floor.localScale.z / -2 * _factorZ + _offset.z);
      _maxPos = new Vector3(-_minPos.x, 0, floor.localScale.z / 2 * _factorZ + _offset.z);
    }

    private void Update()
    {
      transform.position = Vector3.Lerp(transform.position, _target.position + _offset, Time.deltaTime * _speed);

      transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minPos.x, _maxPos.x), transform.position.y,
        Mathf.Clamp(transform.position.z, _minPos.z, _maxPos.z));
    }
  }
}