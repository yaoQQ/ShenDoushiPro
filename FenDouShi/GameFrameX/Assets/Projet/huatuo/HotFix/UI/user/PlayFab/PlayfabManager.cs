//--------------------------------------------------------------------------------------
// PlayFabManager.cs
//
// Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//--------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
public enum CharacterDataEnum
{
    CharacterSex = 1,
    CharacterCoin = 2,
    CharacterDiamond = 3,
    CharacterShop = 4,

    CharacterTest = 5,
}
public static class PlayFabManager
{
    // Flag set after successfull PlayFab Login
    //public static bool IsLoggedIn = false;



    // The user's Title specific DisplayName
    public static string UserDisplayName = null;
    public static GetPlayerCombinedInfoRequestParams InfoRequestParams;
    public static void init()
    {
        //playerfab 配置获取哪些参数
        InfoRequestParams = new GetPlayerCombinedInfoRequestParams();
        InfoRequestParams.GetUserVirtualCurrency = true;
        //   PlayFabManager.InfoRequestParams.GetTitleData = true;
        InfoRequestParams.GetUserAccountInfo = true;
    }
#region 获取玩家信息
    ////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////
    /// <summary>
    /// 获取玩家最新的虚拟金币
    /// </summary>
    public static void UpdateUserVirtualCurrency()
    {
        PlayFabClientAPI.GetPlayerCombinedInfo(
            new GetPlayerCombinedInfoRequest
            {
                InfoRequestParameters = PlayFabManager.InfoRequestParams,
                PlayFabId = GameDataManager.Instance.PlayFabId
            },
            (GetPlayerCombinedInfoResult result) =>
            {
                Dictionary<string, int> userVirtualCurrency = result.InfoResultPayload.UserVirtualCurrency;
                if (userVirtualCurrency.ContainsKey(GameDataManager.GemKey))
                {
                    
                    int gemNum = result.InfoResultPayload.UserVirtualCurrency[GameDataManager.GemKey];
                    // Logger.PrintColor("yellow", "@@@@@@@@@@@@UpdateUserVirtualCurrency() gemNum=" + gemNum);
                    NoticeManager.Instance.Dispatch(PlayFabNotice.UpdatePlayFabUserDiamond, gemNum);
                }
                if (userVirtualCurrency.ContainsKey(GameDataManager.GoldKey))
                {
                    int goldNum = result.InfoResultPayload.UserVirtualCurrency[GameDataManager.GoldKey];
                   // Logger.PrintColor("yellow", "@@@@@@@@@@@@UpdateUserVirtualCurrency() goldNum=" + goldNum);
                    NoticeManager.Instance.Dispatch(PlayFabNotice.UpdatePlayFabUserGold, goldNum);
                }
            },
            (error) =>
            {

                NoticeManager.Instance.Dispatch(PlayFabNotice.OnPlayFabError, error);
            }


            );
    }
    /// <summary>
    /// 获取自己生成的数据
    /// </summary>
    public static void LoadUserData()
    {
        //获取的玩家数据集合
        string[] temStr = Enum.GetNames(typeof(CharacterDataEnum));
        PlayFabClientAPI.GetUserData(
            // Request
            new GetUserDataRequest
            {
 
                Keys = new List<string>(temStr)
            },
            // Success
            (GetUserDataResult result) =>
            {
                Debug.Log("GetUserData completed.");
                UserData = result.Data;
                IsUserDataLoaded = true;
              
                //string jsonStr = "";
                //if (result.Data != null && result.Data.Count > 0)
                //{
                //    jsonStr = GameDataManager.Instance.PlayFabJson.SerializeObject(result.Data);
                //}
                NoticeManager.Instance.Dispatch(PlayFabNotice.LoadUserDataComplete, result.Data);
            },
            // Failure
            (PlayFabError error) =>
            {
                Debug.LogError("GetUserData failed.");
                Debug.LogError(error.GenerateErrorReport());
                NoticeManager.Instance.Dispatch(PlayFabNotice.OnPlayFabError, error);
            }
            );
    }
    /// Load the user's server data
    /// 
    public static void LoadUserDataBykey(string keyStr)
    {
        //获取的玩家数据集合
        //  string[] temStr = Enum.GetNames(typeof(CharacterDataEnum));
        PlayFabClientAPI.GetUserData(
            // Request
            new GetUserDataRequest
            {
                Keys = new List<string> {
                    keyStr
                }
            },
            // Success
            (GetUserDataResult result) =>
            {
                Debug.Log("GetUserData completed.");
                UserData = result.Data;
                //foreach (string key in UserData.Keys)
                //{
                //    UserDataRecord userDataTem = UserData[key];
                //    Debug.Log("@@@@GetUserDataResult key=" + key + " userDataTem=" + userDataTem.Value);
                //    //test 方便编辑器查看
                //  //  PlayFabAuthService.Instance.updateUserDataEditor(key, userDataTem);


                //}
                if (result.Data != null && result.Data.Count > 0)
                {
                    //string jsonStr = GameDataManager.Instance.PlayFabJson.SerializeObject(result.Data);
                  //  if (!string.IsNullOrEmpty(jsonStr))
                        NoticeManager.Instance.Dispatch(PlayFabNotice.LoadUserDataComplete, result.Data);
                }
            },
                // Failure
                (PlayFabError error) =>
                {
                    Debug.LogError("GetUserData failed.");
                    Debug.LogError(error.GenerateErrorReport());
                    NoticeManager.Instance.Dispatch(PlayFabNotice.OnPlayFabError, error);
                }
            );
    }

