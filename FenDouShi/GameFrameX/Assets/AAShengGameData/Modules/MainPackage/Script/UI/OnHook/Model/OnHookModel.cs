using msg.common;
using msg.onHook;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OnHookModel : BaseModel
{
    public int dungeonId;                       // 副本id
    public long startTime;                      // 挂机开始时间
    public DateTime StartDateTime => startTime.GetDateTime();
    public DateTime NowDateTime => TimeManager.GetlocalDateTime();
    public float progressBar => (float)NowDateTime.Subtract(StartDateTime).TotalMinutes / ConfigMgr.GetGameConst(OnHookConst.idle_time_max)[0];
    public int freeCount;                       // 已使用免费次数
    public int buyCount;                        // 已使用的付费次数
    public int compensateCount;                 // 可使用的补偿等级次数
    public List<ResourceInfo> resourceInfos;    // 累计收益

    // 挂机奖励
    public long onHookTime;                      // 挂机累计时长
    public int beforeLevel;                     // 领取前的等级
    public int beforeExp;                       // 领取前的经验
    public int afterLevel;                      // 领取后的等级
    public int afterExp;                        // 领取后的经验
    public List<ResourceInfo> rewardInfos;      // 领取收益

    // 上一次请求挂机信息的时间戳
    public uint reqInfoDateTime;

    // 初始化调用
    protected override void onInit()
    {
        dungeonId = 0;
        startTime = 0;
        freeCount = 0;
        buyCount = 0;
        compensateCount = 0;
        onHookTime = 0;
        beforeLevel = 0;
        beforeExp = 0;
        afterLevel = 0;
        afterExp = 0;
        rewardInfos = null;
    }

    public void OnHookGetInfoResp(OnHookGetInfoResp resp)
    {
        dungeonId = resp.dungeonId;
        startTime = resp.startTime;
        freeCount = resp.freeCount;
        buyCount = resp.buyCount;
        compensateCount = resp.compensateCount;
        resourceInfos = resp.resourceInfos;
    }

    public void OnHookSpeedUpResp(OnHookSpeedUpResp resp)
    {
        freeCount = resp.freeCount;
        buyCount = resp.buyCount;
        compensateCount = resp.compensateCount;

        onHookTime = resp.onHookTime;
        beforeLevel = resp.beforeLevel;
        beforeExp = resp.beforeExp;
        afterLevel = resp.afterLevel;
        afterExp = resp.afterExp;
        rewardInfos = resp.rewardInfos;
    }

    public void OnHookGainRewardResp(OnHookGainRewardResp resp)
    {
        dungeonId = resp.dungeonId;
        startTime = resp.startTime;
        resourceInfos = resp.resourceInfos;

        onHookTime = resp.onHookTime;
        beforeLevel = resp.beforeLevel;
        beforeExp = resp.beforeExp;
        afterLevel = resp.afterLevel;
        afterExp = resp.afterExp;
        rewardInfos = resp.rewardInfos;
    }

    // 挂机经验已满
    public bool TimeIsFull()
    {
        return progressBar >= 1;
    }

    public string GetTimeString()
    {
        TimeSpan timeStamp = NowDateTime.Subtract(StartDateTime);
        var maxMinutes = ConfigMgr.GetGameConst("idle_time_max")[0];
        if (timeStamp.TotalMinutes > maxMinutes)
        {
            timeStamp = new TimeSpan(maxMinutes / 60, maxMinutes % 60, 0);
            timeStamp.ToString();
        }

        string hour = $"{Mathf.FloorToInt((float)timeStamp.TotalSeconds / 3600)}".PadLeft(2, '0');
        string min = $"{Mathf.FloorToInt((float)(timeStamp.TotalSeconds % 3600) / 60)}".PadLeft(2, '0');
        string seconds = $"{Mathf.FloorToInt((float)timeStamp.TotalSeconds % 60)}".PadLeft(2, '0');
        return $"{hour}:{min}:{seconds}";
    }

    // 累计收益
    public List<CommonItemData> GetResources()
    {
        List<CommonItemData> itemDatas = new();
        if (OnHookControl.Instance.Model.resourceInfos != null)
        {
            foreach (msg.common.ResourceInfo i in OnHookControl.Instance.Model.resourceInfos)
            {
                int count = (int)(i.Num / 10000f);
                if (count > 0)
                {
                    itemDatas.Add(new CommonItemData(i.Id, (int)(i.Num / 10000f))
                    {
                        //IsUp = true, // TODO:                     
                    });
                }
            }
        }
        return itemDatas;
    }

    // 当前奖励
    public List<CommonItemData> GetReward()
    {
        List<CommonItemData> itemDatas = new();
        if (rewardInfos != null)
        {
            foreach (msg.common.ResourceInfo i in rewardInfos)
            {
                itemDatas.Add(new CommonItemData(i.Id, (int)i.Num)
                {
                    //IsUp = true, // TODO:                     
                });
            }
        }
        return itemDatas;
    }

    public bool CanReqReward()
    {
        return TimeHelper.GetlocalTimeStamp() - reqInfoDateTime >= 60;
    }

    public bool CanReceiveReward()
    {
        // Logger.PrintLog($"{TimeHelper.GetlocalTimeStamp()} - {startTime.LongTimeStampToUint()} = {TimeHelper.GetlocalTimeStamp() - startTime.LongTimeStampToUint()}");
        return TimeHelper.GetlocalTimeStamp() - startTime.LongTimeStampToUint() >= 60 || GetReward().Count > 0;
    }

    public bool CanShowOfflineReward()
    {
        return TimeHelper.GetlocalTimeStamp() - startTime.LongTimeStampToUint() >= ConfigMgr.GetGameConst("idle_show_offline_time")[0] * 60;
    }
}