using System;
using UnityEngine;

namespace Player
{
  public class PlayerAnimator : MonoBehaviour
  {
    public event Action OnFootstep; 
    
    private Player _player => GetComponent<Player>();
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerAttack _playerAttack;
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Dead = Animator.StringToHash("Dead");
    private static readonly int ResurrectHash = Animator.StringToHash("Resurrect");

    private void Awake()
    {
      _playerAttack.OnAttack += PlayAttackAnimation;
      _player.OnDead += PlayDeadAnim;
      _player.OnResurrected += Resurrect;
    }

    private void Resurrect()
    {
      _animator.SetTrigger(ResurrectHash);
    }

    private void Update()
    {
      _animator.SetFloat(Speed, _playerMover.NormalizedSpeed);
    }

    public void Footstep()
    {
      OnFootstep?.Invoke();
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