    // Flag set when user data has loaded
    public static bool IsUserDataLoaded = false;

    // Access for user data
    public static string GetUserDataValue(string key)
    {

        if (UserData != null && UserData.ContainsKey(key))
        {
            return UserData[key].Value;
        }

        return null;
    }

    // Cache for user's server data
    public static Dictionary<string, UserDataRecord> UserData = new Dictionary<string, UserDataRecord>();

    ////////////////////////////////////////////////////////////////
    /// Write the user's data up to the server

    public static void UpdateUserData(CharacterDataEnum dataEnum, string value)
    {
        string key = dataEnum.ToString();
        Debug.Log("UpdateUserData key=" + key + " dataEnum=" + dataEnum + " value=" + value);
        if (!UserData.ContainsKey(key))
        {
            UserData[key] = new UserDataRecord { Value = value.ToString() };
        }
        else
        {
            UserData[key].Value = value.ToString();
        }

        PlayFabClientAPI.UpdateUserData(
            // Request
            new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { key, UserData[key].Value }
                }
            },
            // Success
            (UpdateUserDataResult response) =>
            {
                Debug.Log("更新数据成功 " + response.Request);
                LoadUserData();
                NoticeManager.Instance.Dispatch(PlayFabNotice.UpdateUserGameDataComplete);

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
    ////////////////////////////////////////////////////////////////
    /// Load the user's account info to get their DisplayName
    /// 
    public static void LoadAccountData()
    {
        PlayFabClientAPI.GetAccountInfo(
            // Request
            new GetAccountInfoRequest
            {
                // No properties means get the calling user's info
            },
            // Success
            (GetAccountInfoResult response) =>
            {
                Debug.Log("GetAccountInfo completed.");
                IsAccountInfoLoaded = true;
                NoticeManager.Instance.Dispatch(PlayFabNotice.LoadAccountDataComplete, response.AccountInfo);
            },
            // Failure
            (PlayFabError error) =>
            {
                NoticeManager.Instance.Dispatch(PlayFabNotice.OnPlayFabError, error);
            }
            );
    }

    // Flag set when the user's AccountInfo is loaded
    public static bool IsAccountInfoLoaded = false;

    ////////////////////////////////////////////////////////////////
    /// Update the user's per-title DisplayName
    /// 
    public static void SetUserDisplayName(string name)
    {
        // UserDisplayName = name;
        Debug.Log("SetUserDisplayName（） completed. name=" + name);
        PlayFabClientAPI.UpdateUserTitleDisplayName(
            // Request
            new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = name
            },
            // Success
            (UpdateUserTitleDisplayNameResult result) =>
            {
                Debug.Log("UpdateUserTitleDisplayName completed.");
                // UserDisplayName = name;
                GameDataManager.Instance. SetPlayerDisPlayName(name);
                // PlayFabAuthService.Instance.Username = UserDisplayName;
                NoticeManager.Instance.Dispatch(PlayFabNotice.SetUserDisplayNameComplete, UserDisplayName);
            },
            // Failure
            (PlayFabError error) =>
            {
                NoticeManager.Instance.Dispatch(PlayFabNotice.OnPlayFabError, error);
            }
            );
    }
#endregion


