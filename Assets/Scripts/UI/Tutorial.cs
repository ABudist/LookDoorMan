using System.Collections;
using UnityEngine;

namespace UI
{
  public class Tutorial : MonoBehaviour
  {
    [SerializeField] private GameObject[] _objs;

    private bool _active;
    
    private IEnumerator Start()
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
        gameObject.SetActive(false);
      }
    }
  }
}