using MapGeneration;
using Player;
using UI;
using UnityEngine;

public class GameState : MonoBehaviour
{
  [SerializeField] private MapGenerator _mapGenerator;
  [SerializeField] private PlayerFactory _playerFactory;
  [SerializeField] private CameraFollower.CameraFollower _cameraFollower;
  [SerializeField] private Joystick _joystick;
  [SerializeField] private Button _attackButton;

  public void Start()
  {
    LevelData data = _mapGenerator.GenerateMap(4, 8);
    Player.Player player = _playerFactory.CreatePlayer(data.PlayerSpawnPosition);
    player.GetComponent<PlayerMover>().Construct(_joystick);
    player.GetComponent<PlayerAttack>().Construct(_attackButton);
    _cameraFollower.SetTarget(player.transform);
  }
}
