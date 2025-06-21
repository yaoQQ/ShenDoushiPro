using UnityEngine;
using UnityEditor;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
public class ShopPlayFabService : SingleMonobehaviour<ShopPlayFabService>
{
    public static string GoldShopName = "GoldShop";
    public static string GemShopName = "GemShop";

    public static void getStoreInfo()
    {
        PlayFabClientAPI.GetStoreItems(
            new GetStoreItemsRequest
            {
                CatalogVersion = "TestCateLog",
                StoreId = "MoneyShop"

            },
             (GetStoreItemsResult result) =>
             {
                 //Debug.Log("PlayFabClientAPI.GetStoreItems() result.CatalogVersion=" + result.CatalogVersion);
                 //Debug.Log("PlayFabClientAPI.GetStoreItems() result.StoreId=" + result.StoreId);
                 //Debug.Log("PlayFabClientAPI.GetStoreItems() result.Store=" + result.Store);
                 //Debug.Log("PlayFabClientAPI.GetStoreItems() result.Source=" + result.Source);
                 List<StoreItem> store = result.Store;
                 for (int i = 0; i < store.Count; i++)
                 {
                     //Debug.LogFormat("store[{0}].ItemId=" + store[i].ItemId, i);
                     //Debug.LogFormat("store[{0}].DisplayPosition=" + store[i].DisplayPosition, i);
                     //Debug.LogFormat("store[{0}].VirtualCurrencyPrices=" + store[i].VirtualCurrencyPrices, i);
                     //Debug.LogFormat("store[{0}].RealCurrencyPrices=" + store[i].RealCurrencyPrices, i);
                     //foreach (KeyValuePair<string, uint> keyPair in store[i].VirtualCurrencyPrices)
                     //{
                     //    Debug.Log("keyPair.Key[" + keyPair.Key + "]=" + keyPair.Value);
                     //}
                
                 }
                 //Debug.Log("PlayFabClientAPI.GetStoreItems() result.MarketingData.Description=" + result.MarketingData.Description);
                 //Debug.Log("PlayFabClientAPI.GetStoreItems() result.MarketingData.DisplayName=" + result.MarketingData.DisplayName);
                 //Debug.Log("PlayFabClientAPI.GetStoreItems() result.MarketingData.Metadata=" + result.MarketingData.Metadata);
             },
            // Failure
            (PlayFabError error) =>
            {
                Debug.LogError("UserSex failed.");
                Debug.LogError(error.GenerateErrorReport());
                NoticeManager.Instance.Dispatch(PlayFabNotice.OnPlayFabError, error);
            }

            );
    }

