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
    private PatrolState _patrolState;

    public void Construct(EnemyStateMachine enemyStateMachine, EnemyMover enemyMover, EnemyAttack enemyAttack, Player.Player player,
      DeadState deadState, Health.Health enemyHealth, PatrolState patrolState)
    {
      _patrolState = patrolState;
      _deadState = deadState;
      enemyHealth.OnEnd += Dead;
      player.GetComponent<Health.Health>().OnEnd += ReturnToPatrol;
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
      if (_coroutine != null)
        _enemyStateMachine.StopCoroutine(_coroutine);
    }

    private IEnumerator AttackCor()
    {
      while (true)
      {
        if (!_player.Active)
        {
          ReturnToPatrol();
          yield break;
        }

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

    private void ReturnToPatrol()
    {
      _enemyMover.Stop();

      _enemyStateMachine.ChangeState(_patrolState);
    }

    private void Dead()
    {
      _enemyStateMachine.ChangeState(_deadState);
    }
  }
}