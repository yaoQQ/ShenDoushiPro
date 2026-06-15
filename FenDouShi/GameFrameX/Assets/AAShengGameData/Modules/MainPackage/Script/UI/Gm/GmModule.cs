using msg.system;

public class GmModule : BaseModule
{
    public override ModuleEnum ModuleName()
    {
        return ModuleEnum.GM;
    }
    public override void InitRegisterNet()
    {
        RegisterNetMsg((uint)Cmd.GmCommandListReq); 
        RegisterNetMsg((uint)Cmd.GmCommandListResp);

        RegisterNetMsg((uint)Cmd.GmCommandReq);//使用gm指令
        RegisterNetMsg((uint)Cmd.GmcommandResp);//使用gm指令回调

        //先找个地方初始化一下
         var refs = new Refs();
        //TODO移动到进游戏后？
        ReqGmList();
    }
    public override void OnNetMsgLister(uint protoIDInt, byte[] buffer)
    {
        Cmd protoID = (Cmd)protoIDInt;
        switch (protoID)
        {
            case Cmd.GmCommandListResp: // 返回背包列表
                var data = ProtobufTool.PDeserialize<GmCommandListResp>(buffer);
                Refs.gmData.SetData(data.cmdLists);
                break;
            case Cmd.GmcommandResp:
                var result = ProtobufTool.PDeserialize<GmcommandResp>(buffer);
                var str = Utility.Text.Format(" 命令执行结果 {0}  提示 {1} ", result.Result, result.Msg);
                Logger.PrintColor("blue", str);
                CommonViewUtils.ShowTopTips(str);
                break;
        }
    }

    public void ReqGmList()
    {
        var gmCommandReq = new GmCommandListReq();
        var pDatabuff = ProtobufTool.PSerializer(gmCommandReq);
        UnityWebSocketManager.Instance.SendAsync((uint)Cmd.GmCommandListReq, pDatabuff);
    }

    public static void OSendGm(string req)
    {
        var gmCommandReq = new GmCommandReq() { Content = string.Format($"/{req}")};
        var pDatabuff = ProtobufTool.PSerializer(gmCommandReq);
        UnityWebSocketManager.Instance.SendAsync((uint)Cmd.GmCommandReq, pDatabuff);
    }

    public override void OnNotificationLister(int noticeType, EventSysArgsBase notice)
    {
        Logger.PrintColor("blue", "$$$$$$$$$$$$$$$$$noticeType=" + noticeType);
        switch (noticeType)
        {
            //case (int)EEventType.Login_connect_Success:
            //    LoginComplete(notice);
            //    break;
            //case (int)EEventType.Login_connect_Out:
            //    OnLoginOut(notice);
            //    break;
            //case (int)EEventType.OnLoginError:
            //    OnLoginError(notice);
            //    break;
        }
    }
}