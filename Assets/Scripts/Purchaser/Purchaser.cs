using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;
using User;

public class Purchaser : MonoBehaviour, IStoreListener
{
  public static Purchaser Instance { get; private set; }
  
  private static IStoreController m_StoreController;
  private static IExtensionProvider m_StoreExtensionProvider;

  IAppleExtensions m_AppleExtensions;

  public static string full_version = "Full.ninja.stels.ansrey";
  public static string no_ads = "Ads.ninja.stels.ansrey";

  void Start()
  {
    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
    
    if (m_StoreController == null)
    {
      InitializePurchasing();
    }
  }

  public void InitializePurchasing()
  {
    if (IsInitialized())
    {
      return;
    }

    var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

    builder.AddProduct(full_version, ProductType.NonConsumable);
    builder.AddProduct(no_ads, ProductType.NonConsumable);

    builder.Configure<IAppleConfiguration>().SetApplePromotionalPurchaseInterceptorCallback(OnPromotionalPurchase);

    UnityPurchasing.Initialize(this, builder);
  }


  private bool IsInitialized()
  {
    return m_StoreController != null && m_StoreExtensionProvider != null;
  }


  public void BuyNoAds()
  {
    BuyNonConsumable(no_ads);
  }

  public void BuyFull()
  {
    BuyNonConsumable(full_version);
  }

  private void BuyNonConsumable(string product)
  {
    BuyProductID(product);
  }
    
  void BuyProductID(string productId)
  {
    if (IsInitialized())
    {
      Product product = m_StoreController.products.WithID(productId);

      if (product != null && product.availableToPurchase)
      {
        Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
        m_StoreController.InitiatePurchase(product);
      }
      else
      {
        Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
      }
    }
    else
    {
      Debug.Log("BuyProductID FAIL. Not initialized.");
    }
  }
    
  public void RestorePurchases()
  {
    // If Purchasing has not yet been set up ...
    if (!IsInitialized())
    {
      // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
      Debug.Log("RestorePurchases FAIL. Not initialized.");
      return;
    }

    // If we are running on an Apple device ... 
    if (Application.platform == RuntimePlatform.IPhonePlayer ||
        Application.platform == RuntimePlatform.OSXPlayer)
    {
      // ... begin restoring purchases
      Debug.Log("RestorePurchases started ...");

      // Fetch the Apple store-specific subsystem.
      var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
      // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
      // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
      apple.RestoreTransactions((result) =>
      {
        // The first phase of restoration. If no more responses are received on ProcessPurchase then 
        // no purchases are available to be restored.
        Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
      });
    }
    // Otherwise ...
    else
    {
      // We are not running on an Apple device. No work is necessary to restore purchases.
      Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
    }
  }


  //  
  // --- IStoreListener
  //

  public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
  {
    // Purchasing has succeeded initializing. Collect our Purchasing references.
    Debug.Log("OnInitialized: PASS");

    // Overall Purchasing system, configured with products for this application.
    m_StoreController = controller;
    // Store specific subsystem, for accessing device-specific store features.
    m_StoreExtensionProvider = extensions;

    m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();

    foreach (var item in controller.products.all)
    {
      if (item.availableToPurchase)
      {
        // Set all these products to be visible in the user's App Store
        m_AppleExtensions.SetStorePromotionVisibility(item, AppleStorePromotionVisibility.Show);
      }
    }
  }


  public void OnInitializeFailed(InitializationFailureReason error)
  {
    // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
    Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
  }

  public void OnInitializeFailed(InitializationFailureReason error, string message)
  {
  }


  public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
  {
    if (String.Equals(args.purchasedProduct.definition.id, full_version, StringComparison.Ordinal))
    {
      for (int i = 0; i < 9; i++)
      {
        UserCharacterData.Open(i);
      }

      UserPurchasingData.SetNoADSPurchased();

      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    else if (String.Equals(args.purchasedProduct.definition.id, no_ads, StringComparison.Ordinal))
    {
      UserPurchasingData.SetNoADSPurchased();
    }

    return PurchaseProcessingResult.Complete;
  }


  public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
  {
    // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
    // this reason with the user to guide their troubleshooting actions.
    Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
  }

  private void OnPromotionalPurchase(Product item)
  {
    if (item.definition.id == full_version)
      BuyFull();
    else
      BuyNoAds();
  }

  public void ContinuePromotionalPurchases()
  {
    m_AppleExtensions.ContinuePromotionalPurchases();
  }

  public string Get_Price(string product)
  {
    if (IsInitialized())
      return m_StoreController.products.WithID(product).metadata.localizedPriceString;

    return "";
  }
}