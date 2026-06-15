//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------
using FairyGUI;
using FightDailyUI;
using msg.fightdaily;
using System.Collections.Generic;


public enum FightDailyModeType
{
    LockGoLevel,
    Fight,
    Sweep,
    Pass,
    UnOpen,
}
public class FightDailyLevelItem : BaseRender
{
    public new G_FightDailyModeItem mRoot
    {
        get { return (G_FightDailyModeItem)base.mRoot; }
    }

    public override string mPackageName => G_FightDailyModeItem.PACKAGE_NAME;
    public override string mComponentName => G_FightDailyModeItem.COMPONENT_NAME;

    /// <summary>
    /// 奖励物品列表（扫荡奖励预览）
    /// </summary>
    private TableView<ItemRender> mItemRender;
    public FightDailyModeType levelBtnType;

    /// <summary>
    /// 进度奖励列表（当前进度奖励）
    /// </summary>
  //  private TableView<ItemRender> mProgressRewardList;

    ItemRender itemRender;
    /// <summary>
    /// //节点创建子
    /// </summary>
    protected override void OnCreate()
    {
        mItemRender = new TableView<ItemRender>(mRoot.itemList);
        itemRender  =  BaseRender.Create<ItemRender>(mRoot.costItem);
      //  mProgressRewardList = new TableView<ItemRender>(mRoot.rewardList); // 注意：XML中是rewardList
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        //当前副本服务器数据
        FightDaily currFightDaily = FightDailyControl.Instance.Model.CurrentFightDaily;
        var fightDailyConfig = this.mData as FightDailyVo;
        if (mRoot.SweepBtn == clickedButton)
        {
            Logger.PrintDebug($"准备处理点击扫荡 {fightDailyConfig.Id} 的点击逻辑");

            FightDailySweepInfo fightDailySweepInfo = FightDailyControl.Instance.Model.GetSweepInfo();
            // 构建扫荡请求数据
            Dictionary<int, int> sweepTimes = new Dictionary<int, int>();
            sweepTimes[fightDailyConfig.Type] = 1; // 扫荡1次

            if (fightDailySweepInfo.isFree)//免费扫荡
            {
                // 发送扫荡请求
                FightDailyControl.Instance.ReqFightDailySweepReq(sweepTimes, fightDailySweepInfo.isFree);
            }
            else//花费扫荡
            {
                if (currFightDaily.buySweepTimes <= 0)
                {
                    CommonViewUtils.ShowTopTips("今日购买次数已用完");
                    return;
                }
                FightDailyTypeVo fightTypeVo = FightDailyControl.Instance.Model.GetFightDailyTypeConfigByVo(fightDailyConfig);
                if (fightTypeVo == null)
                {
                    Logger.PrintError("FightDailyTypeVo is null");
                    return;
                }
                int useWweepIndex = FightDailyControl.Instance.Model.GetUseSweepCount(fightTypeVo);//当前使用的扫荡次数


                List<int> costNum = fightTypeVo.BuySweepCost[useWweepIndex];
                int leftCount = currFightDaily.buySweepTimes;
                string message = $"是否花费{FGUITools.GetFairyCoinIconStr(coinType.coin)}{costNum[1]}进行一次扫荡?<br>今日剩余扫荡次数:{leftCount}";
                MessageBoxVo msgVo = new MessageBoxVo();
                msgVo.title = "提示";
                msgVo.msg = message;
                msgVo.isCheckNoShowTodayKey = "fightDaily_cost_no_show_today";
                msgVo.OkBtnfunc = () =>
                {
                    // 确认扫荡
                    // 发送扫荡请求
                    FightDailyControl.Instance.ReqFightDailySweepReq(sweepTimes, fightDailySweepInfo.isFree);
                };
                msgVo.CancelBtnfunc = () =>
                {
                    Logger.PrintDebug("取消扫荡");
                };
                CommonViewUtils.ShowMessageBox(msgVo);
            }
        }

        /// <summary>
        /// @@@@@@@请求战斗信息
        /// </summary>
        /// <param name="type">战斗类型</param>
        /// <param name="id">战斗ID</param>
        /// <param name="skip">是否跳过战斗</param>
        /// <param name="parameters">额外参数</param>
        // public void ReqBattleInfo(int type, int id, bool skip = false, long[] parameters = null)
        else if (mRoot.FightBtn == clickedButton)
        {

            BattleControl.Instance.ReqBattleInfo(fightDailyConfig.Id, (int)BattleFightType.DailyDungeon, true, null);
            //CommonViewUtils.ShowTopTips("战斗功能未开放！");
            Logger.PrintDebug($"准备处理点击战斗 {fightDailyConfig.Id} 的点击逻辑");
        }
        else if (mRoot.GoBtn == clickedButton)
        {
            CommonViewUtils.ShowTopTips("未配置！");
            Logger.PrintDebug($"准备处理点击跳转 {fightDailyConfig.Id} 的点击逻辑");
        }
        else if (mRoot.UnOpenBtn == clickedButton)
        {
            CommonViewUtils.ShowTopTips("请通关上一关卡！");
            Logger.PrintDebug($"准备处理点击跳转 {fightDailyConfig.Id} 的点击逻辑");
        }
        else if(mRoot.passGroup.visible)
        {
            Logger.PrintDebug($"准备处理点击通关 {fightDailyConfig.Id} 的点击逻辑");
        }
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
     //   Logger.PrintToJson(this.mData);
        var fightDailyVo = this.mData as FightDailyVo;
        if (fightDailyVo == null)
        {
            Logger.PrintError("FightDailyModeData is null");
            return;
        }

        // 设置控制器索引（对应不同难度的背景颜色）
        item.c1.selectedIndex = fightDailyVo.Difficulty - 1; // 类型从1开始，索引从0开始

        // 设置类型名称
        item.modeText.text = fightDailyVo.Name;
        FightDaily currFightDaily = FightDailyControl.Instance.Model.CurrentFightDaily;
        //通过关卡
       // Logger.PrintToJson(currFightDaily);
       // Logger.PrintToJson(fightDailyVo);
        FightDailyModeType buttonType= FightDailyControl.Instance.Model.GetFightDailyButtonType(fightDailyVo);
        Logger.PrintDebug("FightDailyModeType buttonType=" + buttonType);
        ShowBtnType(item, fightDailyVo, buttonType);
        SetGood(fightDailyVo);
    }

