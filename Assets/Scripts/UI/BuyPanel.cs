using UnityEngine;

namespace UI
{
  public class BuyPanel : MonoBehaviour
  {
    public void Close()
    {
      Destroy(gameObject);
    }
    
    public void BuyNoAds()
    {
      Purchaser.Instance.BuyNoAds();
    }

    public void BuyAll()
    {
      Purchaser.Instance.BuyFull();
    }
  }
}