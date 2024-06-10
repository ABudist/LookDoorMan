using UnityEngine;

namespace Enemies
{
  public class EnemyAttackTrail : MonoBehaviour
  {
    [SerializeField] private EnemyAttack _enemyAttack;
    [SerializeField] private TrailRenderer _trail;

    private void Awake()
    {
      _enemyAttack.OnStartAttack += EnableTrail;
      _enemyAttack.OnEndAttack += DisableTrail;
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