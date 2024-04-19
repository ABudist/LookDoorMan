using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
  [RequireComponent(typeof(NavMeshAgent))]
  public class EnemyMover : MonoBehaviour
  {
    public Vector3 Target { get; private set; }
    public float SpeedNormalized => _agent.velocity.magnitude / RunSpeed;

    [SerializeField] private float RunSpeed;
    [SerializeField] private float WalkSpeed;
      
    private NavMeshAgent _agent => GetComponent<NavMeshAgent>();

    public void WalkTo(Vector3 target)
    {
      _agent.speed = WalkSpeed;
      
      Target = target;
      _agent.destination = target;
      LookTo((target - transform.position).normalized);
    }
    
    public void RunTo(Vector3 target)
    {
      _agent.speed = RunSpeed;
      
      Target = target;
      _agent.destination = target;
    }

    public void LookTo(Vector3 dir)
    {
      _agent.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
      _agent.transform.rotation = Quaternion.Euler(0, _agent.transform.rotation.eulerAngles.y, 0);
    }
    
    public void Stop()
    {
      _agent.ResetPath();
    }
  }
}