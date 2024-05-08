using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MangerBattleUI : MonoBehaviour
{
    public static MangerBattleUI inst;

    public GameObject UIRoot;

    public GameObject QuitPanel;
    public GameObject BtnQuit;
    public GameObject BtnQuitYes;
    public GameObject BtnQuitNo;

    public GameObject WinPanel;
    public GameObject BtnWinYes;

    public GameObject LosePanel;
    public GameObject BtnLoseYes;

    public Text CoinTxt;



    // Start is called before the first frame update
    void Awake()
    {
        inst            = this;
        UIRoot          = GameObject.Find("Canvas");
        CoinTxt         = CC.Instance.GetUIGo<Text>(UIRoot, "Score/TextScore");

        QuitPanel       = CC.Instance.GetUIGo<GameObject>(UIRoot, "PanelQuit");
        BtnQuit         = CC.Instance.GetUIGo<GameObject>(UIRoot, "btnStop");
        BtnQuitYes      = CC.Instance.GetUIGo<GameObject>(UIRoot, "PanelQuit/btnYes");
        BtnQuitNo       = CC.Instance.GetUIGo<GameObject>(UIRoot, "PanelQuit/btnNo");

        WinPanel        = CC.Instance.GetUIGo<GameObject>(UIRoot, "PanelWin");
        BtnWinYes       = CC.Instance.GetUIGo<GameObject>(UIRoot, "PanelWin/btnYes");

        LosePanel       = CC.Instance.GetUIGo<GameObject>(UIRoot, "PanelDead");
        BtnLoseYes      = CC.Instance.GetUIGo<GameObject>(UIRoot, "PanelDead/btnYes");

        CC.Instance.RegBtn(BtnQuit, OnClick);
        CC.Instance.RegBtn(BtnQuitYes, OnClick);
        CC.Instance.RegBtn(BtnQuitNo, OnClick);
        CC.Instance.RegBtn(BtnWinYes, OnClick);
        CC.Instance.RegBtn(BtnLoseYes, OnClick);

    }

    void OnClick(GameObject btn)
    {
        if(btn== BtnQuitYes)
        {
            Application.LoadLevel("Start");
        }
        if (btn == BtnQuitNo)
        {
            QuitPanel.gameObject.SetActive(true);
        }
        if (btn == BtnQuitNo)
        {
            QuitPanel.gameObject.SetActive(false);
        }
        if (btn == BtnQuit)
        {
            QuitPanel.gameObject.SetActive(true);
        }

        if (btn == BtnWinYes)
        {
            Application.LoadLevel("Start");
        }

        if (btn == BtnLoseYes)
        {
            Application.LoadLevel("Start");
        }

    }

   
}
