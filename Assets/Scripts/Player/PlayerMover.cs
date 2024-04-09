using UnityEngine;

namespace Player
{
  public class PlayerMover : MonoBehaviour
  {
    [SerializeField] private float _speed;
    
    private Joystick _joystick;
    private Rigidbody _rigidbody;
    
    public void Construct(Joystick joystick)
    {
      _joystick = joystick;
      _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
      _rigidbody.position += new Vector3(_joystick.Horizontal, 0, _joystick.Vertical) * (Time.deltaTime * _speed);
      transform.rotation = Quaternion.LookRotation(new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y));
    }
  }
}