using CarSelector;
using DG.Tweening;
using UnityEngine;

namespace CharacterSelector
{
  public class CharacterSelectPanel : MonoBehaviour
  {
    [SerializeField] private CharactersKit[] _kits;
    [SerializeField] private RectTransform _centralZone;
    [SerializeField] private SelectorView _selectorView;
    [SerializeField] private SwipeInput _swipeInput;
    [SerializeField] private Vector3 _left;
    [SerializeField] private Vector3 _center;
    [SerializeField] private Vector3 _right;
    [SerializeField] private float _durationMoving;

    private bool _blocked;
    private int _centralId;

    private void OnEnable()
    {
      _swipeInput.OnSwipeLeft += MoveLeft;
      _swipeInput.OnSwipeRight += MoveRight;
    }

    private void OnDisable()
    {
      _swipeInput.OnSwipeLeft -= MoveLeft;
      _swipeInput.OnSwipeRight -= MoveRight;
    }

    private void Start()
    {
      SetupKits();
      
      _selectorView.Construct(3, 0);
    }

    public void SetBlocked(bool blocked)
    {
      _blocked = blocked;
    }

    public void ShowKit(CharactersKit kit)
    {
      _kits[_centralId].transform.localPosition = _right;

      for (int i = 0; i < _kits.Length; i++)
      {
        if (_kits[i] == kit)
        {
          _centralId = i;
          break;
        }
      }
      
      _kits[_centralId].transform.localPosition = _center;
      _selectorView.SetSelected(_centralId);
    }

    public void MoveLeft()
    {
      if (_blocked)
        return;

      int oldCentral = _centralId;
      _centralId = GetRightId();
      _kits[_centralId].transform.localPosition = _right;

      _blocked = true;

      DOTween.Sequence()
        .Append(_kits[oldCentral].transform.DOLocalMove(_left, _durationMoving))
        .Join(_kits[_centralId].transform.DOLocalMove(_center, _durationMoving))
        .onComplete += () => _blocked = false;

      _selectorView.SetSelected(_centralId);
    }

    public void MoveRight()
    {
      if (_blocked)
        return;

      int oldCentral = _centralId;
      _centralId = GetLeftId();
      _kits[_centralId].transform.localPosition = _left;

      _blocked = true;

      DOTween.Sequence()
        .Append(_kits[oldCentral].transform.DOLocalMove(_right, _durationMoving))
        .Join(_kits[_centralId].transform.DOLocalMove(_center, _durationMoving))
        .onComplete += () => _blocked = false;

      _selectorView.SetSelected(_centralId);
    }
      
    private CharactersKit GetKitHasCharacter(int characterId)
    {
      foreach (CharactersKit kit in _kits)
      {
        if (kit.HasId(characterId))
          return kit;
      }

      return null;
    }
    
    private int GetLeftId()
    {
      int left = _centralId - 1;

      if (left < 0)
      {
        left = _kits.Length - 1;
      }

      return left;
    }

    private int GetRightId()
    {
      int right = _centralId + 1;

      if (right == _kits.Length)
      {
        right = 0;
      }

      return right;
    }
    
    private void SetupKits()
    {
      Rect rect = RectUtility.GetNormalizedRectInScreenSpace(_centralZone);

      for (int i = 0; i < _kits.Length; i++)
      {
        _kits[i].SpawnButtons(i, _kits.Length, rect);
      }
    }
  }
}