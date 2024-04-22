using System;
using System.Collections.Generic;
using Enemies;
using UI;
using UnityEngine;
using Utils;

namespace Player
{
  public class PlayerAttack : MonoBehaviour
  {
    public event Action OnAttack;
    public event Action OnAttacked;
    public event Action OnStartAttack;

    [SerializeField] private float _cooldown;
    private float _damage;

    private List<Enemy> _enemies = new List<Enemy>(5);
    
    private float _attackTimer;
    private bool _active = true;

    public void Construct(Button attackButton, float damage)
    {
      attackButton.OnClick += TryAttack;
      _damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out Enemy enemy))
      {
        if (!_enemies.Contains(enemy))
        {
          _enemies.Add(enemy);
        }
      }
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.TryGetComponent(out Enemy enemy))
      {
        if (_enemies.Contains(enemy))
        {
          _enemies.Remove(enemy);
        }
      }
    }

    public void StartAttack()
    {
      OnStartAttack?.Invoke();
    }

    public void EndAttack()
    {
      for (int i = 0; i < _enemies.Count; i++)
      {
        if (_enemies[i] == null)
        {
          _enemies.RemoveAt(i);

          i = -1;
        }
      }
      
      foreach (Enemy enemy in _enemies)
      {
        if (Vector3.Distance(transform.position, enemy.transform.position) < Constants.DistToEnemyForDamage &&
            Vector3.Angle((enemy.transform.position - transform.position).normalized, transform.forward) < 50)
        {
          enemy.GetComponent<Health.Health>().TakeDamage(_damage);
        }  
      }
      
      OnAttacked?.Invoke();
    }

    private void Update()
    {
      _attackTimer -= Time.deltaTime;
    }

    public void SetInactive()
    {
      _active = false;
    }

    private void TryAttack()
    {
      if(!_active)
        return;
      
      if (_attackTimer <= 0)
      {
        _attackTimer = _cooldown;

        OnAttack?.Invoke();
      }
    }
  }
}