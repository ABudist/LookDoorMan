using Enemies;
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
  [SerializeField] private EnemyFactory _enemyFactory;

  public void Start()
  {
    Application.targetFrameRate = 50;
    
    LevelData data = _mapGenerator.GenerateMap(3, 3, 1);
    
    Player.Player player = _playerFactory.CreatePlayer(data.PlayerSpawnPosition, _joystick, _attackButton);

    _cameraFollower.SetTarget(player.transform);
    
    _enemyFactory.SpawnEnemies(data, 1, player);
    
   
  }
}
