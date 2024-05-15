using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Localization : MonoBehaviour
{
    Text my_text;

    public string rus, eng;

    void Awake()
    {
        my_text = GetComponent<Text>();

        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            my_text.text = rus;
        }
        else
        {
            my_text.text = eng;
        }
    }
}
