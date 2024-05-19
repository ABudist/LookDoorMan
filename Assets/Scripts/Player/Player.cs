using System;
using UnityEngine;

namespace Player
{
  public class Player : MonoBehaviour
  {
    public event Action OnDead;
    
    private Health.Health _health;
    private PlayerMover _playerMover;
    private PlayerAttack _playerAttack;
    private PlayerSpawnEffect _playerSpawnEffect => GetComponent<PlayerSpawnEffect>();

    public void Construct(Health.Health health, PlayerMover playerMover, PlayerAttack playerAttack)
    {
      Vibration.Init();
      
      _playerAttack = playerAttack;
      _playerMover = playerMover;
      _health = health;
      _health.OnEnd += Dead;
      _health.OnChanged += Vibrate;
      
      _playerSpawnEffect.Show();
    }

    private void Dead()
    {
      _playerMover.SetInactive();
      _playerAttack.SetInactive();
      
      OnDead?.Invoke();

      _health.OnEnd -= Dead;
      _health.OnChanged -= Vibrate;
    }

    private void Vibrate()
    {
      Vibration.VibrateIOS(ImpactFeedbackStyle.Light);
    }
  }
}