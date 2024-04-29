using System;
using UnityEngine;

namespace Utils
{
  public class Trigger : MonoBehaviour
  {
    public Action<GameObject> OnEnter;
    public Action<GameObject> OnExit;
    
    private void OnTriggerEnter(Collider other)
    {
      OnEnter?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
      OnExit?.Invoke(other.gameObject);
    }
  }
}