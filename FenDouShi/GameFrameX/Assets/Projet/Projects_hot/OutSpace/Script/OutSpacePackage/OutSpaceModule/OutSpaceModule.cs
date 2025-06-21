
using System.Collections.Generic;
public class OutSpaceModule : BaseModule
{
    public override ModuleEnum ModuleName()
    {
        return ModuleEnum.OutSpace;
    }
    public override void InitRegisterNet()
    {
        //test@@@
        //  RegisterNetMsg(protoEnum.TestProtoEnum);
    }
    public override void OnNetMsgLister(uint protoID, byte[] buffer)
    {
        //switch (protoID)
        //{
        //    //test@@@
        //    //case protoEnum.TestProtoEnum:
        //    //    testNetFun(buffer);
        //    //    break;
        //}
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

            //notificationList.Add(PlayFabNotice.LoginComplete);
            //notificationList.Add(NoticeType.LoadSceneComplete);
            //加载场景完成


        }
        return notificationList;
    }

    public override void OnNotificationLister(string noticeType, BaseNotice notice)
    {

        switch (noticeType)
        {

            case PlayFabNotice.LoginComplete:

                break;
            case NoticeType.LoadSceneComplete:

                break;


            case NoticeType.Normal_QuitGame:
                //   Driver.Instance.QuitGame();
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
        //test@@@
        // UserPlayerData playerData = (UserPlayerData)obj.GetObj();

        // UIViewManager.Instance.Open(UIViewEnum.Platform_Top_Cost_View);
    }
}
