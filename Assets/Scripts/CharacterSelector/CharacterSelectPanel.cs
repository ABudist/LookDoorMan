using UnityEngine;

namespace CharacterSelector
{
  public class CharacterSelectPanel : MonoBehaviour
  {
    [SerializeField] private CharactersKit[] _kits;
    [SerializeField] private RectTransform _centralZone;

    private bool _blocked;
    private int _centralId;
      
    private void Start()
    {
      SetupKits();
    }

    public void SetBlocked(bool blocked)
    {
      _blocked = blocked;
    }

    public void ShowKit(CharactersKit kit)
    {
      for (int i = 0; i < _kits.Length; i++)
      {
        if (_kits[i] == kit)
        {
          _centralId = i;
          break;
        } 
      }
    }
      
    private void SetupKits()
    {
      Rect rect = RectUtility.GetNormalizedRectInScreenSpace(_centralZone);

      foreach (CharactersKit carsKit in _kits)
      {
        carsKit.SpawnButtons(rect);
      }
    }
  }
}