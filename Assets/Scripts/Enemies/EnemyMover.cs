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
      _agent.transform.rotation = Quaternion.LookRotation((target - transform.position).normalized);
    }
    
    public void RunTo(Vector3 target)
    {
      _agent.speed = RunSpeed;
      
      Target = target;
      _agent.destination = target;
      _agent.transform.rotation = Quaternion.LookRotation((target - transform.position).normalized);
    }
  }
}