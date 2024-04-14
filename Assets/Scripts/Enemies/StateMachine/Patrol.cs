using System.Collections;
using UnityEngine;

namespace Enemies.StateMachine
{
  public class Patrol : IState
  {
    private EnemyMover _mover;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private Vector3 _currentTargetPos;
    private EnemyStateMachine _enemyStateMachine;

    public Patrol(EnemyStateMachine enemyStateMachine, EnemyMover mover, Vector3 startPos, Vector3 endPos)
    {
      _enemyStateMachine = enemyStateMachine;
      _mover = mover;
      _startPos = startPos;
      _endPos = endPos;
    }
    
    public void Enter()
    {
      _enemyStateMachine.StartCoroutine(PatrolCoroutine());
    }

    public void Exit()
    {
      _enemyStateMachine.StopCoroutine(PatrolCoroutine());
    }

    private IEnumerator PatrolCoroutine()
    {
      if (_currentTargetPos == Vector3.zero)
      {
        _currentTargetPos = _endPos;
        _mover.transform.position = (_currentTargetPos - _mover.transform.position) * Random.Range(0.3f, 0.7f);
      }
      
      while (true)
      {
        float distToTarget = Vector3.Distance(_mover.transform.position, _currentTargetPos);
        
        if (distToTarget > 0.1f && _mover.Target != _currentTargetPos)
        {
          _mover.WalkTo(_currentTargetPos);
        }
        else if(distToTarget < 0.1f &&  _mover.Target == _currentTargetPos)
        {
          yield return new WaitForSeconds(Random.Range(1, 4));
          
          if (_currentTargetPos == _endPos)
            _currentTargetPos = _startPos;
          else
            _currentTargetPos = _endPos;
        }
        
        yield return null;
      }
    }
  }
}