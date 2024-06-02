using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Props
{
  public class PropsFactory : MonoBehaviour
  {
    [SerializeField] private PropsObj[] _propsObjPrefabs;
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Heal _healPrefab;

    [SerializeField] private float _maxOffset;
    
    public void Spawn(Vector3 at)
    {
      Instantiate(_propsObjPrefabs[Random.Range(0, _propsObjPrefabs.Length)], CalculatePosition(at), Quaternion.Euler(0, Random.Range(0, 360), 0));
    }

    public void SpawnCoin(Vector3 at)
    {
      Instantiate(_coinPrefab, CalculatePosition(at), quaternion.identity);
    } 
    
    public void SpawnHeal(Vector3 at)
    {
      Instantiate(_healPrefab, CalculatePosition(at), quaternion.identity);
    }

    private Vector3 CalculatePosition(Vector3 point)
    {
      return point + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1, 1)) * _maxOffset;
    }
  }
}