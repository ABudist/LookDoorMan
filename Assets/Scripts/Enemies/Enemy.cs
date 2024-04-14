using UnityEngine;

namespace Enemies
{
  public class Enemy : MonoBehaviour
  {
    public Vector3 TargetPatrolPosition { get; private set; }

    public void Construct(Vector3 targetPatrolPosition)
    {
      TargetPatrolPosition = targetPatrolPosition;
    }
  }
}