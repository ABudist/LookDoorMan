using UI;
using UnityEngine;

namespace Player
{
  public class PlayerFactory : MonoBehaviour
  {
    [SerializeField] private Player _prefab;

    public Player CreatePlayer(Vector3 at, Joystick joystick, Button attackButton, float health, float damage)
    {
      Player spawned = Instantiate(_prefab);
      spawned.transform.position = at;

      PlayerMover playerMover = spawned.GetComponent<PlayerMover>();
      PlayerAttack playerAttack = spawned.GetComponent<PlayerAttack>();
      Health.Health playerHealth = spawned.GetComponent<Health.Health>();

      playerMover.Construct(joystick);
      playerAttack.Construct(attackButton, damage);
      playerHealth.Construct(health);
      spawned.Construct(playerHealth, playerMover, playerAttack);

      return spawned;
    }
  }
}