using System;
using System.Collections;
using UnityEngine;

namespace Enemies
{
  public class EnemyAttack : MonoBehaviour
  {
    public bool IsReady => _ready;
    
    public event Action OnStartAttack;
    public event Action OnEndAttack;
    
    [SerializeField] private float _cooldown;
    private float _timer;

    private bool _ready = true;

    private void Update()
    {
      if (!_ready)
      {
        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
          _ready = true;
          _timer = _cooldown;
        }
      }
    }

    public void TryAttackPlayer()
    {
      if(!_ready)
        return;

      _ready = false;
      
      OnStartAttack?.Invoke();
    }

    public void StartAttack()
    {
    }

    public void EndAttack()
    {
      OnEndAttack?.Invoke();
    }
  }
}