using System;
using UnityEngine;

namespace Player
{
  public class Player : MonoBehaviour
  {
    public bool Active { get; private set; }
    public event Action OnDead;
    public event Action OnResurrected;
    
    private Health.Health _health;
    private PlayerMover _playerMover;
    private PlayerAttack _playerAttack;
    private PlayerSpawnEffect _playerSpawnEffect => GetComponent<PlayerSpawnEffect>();

    public void Construct(GameState gameState, Health.Health health, PlayerMover playerMover, PlayerAttack playerAttack)
    {
      Vibration.Init();
      
      _playerAttack = playerAttack;
      _playerMover = playerMover;
      _health = health;
      _health.OnEnd += Dead;
      _health.OnChanged += Vibrate;

      gameState.OnGameStarted += () =>
      {
        _playerSpawnEffect.Show();
        Active = true;
      };
    }

    public void Resurrect()
    {
      _playerMover.SetActive(true);
      _playerAttack.SetActive(true);

      _health.Restore();
      
      Active = true;
      
      _playerSpawnEffect.Show();
      
      OnResurrected?.Invoke();
    }

    public void Heal()
    {
      _health.Restore();
    }
    
    private void OnDestroy()
    {
      if (_health != null)
      {
        _health.OnEnd -= Dead;
        _health.OnChanged -= Vibrate;
      }
    }

    private void Dead()
    {
      _playerMover.SetActive(false);
      _playerAttack.SetActive(false);

      Active = false;
      
      OnDead?.Invoke();
    }

    private void Vibrate()
    {
      #if !UNITY_EDITOR
      Vibration.VibrateIOS(ImpactFeedbackStyle.Light);
      #endif
    }
  }
}