using UnityEngine;

namespace CharacterSelector
{
  [CreateAssetMenu(menuName = "Configs/Character", fileName = "Character config")]
  public class CharacterConfigSO : ScriptableObject
  {
    public int Id => Prefab.gameObject.GetInstanceID();
    public Player.Player Prefab;
  }
}