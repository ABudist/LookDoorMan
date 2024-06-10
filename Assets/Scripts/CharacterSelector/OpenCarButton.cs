using TMPro;
using UI;
using UnityEngine;
using User;

namespace CarSelector
{
  [RequireComponent(typeof(Button))]
  public class OpenCarButton : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private CharacterSelector.CharacterSelector characterSelector;
    
    private Button _button => GetComponent<Button>();

    private void OnEnable()
    {
      _button.OnClick += TryOpenNextCar;
    }

    private void OnDisable()
    {
      _button.OnClick -= TryOpenNextCar;
    }

    private void Start()
    {
      _priceText.text = UserCharacterData.Cost.ToString();
    }

    private void TryOpenNextCar()
    {
      characterSelector.OpenNextClosedCar();
    }
  }
}