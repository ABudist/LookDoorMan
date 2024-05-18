using System.Collections;
using System.Collections.Generic;
using Enemies;
using MapGeneration;
using Player;
using Props;
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
  [SerializeField] private PropsFactory _propsFactory;
  [SerializeField] private BlackScreen _blackScreen;
  [SerializeField] private BackgroundEffect _background;

  public void Start()
  {
    _blackScreen.From(null);
    
    Application.targetFrameRate = 50;

    int rows = Random.Range(5, 10);
    int columns = Random.Range(5, 10);
    
    int enemiesCount = (rows + columns) / 2;
    int hitsToPlayerDeathFromOneEnemy = 3;
    float enemyDamage = 30;
    float enemyHealth = 100;
    float playerHealth = enemyDamage * hitsToPlayerDeathFromOneEnemy * enemiesCount;
    float playerDamage = enemyHealth / (hitsToPlayerDeathFromOneEnemy - 1);
    
    LevelData data = _mapGenerator.GenerateMap(rows, columns, enemiesCount);
    
    CreateProps(data, enemiesCount, enemiesCount);
    
    data.Floor.Bake();
    
    _background.Create(data.Floor.transform);
    
    Player.Player player = _playerFactory.CreatePlayer(data.PlayerSpawnPosition, _joystick, _attackButton, playerHealth, playerDamage);
    player.OnDead += PlayerDead;
    
    _cameraFollower.SetTarget(player.transform);
    _cameraFollower.SetBorders(data.Floor.transform);
    
    _enemyFactory.SpawnEnemies(data, enemiesCount, player, enemyDamage, enemyHealth);

    data.Exit.OnPlayerExit += Restart;
  }

  private void Restart()
  {
    SoundManager.SoundManager.Instance.PlayOneShot(SoundManager.SoundManager.Instance.Win);
    
    _blackScreen.To(() =>
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    });
  }

  private void CreateProps(LevelData levelData, int propsCount, int coinsCount)
  {
    List<Vector3> targetPositions = new List<Vector3>(levelData.PositionsForProps);

    for (int i = 0; i < propsCount; i++)
    {
      int indx = Random.Range(0, targetPositions.Count);
      
      _propsFactory.Spawn(targetPositions[indx]);
      
      targetPositions.RemoveAt(indx);
    }

    for (int i = 0; i < coinsCount; i++)
    {
      int indx = Random.Range(0, targetPositions.Count);

      _propsFactory.SpawnCoin(targetPositions[indx]);
      
      targetPositions.RemoveAt(indx);
    }
  }

  private void PlayerDead()
  {
    StartCoroutine(DelayAndRestart());
  }

  private IEnumerator DelayAndRestart()
  {
    yield return new WaitForSeconds(3);
    
    Restart();
  }
}
