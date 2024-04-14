using UnityEngine;

namespace Enemies.StateMachine
{
  [RequireComponent(typeof(EnemyMover), typeof(Enemy))]
  public class EnemyStateMachine : MonoBehaviour
  {
    public IState CurrentState { get; private set; }

    private EnemyMover _mover => GetComponent<EnemyMover>();
    private Enemy _enemy => GetComponent<Enemy>();

    private Patrol _patrolState;
    
    public void Start()
    {
      _patrolState = new Patrol(this, _mover, transform.position, _enemy.TargetPatrolPosition);
      ChangeState(_patrolState);
    }

    public void ChangeState(IState next)
    {
      if (CurrentState != null)
      {
        CurrentState.Exit();
      }
      
      next.Enter();
    }
  }
}