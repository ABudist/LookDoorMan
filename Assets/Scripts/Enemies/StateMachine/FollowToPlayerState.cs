using System.Collections;
using UnityEngine;

namespace Enemies.StateMachine
{
  public class FollowToPlayerState : IState
  {
    private Player.Player _player;
    private EnemyStateMachine _enemyStateMachine;
    private EnemyMover _enemyMover;
    private Coroutine _coroutine;
    private AttackState _attackState;

    private const float MIN_DIST_TO_PLAYER = 0.7f;
    
    public void Construct(EnemyStateMachine enemyStateMachine, AttackState attackState, EnemyMover enemyMover, Player.Player player)
    {
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
      _enemyStateMachine.StopCoroutine(_coroutine);
    }

    private IEnumerator Follow()
    {
      while (true)
      {
        _enemyMover.RunTo(_player.transform.position);

        if (Vector3.Distance(_enemyMover.transform.position, _player.transform.position) < MIN_DIST_TO_PLAYER)
        {
          _enemyStateMachine.ChangeState(_attackState);
        }
        
        yield return null;
      }
    }
  }
}