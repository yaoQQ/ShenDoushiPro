

using msg.battle;
using System.Collections.Generic;

public class BattleModel : BaseModel
{
    // 当前战斗信息
    private BattleResp mCurrentBattleInfo;
    
    // 当前战斗回合信息
    private BattleRoundResp mCurrentRoundInfo;
    
    // 战斗结果
    private BattleResultResp mBattleResult;
    
    // 战斗目标信息
    private BattleTargetInfoResp mBattleTargetInfo;
    
    // 战斗报告列表
    private BattleReportListResp mBattleReportList;

    // 初始化调用
    protected override void onInit()
    {
        
    }

    // 监听事件
    protected override void onEventListener()
    {
    }

    /// <summary>
    /// 设置战斗信息
    /// </summary>
    /// <param name="battleInfo"></param>
    public void SetBattleInfo(BattleResp battleInfo)
    {
        mCurrentBattleInfo = battleInfo;
    }

    /// <summary>
    /// 获取当前战斗信息
    /// </summary>
    /// <returns></returns>
    public BattleResp GetBattleInfo()
    {
        return mCurrentBattleInfo;
    }

    /// <summary>
    /// 设置战斗回合信息
    /// </summary>
    /// <param name="roundInfo"></param>
    public void SetBattleRoundInfo(BattleRoundResp roundInfo)
    {
        mCurrentRoundInfo = roundInfo;
    }

    /// <summary>
    /// 获取当前战斗回合信息
    /// </summary>
    /// <returns></returns>
    public BattleRoundResp GetBattleRoundInfo()
    {
        return mCurrentRoundInfo;
    }

    /// <summary>
    /// 设置战斗结果
    /// </summary>
    /// <param name="result"></param>
    public void SetBattleResult(BattleResultResp result)
    {
        mBattleResult = result;
    }

    /// <summary>
    /// 获取战斗结果
    /// </summary>
    /// <returns></returns>
    public BattleResultResp GetBattleResult()
    {
        return mBattleResult;
    }

    /// <summary>
    /// 设置战斗目标信息
    /// </summary>
    /// <param name="targetInfo"></param>
    public void SetBattleTargetInfo(BattleTargetInfoResp targetInfo)
    {
        mBattleTargetInfo = targetInfo;
    }

    /// <summary>
    /// 获取战斗目标信息
    /// </summary>
    /// <returns></returns>
    public BattleTargetInfoResp GetBattleTargetInfo()
    {
        return mBattleTargetInfo;
    }

    /// <summary>
    /// 获取攻击方信息
    /// </summary>
    /// <returns></returns>
    public Side GetAttackerInfo()
    {
        return mCurrentBattleInfo?.Attcker;
    }

    /// <summary>
    /// 获取防守方信息
    /// </summary>
    /// <returns></returns>
    public Side GetDefenderInfo()
    {
        return mCurrentBattleInfo?.Defender;
    }

    /// <summary>
    /// 获取场景ID
    /// </summary>
    /// <returns></returns>
    public int GetSceneId()
    {
        return mCurrentBattleInfo?.sceneId ?? 0;
    }

    /// <summary>
    /// 获取当前回合的行动列表
    /// </summary>
    /// <returns></returns>
    public List<Action> GetCurrentActions()
    {
        return mCurrentRoundInfo?.Actions;
    }

    /// <summary>
    /// 获取当前波次ID
    /// </summary>
    /// <returns></returns>
    public int GetCurrentWaveId()
    {
        return mCurrentRoundInfo?.waveId ?? 0;
    }

    /// <summary>
    /// 获取当前回合数
    /// </summary>
    /// <returns></returns>
    public int GetCurrentRound()
    {
        return mCurrentRoundInfo?.Round ?? 0;
    }

    /// <summary>
    /// 设置战斗报告列表
    /// </summary>
    /// <param name="reportList"></param>
    public void SetBattleReportList(BattleReportListResp reportList)
    {
        mBattleReportList = reportList;
    }

    /// <summary>
    /// 获取战斗报告列表
    /// </summary>
    /// <returns></returns>
    public BattleReportListResp GetBattleReportList()
    {
        return mBattleReportList;
    }

    /// <summary>
    /// 获取指定类型的战斗报告
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public List<BattleReport> GetBattleReportsByType(ReportType type)
    {
        return mBattleReportList?.Reports;
    }

    /// <summary>
    /// 清理战斗数据
    /// </summary>
    public void ClearBattleData()
    {
        mCurrentBattleInfo = null;
        mCurrentRoundInfo = null;
        mBattleResult = null;
        mBattleTargetInfo = null;
        mBattleReportList = null;
    }
}