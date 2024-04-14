using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
  [RequireComponent(typeof(NavMeshAgent))]
  public class EnemyMover : MonoBehaviour
  {
    public Vector3 Target { get; private set; }
    private NavMeshAgent _agent => GetComponent<NavMeshAgent>();

    public void MoveTo(Vector3 target)
    {
      Target = target;
      _agent.destination = target;
      _agent.transform.rotation = Quaternion.LookRotation((target - transform.position).normalized);
    }
  }
}