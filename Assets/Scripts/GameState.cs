using MapGeneration;
using UnityEngine;

public class GameState : MonoBehaviour
{
  [SerializeField] private MapGenerator _mapGenerator;

  public void Start()
  {
    _mapGenerator.GenerateMap(4, 8);
  }
}
