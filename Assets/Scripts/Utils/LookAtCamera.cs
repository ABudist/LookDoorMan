using UnityEngine;

namespace Utils
{
  public class LookAtCamera : MonoBehaviour
  {
    private Transform _camera;

    private void Start()
    {
      _camera = Camera.main.transform;
    }

    private void LateUpdate()
    {
      transform.LookAt(transform.position + _camera.forward);
    }
  }
}