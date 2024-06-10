using System;
using System.Collections;
using UnityEngine;

namespace UI
{
  public class Tutorial : MonoBehaviour
  {
    [SerializeField] private GameState _gameState;
    [SerializeField] private GameObject[] _objs;

    private bool _active;

    private void Awake()
    {
      _gameState.OnGameStarted += Show;
    }

    private void Show()
    {
      _gameState.OnGameStarted -= Show;

      StartCoroutine(Anim());
    }

    private IEnumerator Anim()
    {
      yield return new WaitForSeconds(1f);

      foreach (GameObject obj in _objs)
      {
        obj.SetActive(true);
      }

      _active = true;
    }

    private void Update()
    {
      if (!_active)
      {
        return;
      }
      
      if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
      {
        StopAllCoroutines();
        gameObject.SetActive(false);
      }
    }
  }
}