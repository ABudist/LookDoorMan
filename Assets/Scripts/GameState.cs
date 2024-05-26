using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Enemies;
using MapGeneration;
using Player;
using Props;
using UI;
using UI.GameOverScreen;
using UnityEngine;
using UnityEngine.SceneManagement;
using User;

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
  [SerializeField] private GameOver _gameOver;
  
  private Player.Player _player;

  public void Start()
  {
    _blackScreen.From(UpdateLevel);
    
    Application.targetFrameRate = 50;

    int rows = Mathf.Max(UserLevelData.CurrentLevel + Random.Range(-3, 2), 4, 10);
    int columns = Mathf.Max(UserLevelData.CurrentLevel + Random.Range(-3, 2), 4, 10);
    
    int enemiesCount = (rows + columns) / 2;
    int hitsToPlayerDeathFromOneEnemy = 3;
    float enemyDamage = 30;
    float enemyHealth = 100;
    float playerHealth = (enemyDamage * hitsToPlayerDeathFromOneEnemy * enemiesCount) * 0.8f;
    float playerDamage = enemyHealth / (hitsToPlayerDeathFromOneEnemy - 1);
    
    LevelData data = _mapGenerator.GenerateMap(rows, columns, enemiesCount);
    
    CreateProps(data, enemiesCount, enemiesCount);
    
    data.Floor.Bake();
    
    _background.Create(data.Floor.transform);
    
    _player = _playerFactory.CreatePlayer(data.PlayerSpawnPosition, _joystick, _attackButton, playerHealth, playerDamage);
    _player.OnDead += PlayerDead;
    
    _cameraFollower.SetTarget(_player.transform);
    _cameraFollower.SetBorders(data.Floor.transform);
    
    _enemyFactory.SpawnEnemies(data, enemiesCount, _player, enemyDamage, enemyHealth);

    data.Exit.OnPlayerExit += Win;

    _gameOver.OnContinue += ContinueGame;
  }

  private void UpdateLevel()
  {
    if(UserLevelData.NeedToNextLevel)
      DOTween.Sequence().AppendInterval(1f).onComplete += UserLevelData.NextLevel;
  }

  private void Win()
  {
    SoundManager.SoundManager.Instance.PlayOneShot(SoundManager.SoundManager.Instance.Win);

    UserLevelData.NeedToNextLevel = true;

    Restart();
  }

  private void Restart()
  {
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
    UserLevelData.NeedToNextLevel = false;
    
    _gameOver.gameObject.SetActive(true);
    _gameOver.Show();
  }

  private void ContinueGame()
  {
    _player.Resurrect();
  }
}
