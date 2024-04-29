using UnityEngine;

namespace MapGeneration
{
  public class LevelData
  {
    public Vector3 PlayerSpawnPosition { get; private set; }
    public Vector3[] EnemiesSpawnPosition { get; private set; }
    public Vector3[] EnemiesTargetPosition { get; private set; }
    public Vector3[] PositionsForProps { get; private set; }

    public LevelData(Vector3 playerSpawnPosition, Vector3[] enemiesSpawnPositions, Vector3[] enemiesTargetPositions, Vector3[] positionsForProps)
    {
      PlayerSpawnPosition = playerSpawnPosition;
      EnemiesSpawnPosition = enemiesSpawnPositions;
      EnemiesTargetPosition = enemiesTargetPositions;
      PositionsForProps = positionsForProps;
    }
  }
}