using UnityEngine;

namespace Player
{
  public class PlayerSounds : MonoBehaviour
  {
    private PlayerAttack _playerAttack => GetComponent<PlayerAttack>();
    private PlayerAnimator _animator => GetComponent<PlayerAnimator>();

    private void Awake()
    {
      _playerAttack.OnStartAttack += PlayAttack;
      _animator.OnFootstep += PlayFootstep;
    }

    private void PlayFootstep()
    {
      SoundManager.SoundManager.Instance.PlayOneShotRandomPitch(SoundManager.SoundManager.Instance.Footstep, 0.8f);
    }

    private void PlayAttack()
    {
      SoundManager.SoundManager.Instance.PlayOneShot(SoundManager.SoundManager.Instance.Attack);
    }
  }
}