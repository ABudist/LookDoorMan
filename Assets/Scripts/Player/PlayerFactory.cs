using UnityEngine;

namespace Player
{
  public class PlayerFactory : MonoBehaviour
  {
    [SerializeField] private Player _prefab;

    public Player CreatePlayer(Vector3 at)
    {
      Player spawned = Instantiate(_prefab);

      spawned.transform.position = at;

      return spawned;
    }
  }
}