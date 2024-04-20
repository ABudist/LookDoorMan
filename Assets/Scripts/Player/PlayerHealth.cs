using UnityEngine;

namespace Player
{
  [RequireComponent(typeof(Health.Health))]
  public class PlayerHealth : MonoBehaviour
  {
    [SerializeField] private float _size;
    private Health.Health _health => GetComponent<Health.Health>();

    private void Awake()
    {
      _health.Construct(_size);
      
      _health.OnChanged += Changed;
      _health.OnEnd += Dead;
    }

    private void OnDestroy()
    {
      _health.OnChanged -= Changed;
      _health.OnEnd -= Dead;
    }

    public void TakeDamage(float damage)
    {
      _health.TakeDamage(damage);
    }

    private void Changed()
    {
      
    }

    private void Dead()
    {
      
    }
  }
}