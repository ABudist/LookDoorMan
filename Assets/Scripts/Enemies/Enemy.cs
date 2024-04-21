using Enemies.StateMachine;
using UnityEngine;

namespace Enemies
{
  public class Enemy : MonoBehaviour
  {
    public Vector3 TargetPatrolPosition { get; private set; }
    [SerializeField] private EnemyStateMachine _enemyStateMachine;
    [SerializeField] private EnemyVisionArea _enemyVisionArea;
    private EnemyAttack _enemyAttack => GetComponent<EnemyAttack>();
    private Health.Health _health => GetComponent<Health.Health>();
    
    public void Construct(Vector3 targetPatrolPosition, Player.Player player)
    {
      TargetPatrolPosition = targetPatrolPosition;
      _enemyStateMachine.Construct(player);
      _enemyAttack.Construct(player.GetComponent<Health.Health>());
      _health.Construct(100);
    }

    public void SetInactive()
    {
      GetComponent<CapsuleCollider>().enabled = false;
    }
  }
}