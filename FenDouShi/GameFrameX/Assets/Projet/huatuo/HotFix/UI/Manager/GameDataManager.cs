//--------------------------------------------------------------------------------------
// GameManager.cs
//
// Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//--------------------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
public class GameDataManager :SingleMonobehaviour<GameDataManager>
{
    public ISerializerPlugin PlayFabJson;
    private UserPlayerData playerData;
   // private ShopData shopData;
    public static string GemKey = "GE";
    public static string GoldKey = "GD";


    public override void Init()
    {
        base.Init();
        PlayFabJson = PlayFab.PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer);
        playerData = new UserPlayerData();
        //shopData = new ShopData();
        Debug.Log("GameDataManager.in.Init() complete");
    }
#region PlayerData 操作请求相关数据返回成功后，用函数设置值。服务器默认数据返回直接设置值
    //玩家名
    public string getPlayerDataJson()
    {
        string playerDataStr = GameDataManager.Instance.PlayFabJson.SerializeObject(playerData);

        return playerDataStr;
    }
    public UserPlayerData userPlayerData
    {
        get
        {
            return playerData;
        }
    }
    public string PlayFabId
    {
        get
        {
           return playerData.PlayFabId;
        }
    }
    public void SetPlayerDisPlayName(string name)
    {
        playerData.UserDisplayName = name;
    }
    public void SetPlayPassword(string passWord)
    {
        playerData.Password = passWord;
    }
    
    public void SessionTicket(string sessionTicket)
    {
        playerData.SessionTicket = sessionTicket;
    }
    //玩家名Id
    public void SetPlayFabId(string Id)
    {
        playerData.PlayFabId = Id;
    }
    //玩家邮箱
    public void SetEmail(string email)
    {
        playerData.Email = email;
    }
    //玩家性别
    public void SetSex(int sex)
    {
        playerData.Sex = sex;
    }
    public UserPlayerData updatePlayer(LoginResult result)
    {
        playerData.PlayFabId = result.PlayFabId;
        playerData.SessionTicket = result.SessionTicket;
        //playerData.Password = 
       // GameDataManager.Instance.SetPlayPassword(userPassWord);
        Dictionary<string, int> userCurrency = result.InfoResultPayload.UserVirtualCurrency;
        int gold = result.InfoResultPayload.UserVirtualCurrency[GameDataManager.GoldKey];
        int gem = result.InfoResultPayload.UserVirtualCurrency[GameDataManager.GemKey];
        playerData.Gold = gold;
        playerData.Diamond = gem;
        string userName = result.InfoResultPayload.AccountInfo.TitleInfo.DisplayName;
        string email = result.InfoResultPayload.AccountInfo.PrivateInfo.Email;
        playerData.UserDisplayName = userName;
        playerData.Email = email;
        return playerData;

    }
    public void updateGold(int goldNum)
    {
        Logger.PrintColor("yellow", "@@@@@@@@@@@@updateDiamond() playerData.Gold =" + playerData.Gold + " update goldNum=" + goldNum);
        if (playerData.Gold != goldNum)
        {
            //Logger.PrintColor("yellow", "@@@@@@@@@@@@updateGold() goldNum=" + goldNum);
            playerData.Gold = goldNum;
            //@@@@test lua用
            // NoticeManager.Instance.Dispatch(PlayFabNotice.UpdatePlayFabUserGold, goldNum);

        }

    }
    public void updateDiamond(int gemNum)
    {
        Logger.PrintColor("yellow", "@@@@@@@@@@@@updateDiamond() playerData.Diamond =" + playerData.Diamond+ " update gemNum="+ gemNum);
        if (playerData.Diamond != gemNum)
        {
           // Logger.PrintColor("yellow", "@@@@@@@@@@@@updateGold() gemNum=" + gemNum);
            playerData.Diamond = gemNum;
            //@@@@test lua用
          //  NoticeManager.Instance.Dispatch(PlayFabNotice.UpdatePlayFabUserDiamond, gemNum);
        }
    }
#endregion


    
    
    //public void getUserGameData(Dictionary<string, UserDataRecord> gameData)
    //{
    //    var userGameData = gameData ?? new Dictionary<string, UserDataRecord>();
    //    string shopName = CharacterDataEnum.CharacterTest.ToString();
    //    Debug.Log("@@@@@@@@getUserGameData() userGameData.count=" + userGameData.Count);
    //    if (userGameData.ContainsKey(shopName))
    //    {
    //        Debug.Log("@@getUserGameData() userGameData.ContainsKey(shopName)");

    //        string json = userGameData[shopName].Value;
    //        Debug.Log("@@@getUserGameData()json=" + json);
    //        ShopData shopData = PlayFabJson.DeserializeObject<ShopData>(json);
    //        List<ShopItemData> shopList = shopData.shopItemDataList;
    //        Debug.Log("@@@getUserGameData() DeserializeObject success shopData=" + shopData);
    //        Debug.Log("@@@getUserGameData() DeserializeObject success shopData.shopType=" + shopData.shopType);
    //        Debug.Log("@@@getUserGameData() DeserializeObject success shopList.count=" + shopList.Count);
    //    }
        // characterAccessories = json.DeserializeObject<List<string>>(data["character_accessories"].Value);
    //}
}
