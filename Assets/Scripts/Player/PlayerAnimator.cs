using UnityEngine;

namespace Player
{
  public class PlayerAnimator : MonoBehaviour
  {
    private Player _player => GetComponent<Player>();
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerAttack _playerAttack;
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Dead = Animator.StringToHash("Dead");

    private void Awake()
    {
      _playerAttack.OnAttack += PlayAttackAnimation;
      _player.OnDead += PlayDeadAnim;
    }

    private void Update()
    {
      _animator.SetFloat(Speed, _playerMover.NormalizedSpeed);
    }

    private void PlayDeadAnim()
    {
      _animator.SetTrigger(Dead);
    }

    private void PlayAttackAnimation()
    {
      _animator.SetTrigger(Attack);
    }
  }
}