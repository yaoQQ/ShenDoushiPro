

using msg.arena;

[ControlAttribute]
public class ArenaControl : BaseControl<ArenaControl>
{
    public ArenaModel Model { get; private set; }

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new ArenaModel();
    }

    // 登录成功后调用
    protected override void onLoginSuccess()
    {
        // 登录成功后获取竞技场信息
        ReqArenaInfo();
    }

    // 事件监听处理
    protected override void onEventListener()
    {
        // 竞技场信息回调
        on<ArenaInfoResp>((uint)Cmd.ArenaInfoResp, ArenaInfoResp);
        // 竞技场匹配列表回调
        on<ArenaMatchResp>((uint)Cmd.ArenaMatchResp, ArenaMatchResp);
        // 竞技场刷新列表回调
        on<ArenaRefreshResp>((uint)Cmd.ArenaRefreshResp, ArenaRefreshResp);
        // 竞技场赛季更新回调
        on<ArenaNewSeasonResp>((uint)Cmd.ArenaNewSeasonResp, ArenaNewSeasonResp);
        // 竞技场次数奖励回调
        on<ArenaTimesRewardResp>((uint)Cmd.ArenaTimesRewardResp, ArenaTimesRewardResp);
    }

    // 清理数据调用
    protected override void onClear()
    {
        Model.ClearArenaData();
    }

    /// <summary>
    /// 请求竞技场信息
    /// </summary>
    public void ReqArenaInfo()
    {
        var req = new ArenaInfoReq();
        SendNetMsg((uint)Cmd.ArenaInfoReq, req);
    }

    /// <summary>
    /// 请求竞技场匹配
    /// </summary>
    public void ReqArenaMatch()
    {
        var req = new ArenaMatchReq();
        SendNetMsg((uint)Cmd.ArenaMatchReq, req);
    }

    /// <summary>
    /// 请求刷新竞技场匹配列表
    /// </summary>
    public void ReqArenaRefresh()
    {
        var req = new ArenaRefreshReq();
        SendNetMsg((uint)Cmd.ArenaRefreshReq, req);
    }

    /// <summary>
    /// 请求领取竞技场次数奖励
    /// </summary>
    /// <param name="rewardId">奖励ID</param>
    public void ReqArenaTimesReward(int rewardId)
    {
        var req = new ArenaTimesRewardReq
        {
            Id = rewardId
        };
        SendNetMsg((uint)Cmd.ArenaTimesRewardReq, req);
    }

    /// <summary>
    /// 请求跳过战斗
    /// </summary>
    /// <param name="targetId">目标玩家ID</param>
    public void ReqSkipBattle(long targetId)
    {
        var req = new ArenaMatchReq
        {
           // targetId = targetId
        };
        SendNetMsg((uint)Cmd.ArenaMatchReq, req);
    }

    ///------------------------服务返回----------------

    /// <summary>
    /// 竞技场信息回调
    /// </summary>
    /// <param name="resp">竞技场信息响应</param>
    private void ArenaInfoResp(ArenaInfoResp resp)
    {
        Logger.PrintDebug("竞技场信息回调 resp=" + resp);

        if (resp != null)
        {
            // 设置竞技场信息到模型
            Model.SetArenaInfo(resp);

            // 分发事件通知UI更新
            EventManager.Instance.Dispatch(EEventType.EventArenaInfoUpdate, resp);
        }
    }

    /// <summary>
    /// 竞技场匹配列表回调
    /// </summary>
    /// <param name="resp">匹配列表响应</param>
    private void ArenaMatchResp(ArenaMatchResp resp)
    {
        Logger.PrintDebug("竞技场匹配列表回调 resp=" + resp);

        if (resp != null)
        {
            // 设置匹配列表到模型
            Model.SetMatchList(resp.matchLists);

            // 分发事件通知UI更新
            EventManager.Instance.Dispatch(EEventType.EventArenaMatchUpdate, resp);
        }
    }

    /// <summary>
    /// 竞技场刷新列表回调
    /// </summary>
    /// <param name="resp">刷新列表响应</param>
    private void ArenaRefreshResp(ArenaRefreshResp resp)
    {
        Logger.PrintDebug("竞技场刷新列表回调 resp=" + resp);

        if (resp != null)
        {
            // 更新匹配列表到模型
            Model.SetMatchList(resp.matchLists);

            // 分发事件通知UI更新
            EventManager.Instance.Dispatch(EEventType.EventArenaRefreshUpdate, resp);
        }
    }

    /// <summary>
    /// 竞技场新赛季回调
    /// </summary>
    /// <param name="resp">新赛季响应</param>
    private void ArenaNewSeasonResp(ArenaNewSeasonResp resp)
    {
        Logger.PrintDebug("竞技场新赛季回调 resp=" + resp);

        if (resp != null)
        {
            // 处理新赛季数据
            Model.HandleNewSeason(resp);

            // 分发事件通知UI更新
            EventManager.Instance.Dispatch(EEventType.EventArenaNewSeason, resp);
        }
    }

    /// <summary>
    /// 竞技场次数奖励回调
    /// </summary>
    /// <param name="resp">次数奖励响应</param>
    private void ArenaTimesRewardResp(ArenaTimesRewardResp resp)
    {
        Logger.PrintDebug("竞技场次数奖励回调 resp=" + resp);

        if (resp != null)
        {
            // 处理奖励领取
            Model.HandleTimesReward(resp);

            // 分发事件通知UI更新
            EventManager.Instance.Dispatch(EEventType.EventArenaTimesReward, resp);
        }
    }
}
