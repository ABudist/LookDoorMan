using UnityEngine;

public static class RectUtility
{
  public static Rect GetNormalizedRectInScreenSpace(RectTransform target)
  {
    Rect rect = RectTransformToScreenSpace(target, Camera.main);

    rect.xMin = rect.xMin / Screen.width;
    rect.xMax = 1 - rect.xMin;
    rect.yMin = rect.yMin / Screen.height;
    rect.yMax = 1 - rect.yMin;

    return rect;
  }

  public static Rect RectTransformToScreenSpace(RectTransform transform, Camera cam, bool cutDecimals = false)
  {
    var worldCorners = new Vector3[4];
    var screenCorners = new Vector3[4];

    transform.GetWorldCorners(worldCorners);

    for (int i = 0; i < 4; i++)
    {
      screenCorners[i] = cam.WorldToScreenPoint(worldCorners[i]);
      if (cutDecimals)
      {
        screenCorners[i].x = (int)screenCorners[i].x;
        screenCorners[i].y = (int)screenCorners[i].y;
      }
    }

    return new Rect(screenCorners[0].x,
      screenCorners[0].y,
      screenCorners[2].x - screenCorners[0].x,
      screenCorners[2].y - screenCorners[0].y);
  }
}