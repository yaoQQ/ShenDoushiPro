//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-08-31 18:43:47.163
//------------------------------------------------------------

using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using common;
using Recharge;

/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: RechargeTipsView
///	定义窗口标识UIViewEnum.RechargeTipsView为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
[FGUIViewAttribute(UIViewEnum.RechargeTipsView, typeof(RechargeTipsView))]
public class RechargeTipsView : BaseView
{
	//G_RechargeTipsView 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
	public override string PackageName => G_RechargeTipsView.PACKAGE_NAME;
	public override string ComponentName => G_RechargeTipsView.COMPONENT_NAME;
	G_RechargeTipsView view;

	private TableView<ItemRender> rewardSingleList; //单基础奖励
	private TableView<ItemRender> baseReward; //基础奖励
	private TableView<ItemRender> doubleReward; //双倍奖励
	private Controller controler;//控制器
	private int payId = 0; //充值id

	public RechargeTipsView()
	{
		//（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
		setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.RechargeTipsView, false);
		Logger.PrintColor("yellow", "RechargeTipsView()");
	}
	/// <summary>
	/// 界面加载完成,触发函数
	/// </summary>
	/// <param name="gameObject">当前界面的fairyGUI对象</param>
	protected override void OnFinishLoad(GComponent gameObject)
	{
		Logger.PrintColor("yellow", $"RechargeTipsView onLoadUIEnd complte!! gameObject={gameObject}");
		this.contentPane = gameObject.asCom;
		// contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
		// contentPane.AddRelation(GRoot.inst, RelationType.Size);
		// Center();
		view = contentPane as G_RechargeTipsView;
		modal = true;
	}

	protected override void OnCreate()
	{
		rewardSingleList = new TableView<ItemRender>(view.rewardSingleList);
		baseReward = new TableView<ItemRender>(view.baseReward);
		doubleReward = new TableView<ItemRender>(view.doubleReward);
		controler = view.control;
	}

	protected override void OnEventListener()
	{
		On(EEventType.EventRecharegeInfoUpdate, UpdateView);
	}


	//统一按钮回调
	protected override void OnButtonClick(GButton clickedButton)
	{
		if (view.cancelBtn == clickedButton)
		{
			Hide();
		}
		else if (view.moneyBtn == clickedButton)
		{
			//直冲购买
			RechargeControl.Instance.ReqPayOrderReq(payId);
			Hide();
		}
		else if (view.voucherBtn == clickedButton)
		{
			//代金券购买
			RechargeControl.Instance.ReqVoucherPayReq(payId);
			Hide();
		}
	}

	//打开界面,fairyGUI打开动画播放完触发
	protected override void OnShow(object msg)
	{
		UpdateView();
	}

	/// <summary>
	/// 更新视图
	/// </summary>
	private void UpdateView()
	{
		var data = showArgs as PayVo;
		if (data != null)
		{
			//设置充值id
			payId = data.Id;
			//显示礼包名称
			view.giftName.text = data.Name;
			//是否双倍奖励
			var isDouble = data.FirstRewards.Count > 0 && RechargeControl.Instance.Model.IsFristPay(data.Id);
			controler.selectedIndex = isDouble ? 1 : 0;

			//显示基础奖励
			ShowBaseReward(data.Rewards);
			if (isDouble)
			{
				//显示双倍奖励
				ShowDoubleReward(data.FirstRewards);
			}

			//直冲按钮
			view.moneyBtn.GetTextField().SetVar("title", "￥" + data.Money).FlushVars();
			//代金券按钮
			var have = BagControl.Instance.Model.GetItemCountByItemId(ItemConst.VoucherID);
			var canUseVoucher = data.DisableVoucher == 0 && have >= data.Money;
			if (canUseVoucher)
			{
				view.voucherBtn.visible = true;
				view.voucherBtn.GetTextField().SetVar("title", data.Money + "代金劵").FlushVars();
			}
			else
			{
				view.voucherBtn.visible = false;
			}

			//显示限购
			if (data.Limit > 0)
			{
				var buyMit = RechargeControl.Instance.Model.GetBuyCount(data.Id);
				view.singleTitle2.text = $"（可购买{data.Limit - buyMit}次）";
			}
			else
			{
				view.singleTitle2.text = "";
			}
		}
	}

	//显示基础奖励
	private void ShowBaseReward(List<List<int>> rewards)
	{
		if (controler.selectedIndex == 0)
		{
			//普通充值
			rewardSingleList.setDatas(rewards);
		}
		else if (controler.selectedIndex == 1)
		{
			//双倍充值
			baseReward.setDatas(rewards);
		}
	}

	//显示双倍奖励
	private void ShowDoubleReward(List<List<int>> rewards)
	{
		doubleReward.setDatas(rewards);
	}

}