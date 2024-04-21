using UnityEngine;

namespace Enemies
{
  [RequireComponent(typeof(Animator), typeof(EnemyMover), typeof(EnemyAttack))]
  public class EnemyAnimator : MonoBehaviour
  {
    private EnemyMover _mover => GetComponent<EnemyMover>();
    private Animator _animator => GetComponent<Animator>();
    private EnemyAttack _enemyAttack => GetComponent<EnemyAttack>();
    private Health.Health _health => GetComponent<Health.Health>();
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Dead = Animator.StringToHash("Dead");

    private void Awake()
    {
      _enemyAttack.OnStartAttack += PlayAttack;
      _health.OnEnd += PlayDead;
    }

    private void Update()
    {
      _animator.SetFloat(Speed, _mover.SpeedNormalized);
    }

    private void PlayDead()
    {
      _animator.SetTrigger(Dead);
    }

    private void PlayAttack()
    {
      _animator.SetTrigger(Attack);
    }
  }
}