    ////////////////////////////////////////////////////////////////
    /// Update the user's game stats in bulk
    /// 
    /// This uses a custom event to trigger cloudscript which
    /// performs the stat updates
    /// 
    public static void UpdateStatistics(Dictionary<string, object> values)
    {
        PlayFabClientAPI.WritePlayerEvent(
            // Request
            new WriteClientPlayerEventRequest
            {
                EventName = "update_statistics",
                Body = new Dictionary<string, object>
                {
                    { "stats", values }
                }
            },
            // Success
            (WriteEventResponse response) =>
            {
                Debug.Log("WritePlayerEvent (UpdateStatistics) completed.");
                NoticeManager.Instance.Dispatch(PlayFabNotice.UpdateStatisticComplete);

            },
            // Failure
            (PlayFabError error) =>
            {
                NoticeManager.Instance.Dispatch(PlayFabNotice.OnPlayFabError, error);
            }
            );
    }

    ////////////////////////////////////////////////////////////////
    /// Update a user's individual game stat
    /// 
    /// This uses a custom event to trigger cloudscript which
    /// performs the stat updates
    /// 
    public static void UpdateStatistic(string stat, int value)
    {
        PlayFabClientAPI.WritePlayerEvent(
            // Request
            new WriteClientPlayerEventRequest
            {
                EventName = "update_statistic",
                Body = new Dictionary<string, object>
                {
                    { "stat_name", stat },
                    { "value", value }
                }
            },
            // Success
            (WriteEventResponse response) =>
            {
                Debug.Log("WritePlayerEvent (UpdateStatistic) completed.");
                getLeaderboard(stat, 0, 7);
                NoticeManager.Instance.Dispatch(PlayFabNotice.UpdateStatisticComplete);
            },
            // Failure
            (PlayFabError error) =>
            {
                NoticeManager.Instance.Dispatch(PlayFabNotice.OnPlayFabError, error);
            }
            );
    }
    public static void getLeaderboard(string rankName, int startposition, int maxData)
    {
        PlayFabClientAPI.GetLeaderboard(
           // Request
           new GetLeaderboardRequest
           {
               StatisticName = rankName,
               StartPosition = startposition,
               MaxResultsCount = maxData
           },
           // Success
           (GetLeaderboardResult result) =>
           {

               List<PlayerLeaderboardEntry> rankDataList = result.Leaderboard;
               Debug.Log("GetLeaderboard completed.");
               NoticeManager.Instance.Dispatch(NoticeType.User_Get_GameRank, rankDataList);
               NoticeManager.Instance.Dispatch(PlayFabNotice.GetLeaderboardComplete, result);
           },
           // Failure
           (PlayFabError error) =>
           {
               NoticeManager.Instance.Dispatch(PlayFabNotice.OnPlayFabError, error);
           }
           );
    }


   
    public static void TestUpdateShopData()
    {
       // GameDataManager.Instance.TestUpdateShopData();

    }

#region 商店
  
#endregion

    //public void getUserInfo()
    //{
    //    PlayFabClientAPI.GetUserData
    //}
}
