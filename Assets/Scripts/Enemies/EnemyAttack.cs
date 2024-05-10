using System;
using UnityEngine;
using Utils;

namespace Enemies
{
  public class EnemyAttack : MonoBehaviour
  {
    public bool IsReady => _ready;
    
    public event Action OnStartAttack;
    public event Action OnEndAttack;

    [SerializeField] private float _cooldown;
    private float _damage;
    private float _timer;

    private bool _ready = true;
    private Health.Health _playerHealth;

    public void Construct(Health.Health playerHealth, float damage)
    {
      _playerHealth = playerHealth;
      _damage = damage;
    }
    
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
      if (Vector3.Distance(_playerHealth.transform.position, transform.position) < Constants.DistToInteract &&
          Vector3.Angle(transform.forward, (_playerHealth.transform.position - transform.position).normalized) < 30) 
      {
        _playerHealth.TakeDamage(_damage);
      }
      
      OnEndAttack?.Invoke();
    }
  }
}