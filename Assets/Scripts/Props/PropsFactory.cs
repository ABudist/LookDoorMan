using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Props
{
  public class PropsFactory : MonoBehaviour
  {
    [SerializeField] private PropsObj[] _propsObjPrefabs;

    [SerializeField] private float _maxOffset;
    
    public void Spawn(Vector3 at)
    {
      Instantiate(_propsObjPrefabs[Random.Range(0, _propsObjPrefabs.Length)], at + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1,1)) * _maxOffset, quaternion.identity);
    }
  }
}