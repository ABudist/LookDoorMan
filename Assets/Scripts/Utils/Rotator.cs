using UnityEngine;

namespace Utils
{
  public class Rotator : MonoBehaviour
  {
    private void Update()
    {
      transform.Rotate(0, 1, 0);
    }
  }
}