    /// <summary>
    /// 购买物品
    /// </summary>
    /// <param name="catalogVersion">catelog名</param>
    /// <param name="storeId">商店id</param>
    /// <param name="itemId">物品id</param>
    /// <param name="virtualCurrency">虚拟物品</param>
    /// <param name="price">价格</param>
    /// <param name="addNum">All other item information (such as tags, description, or custom data) is only stored on the root catalog item. </param>
    public static void BuyStoreItem(string catalogVersion, string storeId, string itemId,string virtualCurrency,int price,int addNum)
    {
        PlayFabClientAPI.PurchaseItem(
            new PurchaseItemRequest
            {
                //CatalogVersion = "TestCateLog",
                //StoreId = "MoneyShop",
                //// CharacterId = GameDataManager.Instance.playerData.PlayFabId,
                //ItemId = "Apple",//coin
                ////rice the client expects to pay for the item (in case a new catalog or store was uploaded, with new prices).
                //Price = 5,
                //VirtualCurrency = "GD"
                CatalogVersion = catalogVersion,
                StoreId = storeId,
                // CharacterId = GameDataManager.Instance.playerData.PlayFabId,
                ItemId = itemId,//coin
                VirtualCurrency = virtualCurrency,
                //rice the client expects to pay for the item (in case a new catalog or store was uploaded, with new prices).
                Price = price


            },
             (PurchaseItemResult result) =>
             {

                 /// <summary>
                 /// Details for the items purchased.
                 /// </summary>
                 List<ItemInstance> items = result.Items;
                // Debug.Log("PlayFabClientAPI.GetStoreItems() result.Items.Count=" + result.Items.Count);//1
                 for (int i = 0; i < items.Count; i++)
                 {
                 //    Debug.LogFormat("store[{0}].ItemId=" + items[i].ItemId, i);//Apple
                 //    Debug.LogFormat("store[{0}].Annotation=" + items[i].Annotation, i);
                 //    Debug.LogFormat("store[{0}].BundleContents=" + items[i].BundleContents, i);
                 //    Debug.LogFormat("store[{0}].BundleParent=" + items[i].BundleParent, i);//
                 //    Debug.LogFormat("store[{0}].CatalogVersion=" + items[i].CatalogVersion, i);//TestCateLog
                 //    Debug.LogFormat("store[{0}].CustomData=" + items[i].CustomData, i);
                 //    Debug.LogFormat("store[{0}].DisplayName=" + items[i].DisplayName, i);//Apple
                 //    Debug.LogFormat("store[{0}].ItemClass=" + items[i].ItemClass, i);//Apple
                 //    Debug.LogFormat("store[{0}].ItemInstanceId=" + items[i].ItemInstanceId, i);//6930488F22645FD5
                 //    //Total number of remaining uses, if this is a consumable item.
                 //    Debug.LogFormat("Total number of remaining uses, if this is a consumable item. store[{0}].RemainingUses=" + items[i].RemainingUses, i);//购买次数
                 //    Debug.LogFormat("Currency type for the cost of the catalog item.store[{0}].UnitCurrency=" + items[i].UnitCurrency, i);//GD
                 //    Debug.LogFormat("Cost of the catalog item in the given currency..store[{0}].UnitPrice=" + items[i].UnitPrice, i);//5
                 //    Debug.LogFormat("The number of uses that were added or removed to this item in this call...store[{0}].UsesIncrementedBy=" + items[i].UsesIncrementedBy, i);//1
                 //
                 }

                 //是金币商店物品
                 if (storeId.Equals(ShopPlayFabService.GoldShopName))
                 {
                     addUserVirtualCurrency(addNum);
                 }
             },
            // Failure
            (PlayFabError error) =>
            {

                Debug.LogError(error.GenerateErrorReport());
                NoticeManager.Instance.Dispatch(PlayFabNotice.OnPlayFabError, error);
            }

            );
    }
    /// <summary>
    /// 添加虚拟货币
    /// </summary>
    /// <param name="addGoldNum">添加虚拟货币数量</param>
    public static void addUserVirtualCurrency(int addGoldNum)
    {

        Logger.PrintColor("yellow", "@@@@@@@@@addGoldNum=" + addGoldNum);
        if (addGoldNum <= 0 || addGoldNum > 100000)
        {
            return;
        }
        PlayFabClientAPI.AddUserVirtualCurrency(
            new AddUserVirtualCurrencyRequest()
            {
                Amount = addGoldNum,
                VirtualCurrency = GameDataManager.GoldKey
            },
            (ModifyUserVirtualCurrencyResult result) =>
            {
                //Debug.Log("==============AddUserVirtualCurrency============");
                //Debug.Log("result.PlayFabId=" + result.PlayFabId);
                //Debug.Log("result.VirtualCurrency=" + result.VirtualCurrency);
                //Debug.Log("result.Balance=" + result.Balance);
                //Debug.Log("result.BalanceChange=" + result.BalanceChange);
                PlayFabManager.UpdateUserVirtualCurrency();
            },
            (PlayFabError error) =>
            {
               
                Debug.LogError(error.GenerateErrorReport());
                NoticeManager.Instance.Dispatch(PlayFabNotice.OnPlayFabError, error);
            }
            );
    }
    public static void getStoreCatalogInfo(string cataLogStr)
    {
        PlayFabClientAPI.GetCatalogItems(
            new GetCatalogItemsRequest
            {
                //CatalogVersion = "TestCateLog",
                CatalogVersion = cataLogStr,
            },
             (GetCatalogItemsResult result) =>
             {
                 List<CatalogItem> Catalog = result.Catalog;
                 //for (int i = 0; i < Catalog.Count; i++)
                 //{
                     Debug.LogFormat("Catalog[{0}].ItemId=" + Catalog[0].ItemId, 0);
                     Debug.LogFormat("Catalog[{0}].CatalogVersion=" + Catalog[0].CatalogVersion, 0);
                     Debug.LogFormat("Catalog[{0}].DisplayName=" + Catalog[0].DisplayName, 0);
                     Debug.LogFormat("Catalog[{0}].ItemImageUrl=" + Catalog[0].ItemImageUrl, 0);
                     Debug.LogFormat("Catalog[{0}].ItemClass=" + Catalog[0].ItemClass, 0);
                     Debug.LogFormat("Catalog[{0}].VirtualCurrencyPrices[GE]=" + Catalog[0].VirtualCurrencyPrices[GameDataManager.GemKey], 0);
                     uint te = 0;
                     Catalog[0].VirtualCurrencyPrices.TryGetValue(GameDataManager.GemKey, out te);
                     Debug.Log("@@@@@@@@@result  te=" + te);

                     string goldData = Catalog[0].CustomData;
                     Debug.Log("Catalog[{0}].goldData="+ goldData);
                     Debug.LogFormat("Catalog[{0}].goldData={1}" , 0, goldData);


                 //}
                 // Debug.Log("@@@@@@@@@result="+ result);
                 NoticeManager.Instance.Dispatch(PlayFabNotice.getStoreItemsInfo, Catalog);
             },
            // Failure
            (PlayFabError error) =>
            {
              
               // Debug.LogError(error.GenerateErrorReport());
                NoticeManager.Instance.Dispatch(PlayFabNotice.OnPlayFabError, error);
            }

            );
    }

