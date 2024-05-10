using System.Collections;
using UnityEngine;

namespace UI
{
  public class Finger : MonoBehaviour
  {
    [SerializeField] private Vector2[] _positions;
    private RectTransform _transform => GetComponent<RectTransform>();
    
    private IEnumerator Start()
    {
      _transform.anchoredPosition = _positions[_positions.Length - 1];
      
      while (true)
      {
        for (int i = 0; i < _positions.Length; i++)
        {
          int steps = 10;
          float deltaT = 1f / steps;
          Vector2 startPos = _transform.anchoredPosition;

          for (float t = 0; t < 1; t += deltaT)
          {
            _transform.anchoredPosition = Vector2.Lerp(startPos, _positions[i], t);
            yield return null;
          }
        }
      }
    }
  }
}