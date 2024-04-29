using UnityEngine;
using Utils;

namespace Props
{
  [RequireComponent(typeof(Trigger))]
  public class Coin : MonoBehaviour
  {
    private Trigger _trigger => GetComponent<Trigger>();

    private void OnEnable()
    {
      _trigger.OnEnter += CheckPlayerEnter;
    }

    private void OnDisable()
    {
      _trigger.OnEnter -= CheckPlayerEnter;
    }

    private void CheckPlayerEnter(GameObject obj)
    {
      if (obj.GetComponent<Player.Player>() != null)
      {
        Destroy(gameObject);
      }
    }
  }
}