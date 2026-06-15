//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-09-02 20:20:55.942
//------------------------------------------------------------

using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using common;
using vip;

/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: VipBuyTipsView
///	定义窗口标识UIViewEnum.VipBuyTipsView为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
[FGUIViewAttribute(UIViewEnum.VipBuyTipsView, typeof(VipBuyTipsView))]
public class VipBuyTipsView : BaseView
{
	//G_VipBuyTipsView 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
	public override string PackageName => G_VipBuyTipsView.PACKAGE_NAME;
	public override string ComponentName => G_VipBuyTipsView.COMPONENT_NAME;
	G_VipBuyTipsView view;
	///奖励列表
	private TableView<ItemRender> mRewardList;

	private int vipLevel = 0;
	private int buyType = 0;

	//注册界面事件
	protected override Dictionary<EEventType, OnEventLister> EventList => new()
	{


	};


	public VipBuyTipsView()
	{
		//（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
		setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.VipBuyTipsView, false);
		Logger.PrintColor("yellow", "VipBuyTipsView()");
	}
	/// <summary>
	/// 界面加载完成,触发函数
	/// </summary>
	/// <param name="gameObject">当前界面的fairyGUI对象</param>
	protected override void OnFinishLoad(GComponent gameObject)
	{
		Logger.PrintColor("yellow", $"VipBuyTipsView onLoadUIEnd complte!! gameObject={gameObject}");
		this.contentPane = gameObject.asCom;
		contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
		contentPane.AddRelation(GRoot.inst, RelationType.Size);
		view = contentPane as G_VipBuyTipsView;
		modal = true;
		//奖励列表
		mRewardList = new TableView<ItemRender>(view.rewardLIst);
	}

	//统一按钮回调
	protected override void OnButtonClick(GButton clickedButton)
	{
		if (view.cancleBtn == clickedButton)
		{
			//取消按钮
			Hide();
		}
		else if (view.buyBtn == clickedButton)
		{
			//购买按钮
			if (vipLevel != 0)
			{
				if (buyType == 1)
				{
					//特权礼包
					VipControl.Instance.ReqVipGiftBuyReq(vipLevel);
				}
				else if (buyType == 2)
				{
					//豪华礼包
					VipControl.Instance.ReqLuxuryGiftBuyReq(vipLevel);
				}
				else if (buyType == 3) { 
					//专享礼包
				}

				Hide();
			}
		}
	}

	//打开界面,fairyGUI打开动画播放完触发
	protected override void OnShow(object args)
	{
		if (args is Dictionary<string, object> dict)
		{
			// 现在你可以通过字典来访问属性值
			vipLevel = (int)dict["vipLevel"];
			buyType = (int)dict["buyType"];
			string giftName = "特权礼包";
			if (buyType == 2)
			{
				giftName = "豪华礼包";
			}
			else if (buyType == 3)
			{
				giftName = "专享礼包";
			}
			// 进行其他操作...
			if (vipLevel != 0)
			{
				var vipCfg = ConfigMgr.Instance.GetConfigVoById<VipVo>(vipLevel);
				if (vipCfg != null)
				{
					var cost = vipCfg.GiftCost[0];
					view.tipsContentLabel.text = "是否花费" + cost[1] + "<img src='" + ItemTools.GetItemIcon(cost[0]) + "' width='60' height='60'/>购买" + giftName + "？";
					//奖励列表
					mRewardList.setDatas(vipCfg.GiftRewards);
				}
			}
		}


	}

}