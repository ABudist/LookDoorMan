using UnityEngine;

namespace Player
{
  public class PlayerAnimator : MonoBehaviour
  {
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerAttack _playerAttack;
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Awake()
    {
      _playerAttack.OnAttack += PlayAttackAnimation;
    }

    private void Update()
    {
      _animator.SetFloat(Speed, _playerMover.NormalizedSpeed);
    }

    private void PlayAttackAnimation()
    {
      _animator.SetTrigger(Attack);
    }
  }
}