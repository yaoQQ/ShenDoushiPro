//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由系统自动生成并完善
// 生成时间：2025-08-27 17:03:18.848
//------------------------------------------------------------

using FairyGUI;
using FightDailyUI;
using msg.battle;
using msg.fightdaily;
using System.Collections.Generic;

/// <summary>
/// 每日副本界面
/// Script Name: FightDailyView
/// 此类用于标识UIViewEnum.FightDailyView为对应界面
/// </summary>
[FGUIViewAttribute(UIViewEnum.FightDailyView, typeof(FightDailyView))]
public class FightDailyView : BaseView
{
	//G_FightDailyView 为fairyGUI生成的类名
	public override string PackageName => G_FightDailyView.PACKAGE_NAME;
	public override string ComponentName => G_FightDailyView.COMPONENT_NAME;

    //副本列表
    private TableView<FightDailyItem> fightDailyItemList;
    //奖励列表
    private TableView<FightDailyLevelItem> fightDailyModeItemList;
    G_FightDailyView view;
	
	//注册响应事件
	protected override Dictionary<EEventType, OnEventLister> EventList => new()
	{
		{EEventType.EventFightDailyInfoUpdate, OnFightDailyInfoUpdate},
		{EEventType.EventFightDailySweepUpdate, OnFightDailySweepUpdate},
        {EEventType.EventBattleResult, OnFightResult},

    };
    protected override TopRenderData mTopRenderData => new TopRenderData
    {
        titleName = "每日副本",
        //icon = "ui://Common/Gm/icon_gm",
        helpId = 10000,
        currencyId = 1,
    };
    public FightDailyView()
	{
		//设置当前界面层级
		setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.FightDailyView, true);
		Logger.PrintColor("yellow", "FightDailyView()");
	}
	
	/// <summary>
    /// 界面加载完成,初始化界面
    /// </summary>
    /// <param name="gameObject">当前加载的fairyGUI界面</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
	    Logger.PrintColor("yellow", $"FightDailyView onLoadUIEnd complte!! gameObject={gameObject}");
		this.contentPane = gameObject.asCom;
		contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
		view = contentPane as G_FightDailyView;
		this.modal=true;
		// 初始化界面组件和事件
		InitComponents();


    }
	
	/// <summary>
	/// 初始化界面组件和事件
	/// </summary>
	private void InitComponents()
	{
		Logger.PrintGreen("@@@@@@@@@InitComponents()");
		fightDailyItemList = new TableView<FightDailyItem>(view.DungeonItemList);
		fightDailyItemList.setClickCallBack(OnDungeonItemClick);

        fightDailyModeItemList =new TableView<FightDailyLevelItem>(view.FightDailyModeListView.DungeonModeItemList);
        fightDailyModeItemList.setClickCallBack(OnModeItemClick);
      //  view.FightDailyModeListView.visible=false;

		view.FightDailyModeListView.ListBg.touchable = true; // 允许接收点击事件
		view.FightDailyModeListView.ListBg.onClick.Add(clickHideList);
    }
    /// <summary>
    /// 更新副本列表数据
    /// </summary>
    private void UpdateDungeonList()
    {
        List<FightDaily> dailyList = FightDailyControl.Instance.Model.GetFightDailyInfoList();
        Logger.PrintDebug("UpdateDungeonList() dailyList=" + dailyList);
        if (dailyList != null)
        {
			Logger.PrintDebug("UpdateDungeonList() fightDailyItemList=" + fightDailyItemList);
            fightDailyItemList.setDatas(dailyList);
            fightDailyItemList.setMaxNum(dailyList.Count);
        }
    }
    //统一按钮回调
    protected override void OnButtonClick(GButton clickedButton)
    {
		// 预留按钮点击统一处理
		if (view.closeBtn == clickedButton)
		{
           CloseView();
		}
        // 注册一键领取按钮事件
        else if (view.GetAllBtn== clickedButton)
		{
			OnGetAllBtnClick();

        }


    }

    /// <summary>
    /// 更新副本模式列表数据
    /// </summary>
    private void UpdateModeList(FightDailySweepResp fightDailySweepReq)
    {
		UpdateDungeonList();
		UpdateFightDailyModeItemList();
        // 根据扫荡响应数据创建模式数据
        foreach (var kvp in fightDailySweepReq.Times)
        {
            //var modeData = new FightDailyModeData
            //{
            //    Type = kvp.Key,
            //    FreeSweepTimes = kvp.Value.freeSweepTimes,
            //    BuySweepTimes = kvp.Value.buySweepTimes,
            //    PassedIndex = kvp.Value.passedIndex,
            //    IsUnlock = kvp.Value.isUnlock
            //};
            //modeDataList.Add(modeData);
        }

        // 更新模式列表
        //fightDailyModeItemList.setMaxNum(modeDataList.Count);
        //fightDailyModeItemList.setDatas(modeDataList);
    }
	
	/// <summary>
    /// 副本项点击处理
    /// </summary>
    private void OnDungeonItemClick(object data, int index)
	{
		Logger.PrintDebug($"OnDungeonItemClick() 点击了副本项: {index}");
		
		var dailyData = data as FightDaily;
        Logger.PrintDebug($"OnDungeonItemClick() 点击了副本项:dailyData= {dailyData}");
        if (dailyData == null)
		{
			Logger.PrintError("FightDailyInfo is null in click handler");
			return;
		}
		var fightTypeVo=FightDailyControl.Instance.Model.GetFightDailyTypeConfigByNet(dailyData);
        int isLockNum = FightDailyControl.Instance.Model.isFightItemLock(fightTypeVo);
        bool isLock = isLockNum > 0;
		if (isLock)
		{
			CommonViewUtils.ShowTopTips($"{isLockNum}级解锁");
			return;
		}

        clickHideList();
        FightDailyControl.Instance.Model.CurrentFightDaily = dailyData;
        UpdateFightDailyModeItemList();
    }
	private void UpdateFightDailyModeItemList()
	{
		if (view.FightDailyModeListView.visible)
		{
			FightDailyControl.Instance.Model.UpdateCurrentFightDaily();
			FightDaily fightDaily = FightDailyControl.Instance.Model.CurrentFightDaily;
			List<FightDailyVo> fightDailyConfigList = FightDailyControl.Instance.Model.GetConfigByType(fightDaily.Type);

			fightDailyModeItemList.setDatas(fightDailyConfigList);
			fightDailyModeItemList.setMaxNum(fightDailyConfigList.Count);
		}
    }


    private bool isShow= false;
	private void clickHideList()
	{
        Logger.PrintDebug("点击舞台空白处，关闭副本模式列表");
        if (isShow)//隐藏
        {
          //  view.DungeonModeItemList.position = new UnityEngine.Vector3(this.width + 100, view.DungeonModeItemList.position.y, view.DungeonModeItemList.position.z);

            view.FightDailyModeListView.DungeonModeItemList.TweenMoveX(this.width + view.FightDailyModeListView.DungeonModeItemList.width, 0.3f).OnComplete(() => {
                view.FightDailyModeListView.visible = false;
            });
            Logger.PrintDebug("点击舞台空白处，关闭副本模式列表");
        }
        else//开启
		{
           
            view.FightDailyModeListView.DungeonModeItemList.position = new UnityEngine.Vector3(this.width + view.FightDailyModeListView.DungeonModeItemList.width, view.FightDailyModeListView.DungeonModeItemList.position.y, view.FightDailyModeListView.DungeonModeItemList.position.z);
            view.FightDailyModeListView.visible = true;
            view.FightDailyModeListView.DungeonModeItemList.TweenMoveX(this.width - view.FightDailyModeListView.DungeonModeItemList.width-100, 0.3f).OnComplete(() => {

            });
            Logger.PrintDebug("点击舞台空白处，关闭副本模式列表");
        }
        isShow=!isShow;
    }

    /// <summary>
    /// 模式项点击处理
    /// </summary>
    private void OnModeItemClick(object data, int index)
    {
        Logger.PrintDebug($"OnModeItemClick() 点击了关卡项: {index}");
    }

	
	/// <summary>
	/// 一键领取按钮点击处理
	/// </summary>
	private void OnGetAllBtnClick()
	{
		Logger.PrintDebug("点击了一键领取按钮");
               // 获取所有副本数据
        List<FightDaily> allDailyData = FightDailyControl.Instance.Model.GetFightDailyInfoList();
		if (allDailyData == null || allDailyData.Count == 0)
		{
			CommonViewUtils.ShowTopTips("没有可一键扫荡的副本");
			return;
		}

		// 判断是否有已通关的副本
		bool hasAnyPassed = false;
		Dictionary<int, int> freeSweepDict = new Dictionary<int, int>();
		Dictionary<int, int> buySweepDict = new Dictionary<int, int>();
		
		foreach (var daily in allDailyData)
		{
			// 判断是否已通关（passedInedx > 0 表示已通关）
			if (daily.passedInedx > 0)
			{
				hasAnyPassed = true;
				
				// 收集免费扫荡次数
				if (daily.freeSweepTimes > 0)
				{
					freeSweepDict[daily.Type] = daily.freeSweepTimes;
				}
				
				// 收集付费扫荡次数
				if (daily.buySweepTimes > 0)
				{
					buySweepDict[daily.Type] = daily.buySweepTimes;
				}
			}
		}

		if (!hasAnyPassed)
		{
			CommonViewUtils.ShowTopTips("没有可一键扫荡的副本");
			return;
		}

		// 判断扫荡类型
		if (freeSweepDict.Count > 0)
		{
			// 有免费次数，直接扫荡
			FightDailyControl.Instance.ReqFightDailySweepReq(freeSweepDict, true);
			//Logger.PrintDebug($"一键免费扫荡: {string.Join(", ", freeSweepDict.Select(kv => $"Type:{kv.Key} 次数:{kv.Value}"))}");
		}
		else if (buySweepDict.Count > 0)
		{
			// 无免费次数但有付费次数，弹出确认弹窗
			ShowSweepConfirmDialog(buySweepDict);
		}
		else
		{
			// 无任何扫荡次数
			CommonViewUtils.ShowTopTips("今日扫荡次数已用完");
		}
	}

	/// <summary>
	/// 显示一键扫荡确认弹窗
	/// </summary>
	private void ShowSweepConfirmDialog(Dictionary<int, int> sweepDict)
	{

      
        // 获取副本类型配置
        var fightDailyTypeDic = ConfigMgr.Instance.GetConfig<FightDailyTypeVo>();
		
		// 计算总消耗
		int totalCost = 0;
		System.Text.StringBuilder detailBuilder = new System.Text.StringBuilder();
		
		foreach (var kv in sweepDict)
		{
			if (fightDailyTypeDic.TryGetValue(kv.Key, out var typeVo))
			{
				int costPerTime = 20; // 默认每次20元宝
				if (typeVo.BuySweepCost != null && typeVo.BuySweepCost.Count > 0)
				{
					costPerTime = typeVo.BuySweepCost[0][1]; // 使用第一档的消耗
				}
				
				totalCost += kv.Value * costPerTime;
				detailBuilder.AppendLine($"{typeVo.Name}: {kv.Value}次");
			}
		}

        string message = $"是否消耗{FGUITools.GetFairyCoinIconStr(coinType.coin)}{totalCost}元宝进行一键扫荡？\n\n扫荡详情:\n{detailBuilder}";
        MessageBoxVo msgVo = new MessageBoxVo();
        msgVo.title = "提示";
        msgVo.msg = message;
        msgVo.isCheckNoShowTodayKey = "fightDaily_cost_no_show_today";
        msgVo.OkBtnfunc = () =>
        {
            // 确认扫荡
            FightDailyControl.Instance.ReqFightDailySweepReq(sweepDict, false);
        };
		msgVo.CancelBtnfunc = () =>
		{
            Logger.PrintDebug("取消一键扫荡");
        };
        CommonViewUtils.ShowMessageBox(msgVo);
    
	}
	
	/// <summary>
	/// 关闭界面
	/// </summary>
	private void CloseView()
	{
		UIViewManager.Instance.Hide(UIViewEnum.FightDailyView);
	}
	
	/// <summary>
	/// 每日副本信息更新处理
	/// </summary>
	private void OnFightDailyInfoUpdate(EventSysArgsBase argsBase)
	{
		Logger.PrintDebug("EventFightDailyInfoUpdate事件 OnFightDailyInfoUpdate() ");

		if (view != null && argsBase is EventSysArgs<List<FightDaily>> args)
		{
				UpdateDungeonList();
			UpdateFightDailyModeItemList();
		}
	}

    /// <summary>
    /// 每日副本扫荡更新处理
    /// </summary>
    private void OnFightResult(EventSysArgsBase argsBase)
    {
        Logger.PrintDebug("EventBattleResult事件 OnFightResult() ");
        if (view != null && argsBase is EventSysArgs<BattleResultResp> args)
        {
            BattleResultResp sweepResp = args.args1;
            if (sweepResp != null)
            {
				//攻击胜利
				if (sweepResp.Result == 1)
				{
					CommonViewUtils.ShowTopTips("挑战成功!");
					UpdateDungeonList();
					UpdateFightDailyModeItemList();
                }
				else
				{
                    CommonViewUtils.ShowTopTips("挑战失败!");
                }
            }
        }
    }

    /// <summary>
    /// 每日副本扫荡更新处理
    /// </summary>
    private void OnFightDailySweepUpdate(EventSysArgsBase argsBase)
	{
		Logger.PrintDebug("EventFightDailySweepUpdate事件 更新");
		if (view!=null&&argsBase is EventSysArgs<FightDailySweepResp> args)
		{
            FightDailySweepResp sweepResp = args.args1;
			if (sweepResp != null)
			{
                // 更新界面上的扫荡次数信息
                UpdateModeList(sweepResp);
			}
		}
	}
	
	
	
	//显示界面,fairyGUI显示完成后回调
	protected override void OnShown()
	{
		base.OnShown();
		
		// 请求每日副本数据
		FightDailyControl.Instance.ReqFightDailyInfoReq();
	}
	
	//关闭界面,fairyGUI隐藏完成后回调
	protected override void OnHide()
	{
		base.OnHide();
	}
	
	//当销毁fairyGUI组件时调用
	protected override void OnDestroy() 
	{
		fightDailyItemList = null;
		fightDailyModeItemList = null;
        // 清理资源
    }
}