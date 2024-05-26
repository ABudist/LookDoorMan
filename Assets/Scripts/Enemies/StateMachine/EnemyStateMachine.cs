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

    private PatrolState _patrolState;
    private FollowToPlayerState _followToPlayerStateState;
    private Player.Player _player;
    private AttackState _attackState;
    private DeadState _deadState;

    public void Construct(Player.Player player)
    {
      _player = player;
    }
    
    public void Start()
    {
      _followToPlayerStateState = new FollowToPlayerState();
      _patrolState = new PatrolState();
      _attackState = new AttackState();
      _deadState = new DeadState();
      
      _deadState.Construct(_enemy);
      _followToPlayerStateState.Construct(this, _attackState, _mover, _player, _patrolState);
      _patrolState.Construct(this, _followToPlayerStateState, _mover, _enemyVisionArea, transform.position,
        _enemy.TargetPatrolPosition, GetComponent<Health.Health>(), _player);
      _attackState.Construct(this, _mover, GetComponent<EnemyAttack>(), _player, _deadState, GetComponent<Health.Health>(), _patrolState);
      ChangeState(_patrolState);
    }

    public void ChangeState(IState next)
    {
      if (CurrentState != null)
      {
        if (CurrentState == next)
        {
          return;
        }
        
        CurrentState.Exit();
      }
        
      next.Enter();
      CurrentState = next;
    }
  }
}