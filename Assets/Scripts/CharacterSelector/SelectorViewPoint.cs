using UnityEngine;
using UnityEngine.UI;

namespace CarSelector
{
  public class SelectorViewPoint : MonoBehaviour
  {
    [SerializeField] private Color _selected;
    [SerializeField] private Color _normal;
    private Image _image => GetComponent<Image>();
    
    public void SetSelected(bool selected)
    {
      _image.color = selected ? _selected : _normal;
    }
  }
}