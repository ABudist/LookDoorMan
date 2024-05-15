using UnityEngine;

namespace CarSelector
{
  public class PodiumRotator : MonoBehaviour
  {
    public float Speed
    {
      get
      {
        return _speed;
      }
      set
      {
        _speed = value;
      }
    }
    
    [SerializeField] private float _speed;
    private void Update()
    {
      transform.Rotate(transform.up, _speed, Space.World);
    }
  }
}