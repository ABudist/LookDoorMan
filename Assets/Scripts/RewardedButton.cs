using UnityEngine;
using UnityEngine.EventSystems;
using User;

public class RewardedButton : MonoBehaviour, IPointerClickHandler
{
  private void Start()
  {
    if (!CASAds.Instance.IsRewardedReady())
    {
      gameObject.SetActive(false);
    }
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    CASAds.Instance.ShowRewarded(() =>
    {
      UserWallet.Add(50);
      gameObject.SetActive(false);
    });
  }
}