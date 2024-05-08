using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangerMenu : MonoBehaviour
{

    GameObject UIRoot;

    GameObject BtnStart;
    // Start is called before the first frame update
    void Start()
    {
        UIRoot      = GameObject.Find("Canvas");
        BtnStart    = CC.Instance.GetUIGo<GameObject>(UIRoot, "BtnStart");
        CC.Instance.RegBtn(BtnStart, OnClick);
    }

    void OnClick(GameObject btn)
    {
        if (btn == BtnStart)
        {
            Application.LoadLevel("Main");
        }
        

    }

}
