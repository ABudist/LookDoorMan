using UnityEngine;

namespace MapGeneration
{
  public class LevelData
  {
    public Exit Exit { get; private set; }
    public Floor Floor { get; private set; }
    public Vector3 PlayerSpawnPosition { get; private set; }
    public Vector3[] EnemiesSpawnPosition { get; private set; }
    public Vector3[] EnemiesTargetPosition { get; private set; }
    public Vector3[] PositionsForProps { get; private set; }

    public LevelData(Vector3 playerSpawnPosition, Vector3[] enemiesSpawnPositions, Vector3[] enemiesTargetPositions, Vector3[] positionsForProps, Floor floor,
      Exit exit)
    {
      PlayerSpawnPosition = playerSpawnPosition;
      EnemiesSpawnPosition = enemiesSpawnPositions;
      EnemiesTargetPosition = enemiesTargetPositions;
      PositionsForProps = positionsForProps;
      Floor = floor;
      Exit = exit;
    }
  }
}