using Unity.AI.Navigation;
using UnityEngine;

namespace MapGeneration
{
  public class Floor : MonoBehaviour
  {
    [SerializeField] private NavMeshSurface _surface;
    
    public void Bake()
    {
      _surface.BuildNavMesh();  
    }
  }
}