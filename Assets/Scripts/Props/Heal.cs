using UnityEngine;
using Utils;

namespace Props
{
  public class Heal: MonoBehaviour
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
    if (obj.TryGetComponent(out Player.Player player))
    {
      SoundManager.SoundManager.Instance.PlayOneShot(SoundManager.SoundManager.Instance.Health, 0.5f);
        
      player.Heal();
      
      GameObject effect = Instantiate(_effect);
      effect.transform.position = transform.position + new Vector3(0, 0.3f, 0);

      Destroy(gameObject);
    }
  }
  }
}