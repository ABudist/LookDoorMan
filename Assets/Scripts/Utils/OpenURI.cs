using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
  public class OpenURI : MonoBehaviour, IPointerClickHandler
  {
    [SerializeField] private string _uri;

    public void OnPointerClick(PointerEventData eventData)
    {
      Application.OpenURL(_uri);
    }
  }
}