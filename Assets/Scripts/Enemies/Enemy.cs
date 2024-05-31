using System.Collections;
using DG.Tweening;
using Enemies.StateMachine;
using UnityEngine;

namespace Enemies
{
  public class Enemy : MonoBehaviour
  {
    public Vector3 TargetPatrolPosition { get; private set; }
    [SerializeField] private EnemyStateMachine _enemyStateMachine;
    [SerializeField] private EnemyVisionArea _enemyVisionArea;
    [SerializeField] private GameObject _hideEffectPrefab;
    private EnemyMover _enemyMover => GetComponent<EnemyMover>();
    private EnemyAttack _enemyAttack => GetComponent<EnemyAttack>();
    private Health.Health _health => GetComponent<Health.Health>();
    
    public void Construct(Vector3 targetPatrolPosition, Player.Player player, float damage, float health)
    {
      TargetPatrolPosition = targetPatrolPosition;
      _enemyStateMachine.Construct(player);
      _enemyAttack.Construct(player.GetComponent<Health.Health>(), damage);
      _health.Construct(health);
    }

    public void SetActive(bool active)
    {
      GetComponent<CapsuleCollider>().enabled = active;
      _enemyAttack.enabled = active;
      _enemyMover.SetActive(active);

      StartCoroutine(Hide());
    }

    private IEnumerator Hide()
    {
      yield return new WaitForSeconds(4);

      GameObject effect = Instantiate(_hideEffectPrefab);
      effect.transform.position = transform.position;
      
      yield return DOTween.Sequence().Append(transform.DOMoveY(-2, 2f)).WaitForCompletion();
      
      gameObject.SetActive(false);
    }
  }
}