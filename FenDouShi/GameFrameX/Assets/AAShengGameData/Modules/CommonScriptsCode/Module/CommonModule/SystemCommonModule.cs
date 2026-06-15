using DataTableFrame;
using msg.system;
using System.Collections.Generic;

public class SystemCommonModule : BaseModule
{
    public override ModuleEnum ModuleName()
    {
        return ModuleEnum.SystemCommonModule;
    }
    public override void InitRegisterNet()
    {
        RegisterNetMsg((uint)Cmd.Unknown);
        RegisterNetMsg((uint)Cmd.TipsCodeResp);
  
        RegisterNetMsg((uint)Cmd.SystemRootResp);
        RegisterNetMsg((uint)Cmd.SettingListResp);
        RegisterNetMsg((uint)Cmd.SettingResp);
        RegisterNetMsg((uint)Cmd.ReportResp);


        RegisterNetMsg((uint)Cmd.SystemRootReq);
        RegisterNetMsg((uint)Cmd.SettingListReq);
        RegisterNetMsg((uint)Cmd.SettingReq);
        RegisterNetMsg((uint)Cmd.ReportReq);
    }
    public override void OnNetMsgLister(uint protoIDInt, byte[] buffer)
    {
        Logger.PrintDebug("SystemCommonModule OnNetMsgLister=" + protoIDInt);
        Cmd protoID = (Cmd)protoIDInt;
        switch (protoID)
        {
            case Cmd.Unknown: // 错误码
                break;
            case Cmd.TipsCodeResp:  // // 错误码
                OnTipsCodeResp(buffer);
                break;
            case Cmd.SystemRootResp:// 解锁系统查询回调
                break;
            case Cmd.SettingListResp: // 设置列表回调
                break;
            case Cmd.SettingResp:// 设置回调
                break;
            case Cmd.ReportResp: // 举报回调
                break;
        }
    }
  
    private static void OnTipsCodeResp(byte[] MsgData)
    {
        TipsCodeResp tipsCodeResp = ProtobufTool.PDeserialize<TipsCodeResp>(MsgData);
        // TODO，显示tips
        ResultVo resultVo= ConfigMgr.Instance.GetConfigVoById<ResultVo>(tipsCodeResp.Code);
        if (resultVo != null)
        {
          //  CommonViewUtils.ShowAlertMsg("错误", $"错误码: Code:{tipsCodeResp.Code}，Params:{resultVo.Notice}", "确定", null);
            //MessageBoxVo msgVo = new MessageBoxVo();
            //msgVo.title = "错误";
            //msgVo.msg = $"错误码: Code:{tipsCodeResp.Code}，Params:{resultVo.Notice}";
            //msgVo.isCheckNoShowTodayKey = "OnTipsCodeResp";
            //msgVo.OkBtnfunc = () =>
            //{

            //};
            //msgVo.CancelBtnfunc = () =>
            //{
            //};
            //CommonViewUtils.ShowMessageBox(msgVo);
            CommonViewUtils.ShowTopTips($"提示码:{tipsCodeResp.Code},{resultVo.Notice}");
            Logger.PrintLog($"提示码:{tipsCodeResp.Code},{resultVo.Notice}");
        }
        else
        {
            Logger.PrintError($"错误码:{tipsCodeResp.Code} 未配置");
        }


    }
    public override List<int> GetRegisterNotificationList()
    {
        if (notificationList == null)
        {
            notificationList = new List<int>();

        }
        return notificationList;
    }

    public override void OnNotificationLister(int noticeType, EventSysArgsBase notice)
    {
        switch (noticeType)
        {
        }
    }
  


}