

using msg.arena;
using msg.rank;
using System.Collections.Generic;
/// <summary>
/// 竞技场入口数据
/// </summary>
public class ArenaEntranceData
{
    public ArenaBriefData leftHorArena;
    public ArenaBriefData rightHorArena;
    public ArenaBriefData leftVerticalArena;
    public ArenaBriefData rightVerticalArena;
}

public enum ArenaBlockTypeEnum
{
    leftHor,//左边横框
    righHort,//右边横框
    leftVer,//左边竖
    rightVer//右边竖
}
/// <summary>
/// 竞技场简要数据
/// </summary>
public class ArenaBriefData
{
    public ArenaBlockTypeEnum blockType;
    public RankVo rankVo1;//包含两个子类
    public RankVo rankVo2;
    //public int id;
    //public string name;
    //public string desc;
    public bool isOpen;
    //public int minLevel;
    public int openDay;
    //public long endTime;
    //public int[] rewards;
}
public class ArenaModel : BaseModel
{
    // 竞技场基本信息
    private ArenaInfoResp mArenaInfo;
    
    // 匹配列表
    private List<RankItem> mMatchList = new List<RankItem>();
    
    // 排行榜列表
    private List<RankItem> mTopList = new List<RankItem>();

    // 初始化调用
    protected override void onInit()
    {
        
    }

    // 监听事件
    protected override void onEventListener()
    {
    }
    //=================================================竞技入口======================

    // 转化配置数据为界面所需数据
    public List<ArenaEntranceData> GetConfigToEntranceData()
    {
        var rankDic = ConfigMgr.Instance.GetConfig<RankVo>();
        if (rankDic == null)
        {
            Logger.PrintError("RankVo is null");
            return null;
        }

        List<ArenaEntranceData> arenaEntranceDataList = new List<ArenaEntranceData>();
        ArenaEntranceData currentEntranceData = new ArenaEntranceData();
        ArenaBriefData arenaBriefData = new ArenaBriefData();
        int index = 0;
        int itemCount = 0;
        foreach (var item in rankDic)
        {
            bool isLeft = index % 2 == 0; // 索引为偶数表示左边

         
            bool isHorizontal = item.Value.Style == 1; // 样式为1表示方形块（横框）
            if (itemCount <= 0)
            {
                arenaBriefData.rankVo1 = item.Value;
            }
            else
            {
                arenaBriefData.rankVo2 = item.Value;
            }

            arenaBriefData.blockType = isHorizontal 
                ? (isLeft ? ArenaBlockTypeEnum.leftHor : ArenaBlockTypeEnum.righHort)
                : (isLeft ? ArenaBlockTypeEnum.leftVer : ArenaBlockTypeEnum.rightVer);

            // 这里假设后续需要从item.Value中填充其他数据，暂时注释，实际使用时可按需添加
            // arenaBriefData.id = item.Value.Id;
            // arenaBriefData.name = item.Value.Name;
            // ...

            // 根据索引直接赋值给对应的字段
            switch (index)
            {
                case 0:
                    currentEntranceData.leftHorArena = arenaBriefData;
                    break;
                case 1:
                    currentEntranceData.rightHorArena = arenaBriefData;
                    break;
                case 2:
                    currentEntranceData.leftVerticalArena = arenaBriefData;
                    break;
                case 3:
                    currentEntranceData.rightVerticalArena = arenaBriefData;
                    break;
            }

            index++;
            itemCount++;
            if (index >= 4) // 收集满4个数据后添加到列表
            {
                arenaEntranceDataList.Add(currentEntranceData);
                currentEntranceData = new ArenaEntranceData();
                index = 0;
            }
            //ArenaBriefData包含2个item子对象，如果itemCount>=2（数据填满），则创建新的ArenaBriefData对象
            if (itemCount >= 2)
            {
                arenaBriefData=new ArenaBriefData();
                itemCount = 0;
            }
        }

        // 添加未收集满4个但已有数据的入口
        if (index > 0)
        {
            arenaEntranceDataList.Add(currentEntranceData);
        }

        return arenaEntranceDataList;
    }

