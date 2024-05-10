using System.Collections;
using UnityEngine;
using Utils;

namespace Enemies.StateMachine
{
  public class PatrolState : IState
  {
    private EnemyMover _mover;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private Vector3 _currentTargetPos;
    private EnemyStateMachine _enemyStateMachine;
    private Health.Health _health;
    private EnemyVisionArea _enemyVisionArea;
    private FollowToPlayerState _followToPlayerState;
    private Player.Player _player;

    private Coroutine _coroutine;
    private Coroutine _coroutine2;

    public void Construct(EnemyStateMachine enemyStateMachine, FollowToPlayerState followToPlayerState, EnemyMover mover, EnemyVisionArea enemyVisionArea, 
      Vector3 startPos, Vector3 endPos, Health.Health health, Player.Player player)
    {
      _player = player;
      _health = health;
      _followToPlayerState = followToPlayerState;
      _enemyVisionArea = enemyVisionArea;
      _enemyStateMachine = enemyStateMachine;
      _mover = mover;
      _startPos = startPos;
      _endPos = endPos;
        
      health.OnChanged += Attacked;
    }
    
    public void Enter()
    {
      _enemyVisionArea.OnPlayerEnter += PlayerEnter;
      _coroutine = _enemyStateMachine.StartCoroutine(PatrolCoroutine());
      _coroutine2 = _enemyStateMachine.StartCoroutine(CheckPlayerDistance());
    }

    public void Exit()
    {
      _enemyVisionArea.Hide();
      _enemyVisionArea.OnPlayerEnter -= PlayerEnter;
      _health.OnChanged -= Attacked;
      
      _enemyStateMachine.StopCoroutine(_coroutine);
      _enemyStateMachine.StopCoroutine(_coroutine2);
    }

    private IEnumerator PatrolCoroutine()
    {
      if (_currentTargetPos == Vector3.zero)
      {
        _currentTargetPos = _endPos;
      }
      
      while (true)
      {
        float distToTarget = Vector3.Distance(_mover.transform.position, _currentTargetPos);
          
        if (distToTarget > Constants.DistToInteract && _mover.Target != _currentTargetPos)
        {
          _mover.WalkTo(_currentTargetPos);
        }
        else if(distToTarget < Constants.DistToInteract &&  _mover.Target == _currentTargetPos)
        {
          yield return new WaitForSeconds(Random.Range(1, 4));

          ChangeDirection();
        }
        
        yield return null;
      }
    }

    private void ChangeDirection()
    {
      if (_currentTargetPos == _endPos)
        _currentTargetPos = _startPos;
      else
        _currentTargetPos = _endPos;
    }

    private IEnumerator CheckPlayerDistance()
    {
      while (true)
      {
        if (Vector3.Distance(_mover.transform.position, _player.transform.position) < Constants.DistToInteract)
        {
          _enemyStateMachine.ChangeState(_followToPlayerState);
        }
        
        yield return null;
      }
    }

    private void PlayerEnter(Player.Player obj)
    {
      _enemyStateMachine.ChangeState(_followToPlayerState);
    }

    private void Attacked()
    {
      _enemyStateMachine.ChangeState(_followToPlayerState);
    }
  }
}