using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MangerGame : MonoBehaviour
{
    public static MangerGame inst;
    //where hero born;
    public Transform SpawnHeroPoint; 
    public List<GameObject> HeroList = new List<GameObject>();
    public GameObject HeroGO;
    public List<GameObject> FxList = new List<GameObject>();
    public List<GameObject> SoundList = new List<GameObject>();

    public bool IsGameStart = false;
    public bool IsDead = false;

    public int CoinScore;

    void Awake()
    {
        inst = this;
        SpawnHeroPoint = GameObject.Find("SpawnHeroPoint").transform;
        IniGame();
      
    }


    public void IniGame()
    {
        //Create hero
        CreatHero();
        IsGameStart = true;
    }

    public void CreatHero()
    {
        string HeroID = PlayerPrefs.GetString("HeroID");
        HeroID = "Hero1001";
        switch (HeroID)
        {
            case "Hero1001":
                HeroGO = Instantiate(HeroList[0], SpawnHeroPoint.position,Quaternion.identity);
                break;
            case "Hero1002":
                HeroGO =  Instantiate(HeroList[1], SpawnHeroPoint.position, Quaternion.identity);
                break;
            case "Hero1003":
                HeroGO =  Instantiate(HeroList[2], SpawnHeroPoint.position, Quaternion.identity);
                break;
            case "Hero1004":
                HeroGO = Instantiate(HeroList[3], SpawnHeroPoint.position, Quaternion.identity);
                break;
        }
    }


    public void  GameWin()
    {
        MangerBattleUI.inst.WinPanel.SetActive(true);

    }

    public void GameLost()
    {
        IsDead = true;
        StartCoroutine(WaitToLoadLoseUI());
    }

    public void CreatFx(int Index,Transform  Targt)
    {
        GameObject FxGo = Instantiate(FxList[Index], Targt.position, Quaternion.identity);
        Destroy(FxGo, 2);
    }

    public void CreatSound(int Index)
    {
        GameObject FxGo = Instantiate(SoundList[Index]);
        Destroy(FxGo, 2);
    }

    public void GetCoin(int score)
    {
        CoinScore += score;
        MangerBattleUI.inst.CoinTxt.text = CoinScore.ToString();
    }

    IEnumerator WaitToLoadLoseUI()
    {
        yield return new WaitForSeconds(0.5f);
        MangerBattleUI.inst.LosePanel.SetActive(true);
    }


}
