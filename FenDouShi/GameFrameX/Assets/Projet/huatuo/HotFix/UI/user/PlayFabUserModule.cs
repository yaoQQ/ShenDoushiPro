using PlayFab;
using PlayFab.ClientModels;
using SimpleJSON;
using System.Collections.Generic;

public class PlayFabUserModule : BaseModule
{
    public override ModuleEnum ModuleName()
    {
        return ModuleEnum.PlayFabUser;
    }
    public override void InitRegisterNet()
    {

    }
    public override void OnNetMsgLister(uint protoID, byte[] buffer)
    {
    }
    public override void OnJsonMsgLister(uint protoID, string jsonData)
    {

    }

    public override List<string> GetRegisterNotificationList()
    {
        if (notificationList == null)
        {
            notificationList = new List<string>();

            notificationList.Add(PlayFabNotice.SetUserDisplayNameComplete);
            notificationList.Add(PlayFabNotice.UpdateStatisticComplete);

            notificationList.Add(PlayFabNotice.GetLeaderboardComplete);
            notificationList.Add(PlayFabNotice.GetTitleDataValueComplete);
            notificationList.Add(PlayFabNotice.GetUserDataValueComplete);
            notificationList.Add(PlayFabNotice.UpdateUserGameDataComplete);

     

            notificationList.Add(PlayFabNotice.UpdateUserInfo);
            notificationList.Add(PlayFabNotice.OnPlayFabError);
        }
        return notificationList;
    }

    public override void OnNotificationLister(string noticeType, BaseNotice notice)
    {
        ObjectNotice onValue = notice as ObjectNotice;
        switch (noticeType)
        {
            case PlayFabNotice.SetUserDisplayNameComplete:
                SetUserDisplayNameComplete(notice);
                break;
            case PlayFabNotice.UpdateStatisticComplete:
                UpdateStatisticComplete(notice);
                break;

            case PlayFabNotice.GetLeaderboardComplete:
                GetLeaderboardComplete(notice);
                break;
            case PlayFabNotice.GetTitleDataValueComplete:
                GetTitleDataValueComplete(notice);
                break;
            case PlayFabNotice.GetUserDataValueComplete:
                GetUserDataValueComplete(notice);
                break;
            case PlayFabNotice.UpdateUserGameDataComplete:
                UpdateUserGameDataComplete(notice);
                break;

         
            case PlayFabNotice.OnPlayFabError:
                OnPlayFabError(notice);
                break;
        }
    }
    private void SetUserDisplayNameComplete(BaseNotice rep)
    {
        if (rep == null)
        {
            return;
        }
        ObjectNotice obj = (ObjectNotice)rep;
        string userName = (string)obj.GetObj();
        GameDataManager.Instance.SetPlayerDisPlayName(userName);
        Logger.PrintDebug("@@@设置玩家名字成功=" + userName);
        NoticeManager.Instance.Dispatch(NoticeType.User_Update_UserBaseInfo);
    }

    private void UpdateStatisticComplete(BaseNotice notice)
    {
        Logger.PrintDebug("@@@更新排行榜UpdateStatisticComplete()");
    }

    private void GetLeaderboardComplete(BaseNotice notice)
    {
        Logger.PrintDebug("@@@获取排行榜GetLeaderboardComplete()");
    }

    private void GetTitleDataValueComplete(BaseNotice notice)
    {
        Logger.PrintDebug("@@@GetTitleDataValueComplete()");
    }
    private void GetUserDataValueComplete(BaseNotice notice)
    {
        Logger.PrintDebug("@@@GetUserDataValueComplete()");
    }

    private void UpdateUserGameDataComplete(BaseNotice notice)
    {
        Logger.PrintDebug("@@@UpdateUserGameDataComplete()");
    }



    

    
    private void OnPlayFabError(BaseNotice notice)
    {
        if (notice == null)
        {
            return;
        }
        ObjectNotice obj = (ObjectNotice)notice;
        PlayFabError errorResult = (PlayFabError)obj.GetObj();
        Logger.PrintError("OnPlayFabError=" + errorResult.GenerateErrorReport());
      
    }

    //===============================发送数据===========
    //设置玩家性别
    public static void updateUserSex(int sex)
    {
        PlayFabManager.UpdateUserData(CharacterDataEnum.CharacterSex, sex.ToString());
    }
    //设置玩家名字
    public static void SetUserDisplayName(string updateNickName)
    {
        PlayFabManager.SetUserDisplayName(updateNickName);
    }

    /// <summary>
    /// 获取排行榜数据
    /// </summary>
    /// <param name="leadboardName">排行榜名 eliminate，bowling，high_score
    /// RankDataProxy.eliminateRank="eliminate"，
    /// RankDataProxy.bowlingRank="bowling"，
    /// RankDataProxy.high_score="high_score"</param>
    /// <param name="begainIndex">开始排行榜条目</param>
    /// <param name="leaderCount">获取的数量</param>
    public static void getLeaderboard(string leadboardName, int begainIndex, int leaderCount)
    {
        PlayFabManager.getLeaderboard(leadboardName, begainIndex, leaderCount);
    }
    /// <summary>
    /// 更新玩家排行榜数据
    /// </summary>
    /// <param name="leadboardNam">排行榜名字</param>
    /// <param name="playerValue">数值</param>
    public static void UpdateStatistic(string leadboardName, int playerValue)
    {
        PlayFabManager.UpdateStatistic(leadboardName, playerValue);
    }
   
}