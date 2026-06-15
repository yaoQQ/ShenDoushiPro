

using common;
using FightDailyUI;
using msg.fightdaily;
using System.Collections.Generic;

//扫荡信息
public class FightDailySweepInfo
{
    public bool isFree;
    public int goodId;
    public int cost;

}

public class FightDailyModel : BaseModel
{
    // 每日副本信息列表
    private List<FightDaily> mFightDailyInfoList;

    public FightDaily CurrentFightDaily
    {
        get; set;
    }
    public void UpdateCurrentFightDaily()
    {
        if (CurrentFightDaily != null)
        {
            for(int i = 0; i < mFightDailyInfoList.Count; i++)
            {
                if (mFightDailyInfoList[i].Type == CurrentFightDaily.Type)
                {
                    CurrentFightDaily = mFightDailyInfoList[i];
                }
            }
           
        }
    }
    // 初始化方法
    protected override void onInit()
    {
        mFightDailyInfoList = new List<FightDaily>();
    }

    // 监听事件
    protected override void onEventListener()
    {
    }
    /// <summary>
    /// /副本类型是否锁定,功能开启相关
    /// </summary>
    /// <param name="fightTypeVo">关卡配置</param>
    /// <returns>1表示锁定，0表示解锁，返回大于1的数字表示需要达到的等级</returns>
    public int isFightItemLock(FightDailyTypeVo fightTypeVo)
    {
        var funcDic = ConfigMgr.Instance.GetConfig<FuncVo>();
        funcDic.TryGetValue(fightTypeVo.FuncId, out var funcVo);
        if (funcVo == null)
        {
            Logger.PrintError($"副本查找 funcVo is FuncId={fightTypeVo.FuncId} 失败！");
            return 1;
        }
        var funcConditionDic = ConfigMgr.Instance.GetConfig<FuncConditionVo>();
        funcConditionDic.TryGetValue(funcVo.OpenCondition, out var openConditionVo);
        if (openConditionVo == null)
        {
            Logger.PrintError($"副本查找开启条件 openConditionVo is openConditionVo={funcVo.OpenCondition} 失败！");
            return 1;
        }
        int roleLevel = RoleControl.Instance.Model.getRoleInfo().Level;
        if (roleLevel >= openConditionVo.Level)
        {
           return 0;

        }
      return openConditionVo.Level;
    }
    /// <summary>
    /// 设置每日副本信息
    /// </summary>
    /// <param name="infoList">每日副本信息列表</param>
    public void SetFightDailyInfo(List<FightDaily> infoList)
    {
        mFightDailyInfoList = infoList;
    }

    /// <summary>
    /// 获取每日副本信息列表
    /// </summary>
    /// <returns></returns>
    public List<FightDaily> GetFightDailyInfoList()
    {
        return mFightDailyInfoList;
    }

    /// <summary>
    /// 根据类型获取每日副本信息
    /// </summary>
    /// <param name="type">副本类型</param>
    /// <returns></returns>
    public FightDaily GetFightDailyByType(int type)
    {
        if (mFightDailyInfoList == null)
            return null;

        return mFightDailyInfoList.Find(item => item.Type == type);
    }

    /// <summary>
    /// 更新每日副本扫荡次数
    /// </summary>
    /// <param name="type">副本类型</param>
    /// <param name="freeSweepTimes">免费扫荡次数</param>
    /// <param name="buySweepTimes">付费扫荡次数</param>
    public void UpdateSweepTimes(int type, int freeSweepTimes, int buySweepTimes)
    {
        FightDaily daily = GetFightDailyByType(type);
        if (daily != null)
        {
            daily.freeSweepTimes = freeSweepTimes;
            daily.buySweepTimes = buySweepTimes;
        }
    }

    //通过副本类型 ，获取配置信息
    public List<FightDailyVo> GetConfigByType(int deilyType)
    {
        var Dic = ConfigMgr.Instance.GetConfig<FightDailyVo>();
        List<FightDailyVo> getList = new List<FightDailyVo>();
        foreach (var pair in Dic)
        {
            if (pair.Value.Type == deilyType)
            {
                getList.Add(pair.Value);
            }
        }
        // 对列表按从小到大排序
        getList.Sort((x, y) => x.Index.CompareTo(y.Index));
        return getList;
    }
    //关卡按钮状态类型
    public FightDailyModeType GetFightDailyButtonType(FightDailyVo fightDailyVo)
    {
        FightDaily currFightDaily = FightDailyControl.Instance.Model.CurrentFightDaily;
        //通过关卡
        int passedInedx = currFightDaily.passedInedx;
        passedInedx = passedInedx + 1;

        Logger.PrintDebug("FightDailyModeType passedInedx=" + passedInedx);
        Logger.PrintDebug("FightDailyModeType fightDailyVo.Index=" + fightDailyVo.Index);
        if (passedInedx >= fightDailyVo.Index)
        {
            // 设置扫荡按钮状态
            if (fightDailyVo.Index == passedInedx - 1)
            {
                Logger.PrintDebug("FightDailyModeType 设置扫荡按钮状态 fightDailyVo.Index - 1=" + (fightDailyVo.Index - 1));
                return FightDailyModeType.Sweep;
            }
            else if (fightDailyVo.Index == passedInedx)
            {
                return FightDailyModeType.Fight;
            }
            else if (passedInedx > fightDailyVo.Index)
            {
                return FightDailyModeType.Pass;
            }
            else if (passedInedx < fightDailyVo.Index)
            {
                return FightDailyModeType.LockGoLevel;
            }
        }

        ////通过
        //List<FightDailyVo> allLevelList= GetConfigByType(currFightDaily.Type);
        //if (passedInedx>= allLevelList[allLevelList.Count - 1].Index)
        //{
        //    return FightDailyModeType.Pass;
        //}
        return FightDailyModeType.UnOpen;
    }
    //关卡是否锁定
    public bool isLevelLock(FightDailyVo fightDailyVo)
    {
        FightDaily currFightDaily = FightDailyControl.Instance.Model.CurrentFightDaily;
        //通过关卡
        int passedInedx = currFightDaily.passedInedx;
        passedInedx = passedInedx + 1;
        if (passedInedx >= fightDailyVo.Index)
        {
            return false;
        }
        return true;
    }