    /// <summary>
    /// /竞技是否锁定,功能开启相关
    /// </summary>
    /// <param name="fightTypeVo">关卡配置</param>
    /// <returns>1表示锁定，0表示解锁，返回大于1的数字表示需要达到的等级</returns>
    public int IsArenaEntranceItemLock(RankVo rankVo)
    {
        var funcDic = ConfigMgr.Instance.GetConfig<FuncVo>();
        funcDic.TryGetValue(rankVo.System, out var funcVo);
        if (funcVo == null)
        {
            Logger.PrintError($"竞技查找 funcVo is rankVo.System={rankVo.System} 失败！");
            return 1;
        }
        var funcConditionDic = ConfigMgr.Instance.GetConfig<FuncConditionVo>();
        funcConditionDic.TryGetValue(funcVo.ShowCondition, out var openConditionVo);
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
    public string GetArenaEntranceLockStr(RankVo rankVo)
    {
        var funcDic = ConfigMgr.Instance.GetConfig<FuncVo>();
        funcDic.TryGetValue(rankVo.System, out var funcVo);
        if (funcVo == null)
        {
            Logger.PrintError($"竞技查找 funcVo is rankVo.System={rankVo.System} 失败！");
            return "未解锁";
        }
        var funcConditionDic = ConfigMgr.Instance.GetConfig<FuncConditionVo>();
        funcConditionDic.TryGetValue(funcVo.ShowCondition, out var openConditionVo);
        if (openConditionVo == null)
        {
            Logger.PrintError($"副本查找开启条件 openConditionVo is openConditionVo={funcVo.OpenCondition} 失败！");
            return "未解锁";
        }
        int roleLevel = RoleControl.Instance.Model.getRoleInfo().Level;
        if (roleLevel >= openConditionVo.Level)
        {
            return "";

        }
        string str = $"等级{openConditionVo.Level}解锁";
        if (openConditionVo.VipLevel > 0)
        {
            str += $"，VIP{openConditionVo.VipLevel}解锁";
        }
        if (openConditionVo.DungeonId > 0)
        {
            str += $"，通关{openConditionVo.DungeonId}解锁";
        }
        if (openConditionVo.OpenDay > 0)
        {
            str += $"，开服天数{openConditionVo.OpenDay}解锁";
        }
        return str;
    }
    //=================================================竞技场======================
    /// <summary>
    /// 设置竞技场信息
    /// </summary>
    /// <param name="info">竞技场信息</param>
    public void SetArenaInfo(ArenaInfoResp info)
    {
        mArenaInfo = info;
        
        if (info != null && info.topLists != null)
        {
            mTopList = new List<RankItem>(info.topLists);
        }
    }

    /// <summary>
    /// 获取竞技场信息
    /// </summary>
    /// <returns>竞技场信息</returns>
    public ArenaInfoResp GetArenaInfo()
    {
        return mArenaInfo;
    }

    /// <summary>
    /// 设置匹配列表
    /// </summary>
    /// <param name="matchList">匹配列表</param>
    public void SetMatchList(List<RankItem> matchList)
    {
        if (matchList != null)
        {
            mMatchList = new List<RankItem>(matchList);
        }
        else
        {
            mMatchList.Clear();
        }
    }

    /// <summary>
    /// 获取匹配列表
    /// </summary>
    /// <returns>匹配列表</returns>
    public List<RankItem> GetMatchList()
    {
        return new List<RankItem>(mMatchList);
    }

    /// <summary>
    /// 获取排行榜列表
    /// </summary>
    /// <returns>排行榜列表</returns>
    public List<RankItem> GetTopList()
    {
        return new List<RankItem>(mTopList);
    }

    /// <summary>
    /// 获取剩余免费次数
    /// </summary>
    /// <returns>剩余免费次数</returns>
    public int GetFreeTimes()
    {
        return mArenaInfo?.freeTimes ?? 0;
    }

    /// <summary>
    /// 获取当前积分
    /// </summary>
    /// <returns>当前积分</returns>
    public long GetScore()
    {
        return mArenaInfo?.Score ?? 0;
    }

    /// <summary>
    /// 获取当前排名
    /// </summary>
    /// <returns>当前排名</returns>
    public int GetRank()
    {
        return mArenaInfo?.Rank ?? 0;
    }

    /// <summary>
    /// 是否可以跳过战斗
    /// </summary>
    /// <returns>是否可以跳过</returns>
    public bool CanSkip()
    {
        return mArenaInfo?.canSkip ?? false;
    }

    /// <summary>
    /// 获取赛季时间
    /// </summary>
    /// <returns>赛季时间</returns>
    public long GetSeasonTime()
    {
        return mArenaInfo?.seasonTime ?? 0;
    }

    /// <summary>
    /// 获取赛季战斗次数
    /// </summary>
    /// <returns>赛季战斗次数</returns>
    public int GetSeasonFightTimes()
    {
        return mArenaInfo?.seasonFightTimes ?? 0;
    }

    /// <summary>
    /// 获取可领取的次数奖励
    /// </summary>
    /// <returns>可领取的奖励ID数组</returns>
    public int[] GetTimesRewards()
    {
        return mArenaInfo?.timesRewards ?? new int[0];
    }

    /// <summary>
    /// 获取当前竞技场等级配置
    /// </summary>
    /// <returns>当前竞技场等级</returns>
    public int GetCurArenaLevel()
    {
        // 根据积分计算当前竞技场等级
        if (mArenaInfo == null) return 1;
        
        long score = mArenaInfo.Score;
        // 这里应该根据配置表计算，暂时简单实现
        if (score < 1000) return 1;
        else if (score < 2000) return 2;
        else if (score < 3000) return 3;
        else if (score < 4000) return 4;
        else return 5;
    }

    /// <summary>
    /// 获取已领取的积分奖励
    /// </summary>
    /// <returns>已领取的奖励ID数组</returns>
    public int[] GetScoreRewards()
    {
        return mArenaInfo?.timesRewards ?? new int[0];
    }

    /// <summary>
    /// 检查奖励是否已领取
    /// </summary>
    /// <param name="rewardId">奖励ID</param>
    /// <returns>是否已领取</returns>
    public bool IsRewardGet(int rewardId)
    {
        var rewards = GetScoreRewards();
        return System.Array.IndexOf(rewards, rewardId) >= 0;
    }

    /// <summary>
    /// 处理新赛季数据
    /// </summary>
    /// <param name="resp">新赛季响应</param>
    public void HandleNewSeason(ArenaNewSeasonResp resp)
    {
        if (resp != null && mArenaInfo != null)
        {
            // 更新赛季数据
            mArenaInfo.Score = resp.newScore;
            // 其他新赛季逻辑处理
        }
    }

    /// <summary>
    /// 处理次数奖励领取
    /// </summary>
    /// <param name="resp">次数奖励响应</param>
    public void HandleTimesReward(ArenaTimesRewardResp resp)
    {
        if (resp != null && mArenaInfo != null && mArenaInfo.timesRewards != null)
        {
            // 从可领取列表中移除已领取的奖励
            var rewardsList = new List<int>(mArenaInfo.timesRewards);
            rewardsList.Remove(resp.Id);
            mArenaInfo.timesRewards = rewardsList.ToArray();
        }
    }

    /// <summary>
    /// 清理竞技场数据
    /// </summary>
    public void ClearArenaData()
    {
        mArenaInfo = null;
        mMatchList.Clear();
        mTopList.Clear();
    }
}