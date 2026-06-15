

using msg.trial;


[ControlAttribute]
public class AthenaTrialControl : BaseControl<AthenaTrialControl>
{
    public  AthenaTrialModel Model { get; private set; }

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new AthenaTrialModel();
    }

    protected override void onLoginSuccess()
    {
        ReqTrialGetInfo();
    }

    // 事件监听处理
    protected override void onEventListener()
    {
        on<TrialGetInfoResp>((uint)Cmd.TrialGetInfoResp, OnTrialGetInfoResp);// 雅典娜的试炼数据获取回调
        on<TrialSweepResp>((uint)Cmd.TrialSweepResp, OnTrialSweepResp);// 扫荡回调
        on<TrialReceiveResp>((uint)Cmd.TrialReceiveResp, OnTrialReceiveResp);// 领取阶段奖励回调
        on<TrialSweepBuyResp>((uint)Cmd.TrialSweepBuyResp, OnTrialSweepBuyResp);// 购买扫荡次数回调
        on<TrialLevelStrategyResp>((uint)Cmd.TrialLevelStrategyResp, OnTrialLevelStrategyResp);// 关卡攻略回调
    }


    /// <summary>
    /// 雅典娜的试炼数据获取请求
    /// </summary>
    public void ReqTrialGetInfo()
    {
        SendNetMsg((uint)Cmd.TrialGetInfoReq, new TrialGetInfoReq());
    }

    /// <summary>
    /// 领取阶段奖励请求
    /// </summary>
    /// <param name="stage"></param>
    public void ReqTrialReceive(int stage)
    {
        SendNetMsg((uint)Cmd.TrialReceiveReq, new TrialReceiveReq() { Id = stage });
    }

    /// <summary>
    /// 扫荡请求
    /// </summary>
    /// <param name="stage"></param>
    public void ReqTrialSweep(int stage)
    {
        SendNetMsg((uint)Cmd.TrialSweepReq, new TrialSweepReq() { Id = stage });
    }

    /// <summary>
    /// 购买扫荡次数请求
    /// </summary>
    /// <param name="type"></param>
    public void ReqTrialSweepBuy(int type)
    {
        SendNetMsg((uint)Cmd.TrialSweepBuyReq, new TrialSweepBuyReq() { Type = type });
    }

    /// <summary>
    /// 关卡攻略请求
    /// </summary>
    /// <param name="id"></param>
    public void ReqTrialLevelStrategy(int id)
    {
        SendNetMsg((uint)Cmd.TrialLevelStrategyReq, new TrialLevelStrategyReq() { Id = id });
    }

    /// <summary>
    /// 购买挑战次数
    /// </summary>
    /// <param name="type"></param>
    public void OpenBuyChallengeView(int type)
    {
        var canBuyCnt = Model.GetCanBuyChallengeCnt(type);
        if (canBuyCnt <= 0)
        {
            CommonViewUtils.ShowTopTips("今日购买已达上限");
            return;
        }
        var costId = 0;
        var costNum = 0;
        var cfg = Model.GetTypeCfgByTypeId(type);
        var buyCnt = cfg.PaySweepCnt - canBuyCnt;
        var mCnt = cfg.PaySweepCost.Count;
        var costIdx = 0;
        for (var i = 0; i < mCnt; i++)
        {
            if (i != buyCnt) continue;
            costIdx = i;
            break;
        }
        if (costIdx == 0)
        {
            costIdx = mCnt - 1;
        }
        costId = cfg.PaySweepCost[costIdx][0];
        costNum = cfg.PaySweepCost[costIdx][1];
        var iconUrl = ItemTools.GetItemIcon(costId);
        var buyStr = Utility.Text.Format("花费<img src='{0}'width='40' height='40'/>{1}购买一次扫荡次数", iconUrl, costNum);
        var msgVo = new MessageBoxVo
        {
            title = "提示",
            msg = buyStr,
            TipStr = "今日不再提示",
            RightText = Utility.Text.Format("剩余购买次数:{0}", canBuyCnt),
            isCheckNoShowTodayKey = Utility.Text.Format("{0}_AthenaTrial_cost_no_show_today", RoleControl.Instance?.Model?.getRoleInfo()?.roleId ?? 0),
            CheckNoShowState = ECheckNoShowState.Today,
            OkBtnfunc = () =>
            {
                ReqTrialSweepBuy(type);
            }
        };
        CommonViewUtils.ShowMessageBox(msgVo);
    }

    private void OnTrialGetInfoResp(TrialGetInfoResp resp)
    {
        Model.SetTrialInfo(resp);
    }

    private void OnTrialSweepResp(TrialSweepResp resp)
    {
        Model.OnTrialSweepResp(resp.Id);
    }

    private void OnTrialReceiveResp(TrialReceiveResp resp)
    {
        Model.OnTrialReceiveResp(resp);
    }
    private void OnTrialSweepBuyResp(TrialSweepBuyResp resp)
    {
        Model.OnTrialSweepBuyResp(resp.Type);
    }

    private void OnTrialLevelStrategyResp(TrialLevelStrategyResp resp)
    {
        if (resp.userInfoes?.Count <= 0)
        {
            CommonViewUtils.ShowTopTips("暂无通关记录");
            return;
        }
        if (!UIViewManager.Instance.GetIsShowing(UIViewEnum.AthenaTrialPlayBackView))
        {
            UIViewManager.Instance.Show(UIViewEnum.AthenaTrialPlayBackView, resp.userInfoes);
        }
    }

    // 清理数据调用
    protected override void onClear()
    {
    }
}