    //关卡是否可以扫荡
    public bool isLevelSweep(FightDailyVo fightDailyVo)
    {
        FightDaily currFightDaily = FightDailyControl.Instance.Model.CurrentFightDaily;
        //通过关卡
        int passedInedx = currFightDaily.passedInedx;
        passedInedx = passedInedx + 1;
        if (passedInedx >= fightDailyVo.Index)
        {
            // 设置扫荡按钮状态
            bool canSweep = passedInedx > fightDailyVo.Index;
            return canSweep;
        }
        return false;
    }

    //获取扫荡的信息
    public FightDailySweepInfo GetSweepInfo()
    {
        FightDaily currFightDaily = FightDailyControl.Instance.Model.CurrentFightDaily;

        FightDailySweepInfo sweepInfo = new FightDailySweepInfo();
        // 检查是否有免费扫荡次数
        bool isFree = currFightDaily.freeSweepTimes > 0;
        sweepInfo.isFree = isFree;
        if (!isFree)
        {

            sweepInfo.isFree = false;
            // if(currFightDaily.buySweepTimes >= dailyTypeVo.BuySweepCost.Count)
            if (currFightDaily.buySweepTimes <= 0)
                return sweepInfo;
            FightDailyTypeVo dailyTypeVo = GetFightDailyTypeConfigByNet(currFightDaily);
            int sweepIndex = dailyTypeVo.BuySweep - currFightDaily.buySweepTimes;//当前扫荡次数
            if (sweepIndex < 0 || sweepIndex > dailyTypeVo.BuySweepCost.Count - 1)
            {
                Logger.PrintError($"今日购买次数或配置错误 无法获取配置表付费扫荡单次消耗数据 sweepIndex={sweepIndex} dailyTypeVo.BuySweep ={dailyTypeVo.BuySweep} currFightDaily.buySweepTimes={currFightDaily.buySweepTimes}");
                return sweepInfo;
            }
            int goodId = dailyTypeVo.BuySweepCost[sweepIndex][0];
            int cost = dailyTypeVo.BuySweepCost[sweepIndex][1];
            sweepInfo.goodId = goodId;
            sweepInfo.cost = cost;
        }
        return sweepInfo;
    }

   // 当前使用的扫荡次数
    public int GetUseSweepCount(FightDailyTypeVo dailyTypeVo)
    {
        FightDaily currFightDaily = FightDailyControl.Instance.Model.CurrentFightDaily;
        int sweepIndex = dailyTypeVo.BuySweep - currFightDaily.buySweepTimes;//当前使用扫荡次数

        return sweepIndex;
    }


    //====================================获取配置表================
    public FightDailyVo GetFightDailyByFightDaily(FightDaily daily)
    {
        var dic = ConfigMgr.Instance.GetConfig<FightDailyVo>();
        int currLevel = daily.passedInedx + 1;
        foreach (var kvp in dic)
        {
            if (kvp.Value.Index == currLevel)
            {
                return kvp.Value;
            }
            if (kvp.Value.Index > currLevel)
            {
                return null;
            }
        }
        return null;
    }
    public FightDailyTypeVo GetFightDailyTypeConfigByNet(FightDaily dailyData)
    {
        ;
        var fightTypeDic = ConfigMgr.Instance.GetConfig<FightDailyTypeVo>();
        fightTypeDic.TryGetValue(dailyData.Type, out FightDailyTypeVo fightTypeVo);
        return fightTypeVo;
    }
    public FightDailyTypeVo GetFightDailyTypeConfigByVo(FightDailyVo fight)
    {
        var fightTypeDic = ConfigMgr.Instance.GetConfig<FightDailyTypeVo>();
        fightTypeDic.TryGetValue(fight.Type, out FightDailyTypeVo fightTypeVo);
        return fightTypeVo;
    }

}