    /// <summary>
    /// 购买商品集合
    /// </summary>
    public static void startPurchase()
    {
        var primaryCatalogName = "TestCateLog"; // In your game, this should just be a constant matching your primary catalog
        var storeId = "MoneyShop"; // At this point in the process, it's just maintaining the same storeId used above
        var request = new StartPurchaseRequest
        {
            CatalogVersion = primaryCatalogName,
            StoreId = storeId,
            Items = new List<ItemPurchaseRequest> {
             // The presence of these lines are based on the results from GetStoreItems, and user selection - Yours will be more generic
            new ItemPurchaseRequest { ItemId = "Apple", Quantity = 20,},
            new ItemPurchaseRequest { ItemId = "coin", Quantity = 100,},
            new ItemPurchaseRequest { ItemId = "gem", Quantity = 2,},
             }
        };
        PlayFabClientAPI.StartPurchase(
            request,
            (StartPurchaseResult result) =>
            {
                Debug.Log("Purchase started: OrderId=" + result.OrderId);
                Debug.Log("Purchase started: Contents=" + result.Contents);
                Debug.Log("Purchase started: PaymentOptions=" + result.PaymentOptions);
                Debug.Log("Purchase started: PaymentOptions.ProviderName=" + result.PaymentOptions[0].ProviderName);
                Debug.Log("Purchase started: VirtualCurrencyBalances=" + result.VirtualCurrencyBalances);
            },
            (PlayFabError error) =>
            {
                Debug.LogError("UserSex failed.");
                Debug.LogError(error.GenerateErrorReport());
                NoticeManager.Instance.Dispatch(PlayFabNotice.OnPlayFabError, error);
            }
            );
    }

