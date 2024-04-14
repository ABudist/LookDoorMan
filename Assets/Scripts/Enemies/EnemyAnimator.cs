using UnityEngine;

namespace Enemies
{
  [RequireComponent(typeof(Animator))]
  public class EnemyAnimator : MonoBehaviour
  {
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private Animator _animator;
    
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Update()
    {
      _animator.SetFloat(Speed, _mover.SpeedNormalized);
    }
  }
}