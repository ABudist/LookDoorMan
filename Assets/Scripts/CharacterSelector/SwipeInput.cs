using System;
using UnityEngine;

namespace CarSelector
{
  public class SwipeInput : MonoBehaviour
  {
    public event Action OnSwipeLeft;
    public event Action OnSwipeRight;

    private const float MAX_SWIPE_TIME = 0.5f;
    private const float MIN_SWIPE_DISTANCE = 0.17f;

    [SerializeField] private bool debugWithArrowKeys = true;

    private Vector2 _startTouchPos;
    private float _startTime;

    public void Update()
    {
      if (Input.touches.Length > 0)
      {
        Touch t = Input.GetTouch(0);

        if (t.phase == TouchPhase.Began)
        {
          _startTouchPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);
          _startTime = Time.time;
        }

        if (t.phase == TouchPhase.Ended)
        {
          if (Time.time - _startTime > MAX_SWIPE_TIME)
            return;

          Vector2 endPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);

          Vector2 swipe = new Vector2(endPos.x - _startTouchPos.x, endPos.y - _startTouchPos.y);

          if (swipe.magnitude < MIN_SWIPE_DISTANCE)
            return;

          if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
          {
            if (swipe.x > 0)
            {
              OnSwipeRight?.Invoke();
            }
            else
            {
              OnSwipeLeft?.Invoke();
            }
          }
        }
      }

      if (debugWithArrowKeys)
      {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
          OnSwipeRight?.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
          OnSwipeLeft?.Invoke();
        }
      }
    }
  }
}