    /// <summary>
    /// 真钱物品应与高级VC物品分开存放，并且再次与免费VC物品分开
    /// 。如果单个商店允许使用多种货币，
    /// 则该商店中的所有商品都应始终使用同一组多种货币。
    /// 根据需要创建任意数量的商店，以提供流畅的客户体验。
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="currencyKey"></param>
    /// <param name="providerName"></param>
    public static void DefinePaymentCurrency(string orderId, string currencyKey, string providerName)
    {
        var request = new PayForPurchaseRequest
        {
            OrderId = orderId, // orderId comes from StartPurchase above
            Currency = currencyKey, // User defines which currency they wish to use to pay for this purchase (all items must have a defined/non-zero cost in this currency)
            ProviderName = providerName // providerName comes from the PaymentOptions in the result from StartPurchase above.
        };
        PlayFabClientAPI.PayForPurchase(request,
            (PayForPurchaseResult result) =>
            {
                Debug.Log("Purchase started: CreditApplied=" + result.CreditApplied);
                Debug.Log("Purchase started: OrderId=" + result.OrderId);
                Debug.Log("Purchase started: ProviderData=" + result.ProviderData);
                Debug.Log("Purchase started: PurchaseConfirmationPageURL=" + result.PurchaseConfirmationPageURL);
                Debug.Log("Purchase started: PurchaseCurrency=" + result.PurchaseCurrency);
                Debug.Log("Purchase started: PurchasePrice=" + result.PurchasePrice);
                Debug.Log("Purchase started: Status=" + result.Status);
                Debug.Log("交易授予的虚拟货币（如有）: VCAmount=" + result.VCAmount);
                Debug.Log("用户的当前虚拟货币余额: VirtualCurrency=" + result.VirtualCurrency);
            },
             (PlayFabError error) =>
             {
                 Debug.LogError("UserSex failed.");
                 Debug.LogError(error.GenerateErrorReport());
                 NoticeManager.Instance.Dispatch(PlayFabNotice.OnPlayFabError, error);
             }
            );
    }
    // Unity/C# 一旦完全定义了购买，您就可以完成该过程，如下所示。
    public static void FinishPurchase(string orderId)
    {
        var request = new ConfirmPurchaseRequest { OrderId = orderId };
        PlayFabClientAPI.ConfirmPurchase(
            request,
            (ConfirmPurchaseResult result) =>
            {
                Debug.Log("Purchase started: OrderId=" + result.OrderId);
                Debug.Log("Purchase Array of items purchased.: Items=" + result.Items);
                List<ItemInstance> items = result.Items;
                for (int i = 0; i < items.Count; i++)
                {
                    Debug.LogFormat("store[{0}].ItemId=" + items[i].ItemId, i);
                    Debug.LogFormat("store[{0}].DisplayName=" + items[i].DisplayName, i);
                    Debug.LogFormat("store[{0}].CatalogVersion=" + items[i].CatalogVersion, i);
                    Debug.LogFormat("store[{0}].BundleParent=" + items[i].BundleParent, i);
                    Debug.LogFormat("store[{0}].ItemInstanceId=" + items[i].ItemInstanceId, i);
                    Debug.LogFormat("store[{0}].UnitCurrency=" + items[i].UnitCurrency, i);
                    Debug.LogFormat("store[{0}].UnitPrice=" + items[i].UnitPrice, i);
                    Debug.LogFormat("The number of uses that were added or removed to this item in this call. store[{0}].UnitPrice=" + items[i].UsesIncrementedBy, i);
                    Debug.LogFormat("Total number of remaining uses, if this is a consumable item. store[{0}].RemainingUses=" + items[i].RemainingUses, i);
                }
              
                PlayFabManager.UpdateUserVirtualCurrency();
            },
              (PlayFabError error) =>
              {
                  Debug.LogError("UserSex failed.");
                  Debug.LogError(error.GenerateErrorReport());
                  NoticeManager.Instance.Dispatch(PlayFabNotice.OnPlayFabError, error);
              }
            );
    }
   
    
}