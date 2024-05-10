using UnityEngine;

namespace MapGeneration
{
  public class BackgroundEffect : MonoBehaviour
  {
    [SerializeField] private GameObject _cubePrefab;
    
    public void Create(Transform floor)
    {
      float step = 3;
      
      Vector3 startPos = new Vector3(floor.localScale.x * -2f, 0, floor.localScale.z * -2f);
      Vector3 endPos = new Vector3(floor.localScale.x * 2f, 0, floor.localScale.z * 2f);

      Vector3 floorMin = new Vector3(floor.localScale.x / -2, 0, floor.localScale.z / -2);
      Vector3 floorMax = -floorMin;
        
      for (float x = startPos.x; x < endPos.x; x += step)
      {
        for (float z = startPos.z; z < endPos.z; z += step)
        {
          if (Random.Range(0, 3) == 0)
          {
            if (x < floorMin.x || x > floorMax.x || z < floorMin.z || z > floorMax.z)
            {
              GameObject cube = Instantiate(_cubePrefab);
              float scale = Random.Range(1f, 2f);
              cube.transform.localScale = new Vector3(scale, cube.transform.localScale.y, scale);
              cube.transform.position = new Vector3(x, cube.transform.localScale.y / -Random.Range(0.5f, 2f), z);
            }
          }
        }
      }
    }
  }
}