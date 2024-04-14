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
    LevelData data = _mapGenerator.GenerateMap(6, 8, 6);
    Player.Player player = _playerFactory.CreatePlayer(data.PlayerSpawnPosition);
    player.GetComponent<PlayerMover>().Construct(_joystick);
    player.GetComponent<PlayerAttack>().Construct(_attackButton);
    _cameraFollower.SetTarget(player.transform);

    for (int i = 0; i < 6; i++)
    {
      if (data.EnemiesSpawnPosition[i] == Vector3.zero)
      {
        break;
      } 
      
      Enemy enemy =  _enemyFactory.Spawn(data.EnemiesSpawnPosition[i]);
      enemy.Construct(data.EnemiesTargetPosition[i]);
    }
    
  }
}
