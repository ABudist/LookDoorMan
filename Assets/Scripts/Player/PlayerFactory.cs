using UI;
using UnityEngine;

namespace Player
{
  public class PlayerFactory : MonoBehaviour
  {
    [SerializeField] private Player _prefab;

    public Player CreatePlayer(Vector3 at, Joystick joystick, Button attackButton)
    {
      Player spawned = Instantiate(_prefab);
      spawned.transform.position = at;

      spawned.GetComponent<PlayerMover>().Construct(joystick);
      spawned.GetComponent<PlayerAttack>().Construct(attackButton);
      spawned.GetComponent<Health.Health>().Construct(100);
      
      return spawned;
    }
  }
}