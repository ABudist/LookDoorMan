using UnityEngine;

namespace UI
{
  public class Tutorial : MonoBehaviour
  {
    private void Update()
    {
      if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
      {
        gameObject.SetActive(false);
      }
    }
  }
}