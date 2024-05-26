using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace UI.GameOverScreen
{
  public class Continue : MonoBehaviour
  {
    [SerializeField] private Button _continueBtn;
    [SerializeField] private Button _restartBtn;
    [SerializeField] private float _appearingDuration;
    [SerializeField] private float _timeToShowRestart;
    
    private Vector3 _continueBtnScale;
    private Vector3 _restartBtnScale;
    private Action _onContinueClicked;
    private Action _onRestartClicked;
    private Sequence _animLoop;

    private bool _isInitialized;
      
    private void OnEnable()
    {
      if(!_isInitialized)
      {
        _continueBtnScale = _continueBtn.transform.localScale;
        _restartBtnScale = _restartBtn.transform.localScale;

        _isInitialized = true;
      }
     
      _continueBtn.OnClick += ContinueClicked;
      _restartBtn.OnClick += RestartClicked;
    }

    private void OnDisable()
    {
      _continueBtn.OnClick -= ContinueClicked;
      _restartBtn.OnClick -= RestartClicked;
    }
    
    public void Show(Action onContinueClicked, Action onRestartClicked)
    {
      _continueBtn.transform.localScale = Vector3.zero;
      _restartBtn.transform.localScale = Vector3.zero;
      
      _onRestartClicked = onRestartClicked;
      _onContinueClicked = onContinueClicked;
      
      StartCoroutine(Show());
    }

    public void Hide(Action callback)
    {
      _animLoop.Kill();
      
      DOTween.Sequence()
        .Append(_continueBtn.transform.DOScale(Vector3.zero, _appearingDuration))
        .Join(_restartBtn.transform.DOScale(Vector3.zero, _appearingDuration))
        .OnComplete(() => { callback?.Invoke(); });
    }

    private IEnumerator Show()
    {
      yield return _continueBtn.transform.DOScale(_continueBtnScale, _appearingDuration).WaitForCompletion();

      _animLoop = DOTween.Sequence()
        .Append(_continueBtn.transform.DOScale(_continueBtnScale * 1.2f, 0.7f))
        .Append(_continueBtn.transform.DOScale(_continueBtnScale, 0.7f))
        .SetLoops(-1);

      yield return new WaitForSeconds(_timeToShowRestart);

      yield return _restartBtn.transform.DOScale(_restartBtnScale, _appearingDuration).WaitForCompletion();
    }

    private void ContinueClicked()
    {
      _onContinueClicked?.Invoke();
    }

    private void RestartClicked()
    {
      _onRestartClicked?.Invoke();
    }
  }
}