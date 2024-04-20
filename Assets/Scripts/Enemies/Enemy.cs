using Enemies.StateMachine;
using Player;
using UnityEngine;

namespace Enemies
{
  public class Enemy : MonoBehaviour
  {
    public Vector3 TargetPatrolPosition { get; private set; }
    [SerializeField] private EnemyStateMachine _enemyStateMachine;
    [SerializeField] private EnemyVisionArea _enemyVisionArea;
    private EnemyAttack _enemyAttack => GetComponent<EnemyAttack>();
    
    public void Construct(Vector3 targetPatrolPosition, Player.Player player)
    {
      TargetPatrolPosition = targetPatrolPosition;
      _enemyStateMachine.Construct(player);
      _enemyAttack.Construct(player.GetComponent<PlayerHealth>());
    }
  }
}