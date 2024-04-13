using System;
using UI;
using UnityEngine;

namespace Player
{
  public class PlayerAttack : MonoBehaviour
  {
    public event Action OnAttack;
    public event Action OnAttacked;
    public event Action OnStartAttack;

    [SerializeField] private float _cooldown;

    private float _attackTimer;

    public void Construct(Button attackButton)
    {
      attackButton.OnClick += TryAttack;
    }

    public void StartAttack()
    {
      OnStartAttack?.Invoke();
    }

    public void EndAttack()
    {
      OnAttacked?.Invoke();
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