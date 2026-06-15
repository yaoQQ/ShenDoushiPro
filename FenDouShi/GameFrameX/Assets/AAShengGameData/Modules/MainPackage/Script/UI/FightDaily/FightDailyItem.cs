//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------

using FairyGUI;
using FightDailyUI;
using msg.fightdaily;
using System.Collections.Generic;

public class FightDailyItem : BaseRender
{
    public new G_FightDailyItem mRoot
    {
        
        get { return (G_FightDailyItem)base.mRoot; }
    }

    public override string mPackageName => G_FightDailyItem.PACKAGE_NAME;
    public override string mComponentName => G_FightDailyItem.COMPONENT_NAME;

    /// <summary>
    /// 奖励物品列表
    /// </summary>
    private TableView<ItemRender> mItemRender;

    /// <summary>
    /// //节点创建子
    /// </summary>
    protected override void OnCreate()
    {
        mItemRender = new TableView<ItemRender>(mRoot.itemList);
    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    protected override void InitEventLister()
    {
    }



    /// <summary>
    /// 刷新数据
    /// </summary>
    protected override void DataChanged()
    {
        var item = this.mRoot;
        FightDaily dailyData = this.mData as FightDaily;
        if (dailyData == null)
        {
            Logger.PrintError("FightDailyInfo is null");
            return;
        }
        Logger.PrintToJson(dailyData);
        var fightTypeDic=  ConfigMgr.Instance.GetConfig<FightDailyTypeVo>();
        fightTypeDic.TryGetValue(dailyData.Type, out FightDailyTypeVo fightTypeVo);
        if (fightTypeVo == null)
        {
            Logger.PrintError("FightDailyTypeVo is null"); 
            return;
        }
        // 设置副本信息
        //免费扫荡次数
        item.dailyText.text = $"{dailyData.freeSweepTimes}/{fightTypeVo.FreeSweep}";//免费扫荡次数
        int currLevel = dailyData.passedInedx + 1;
        item.levelDetailText.text = $"第{currLevel}关";
        // 设置背景
        item.bgLoad.url = UIHelper.GetFguiUrl(mPackageName, fightTypeVo.TabIcon);
        item.nameBgLoad.url = UIHelper.GetFguiUrl(mPackageName, fightTypeVo.NameRes);
        item.dailyTitle.text = "当前免费扫荡次数：";
        SetGood(dailyData);
        SetLock(item, fightTypeVo);
    }
    //设置奖品，为对应关卡配置表
    private void SetGood(FightDaily dailyData)
    {
        FightDailyVo fightDailyVo = FightDailyControl.Instance.Model.GetFightDailyByFightDaily(dailyData);
        if (fightDailyVo == null)
        {
            Logger.PrintError("获取当前关卡失败 fightDailyVo is null id="+ (dailyData.passedInedx+1));
            return;
        }
        var mLent = fightDailyVo.FirstRewards.Count;
        var rewards = new List<CommonItemData>();
        for (int i = 0; i < mLent; i++)
        {
            var goodItem = fightDailyVo.FirstRewards[i];
            rewards.Add(new CommonItemData(goodItem[0], goodItem[1]));
        }
        string rewardsJson = Newtonsoft.Json.JsonConvert.SerializeObject(dailyData);
        Logger.PrintGreen("每日副本数据发送 rewardsJson=" + rewardsJson);
        mItemRender.setDatas(rewards);
    }

    //副本类型是否锁定,功能开启相关
    private void SetLock(G_FightDailyItem item,FightDailyTypeVo fightTypeVo)
    {
        int isLockNum = FightDailyControl.Instance.Model.isFightItemLock(fightTypeVo);
        bool isLock = isLockNum > 0;
        item.isLock.visible = isLock;
        if (isLock)
        {
            item.unLockText.text = $"{isLockNum}级解锁";
        }
       
       
    }

    /// <summary>
    /// 根据副本类型和进度生成奖励
    /// </summary>
    private List<CommonItemData> GenerateRewardsByType(FightDaily dailyData)
    {
        var rewards = new List<CommonItemData>();
        
        // 从配置表中获取对应副本的奖励配置
        FightDailyVo configVo;
        //if (!ConfigMgr.Instance.GetConfig<FightDailyVo>(dailyData.Id, out configVo))
        //{
        //    Logger.PrintError($"FightDailyVo config not found for id: {dailyData.Id}");
        //    return rewards;
        //}

        //// 根据是否首通决定使用首通奖励还是扫荡奖励
        //var rewardList = dailyData.isFirstPass ? configVo.FirstRewards : configVo.SweepRewards;
        
        //if (rewardList != null)
        //{
        //    foreach (var reward in rewardList)
        //    {
        //        if (reward.Count >= 2)
        //        {
        //            rewards.Add(new CommonItemData(reward[0], reward[1]));
        //        }
        //    }
        //}
        
        return rewards;
    }

}