using msg.onHook;
using System.Diagnostics;
using System.Text;

[Control]
public class OnHookControl : BaseControl<OnHookControl>
{
    public OnHookModel Model { get; private set; }

    bool isLogin = false;

    public bool isHookLevelUp = true;

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new OnHookModel();
    }

    // 事件监听处理
    protected override void onEventListener()
    {
        on<OnHookGetInfoResp>((int)Cmd.OnHookGetInfoResp, OnHookGetInfoResp);
        on<OnHookSpeedUpResp>((int)Cmd.OnHookSpeedUpResp, OnHookSpeedUpResp);
        on<OnHookGainRewardResp>((int)Cmd.OnHookGainRewardResp, OnHookGainRewardResp);
    }

    // 清理数据调用
    protected override void onClear() { }

    protected override void onLoginSuccess()
    {
        isLogin = false;
        OnHookGetInfoReq();
    }

    //挂机信息请求
    public void OnHookGetInfoReq()
    {
        //if (!SystemOpenControl.Instance.Model.GetIsSystemOpen(28000)) return;

        Log("[挂机]请求挂机信息");
        Model.reqInfoDateTime = TimeHelper.GetlocalTimeStamp() - TimeHelper.GetlocalTimeStamp() % 60 + 2;
        SendNetMsg((int)Cmd.OnHookGetInfoReq, new OnHookGainRewardReq());
    }

    void OnHookGetInfoResp(OnHookGetInfoResp resp)
    {
#if UNITY_EDITOR
        StringBuilder sb = new($"[挂机]挂机信息回调:dungeonId:{resp.dungeonId},startTime:{resp.startTime.GetDateTime()},freeCount:{resp.freeCount},buyCount:{resp.buyCount},compensateCount:{resp.compensateCount},resourceInfos:{resp.resourceInfos.Count}\n");
        foreach (var i in resp.resourceInfos)
        {
            sb.AppendLine($"累计收益:道具信息：type:{i.Type}, uniqueId:{i.uniqueId}, id;{i.Id}, num:{i.Num}, expire:{i.Expire}, tag:{i.Tag}");
        }
        Logger.PrintLog(sb.ToString());
#endif

        Model.OnHookGetInfoResp(resp);
        EventManager.Instance.Dispatch(EEventType.OnHookGetInfoResp);

        if (!isLogin)
        {
            isLogin = true;

            if (Model.CanShowOfflineReward())
            {
                UIViewManager.Instance.Show(UIViewEnum.OnHookView_ShowReward);
            }
        }
    }

    //挂机加速请求
    public void OnHookSpeedUpReq()
    {
        Log("[挂机]请求挂机加速");
        isHookLevelUp = true;
        SendNetMsg((int)Cmd.OnHookSpeedUpReq, new OnHookGainRewardReq());
    }

    void OnHookSpeedUpResp(OnHookSpeedUpResp resp)
    {
        isHookLevelUp = false;
        Log($"[挂机]挂机加速回调:freeCount:{resp.freeCount},buyCount:{resp.buyCount},compensateCount:{resp.compensateCount},level:{resp.beforeLevel},exp:{resp.beforeExp},rewardInfos:{resp.rewardInfos.Count}");
        foreach (var i in resp.rewardInfos)
        {
            Log($"道具信息：type:{i.Type}, uniqueId:{i.uniqueId}, id;{i.Id}, num:{i.Num}, expire:{i.Expire}, tag:{i.Tag}");
        }

        Model.OnHookSpeedUpResp(resp);
        EventManager.Instance.Dispatch(EEventType.OnHookSpeedUpResp);
        UIViewManager.Instance.Show(UIViewEnum.OnHookView_ReceiveReward);
    }

    //挂机领取奖励请求
    public void OnHookGainRewardReq()
    {
        Log("[挂机]挂机领取奖励请求");
        SendNetMsg((int)Cmd.OnHookGainRewardReq, new OnHookGainRewardReq());
    }

    //挂机领取奖励回调
    public void OnHookGainRewardResp(OnHookGainRewardResp resp)
    {
        Log($"[挂机]挂机领取奖励回调:dungeonId:{resp.dungeonId},startTime:{resp.startTime},\n" +
            $"level:{resp.beforeLevel}->{resp.afterLevel},exp:{resp.beforeExp}->{resp.afterExp},\n" +
            $"rewardInfos:{resp.rewardInfos.Count},resourceInfos:{resp.resourceInfos.Count}");
        foreach (var i in resp.rewardInfos)
        {
            Log($"[奖励]道具信息：type:{i.Type}, uniqueId:{i.uniqueId}, id;{i.Id}, num:{i.Num}, expire:{i.Expire}, tag:{i.Tag}");
        }
        foreach (var i in resp.resourceInfos)
        {
            Log($"[累计收益]道具信息：type:{i.Type}, uniqueId:{i.uniqueId}, id;{i.Id}, num:{i.Num}, expire:{i.Expire}, tag:{i.Tag}");
        }

        Model.OnHookGainRewardResp(resp);
        EventManager.Instance.Dispatch(EEventType.OnHookGainRewardResp);

        UIViewManager.Instance.Show(UIViewEnum.OnHookView_ReceiveReward);
    }

    [Conditional("UNITY_EDITOR")]
    public void Log(string str)
    {
        Logger.PrintLog(str);
    }

    // 系统开启
    public bool IsSystemOpen()
    {
        return SystemOpenControl.Instance.Model.GetIsSystemOpen(OnHookConst.systemOpenId);
    }
}
