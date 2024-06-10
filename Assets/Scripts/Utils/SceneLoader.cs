using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
  public class SceneLoader : MonoBehaviour
  {
    public void LoadScene(string name)
    {
      SceneManager.LoadScene(name);
    }
  }
}