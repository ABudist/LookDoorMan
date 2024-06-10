using CharacterSelector;
using UI;
using UnityEngine;

namespace Player
{
  public class PlayerFactory : MonoBehaviour
  {
    public Player CreatePlayer(GameState gameState, Vector3 at, Joystick joystick, Button attackButton, float health, float damage)
    {
      Player spawned = Instantiate(Characters.Instance.GetSelectedPrefab());
      spawned.transform.position = at;

      PlayerMover playerMover = spawned.GetComponent<PlayerMover>();
      PlayerAttack playerAttack = spawned.GetComponent<PlayerAttack>();
      Health.Health playerHealth = spawned.GetComponent<Health.Health>();

      playerMover.Construct(joystick);
      playerAttack.Construct(attackButton, damage);
      playerHealth.Construct(health);
      spawned.Construct(gameState, playerHealth, playerMover, playerAttack);

      return spawned;
    }
  }
}