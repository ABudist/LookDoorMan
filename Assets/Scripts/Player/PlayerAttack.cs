using System;
using UI;
using UnityEngine;

namespace Player
{
  public class PlayerAttack : MonoBehaviour
  {
    public event Action OnAttack;
    
    [SerializeField] private float _cooldown;

    private float _attackTimer;

    public void Construct(Button attackButton)
    {
      attackButton.OnClick += TryAttack;
    }

    private void Update()
    {
      _attackTimer -= Time.deltaTime;
    }

    private void TryAttack()
    {
      if (_attackTimer <= 0)
      {
        _attackTimer = _cooldown;

        OnAttack?.Invoke();
      }
    }
  }
}