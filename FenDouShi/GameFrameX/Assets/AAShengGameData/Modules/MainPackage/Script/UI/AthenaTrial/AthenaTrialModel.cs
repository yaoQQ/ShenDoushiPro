

using msg.trial;
using System;
using System.Runtime.CompilerServices;

public partial class AthenaTrialModel : BaseModel
{

    private TrialGetInfoResp trialGetInfoResp;

    // 初始化调用
    protected override void onInit()
    {

    }

    // 监听事件
    protected override void onEventListener()
    {
    }

    /// <summary>
    /// 雅典娜的试炼数据获取回调
    /// </summary>
    /// <param name="info"></param>
    public void SetTrialInfo(TrialGetInfoResp info)
    {
        trialGetInfoResp = info;
        UpDataRedPoint();
        Dispatch(EEventType.AthenaTrialInfoUpdate);
    }

    private void UpDataRedPoint()
    {
        var typeCfg = GetTypeList();
        foreach (var type in typeCfg)
        {
            var typeId = type.Id;
            var isShow = GetIsShowCopyTypeRedPoint(typeId);
            var redName = ERedPointType.AthenaTrial + typeId;
            RedPointManager.Instance.SetState(redName, isShow);
        }
    }

    /// <summary>
    /// 页签红点
    /// </summary>
    /// <param name="typeId"></param>
    /// <returns></returns>
    public bool GetIsShowCopyTypeRedPoint(int typeId)
    {
        var isOpen = GetIsCopyTypeOpen(typeId);
        if (!isOpen)
        {
            return false;
        }
        var isShow = GetIsShowCopyTypeRedPoint_freeChallenge(typeId);
        if (!isShow)
        {
            isShow = GetIsShowCopyTypeRedPoint_stage(typeId);
        }
        return isShow;
    }

    public bool GetIsShowCopyTypeRedPoint_stage(int typeId)
    {
        var isOpen = GetIsCopyTypeOpen(typeId);
        if (!isOpen)
        {
            return false;
        }
        var isShow = false;
        var stageCfgs = GetStageCfgByType(typeId);
        foreach (var stage in stageCfgs.Values)
        {
            isShow = GetIsCanDrawStageReward(stage.Id);
            if (isShow)
            {
                break;
            }
        }
        return isShow;
    }

    /// <summary>
    /// 免费扫荡
    /// </summary>
    /// <param name="typeId"></param>
    /// <returns></returns>
    public bool GetIsShowCopyTypeRedPoint_freeChallenge(int typeId)
    {
        var isOpen = GetIsCopyTypeOpen(typeId);
        if (!isOpen)
        {
            return false;
        }
        var freeCnt = GetFreeChallengeCnt(typeId);
        return freeCnt > 0;
    }



    /// <summary>
    /// 通关最大等级
    /// </summary>
    /// <param name="type"></param>
    public int GetPassMaxLevel(int type)
    {
        if (trialGetInfoResp?.typeLevels != null && trialGetInfoResp.typeLevels.TryGetValue(type, out var max))
        {

            return max;
        }
        return 0;
    }

    /// <summary>
    /// 关卡进度开始的展示level
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetShowLevel(int type)
    {
        var passLevel = GetPassMaxLevel(type);
        var stageNum = 10;
        if (passLevel < stageNum)
        {
            return 0;
        }
        var result = (passLevel / stageNum) * stageNum;
        return result;
    }

    /// <summary>
    /// 阶段奖励，阶段id->领取状态，0不可领取1可领取2已领取
    /// </summary>
    /// <param name="stageId"></param>
    /// <returns></returns>
    public int GetStageRewardState(int stageId)
    {
        if (trialGetInfoResp?.degreeRewards != null && trialGetInfoResp.degreeRewards.TryGetValue(stageId, out var state))
        {
            return state;
        }
        return 0;
    }

    private void SetStageRewardState(int stageId, int flag)
    {
        if (trialGetInfoResp?.degreeRewards != null && trialGetInfoResp.degreeRewards.TryGetValue(stageId, out var state))
        {
            trialGetInfoResp.degreeRewards[stageId] = flag;
        }
        UpDataRedPoint();
    }

    /// <summary>
    /// 已领取
    /// </summary>
    /// <param name="stageId"></param>
    /// <returns></returns>
    public bool GetIsDrawStageReward(int stageId)
    {
        return GetStageRewardState(stageId) == 2;
    }

    /// <summary>
    /// 试炼类型是否开启
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool GetIsCopyTypeOpen(int type)
    {
        var typeCfg = AthenaTrialControl.Instance.Model.GetTypeCfgByTypeId(type);
        var isOpen = true;
        if (typeCfg.OpenTime?.Count > 0)
        {
            var weekDay = TimeManager.GetChinaWeekDay();
            isOpen = typeCfg.OpenTime.Contains(weekDay);
        }
        return isOpen;
    }

