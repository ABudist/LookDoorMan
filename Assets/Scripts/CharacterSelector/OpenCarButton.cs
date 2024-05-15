using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

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
     // _priceText.text = Data.CAR_PRICE.ToString();
    }

    private void TryOpenNextCar()
    {
      characterSelector.OpenNextClosedCar();
    }
  }
}