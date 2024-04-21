using MapGeneration;
using UnityEngine;

namespace Enemies
{
  public class EnemyFactory : MonoBehaviour
  {
    [SerializeField] private Enemy[] _enemyPrefabs;

    public void SpawnEnemies(LevelData levelData, int count, Player.Player player)
    {
      for (int i = 0; i < count; i++)
      {
        if (levelData.EnemiesSpawnPosition[i] == Vector3.zero)
        {
          break;
        } 
      
        Enemy enemy = Spawn(levelData.EnemiesSpawnPosition[i]);
        enemy.Construct(levelData.EnemiesTargetPosition[i], player);
      }
    }
    
    private Enemy Spawn(Vector3 at)
    {
      return Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)], at, Quaternion.identity);
    }
  }
}