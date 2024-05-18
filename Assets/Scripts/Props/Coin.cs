using UnityEngine;
using User;
using Utils;

namespace Props
{
  [RequireComponent(typeof(Trigger))]
  public class Coin : MonoBehaviour
  {
    [SerializeField] private GameObject _effect;
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
        SoundManager.SoundManager.Instance.PlayOneShot(SoundManager.SoundManager.Instance.CoinTook, 0.5f);
        
        UserWallet.Add(1);

        GameObject effect = Instantiate(_effect);
        effect.transform.position = transform.position;
        
        Destroy(gameObject);
      }
    }
  }
}