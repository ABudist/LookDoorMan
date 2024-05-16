using DG.Tweening;
using Health;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using User;

namespace CharacterSelector
{
  public class SelectCharacterButton : MonoBehaviour, IPointerClickHandler
  {
    public CharacterConfigSO CurrentConfig => _currentConfig;
      
    [SerializeField] private Vector3 _scale;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private Vector3 _position;
    [SerializeField] private GameObject _lock;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _highlightedColor;

    private Outline _outline => GetComponent<Outline>();
    private CharacterSelector _characterSelector;
    private CharacterConfigSO _currentConfig;
    private Vector3 _origScale;
    private Sequence _selectedAnim;

    public void Construct(CharacterConfigSO characterConfigConfig, Rect cutZone, CharacterSelector characterSelector)
    {
      _characterSelector = characterSelector;
      _currentConfig = characterConfigConfig;
      _origScale = transform.localScale;

      CreateCharacter(characterConfigConfig, cutZone);

      UpdateView();

      if (UserCharacterData.CurrentId == characterConfigConfig.Id)
      {
        _characterSelector.TrySelect(this);
      }
    }

    private void OnEnable()
    {
      UserCharacterData.OnChanged += UpdateView;
    }

    private void OnDisable()
    {
      UserCharacterData.OnChanged -= UpdateView;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      if (UserCharacterData.CurrentId == _currentConfig.Id)
      {
        _characterSelector.SelectCurrentCharacter();
      }
      else
      {
        _characterSelector.TrySelect(this);
      }
    }

    public void SetHighlight(bool active)
    {
      _outline.enabled = active;
      _outline.effectColor = _highlightedColor;
    }

    public void UpdateView()
    {
      _lock.SetActive(!UserCharacterData.IsOpened(_currentConfig.Id));
      _outline.enabled = UserCharacterData.CurrentId == _currentConfig.Id;
      _outline.effectColor = _selectedColor;

      _selectedAnim.Kill();
      transform.localScale = _origScale;
      
      if (UserCharacterData.CurrentId == _currentConfig.Id)
      {
        _selectedAnim = DOTween.Sequence();

        _selectedAnim.Append(transform.DOScale(_origScale * 0.9f, 0.5f))
          .Append(transform.DOScale(_origScale, 0.5f))
          .SetLoops(-1);
      }
    }

    private void CreateCharacter(CharacterConfigSO characterConfigConfig, Rect cutZone)
    {
      Player.Player character = Instantiate(characterConfigConfig.Prefab, transform);

      DisableCoreComponents(character.gameObject);
      SetupTransform(character.transform);
    }

    private void SetupTransform(Transform car)
    {
      car.localScale = _scale;
      car.localRotation = Quaternion.Euler(_rotation);
      car.localPosition = _position;
    }

    private void DisableCoreComponents(GameObject target)
    {
      Destroy(target.GetComponent<PlayerMover>());
      Destroy(target.GetComponent<PlayerAttack>());
      Destroy(target.GetComponent<Player.Player>());
      Destroy(target.GetComponent<CapsuleCollider>());
      Destroy(target.GetComponent<SphereCollider>());
      Destroy(target.GetComponent<PlayerAnimator>());
      
      target.GetComponentInChildren<HealthView>().gameObject.SetActive(false);
    }
  }
}