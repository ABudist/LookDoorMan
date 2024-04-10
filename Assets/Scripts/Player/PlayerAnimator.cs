using UnityEngine;

namespace Player
{
  public class PlayerAnimator : MonoBehaviour
  {
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMover _playerMover;
    
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Update()
    {
      _animator.SetFloat(Speed, _playerMover.NormalizedSpeed);
    }
  }
}