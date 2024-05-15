using System.Collections.Generic;
using CarSelector;
using UnityEngine;

namespace CharacterSelector
{
  public class SelectorView : MonoBehaviour
  {
    [SerializeField] private SelectorViewPoint _selectorViewPointPrefab;
    [SerializeField] private Transform _pointsContainer;

    private List<SelectorViewPoint> _points = new List<SelectorViewPoint>();
    
    public void Construct(int count, int selected)
    {
      for (int i = 0; i < count; i++)
      {
        SelectorViewPoint selectorViewPoint = Instantiate(_selectorViewPointPrefab, _pointsContainer);

        _points.Add(selectorViewPoint);
      }
      
      SetSelected(selected);
    }

    public void SetSelected(int selected)
    {
      for(int i = 0; i < _points.Count; i++)
      {
        if (i == selected)
        {
          _points[i].SetSelected(true);
        }
        else
        {
          _points[i].SetSelected(false);
        }
      }
    }
  }
}