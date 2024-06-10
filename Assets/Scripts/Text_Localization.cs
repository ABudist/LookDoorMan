using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Text_Localization : MonoBehaviour
{
  private TextMeshProUGUI _textMeshProUGUI;
  private Text _text;

  public string rus, eng;

  void Awake()
  {
    _text = GetComponent<Text>();

    if (_text == null)
    {
      _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
      _textMeshProUGUI.text = GetValue();
    }
    else
    {
      _text.text = GetValue();
    }
  }

  private string GetValue()
  {
    if (Application.systemLanguage == SystemLanguage.Russian)
    {
      return rus;
    }
    else
    {
      return eng;
    }
  }
}