using Camera;
using MapGeneration;
using Player;
using UnityEngine;

public class GameState : MonoBehaviour
{
  [SerializeField] private MapGenerator _mapGenerator;
  [SerializeField] private PlayerFactory _playerFactory;
  [SerializeField] private CameraFollower _cameraFollower;

  public void Start()
  {
    LevelData data = _mapGenerator.GenerateMap(4, 8);
    Player.Player player = _playerFactory.CreatePlayer(data.PlayerSpawnPosition);
    _cameraFollower.SetTarget(player.transform);
  }
}
