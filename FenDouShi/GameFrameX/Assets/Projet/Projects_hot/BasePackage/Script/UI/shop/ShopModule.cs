

using UnityEngine;
using System.Collections.Generic;
using PlayFab.ClientModels;
using System.Collections;
using SimpleJSON;

public class ShopModule : BaseModule
{

    public override ModuleEnum ModuleName()
    {
        return ModuleEnum.Shop;
    }
    public override void InitRegisterNet()
    {
        //test@@@
        // RegisterNetMsg(protoEnum.TestProtoEnum);
    }
    public override void OnNetMsgLister(uint protoID, byte[] buffer)
    {
        switch (protoID)
        {
            //test@@@
            // case protoEnum.TestProtoEnum:
            case 0:
            
                break;
        }
    }
    public override void OnJsonMsgLister(uint protoID, string jsonData)
    {

    }


    public override List<string> GetRegisterNotificationList()
    {
        if (notificationList == null)
        {
            notificationList = new List<string>();

            notificationList.Add(PlayFabNotice.LoginComplete);
            notificationList.Add(PlayFabNotice.LoadAccountDataComplete);

            notificationList.Add(PlayFabNotice.LoadUserDataComplete);

            notificationList.Add(PlayFabNotice.getStoreItemsInfo);
            notificationList.Add(PlayFabNotice.UpdatePlayFabUserDiamond);
             notificationList.Add(PlayFabNotice.UpdatePlayFabUserGold);
        }
        return notificationList;
    }

    public override void OnNotificationLister(string noticeType, BaseNotice notice)
    {
        Logger.PrintColor("blue", "$$$$$$$$$$$$$$$$$noticeType=" + noticeType);
        switch (noticeType)
        {

            //========商店
            case PlayFabNotice.getStoreItemsInfo:
                StoreItemListCallBack(notice);
                break;
            case PlayFabNotice.UpdatePlayFabUserDiamond:
                UpdateUserDiamond(notice);
                break;
            case PlayFabNotice.UpdatePlayFabUserGold:
                UpdateUserGold(notice);
                break;

            case PlayFabNotice.UpdateUserInfo:
                UpdateUserinfo(notice);
                break;


            case NoticeType.Normal_QuitGame:
                //   Driver.Instance.QuitGame();
                break;
        }
    }
    #region =======================商店=========================
    private const string playFabShopName = "money";//PlayFab 网站设置
    private const string playFabShopGoldName = "gold";
    public static void getStoreMoneyCatalogData()
    {
        Logger.PrintColor("blue", " getStoreCatalogData()");
        ShopPlayFabService.getStoreCatalogInfo(playFabShopName);
    }
    public static void getStoreGoldCatalogData()
    {
        Logger.PrintColor("blue", " getStoreCatalogData()");
        ShopPlayFabService.getStoreCatalogInfo(playFabShopGoldName);
    }
    private void StoreItemListCallBack(BaseNotice notice)
    {
        if (notice == null)
        {
            return;
        }
        ObjectNotice obj = (ObjectNotice)notice;
        Logger.PrintDebug("@@@@@@@@@@@@@@@@@@@@@StoreItemListCallBack()");
        List<CatalogItem> Catalog = (List<CatalogItem>)obj.GetObj();
        ShopPlayFabData.Instance.SaveShopItemList(Catalog);
        CommonView.OpenShop(ShopType.Gold, ShopFromType.platform, openCallBack);
    }
    //--手机登录界面登录
    private void openCallBack()
    {

        Logger.PrintColor("blue", " openCallBack()");

        //PlayFabUserModule.getStoreCatalogData();
    }
    public static void BuyGoldStoreItem(int index)
    {
        CatalogItem storeItem = ShopPlayFabData.Instance.getCataLogItemByIndex(index);
        Logger.PrintDebug("BuyGoldStoreItem() storeItem.ItemId=" + (storeItem.ItemId));

        string catalogVersion = storeItem.CatalogVersion;
        string storeId = "GoldShop";
        string itemId = storeItem.ItemId;
        string virtualCurrency = "GE";
        Dictionary<string, uint> DataTable = storeItem.VirtualCurrencyPrices;
        int price = (int)DataTable["GE"];
        string jsonToTable = storeItem.CustomData;
        Logger.PrintDebug("@@@@@@@jsonToTable=" + jsonToTable);
        JSONNode root = JSONNode.Parse(jsonToTable);
        //var data = root[0]["data"].AsArray;

        //for (int i = 0; i < data.Count; i++)
        //{
        //    FacebookUserInfo con = new FacebookUserInfo();

        //    con.id = root[0]["data"][i]["user"]["id"];
        //    con.name = root[0]["data"][i]["user"]["name"];
        //  con.score = root[0]["data"][i]["score"].AsInt;

        //    facebookRankList.Add(con);
        //}
        Logger.PrintDebug("@@@@@@@storeItem.CustomData=" + storeItem.CustomData);


        Logger.PrintDebug("DataTable[GE]=" + (DataTable["GE"]));

        int addNum = root["addNum"].AsInt;
        Logger.PrintDebug("@@@@@@@addNum=" + addNum);
        CommonView.showTopTips("addNum=" + addNum);
        ShopPlayFabService.BuyStoreItem(catalogVersion, storeId, itemId, virtualCurrency, price, addNum);
    }

    public void BuyGemStoreItem(int index)
    {
        CatalogItem storeItem = ShopPlayFabData.Instance.getCataLogItemByIndex(index);
        Logger.PrintDebug("BuyGoldStoreItem() storeItem.ItemId=" + (storeItem.ItemId));
        string catalogVersion = storeItem.CatalogVersion;
        string storeId = "GemShop";
        string itemId = storeItem.ItemId;
        string virtualCurrency = "GE";
        int price = (int)storeItem.VirtualCurrencyPrices[virtualCurrency];

        ShopPlayFabService.BuyStoreItem(catalogVersion, storeId, itemId, virtualCurrency, price, 0);
    }
    #endregion
    private void UpdateUserDiamond(BaseNotice notice)
    {
        if (notice == null)
        {
            return;
        }
        ObjectNotice obj = (ObjectNotice)notice;
        int demonNum = (int)obj.GetObj();
        GameDataManager.Instance.updateDiamond(demonNum);
        NoticeManager.Instance.Dispatch(NoticeType.User_Update_Diamond, demonNum);
        NoticeManager.Instance.Dispatch(NoticeType.Mall_Update_Diamond, demonNum);
        Logger.PrintDebug("@@@UpdateUserDiamond()");
    }
    private void UpdateUserGold(BaseNotice notice)
    {
        if (notice == null)
        {
            return;
        }
        ObjectNotice obj = (ObjectNotice)notice;
        int goldNum = (int)obj.GetObj();
        GameDataManager.Instance.updateGold(goldNum);
        NoticeManager.Instance.Dispatch(NoticeType.User_Update_Money, goldNum);
        NoticeManager.Instance.Dispatch(NoticeType.Mall_Update_Money, goldNum);
        Logger.PrintDebug("@@@UpdateUserGold()");
    }
    private void UpdateUserinfo(BaseNotice notice)
    {
        Logger.PrintDebug("@@@UpdateUserinfo()");
    }
}