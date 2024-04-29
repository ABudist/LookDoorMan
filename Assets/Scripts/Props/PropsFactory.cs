using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Props
{
  public class PropsFactory : MonoBehaviour
  {
    [SerializeField] private PropsObj[] _propsObjPrefabs;
    [SerializeField] private Coin _coinPrefab;

    [SerializeField] private float _maxOffset;
    
    public void Spawn(Vector3 at)
    {
      Instantiate(_propsObjPrefabs[Random.Range(0, _propsObjPrefabs.Length)], CalculatePosition(at), quaternion.identity);
    }

    public void SpawnCoin(Vector3 at)
    {
      Instantiate(_coinPrefab, CalculatePosition(at), quaternion.identity);
    }

    private Vector3 CalculatePosition(Vector3 point)
    {
      return point + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1, 1)) * _maxOffset;
    }
  }
}