using MapGeneration;
using Player;
using UnityEngine;

public class GameState : MonoBehaviour
{
  [SerializeField] private MapGenerator _mapGenerator;
  [SerializeField] private PlayerFactory _playerFactory;

  public void Start()
  {
    LevelData data = _mapGenerator.GenerateMap(4, 8);
    _playerFactory.CreatePlayer(data.PlayerSpawnPosition);
  }
}
