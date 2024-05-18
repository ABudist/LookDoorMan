using System;
using System.Collections;
using System.Collections.Generic;
using CharacterSelector;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CarSelector
{
  public class OpenCarAnimation : MonoBehaviour
  {
    [SerializeField] private Podium _podium;
    [SerializeField] private float _delayBtwSteps;
    [SerializeField] private GameObject _explosiveEffect;
    
    private Vector3 _origScale;
    private const float SCALER = 0.8f;
    
    public void StartAnimation(List<SelectCharacterButton> closedCarsButtons, SelectCharacterButton btnToCharacterOpen, Action onEnd)
    {
      _origScale = closedCarsButtons[0].transform.localScale;
      StartCoroutine(OpenCarAnim(closedCarsButtons, btnToCharacterOpen, onEnd));
    }

    private IEnumerator OpenCarAnim(List<SelectCharacterButton> btns, SelectCharacterButton btnToCharacterOpen, Action onEnd)
    {
      SelectCharacterButton prevBtn = null;

      int lastId = -1;
      
      for (int i = 0; i < btns.Count; i++)
      {
        if (prevBtn != null)
        {
          HighlightButton(prevBtn, false);
        }

        int nextId = Random.Range(0, btns.Count);

        do
        {
          nextId = Random.Range(0, btns.Count);
        }
        while (nextId == lastId);

        lastId = nextId;
        
        prevBtn = btns[nextId];
        HighlightButton(prevBtn, true);
        
        yield return new WaitForSeconds(_delayBtwSteps);
      }
      
      HighlightButton(prevBtn, false);
      HighlightButton(btnToCharacterOpen, true);
      
      yield return new WaitForSeconds(_delayBtwSteps * 2);
      
      HighlightButton(btnToCharacterOpen, false);

      yield return StartCoroutine(PodiumAnim(btnToCharacterOpen.CurrentConfig));
      
      onEnd?.Invoke();
    }

    private IEnumerator PodiumAnim(CharacterConfigSO characterConfigConfig)
    {
      Vector3 startPos = _podium.transform.position;
      Vector3 startScale = _podium.transform.localScale;
      PodiumRotator podiumRotator = _podium.GetComponent<PodiumRotator>();
      float rotatorSpeed = podiumRotator.Speed;
      
      Vector3 centerScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2));
      centerScreen.z = startPos.z;

      yield return DOTween.Sequence()
        .Append(_podium.transform.DOMove(centerScreen, 1f))
        .Join(_podium.transform.DOScale(startScale * 1.5f, 1f))
        .WaitForCompletion();

      SoundManager.SoundManager.Instance.PlayOneShot(SoundManager.SoundManager.Instance.Twist);
      
      yield return DOVirtual.Float(rotatorSpeed, rotatorSpeed * 100, 1f, value => { podiumRotator.Speed = value; }).WaitForCompletion();
        
      Instantiate(_explosiveEffect, _podium.SpawnedObjPos, Quaternion.identity);
      
      SoundManager.SoundManager.Instance.PlayOneShot(SoundManager.SoundManager.Instance.CharacterOpened);

      _podium.SetCharacter(characterConfigConfig);

      yield return DOTween.Sequence()
        .Append(DOVirtual.Float(podiumRotator.Speed, rotatorSpeed, 1.2f, value => { podiumRotator.Speed = value; }))
        .AppendInterval(0.5f)
        .Append(_podium.transform.DOMove(startPos, 1f))
        .Join(_podium.transform.DOScale(startScale, 1f))
        .WaitForCompletion();
    }

    private void HighlightButton(SelectCharacterButton target, bool highlight)
    {
      target.SetHighlight(highlight);

      if (highlight)
      {
        target.transform.localScale = _origScale * SCALER;
        SoundManager.SoundManager.Instance.PlayOneShot(SoundManager.SoundManager.Instance.Click);
      }
      else
      {
        target.transform.localScale = _origScale;
      }
    }
  }
}