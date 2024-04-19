using UnityEngine;

namespace Enemies
{
  [RequireComponent(typeof(Animator), typeof(EnemyMover), typeof(EnemyAttack))]
  public class EnemyAnimator : MonoBehaviour
  {
    private EnemyMover _mover => GetComponent<EnemyMover>();
    private Animator _animator => GetComponent<Animator>();
    private EnemyAttack _enemyAttack => GetComponent<EnemyAttack>();
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Awake()
    {
      _enemyAttack.OnStartAttack += PlayAttack;
    }

    private void Update()
    {
      _animator.SetFloat(Speed, _mover.SpeedNormalized);
    }

    private void PlayAttack()
    {
      _animator.SetTrigger(Attack);
    }
  }
}