using System.Collections;
using UnityEngine;
using Utils;

namespace Enemies.StateMachine
{
  public class AttackState : IState
  {
    private EnemyStateMachine _enemyStateMachine;
    private EnemyMover _enemyMover;
    private Player.Player _player;
    private EnemyAttack _enemyAttack;

    private Coroutine _coroutine;
    private DeadState _deadState;

    public void Construct(EnemyStateMachine enemyStateMachine, EnemyMover enemyMover, EnemyAttack enemyAttack, Player.Player player, 
      DeadState deadState, Health.Health enemyHealth)
    {
      _deadState = deadState;
      enemyHealth.OnEnd += Dead;
      player.GetComponent<Health.Health>().OnEnd += Dead;
      _enemyAttack = enemyAttack;
      _player = player;
      _enemyMover = enemyMover;
      _enemyStateMachine = enemyStateMachine;
    }

    public void Enter()
    {
      _coroutine = _enemyStateMachine.StartCoroutine(AttackCor());
    }

    public void Exit()
    {
      _enemyStateMachine.StopCoroutine(_coroutine);
    }

    private IEnumerator AttackCor()
    {
      while (true)
      {
        if (!_enemyAttack.IsReady)
        {
          _enemyMover.LookTo((_player.transform.position - _enemyMover.transform.position).normalized);
          yield return null;
          continue;
        }

        if (Vector3.Distance(_enemyMover.transform.position, _player.transform.position) > Constants.DistToInteract)
        {
          _enemyMover.RunTo(_player.transform.position);
        }
        else
        {
          if (_enemyAttack.IsReady)
          {
            _enemyMover.Stop();
            _enemyMover.LookTo((_player.transform.position - _enemyMover.transform.position).normalized);
            _enemyAttack.TryAttackPlayer();
          }
        }

        yield return null;
      }
    }

    private void Dead()
    {
      _enemyStateMachine.ChangeState(_deadState);
    }
  }
}