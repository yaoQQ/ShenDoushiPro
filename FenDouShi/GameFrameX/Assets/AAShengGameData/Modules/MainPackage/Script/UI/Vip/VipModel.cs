

using System.Collections.Generic;
using common;
using msg.vip;

public class VipModel : BaseModel
{
    //当前vip信息
    private VipInfoResp vipInfo;
    private List<int> vipGiftLevels;
    private List<int> luxuryGiftLevels;
    private List<int> exclusiveGiftLevels;
    private List<int> dailyRewardsLevels;

    /// <summary>
    /// 设置vip信息
    /// </summary>
    /// <param name="info"></param>
    public void SetVipInfo(VipInfoResp info)
    {

        if (vipInfo != null && vipInfo.Level < info.Level)
        {
            //vip等级更新
            UIViewManager.Instance.Show(UIViewEnum.VipLevelUpView, info.Level);
        }
        vipInfo = info;
        vipGiftLevels = new List<int>(vipInfo.vipGiftLevels ?? new int[0]);
        luxuryGiftLevels = new List<int>(vipInfo.luxuryGiftLevels ?? new int[0]);
        exclusiveGiftLevels = new List<int>(vipInfo.exclusiveGiftLevels ?? new int[0]);
        dailyRewardsLevels = new List<int>(vipInfo.dailyRewardsLevels ?? new int[0]);
        Dispatch(EEventType.EventVipInfoUpdate);
    }

    /// <summary>
    /// 获取vip信息
    /// </summary>
    /// <returns></returns>
    public VipInfoResp GetVipInfo()
    {
        return vipInfo;
    }

    /// <summary>
    /// 增加已购买过的vip礼包
    /// </summary>
    /// <param name="level"></param>
    public void AddVipGift(int level)
    {
        if (vipGiftLevels != null)
        {
            vipGiftLevels.Add(level);
            Dispatch(EEventType.EventVipGiftUpdate);
        }
    }
    /// <summary>
    /// 获取vip礼包等级
    /// </summary>
    /// <returns></returns>
    public List<int> GetVipGiftLevels()
    {
        return vipGiftLevels;
    }

    /// <summary>
    /// 增加已购买过的vip奢侈礼包
    /// </summary>
    /// <param name="level"></param>
    public void AddLuxuryGift(int level)
    {
        if (luxuryGiftLevels != null)
        {
            luxuryGiftLevels.Add(level);
            Dispatch(EEventType.EventVipLuxuryGiftUpdate);
        }
    }

    /// <summary>
    /// 获取vip奢侈礼包等级
    /// </summary>
    /// <returns></returns>
    public List<int> GetLuxuryGiftLevels()
    {
        return luxuryGiftLevels;
    }

    /// <summary>
    /// 增加已购买过的vip独家礼包
    /// </summary>
    /// <param name="level"></param>
    public void AddExclusiveGift(int level)
    {
        if (exclusiveGiftLevels != null)
        {
            exclusiveGiftLevels.Add(level);
            Dispatch(EEventType.EventVipExclusiveGiftUpdate);
        }
    }

    /// <summary>
    /// 获取vip独家礼包等级
    /// </summary>
    /// <returns></returns>
    public List<int> GetExclusiveGiftLevels()
    {
        return exclusiveGiftLevels;
    }

    /// <summary>
    /// 增加已购买过的vip每日奖励
    /// </summary>
    /// <param name="level"></param>
    public void AddDailyReward(int level)
    {
        if (dailyRewardsLevels != null)
        {
            dailyRewardsLevels.Remove(level);
            Dispatch(EEventType.EventVipDailyRewardsUpdate);
        }
    }

    /// <summary>
    /// 获取vip每日奖励等级
    /// </summary>
    /// <returns></returns>
    public List<int> GetDailyRewardsLevels()
    {
        return dailyRewardsLevels;
    }



}