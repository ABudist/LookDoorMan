using System;
using UnityEngine;
using Utils;

namespace MapGeneration
{
  [RequireComponent(typeof(Doors))]
  public class Exit : MonoBehaviour
  {
    public event Action OnPlayerExit;
    
    [SerializeField] private Trigger _openDoorsTrigger;
    [SerializeField] private Trigger _exitTrigger;
    
    private Doors _doors => GetComponent<Doors>();

    private void Awake()
    {
      _openDoorsTrigger.OnEnter += TryDoorsOpen;
      _openDoorsTrigger.OnExit += TryDoorsClose;
      _exitTrigger.OnEnter += TryWinGame;
    }

    private void OnDestroy()
    {
      _openDoorsTrigger.OnEnter -= TryDoorsOpen;
      _openDoorsTrigger.OnExit -= TryDoorsClose;
      _exitTrigger.OnEnter -= TryWinGame;
    }

    private void TryWinGame(GameObject obj)
    {
      if (obj.GetComponent<Player.Player>())
      {
        OnPlayerExit?.Invoke();
      }
    }

    private void TryDoorsOpen(GameObject obj)
    {
      if (obj.GetComponent<Player.Player>() != null)
      {
        _doors.Open();
      }
    }

    private void TryDoorsClose(GameObject obj)
    {
      if (obj.GetComponent<Player.Player>() != null)
      {
        _doors.Close();
      }
    }
  }
}