    //显示按钮类型
    private void ShowBtnType(G_FightDailyModeItem item, FightDailyVo fightDailyVo,FightDailyModeType buttonType)
    {
        item.passGroup.visible = false;
        item.TypeBtnGroup.visible = true;
        item.SweepBtn.visible = false;
        item.FightBtn.visible = false;
        item.GoBtn.visible = false;
        item.UnOpenBtn.visible = false;
        item.costItem.visible=false;
        item.costNum.text = "";
        switch (buttonType)
        {
            case FightDailyModeType.UnOpen:
                item.GetInfoText.text = "[color=#FF0000]请通关上一层[/color]";
                item.UnOpenBtn.visible = true;
                break;
            case FightDailyModeType.Fight:
                item.GetInfoText.text = "推荐战力:"+fightDailyVo.RecommendPower;
                item.FightBtn.visible = true;
                break;
            case FightDailyModeType.Sweep:
                item.GetInfoText.text = "";
                item.SweepBtn.visible = true;
                item.SweepBtn.enabled = true;
                FightDaily currFightDaily = FightDailyControl.Instance.Model.CurrentFightDaily;
                // 检查是否有免费扫荡次数

                FightDailySweepInfo fightDailySweepInfo = FightDailyControl.Instance.Model.GetSweepInfo();
                Logger.PrintToJson(fightDailySweepInfo);
                item.costGroup.visible = !fightDailySweepInfo.isFree;
                if (!fightDailySweepInfo.isFree)
                {
                    FightDailyTypeVo dailyTypeVo = FightDailyControl.Instance.Model.GetFightDailyTypeConfigByNet(currFightDaily);
                    item.costNum.text = "";
                    if (currFightDaily.buySweepTimes <= 0)
                    {
                        Logger.PrintDebug("今日购买次数已用完");
                        Logger.PrintDebug($"currFightDaily.buySweepTimes={currFightDaily.buySweepTimes} dailyTypeVo.BuySweepCost.Count={dailyTypeVo.BuySweepCost.Count} ");
                        item.SweepBtn.enabled = false;//灰色
                        item.costItem.visible = false;//不显示消费
                        return;
                    }
                    item.costItem.visible = true;
                    //ailyTypeVo.BuySweep付费扫荡次数
                    //currFightDaily.buySweepTimes 服务器剩余次数
                    int useWweepIndex = FightDailyControl.Instance.Model.GetUseSweepCount(dailyTypeVo);//当前使用的扫荡次数
                    if (useWweepIndex < 0 || useWweepIndex > dailyTypeVo.BuySweepCost.Count - 1)
                    {
                        Logger.PrintError($"今日购买次数或配置错误 无法获取配置表付费扫荡单次消耗数据 sweepIndex={useWweepIndex} dailyTypeVo.BuySweep ={dailyTypeVo.BuySweep} currFightDaily.buySweepTimes={currFightDaily.buySweepTimes}");
                        return;
                    }
                    //可以使用购买
                    if (fightDailySweepInfo.goodId > 0)
                    {
                        Logger.PrintGreen("扫荡  goodId=" + fightDailySweepInfo.goodId);
                        Logger.PrintGreen("扫荡  cost=" + fightDailySweepInfo.cost);
                         CommonItemData goodItem = new CommonItemData(fightDailySweepInfo.goodId, fightDailySweepInfo.cost);
                        itemRender.setData(goodItem);
                        item.costNum.text = fightDailySweepInfo.cost.ToString();
                    }
                }



                //bool isFree = currFightDaily.freeSweepTimes > 0;
                //if (!isFree)
                //{
                //    FightDailyTypeVo dailyTypeVo = FightDailyControl.Instance.Model.GetFightDailyTypeConfigByNet(currFightDaily);
                //    // if(currFightDaily.buySweepTimes >= dailyTypeVo.BuySweepCost.Count)
                //    if (currFightDaily.buySweepTimes <=0)
                //    {
                //        Logger.PrintDebug("今日购买次数已用完");
                //        Logger.PrintDebug($"currFightDaily.buySweepTimes={currFightDaily.buySweepTimes} dailyTypeVo.BuySweepCost.Count={dailyTypeVo.BuySweepCost.Count} ");
                //        item.SweepBtn.enabled = false;
                //        item.costItem.visible = false;
                //        item.costNum.text = "";
                //        return;
                //    }
                //    item.costItem.visible = true;
                //    //ailyTypeVo.BuySweep付费扫荡次数
                //    //currFightDaily.buySweepTimes 服务器剩余次数
                //    int sweepIndex = dailyTypeVo.BuySweep - currFightDaily.buySweepTimes;//当前扫荡次数
                //    if(sweepIndex<0|| sweepIndex> dailyTypeVo.BuySweepCost.Count - 1)
                //    {
                //        Logger.PrintError($"今日购买次数或配置错误 无法获取配置表付费扫荡单次消耗数据 sweepIndex={sweepIndex} dailyTypeVo.BuySweep ={dailyTypeVo.BuySweep} currFightDaily.buySweepTimes={currFightDaily.buySweepTimes}");
                //        return;
                //    }
                //    int goodId = dailyTypeVo.BuySweepCost[sweepIndex][0];
                //    int cost = dailyTypeVo.BuySweepCost[sweepIndex][1];
                //    item.costNum.text = cost.ToString();
                //    var mLent = fightDailyVo.FirstRewards.Count;
                //    Logger.PrintGreen("扫荡  goodId=" + goodId);
                //    Logger.PrintGreen("扫荡  cost=" + cost);
                //    CommonItemData goodItem = new CommonItemData(goodId, cost);
                //    itemRender.setData(goodItem);


                //    string rewardsJson = Newtonsoft.Json.JsonConvert.SerializeObject(goodItem);
                //    Logger.PrintGreen("每日副本数据发送 rewardsJson=" + rewardsJson);
                //}
  
                break;
            case FightDailyModeType.Pass:
                item.GetInfoText.text = "";
                item.TypeBtnGroup.visible = false;
                item.passGroup.visible = true;
                break;
            case FightDailyModeType.LockGoLevel:
                item.GetInfoText.text = "请通关上一层";
                item.GoBtn.visible = true;

                //FightDaily currFightDaily = FightDailyControl.Instance.Model.CurrentFightDaily;
                //int passedIndex = currFightDaily.passedInedx+1;
                // 根据类型设置解锁条件
                switch (fightDailyVo.Type)
                {
                    case 1:
                        item.GetInfoText.text = $"命运之殿999层开启(待开发)";
                        break;
                    case 2:
                        item.GetInfoText.text = "命运之殿999层开启(待开发)";
                        break;
                    case 3:
                        item.GetInfoText.text = "命运之殿999层开启(待开发)";
                        break;
                    case 4:
                        item.GetInfoText.text = "命运之殿999层开启(待开发)";
                        break;
                    default:
                        item.GetInfoText.text = "命运之殿999层开启(待开发)";
                        break;
                }
                break;
        }
    }

