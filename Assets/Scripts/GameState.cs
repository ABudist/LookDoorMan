using System.Collections;
using Enemies;
using MapGeneration;
using Player;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    int enemiesCount = 6;
    int hitsToPlayerDeathFromOneEnemy = 3;
    float enemyDamage = 30;
    float enemyHealth = 100;
    float playerHealth = enemyDamage * hitsToPlayerDeathFromOneEnemy * enemiesCount;
    float playerDamage = enemyHealth / (hitsToPlayerDeathFromOneEnemy - 1);
    
    LevelData data = _mapGenerator.GenerateMap(3, 8, enemiesCount);
      
    Player.Player player = _playerFactory.CreatePlayer(data.PlayerSpawnPosition, _joystick, _attackButton, playerHealth, playerDamage);

    player.OnDead += PlayerDead;
    
    _cameraFollower.SetTarget(player.transform);
    
    _enemyFactory.SpawnEnemies(data, enemiesCount, player, enemyDamage, enemyHealth);
  }

  private void PlayerDead()
  {
    StartCoroutine(DelayAndRestart());
  }

  private IEnumerator DelayAndRestart()
  {
    yield return new WaitForSeconds(3);
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
