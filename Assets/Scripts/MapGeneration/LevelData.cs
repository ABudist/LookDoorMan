using UnityEngine;

namespace MapGeneration
{
  public class LevelData
  {
    public Vector3 PlayerSpawnPosition { get; private set; }

    public LevelData(Vector3 playerSpawnPosition)
    {
      PlayerSpawnPosition = playerSpawnPosition;
    }
  }
}