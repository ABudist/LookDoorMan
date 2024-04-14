using UnityEngine;

namespace Enemies
{
  public class EnemyFactory : MonoBehaviour
  {
    [SerializeField] private Enemy[] _enemyPrefabs;

    public Enemy Spawn(Vector3 at)
    {
      return Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)], at, Quaternion.identity);
    }
  }
}