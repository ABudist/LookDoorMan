using UnityEngine;

namespace Player
{
  public class PlayerFactory : MonoBehaviour
  {
    [SerializeField] private Player _prefab;

    public void CreatePlayer(Vector3 at)
    {
      Player spawned = Instantiate(_prefab);

      spawned.transform.position = at;
    }
  }
}