using CharacterSelector;
using Health;
using Player;
using UnityEngine;

namespace CarSelector
{
  public class Podium : MonoBehaviour
  {
    public Vector3 SpawnedObjPos => _spawnedObj.transform.position + new Vector3(0, 0.8f, -1f);
    
    [SerializeField] private Vector3 _carScale;
    [SerializeField] private Vector3 _carRotation;
    [SerializeField] private Vector3 _carLocalPosition;

    private GameObject _spawnedObj;
    
    public void SetCharacter(CharacterConfigSO characterConfigConfig)
    {
      if (_spawnedObj != null)
      {
        Destroy(_spawnedObj);
      }
      
      _spawnedObj = Instantiate(characterConfigConfig.Prefab, transform).gameObject;
      
      SetupTransform(_spawnedObj.transform);

      Cleanup(_spawnedObj.gameObject);
    }
    
    private void SetupTransform(Transform car)
    {
      car.localScale = _carScale;
      car.localRotation = Quaternion.Euler(_carRotation);
      car.localPosition = _carLocalPosition;
    }

    private void Cleanup(GameObject target)
    {
      Destroy(target.GetComponent<PlayerMover>());
      Destroy(target.GetComponent<PlayerAttack>());
      Destroy(target.GetComponent<Player.Player>());
      Destroy(target.GetComponent<CapsuleCollider>());
      Destroy(target.GetComponent<SphereCollider>());
      Destroy(target.GetComponent<PlayerAnimator>());
      
      target.GetComponentInChildren<HealthView>().gameObject.SetActive(false);
    }
  }
}