    private void SetGood(FightDailyVo fightDailyVo)
    {
        if (fightDailyVo == null)
        {
            Logger.PrintError("fightDailyVo is null");
            return;
        }
        var mLent = fightDailyVo.FirstRewards.Count;
        var rewards = new List<CommonItemData>();
        for (int i = 0; i < mLent; i++)
        {
            var goodItem = fightDailyVo.FirstRewards[i];
            rewards.Add(new CommonItemData(goodItem[0], goodItem[1]));
        }
        string rewardsJson = Newtonsoft.Json.JsonConvert.SerializeObject(rewards);
        Logger.PrintGreen("每日副本数据发送 rewardsJson=" + rewardsJson);
        mItemRender.setDatas(rewards);
    }
    ///// <summary>
    ///// 生成扫荡奖励预览
    ///// </summary>
    //private List<CommonItemData> GenerateSweepRewards(int type, int passedIndex)
    //{
    //    var rewards = new List<CommonItemData>();
        
    //    if (passedIndex <= 0)
    //        return rewards;

    //    // 获取该类型的所有副本配置
    //    var fightDailyList = new List<FightDailyVo>();
    //    var allConfigs = ConfigMgr.Instance.GetAllConfig<FightDailyVo>();
        
    //    foreach (var config in allConfigs)
    //    {
    //        if (config.Type == type && config.Index <= passedIndex)
    //        {
    //            fightDailyList.Add(config);
    //        }
    //    }

