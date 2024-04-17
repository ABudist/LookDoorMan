using System;
using UnityEngine;

namespace Enemies
{
  public class EnemyVisionArea : MonoBehaviour
  {
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
      if (obj.GetComponent<Player.Player>() != null)
      {
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
  }
}