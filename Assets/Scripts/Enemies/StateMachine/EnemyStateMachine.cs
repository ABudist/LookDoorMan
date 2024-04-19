using UnityEngine;

namespace Enemies.StateMachine
{
  [RequireComponent(typeof(EnemyMover), typeof(Enemy))]
  public class EnemyStateMachine : MonoBehaviour
  {
    public IState CurrentState { get; private set; }

    [SerializeField] private EnemyVisionArea _enemyVisionArea;
    
    private EnemyMover _mover => GetComponent<EnemyMover>();
    private Enemy _enemy => GetComponent<Enemy>();         

    private PatrolState _patrolStateState;
    private FollowToPlayerState _followToPlayerStateState;
    private Player.Player _player;
    private AttackState _attackState;

    public void Construct(Player.Player player)
    {
      _player = player;
    }
    
    public void Start()
    {
      _followToPlayerStateState = new FollowToPlayerState();
      _patrolStateState = new PatrolState();
      _attackState = new AttackState();
      
      _followToPlayerStateState.Construct(this, _attackState, _mover, _player);
      _patrolStateState.Construct(this, _followToPlayerStateState, _mover, _enemyVisionArea, transform.position, _enemy.TargetPatrolPosition);
      _attackState.Construct(this, _mover, GetComponent<EnemyAttack>(), _player);
      ChangeState(_patrolStateState);
    }

    public void ChangeState(IState next)
    {
      if (CurrentState != null)
      {
        CurrentState.Exit();
      }

      next.Enter();
      CurrentState = next;
    }
  }
}