
using msg.battle;
using System.Collections.Generic;


//战斗类型---》对应
//战斗类型---》对应
public enum BattleFightType
{
    MainStory = 1,    // 主线副本
    DailyDungeon = 2, // 每日副本
    Arena = 3,        // 竞技场
    BattleReplay = 4, // 战斗录像
    Sparring = 5,     // 切磋
    TrialTower = 6,   // 试炼塔
    Championship = 7  // 争霸赛
}

[ControlAttribute]
public class BattleControl : BaseControl<BattleControl>
{
    public BattleModel Model { get; private set; }

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new BattleModel();
    }

    protected override void onLoginSuccess()
    {
        // 登录成功后可以根据需要请求战斗数据
    }

    // 事件监听处理
    protected override void onEventListener()
    {
        // 战斗信息回调
        on<BattleResp>((uint)Cmd.BattleResp, BattleResp);
        // 战斗回合回调
        on<BattleRoundResp>((uint)Cmd.BattleRoundResp, BattleRoundResp);
        // 战斗结果回调
        on<BattleResultResp>((uint)Cmd.BattleResultResp, BattleResultResp);
        // 战斗目标信息回调
        on<BattleTargetInfoResp>((uint)Cmd.BattleTargetInfoResp, BattleTargetInfoResp);
        // 战斗报告列表回调
        on<BattleReportListResp>((uint)Cmd.BattleReportListResp, BattleReportListResp);
    }

    /// <summary>
    /// @@@@@@@请求战斗信息
    /// </summary>
    /// <param name="type">战斗类型</param>
    /// <param name="id">战斗ID</param>
    /// <param name="skip">是否跳过战斗</param>
    /// <param name="parameters">额外参数</param>
    public void ReqBattleInfo(int id, int type, bool skip = true, long[] parameters = null)
    {
        var req = new BattleReq
        {
            Type = type,
            Id = id,
            Skip = skip,
            Params = parameters ?? new long[0]
        };
        Logger.PrintGreen($"@@@@@@ReqBattleInfo type={type} id={id} skip={skip} parameters={parameters}");
        SendNetMsg((uint)Cmd.BattleReq, req);
    }

    /// <summary>
    /// 请求战斗回合
    /// </summary>
    /// <param name="type">战斗类型</param>
    /// <param name="waveId">波次ID</param>
    /// <param name="round">回合数</param>
    public void ReqBattleRound(int type, int waveId, int round)
    {
        var req = new BattleRoundReq
        {
            Type = type,
            waveId = waveId,
            Round = round
        };
        SendNetMsg((uint)Cmd.BattleRoundReq, req);
    }

    /// <summary>
    /// 请求战斗目标信息
    /// </summary>
    /// <param name="type">目标类型</param>
    /// <param name="id">目标ID</param>
    /// <param name="parameters">参数</param>
    public void ReqBattleTargetInfo(int type, int id, long[] parameters = null)
    {
        var req = new BattleTargetInfoReq
        {
            Type = type,
            Id = id,
            Params = parameters ?? new long[0]
        };
        SendNetMsg((uint)Cmd.BattleTargetInfoReq, req);
    }

    /// <summary>
    /// 请求跳过战斗
    /// </summary>
    /// <param name="type">战斗类型</param>
    /// <param name="id">战斗ID</param>
    public void ReqSkipBattle(int type, int id)
    {
        var req = new BattleSkipReq
        {
            Type = type
        };
        SendNetMsg((uint)Cmd.BattleSkipReq, req);
    }

    /// <summary>
    /// 请求结束战斗
    /// </summary>
    /// <param name="type">战斗类型</param>
    /// <param name="id">战斗ID</param>
    public void ReqEndBattle(int type, int id)
    {
        var req = new BattleEndReq
        {
            Type = type
        };
        SendNetMsg((uint)Cmd.BattleEndReq, req);
    }

    /// <summary>
    /// 请求战斗报告列表
    /// </summary>
    /// <param name="reportType">报告类型</param>
    public void ReqBattleReportList(ReportType reportType)
    {
        var req = new BattleReportListReq
        {
            Type = reportType
        };
        SendNetMsg((uint)Cmd.BattleReportListReq, req);
    }

    ///------------------------服务返回----------------

    /// <summary>
    /// 战斗信息回调
    /// </summary>
    /// <param name="resp"></param>
    private void BattleResp(BattleResp resp)
    {
        Logger.PrintDebug("战斗信息回调 resp=" + resp);
        Logger.PrintToJson(resp);
        if (resp != null)
        {
            // 设置战斗信息到模型
            Model.SetBattleInfo(resp);

            // 分发事件通知UI更新
            EventManager.Instance.Dispatch(EEventType.EventBattleInfoUpdate, resp);
        }
    }

    /// <summary>
    /// 战斗回合回调
    /// </summary>
    /// <param name="resp"></param>
    private void BattleRoundResp(BattleRoundResp resp)
    {
        Logger.PrintDebug("战斗回合回调 resp=" + resp);

        if (resp != null)
        {
            // 更新回合信息到模型
            Model.SetBattleRoundInfo(resp);

            // 分发事件通知UI更新
            EventManager.Instance.Dispatch(EEventType.EventBattleRoundUpdate, resp);
        }
    }

    /// <summary>
    ///@@@@@@@@ 战斗结果回调
    /// </summary>
    /// <param name="resp"></param>
    private void BattleResultResp(BattleResultResp resp)
    {
        Logger.PrintDebug("战斗结果回调 resp=" + resp);

        if (resp != null)
        {
            // 设置战斗结果到模型
            Model.SetBattleResult(resp);

            // 分发事件通知UI更新
            EventManager.Instance.Dispatch(EEventType.EventBattleResult, resp);
        }
    }

    /// <summary>
    /// 战斗目标信息回调
    /// </summary>
    /// <param name="resp"></param>
    private void BattleTargetInfoResp(BattleTargetInfoResp resp)
    {
        Logger.PrintDebug("战斗目标信息回调 resp=" + resp);
        
        if (resp != null)
        {
            // 设置目标信息到模型
            Model.SetBattleTargetInfo(resp);
            
            // 分发事件通知UI更新
            EventManager.Instance.Dispatch(EEventType.EventBattleTargetInfoUpdate, resp);
        }
    }

    /// <summary>
    ///@@@@ 战斗报告列表回调
    /// </summary>
    /// <param name="resp"></param>
    private void BattleReportListResp(BattleReportListResp resp)
    {
        Logger.PrintDebug("战斗报告列表回调 resp=" + resp);
        
        if (resp != null)
        {
            // 设置报告列表到模型
            Model.SetBattleReportList(resp);
            
            // 分发事件通知UI更新
            EventManager.Instance.Dispatch(EEventType.EventBattleReportListUpdate, resp);
        }
    }

    // 清理数据调用
    protected override void onClear()
    {
        Model.ClearBattleData();
    }
}
