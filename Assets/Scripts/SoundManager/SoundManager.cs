using UnityEngine;

namespace SoundManager
{
  public class SoundManager : MonoBehaviour
  {
    public static SoundManager Instance { get; private set; }

    public AudioClip Click;
    public AudioClip CoinTook;
    public AudioClip Attack;
    public AudioClip Footstep;
    public AudioClip Punch;
    public AudioClip Death;
    public AudioClip Appearing;
    public AudioClip Door;
    public AudioClip Win;
    public AudioClip CharacterOpened;
    public AudioClip Twist;
    public AudioClip Health;
    
    private AudioSource _src;
    private AudioSource _srcPitched;

    private void Awake()
    {
      if (Instance == null)
      {
        Instance = this;
        DontDestroyOnLoad(gameObject);
      }
      else
      {
        Destroy(gameObject);
      }

      _src = gameObject.AddComponent<AudioSource>();
      _srcPitched = gameObject.AddComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip, float volume = 1)
    {
      _src.PlayOneShot(clip, volume);
    }
    
    public void PlayOneShotRandomPitch(AudioClip clip, float volume = 1)
    {
      _srcPitched.pitch = Random.Range(0.8f, 1.2f);
      _srcPitched.PlayOneShot(clip, volume);
    }
  }
}