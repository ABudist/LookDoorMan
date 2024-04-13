using System;
using UnityEngine;

namespace Player
{
  public class PlayerAttackTrail : MonoBehaviour
  {
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private TrailRenderer _trail;

    private void Awake()
    {
      _playerAttack.OnStartAttack += EnableTrail;
      _playerAttack.OnAttacked += DisableTrail;
    }

    private void Start()
    {
      _trail.enabled = false;
    }

    private void DisableTrail()
    {
      _trail.Clear();
      _trail.enabled = false;
    }

    private void EnableTrail()
    {
      _trail.enabled = true;
    }
  }
}