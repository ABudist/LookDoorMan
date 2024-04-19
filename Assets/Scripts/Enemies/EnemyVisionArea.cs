using System;
using UnityEngine;

namespace Enemies
{
  public class EnemyVisionArea : MonoBehaviour
  {
    public event Action<Player.Player> OnPlayerEnter;
    
    [SerializeField] private VisionArea.VisionArea _visionArea;
    [SerializeField] private Color _playerDetectColor;

    private void Awake()
    {
      _visionArea.OnVisionEnter += OnEnter;
      _visionArea.OnVisionExit += OnExit;
    }

    private void OnDestroy()
    {
      _visionArea.OnVisionEnter -= OnEnter;
      _visionArea.OnVisionExit -= OnExit;
    }
    
    private void OnEnter(GameObject obj)
    {
      if (obj.TryGetComponent(out Player.Player player))
      {
        OnPlayerEnter?.Invoke(player);
        _visionArea.SetColor(_playerDetectColor);
      }
    }

    private void OnExit(GameObject obj)
    {
      if (obj.GetComponent<Player.Player>() != null)
      {
        _visionArea.SetOrigColor();
      }
    }

    public void Hide()
    {
      _visionArea.gameObject.SetActive(false);
    }
  }
}