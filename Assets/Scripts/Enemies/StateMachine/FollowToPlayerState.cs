using System.Collections;
using UnityEngine;
using Utils;

namespace Enemies.StateMachine
{
  public class FollowToPlayerState : IState
  {
    private Player.Player _player;
    private EnemyStateMachine _enemyStateMachine;
    private EnemyMover _enemyMover;
    private Coroutine _coroutine;
    private AttackState _attackState;
    private PatrolState _patrolState;

    public void Construct(EnemyStateMachine enemyStateMachine, AttackState attackState, EnemyMover enemyMover, Player.Player player, PatrolState patrolState)
    {
      _patrolState = patrolState;
      _attackState = attackState;
      _player = player;
      _enemyStateMachine = enemyStateMachine;
      _enemyMover = enemyMover;
    }
    
    public void Enter()
    {
      _coroutine = _enemyStateMachine.StartCoroutine(Follow());
    }

    public void Exit()
    {
      if(_coroutine != null)
        _enemyStateMachine.StopCoroutine(_coroutine);
    }

    private IEnumerator Follow()
    {
      while (true)
      {
        if (!_player.Active)
        {
          _enemyStateMachine.ChangeState(_patrolState);
          yield break;
        }
        
        _enemyMover.RunTo(_player.transform.position);

        if (Vector3.Distance(_enemyMover.transform.position, _player.transform.position) < Constants.DistToInteract)
        {
          _enemyStateMachine.ChangeState(_attackState);
          yield break;
        }
        
        yield return null;
      }
    }
  }
}