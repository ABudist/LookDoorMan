using Enemies.StateMachine;
using UnityEngine;

namespace Enemies
{
  public class Enemy : MonoBehaviour
  {
    public Vector3 TargetPatrolPosition { get; private set; }
    [SerializeField] private EnemyStateMachine _enemyStateMachine;
    [SerializeField] private EnemyVisionArea _enemyVisionArea;
    private EnemyMover _enemyMover => GetComponent<EnemyMover>();
    private EnemyAttack _enemyAttack => GetComponent<EnemyAttack>();
    private Health.Health _health => GetComponent<Health.Health>();
    
    public void Construct(Vector3 targetPatrolPosition, Player.Player player, float damage, float health)
    {
      TargetPatrolPosition = targetPatrolPosition;
      _enemyStateMachine.Construct(player);
      _enemyAttack.Construct(player.GetComponent<Health.Health>(), damage);
      _health.Construct(health);
    }

    public void SetInactive()
    {
      GetComponent<CapsuleCollider>().enabled = false;
      _enemyAttack.enabled = false;
      _enemyMover.SetInactive();
    }
  }
}