    /// <summary>
    /// 开放时间
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string GetCopyTypeOpenStr(int type)
    {
        var typeCfg = AthenaTrialControl.Instance.Model.GetTypeCfgByTypeId(type);
        var descStr = "每周";
        for (var i = 0; i < typeCfg.OpenTime.Count; i++)
        {
            var weekDay = typeCfg.OpenTime[i];
            descStr = Utility.Text.Format("{0}{1}/", descStr, weekDay);
        }
        descStr = descStr.Substring(0, descStr.Length - 1);
        descStr = Utility.Text.Format("{0}开启", descStr);
        return descStr;
    }


    /// <summary>
    /// 可领取
    /// </summary>
    /// <param name="stageId"></param>
    /// <returns></returns>
    public bool GetIsCanDrawStageReward(int stageId)
    {
        return GetStageRewardState(stageId) == 1;
    }


    /// <summary>
    /// 剩余免费扫荡次数
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetFreeChallengeCnt(int type)
    {
        if (trialGetInfoResp?.freeSweepCnts != null && trialGetInfoResp.freeSweepCnts.TryGetValue(type, out var state))
        {
            return state;
        }
        return 0;
    }

    private void SetFreeChallengeCnt(int type, int cnt)
    {
        if (trialGetInfoResp?.freeSweepCnts != null && trialGetInfoResp.freeSweepCnts.TryGetValue(type, out var state))
        {
            trialGetInfoResp.freeSweepCnts[type] = Math.Max(cnt, 0);
        }
        UpDataRedPoint();
    }

    /// <summary>
    /// 剩余购买扫荡次数
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetRemainBuyChallengeCnt(int type)
    {
        if (trialGetInfoResp?.paySweepCnts != null && trialGetInfoResp.paySweepCnts.TryGetValue(type, out var state))
        {
            return state;
        }
        return 0;
    }

    public void SetRemainBuyChallengeCnt(int type, int cnt)
    {
        if (trialGetInfoResp?.paySweepCnts != null && trialGetInfoResp.paySweepCnts.TryGetValue(type, out var state))
        {
            trialGetInfoResp.paySweepCnts[type] = Math.Max(cnt, 0);
        }
    }

    /// <summary>
    /// 扫荡回调
    /// </summary>
    /// <param name="stageId"></param>
    public void OnTrialSweepResp(int stageId)
    {
        var typeId = GetTypeIdByStageId(stageId);
        var freeCnt = GetFreeChallengeCnt(typeId);
        if (freeCnt > 0)
        {
            SetFreeChallengeCnt(typeId, freeCnt - 1);
        }
        else
        {
            var buyCnt = GetRemainBuyChallengeCnt(typeId);
            if (buyCnt > 0)
            {
                SetRemainBuyChallengeCnt(typeId, buyCnt - 1);
            }
        }
        //UIViewManager.Instance.Hide(UIViewEnum.AthenaTrialSweepView);
        Dispatch(EEventType.AthenaTrialInfoUpdate);
    }

    public void OnTrialSweepBuyResp(int type)
    {
        var cnt = GetCanBuyChallengeCnt(type);
        SetCanBuyChallengeCnt(type, cnt - 1);
        var buyCnt = GetRemainBuyChallengeCnt(type);
        SetRemainBuyChallengeCnt(type, buyCnt + 1);
        Dispatch(EEventType.AthenaTrialInfoUpdate);
    }

    /// <summary>
    /// 可购买扫荡次数
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetCanBuyChallengeCnt(int type)
    {
        if (trialGetInfoResp?.canPaySweepCnts != null && trialGetInfoResp.canPaySweepCnts.TryGetValue(type, out var state))
        {
            return state;
        }
        return 0;
    }

    private void SetCanBuyChallengeCnt(int type, int num)
    {
        if (trialGetInfoResp?.canPaySweepCnts != null && trialGetInfoResp.canPaySweepCnts.TryGetValue(type, out var state))
        {
            trialGetInfoResp.canPaySweepCnts[type] = Math.Max(num, 0);
        }
    }

    public int GetSweepRemainAllCnt(int type)
    {
        var cnt1 = GetRemainBuyChallengeCnt(type);
        var cnt2 = GetFreeChallengeCnt(type);
        return cnt1 + cnt2;
    }

    public void OnTrialReceiveResp(TrialReceiveResp resp)
    {
        SetStageRewardState(resp.Id, 2);
        Dispatch(EEventType.AthenaTrialInfoUpdate);
    }
}