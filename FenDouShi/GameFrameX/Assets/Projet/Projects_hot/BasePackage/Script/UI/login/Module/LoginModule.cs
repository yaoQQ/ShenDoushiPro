using UnityEngine;
using System.Collections.Generic;
using PlayFab.ClientModels;
using System.Collections;

public class LoginModule  : BaseModule
{
 
    public override ModuleEnum ModuleName()
    {
        return ModuleEnum.Login;
    }
    public override void InitRegisterNet()
    {
        //test@@@
        RegisterNetMsg(protoEnum.TestProtoEnum);
    }
    public override void OnNetMsgLister(uint protoID, byte[] buffer)
    {
        switch (protoID)
        {
            //test@@@
            case 0:
                testNetFun(buffer);
                break;
        }
    }
    public override void OnJsonMsgLister(uint protoID, string jsonData)
    {

    }

    private void testNetFun(byte[] buffer)
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
            notificationList.Add(PlayFabNotice.OnPlayFabError);
            
        }
        return notificationList;
    }

    public override void OnNotificationLister(string noticeType, BaseNotice notice)
    {
        Logger.PrintColor("blue", "$$$$$$$$$$$$$$$$$noticeType=" + noticeType);
        switch (noticeType)
        {
           
            case PlayFabNotice.LoginComplete:
                LoginComplete(notice);
                break;
            case PlayFabNotice.LoadAccountDataComplete:
                LoadAccountDataComplete(notice);
                break;
            case PlayFabNotice.LoadUserDataComplete:
                LoadUserDataComplete(notice);
                break;


            case NoticeType.Normal_QuitGame:
                //   Driver.Instance.QuitGame();
                break;
            case PlayFabNotice.OnPlayFabError:
                OnPlayFabError(notice);
                break;
        }
    }
    private void LoginComplete(BaseNotice rep)
    {
        if (rep == null)
        {
            return;
        }
        ObjectNotice obj = (ObjectNotice)rep;
        // ---设置玩家数据
        
        UserPlayerData playerData = (UserPlayerData)obj.GetObj();
        Logger.PrintDebug("@@@LoginComplete()");
        //Logger.PrintDebug("@@@playerData=" + playerData.ToString());
        //Logger.PrintDebug("@@@playerData.UserDisplayName=" + playerData.UserDisplayName);
        //Logger.PrintDebug("@@@playerData.Email=" + playerData.Email);
        //Logger.PrintDebug("@@@playerData.PlayFabId=" + playerData.PlayFabId);
        //Logger.PrintDebug("@@@playerData.Diamond=" + playerData.Diamond);
        Logger.PrintDebug("@@@playerData.Gold=" + playerData.Gold);
       // this.UpdateUserinfo(noticeType, rep);
        // PlatformUserProxy: GetInstance():setUserInfoData(playerData)
        NoticeManager.Instance.Dispatch(NoticeType.User_Update_UserBaseInfo);

        // UIViewManager.Instance.DestroyView(UIViewEnum.LoginView2);
        UIViewManager.Instance.Close(UIViewEnum.LoginView2);
        MainThread.Instance.StartCoroutine(DelayEnter());
    }
    private IEnumerator  DelayEnter()
    {
        yield return new WaitForSeconds(1);
        Logger.PrintColor("red", "@@@@@@@@@DelayEnter()222222222222222");
        Debug.Log("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@2222ProjectControler.cameraPostPro=" + ProjectControler.cameraPostPro);
        //加载littlePrice场景包
        //UIViewManager.Instance.Open(UIViewEnum.Platform_Top_Cost_View);
        // UIViewManager.Instance.DestroyView(UIViewEnum.LoginView2);
        //test
        // PreloadManager.Instance.PreLoadPackage(ProjectControler.cameraPostPro);
        PreloadManager.Instance.PreLoadPackage(ProjectControler.OutSpacePro);
    }
    private void LoadAccountDataComplete(BaseNotice rep)
    {
        if (rep == null)
        {
            return;
        }
        ObjectNotice obj = (ObjectNotice)rep;
        UserAccountInfo AccountData = (UserAccountInfo)obj.GetObj();
        Logger.PrintDebug("@@@LoadAccountDataComplete AccountData=" + AccountData);
    }
    private void LoadUserDataComplete(BaseNotice rep)
    {
        if (rep == null)
        {
            return;
        }
        ObjectNotice obj = (ObjectNotice)rep;
        Dictionary<string, UserDataRecord> userData = (Dictionary<string, UserDataRecord>)obj.GetObj();

        PlatformUserProxy.UpdateUserGameData(userData);
        NoticeManager.Instance.Dispatch(NoticeType.User_Update_UserBaseInfo);
        NoticeManager.Instance.Dispatch(NoticeType.Load_Success_In_Game);
    }
    private void OnPlayFabError(BaseNotice notice)
    {
        if (notice == null)
        {
            return;
        }
        ObjectNotice obj = (ObjectNotice)notice;
        PlayFab.PlayFabError errorResult = (PlayFab.PlayFabError)obj.GetObj();
       // Logger.PrintError("OnPlayFabError=" + errorResult.GenerateErrorReport());
        CommonView.showTopTips("error:"+ errorResult.GenerateErrorReport());
    }
    //========================发送数据=========
    //登入游戏
    public static void LoginPlatform(Authtypes authType, string userAccount = null, string userPassWord = null)
    {
        PlayFabAuthService.Instance.Authenticate(authType, userAccount, userPassWord);
    }
    //请求玩家数据
    public static void reqUserGameData()
    {
        
        PlayFabManager.LoadAccountData();//--加载游戏中保存的数据 
        //  PlayFabManager.LoadTitleData();

        PlayFabManager.LoadUserData();////--加载游戏中保存的数据 
    }

 
}