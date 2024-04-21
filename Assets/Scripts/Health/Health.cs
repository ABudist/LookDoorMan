using System;
using UnityEngine;

namespace Health
{
  public class Health : MonoBehaviour
  {
    public event Action OnEnd;
    public event Action OnChanged;
    public float Value { get; private set; }
    public float NormalizedValue => Value / _maximum;
    
    private float _maximum;

    public void Construct(float maximum)
    {
      _maximum = maximum;
      Value = maximum;
    }

    public void TakeDamage(float damage)
    {
      if(Value <= 0)
        return;
      
      Value -= damage;

      OnChanged?.Invoke();
      
      if (Value <= 0)
      {
        OnEnd?.Invoke();
      }
    }
  }
}