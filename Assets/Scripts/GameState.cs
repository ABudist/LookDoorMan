using MapGeneration;
using Player;
using UnityEngine;

public class GameState : MonoBehaviour
{
  [SerializeField] private MapGenerator _mapGenerator;
  [SerializeField] private PlayerFactory _playerFactory;
  [SerializeField] private CameraFollower.CameraFollower _cameraFollower;
  [SerializeField] private Joystick _joystick;

  public void Start()
  {
    LevelData data = _mapGenerator.GenerateMap(4, 8);
    Player.Player player = _playerFactory.CreatePlayer(data.PlayerSpawnPosition);
    player.GetComponent<PlayerMover>().Construct(_joystick);
    _cameraFollower.SetTarget(player.transform);
  }
}