    //    // 按索引排序
    //    fightDailyList.Sort((a, b) => a.Index.CompareTo(b.Index));

    //    // 计算累计扫荡奖励（使用扫荡奖励）
    //    var rewardDict = new Dictionary<int, int>();
    //    foreach (var daily in fightDailyList)
    //    {
    //        if (daily.SweepRewards != null)
    //        {
    //            foreach (var reward in daily.SweepRewards)
    //            {
    //                if (reward.Count >= 2)
    //                {
    //                    int itemId = reward[0];
    //                    int count = reward[1];
    //                    if (rewardDict.ContainsKey(itemId))
    //                        rewardDict[itemId] += count;
    //                    else
    //                        rewardDict[itemId] = count;
    //                }
    //            }
    //        }
    //    }

    //    // 转换为CommonItemData列表
    //    foreach (var kvp in rewardDict)
    //    {
    //        rewards.Add(new CommonItemData(kvp.Key, kvp.Value));
    //    }
        
    //    return rewards;
    //}

    ///// <summary>
    ///// 获取进度奖励列表
    ///// </summary>
    //private List<CommonItemData> GetProgressRewards(FightDailyTypeVo typeVo, int passedIndex)
    //{
    //    var rewards = new List<CommonItemData>();
        
    //    if (typeVo.ProgressRewards == null || typeVo.ProgressRewards.Count == 0)
    //        return rewards;

    //    // 找到所有已达成但未领取的进度奖励
    //    foreach (var progressReward in typeVo.ProgressRewards)
    //    {
    //        if (passedIndex >= progressReward.Condition)
    //        {
    //            foreach (var reward in progressReward.Rewards)
    //            {
    //                if (reward.Count >= 2)
    //                {
    //                    rewards.Add(new CommonItemData(reward[0], reward[1]));
    //                }
    //            }
    //        }
    //    }
        
    //    return rewards;
    //}
}