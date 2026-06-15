//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-09-02 20:20:34.226
//------------------------------------------------------------

using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using common;
using vip;
using System;
using System.Linq;
using UnityEngine.PlayerLoop;

/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: VipView
///	定义窗口标识UIViewEnum.VipView为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
[FGUIViewAttribute(UIViewEnum.VipView, typeof(VipView))]
public class VipView : BaseView
{
	//G_VipView 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
	public override string PackageName => G_VipView.PACKAGE_NAME;
	public override string ComponentName => G_VipView.COMPONENT_NAME;
	G_VipView view;
	//控制器
	private Controller mRightTabController;
	//当前显示vip等级
	private int showVipLevel = 0;
	private int maxVipLevel = 0;
	//特权列表
	private TableView<VipDescRender> mPrivilegeList;
	//特权购买奖励列表
	private TableView<ItemRender> mVipBuyRewardList;
	//豪华礼包
	private TableView<ItemRender> mLuxuryGiftList;
	//专享礼包
	private TableView<ItemRender> mExclusiveGiftList;
	//每日礼包
	private TableView<MonthRewardRender> mDailyGiftList;


	public VipView()
	{
		//（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
		setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.VipView, false);
		Logger.PrintColor("yellow", "VipView()");
	}

	protected override void OnEventListener()
	{
		//特权礼包更新
		On(EEventType.EventVipGiftUpdate, OnEventVipGiftUpdate);
		//豪华礼包更新
		On(EEventType.EventVipLuxuryGiftUpdate, OnEventVipLuxuryGiftUpdate);
		//每日奖励领取返回
		On(EEventType.EventVipDailyRewardsUpdate, OnEventVipDailyRewardsUpdate);
	}

	/// <summary>
	/// 界面加载完成,触发函数
	/// </summary>
	/// <param name="gameObject">当前界面的fairyGUI对象</param>
	protected override void OnFinishLoad(GComponent gameObject)
	{
		Logger.PrintColor("yellow", $"VipView onLoadUIEnd complte!! gameObject={gameObject}");
		this.contentPane = gameObject.asCom;
		contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
		contentPane.AddRelation(GRoot.inst, RelationType.Size);
		view = contentPane as G_VipView;
		modal = true;
		//当前页面控制器
		mRightTabController = view.GetController("rightTab");
		//特权列表
		mPrivilegeList = new TableView<VipDescRender>(view.vipDescList);
		//奖励列表
		mVipBuyRewardList = new TableView<ItemRender>(view.vipBuyRewardList);
		//豪华礼包
		mLuxuryGiftList = new TableView<ItemRender>(view.luxuryList);
		//专享礼包
		mExclusiveGiftList = new TableView<ItemRender>(view.exclusiveList);
		//每日礼包	
		mDailyGiftList = new TableView<MonthRewardRender>(view.monthRewardList);
	}

	//统一按钮回调
	protected override void OnButtonClick(GButton clickedButton)
	{
		if (view.closeBtn == clickedButton)
		{
			//关闭按钮点击
			Hide();
		}
		else if (view.rightArow == clickedButton)
		{
			//右侧箭头点击
			var nextVipCfg = ConfigMgr.Instance.GetConfigVoById<VipVo>(showVipLevel + 1);
			if (nextVipCfg != null)
			{
				showVipLevel++;
				LoadVipContent();
			}
		}
		else if (view.leftArow == clickedButton)
		{
			//左侧箭头点击
			var nextVipCfg = ConfigMgr.Instance.GetConfigVoById<VipVo>(showVipLevel - 1);
			if (nextVipCfg != null)
			{
				showVipLevel--;
				LoadVipContent();
			}
		}
		else if (view.buyBtn == clickedButton)
		{
			//特权购买按钮点击
			UIViewManager.Instance.Show(UIViewEnum.VipBuyTipsView, new Dictionary<string, object> { { "vipLevel", showVipLevel }, { "buyType", 1 } });
		}
		else if (view.exclusiveBtn == clickedButton)
		{
			//专享礼包购买按钮点击
			UIViewManager.Instance.Show(UIViewEnum.VipBuyTipsView, new Dictionary<string, object> { { "vipLevel", showVipLevel }, { "buyType", 3 } });
		}
		else if (view.luxuryBtn == clickedButton)
		{
			//豪华礼包购买
			UIViewManager.Instance.Show(UIViewEnum.VipBuyTipsView, new Dictionary<string, object> { { "vipLevel", showVipLevel }, { "buyType", 2 } });
		}
		else if (view.getRewardBtn == clickedButton)
		{
			//领取月卡奖励
			if (view.getRewardBtn.grayed)
			{
				//不可以领取，未完成
				CommonViewUtils.ShowTopTips("贵族等级未达到领取要求");
			}
			else
			{
				VipControl.Instance.ReqDailyReceiveReq(showVipLevel);
			}

		}
	}

	//打开界面,fairyGUI打开动画播放完触发
	protected override void OnShow(object msg)
	{
		LoadVipInfo();
		mRightTabController.selectedIndex = 0;
		//加载vip内容
		LoadVipContent();
	}

	/// <summary>
	/// 加载vip信息
	/// </summary>
	private void LoadVipInfo()
	{
		var vipInfo = VipControl.Instance.Model.GetVipInfo();
		if (vipInfo != null)
		{
			//当前显示vip等级
			showVipLevel = vipInfo.Level <= 0 ? 1 : vipInfo.Level;
			//当前vip数据
			view.vipLevel.text = vipInfo.Level + "";
			var nextVipCfg = ConfigMgr.Instance.GetConfigVoById<VipVo>(vipInfo.Level + 1);
			var curExp = BagControl.Instance.Model.GetItemCountByItemId(ItemConst.VipExp);
			if (nextVipCfg != null)
			{
				view.vipExpLabel.text = curExp + "/" + nextVipCfg.Exp;
				view.vipExpProgress.value = (float)curExp / nextVipCfg.Exp * 100;
				var needRecharge = Math.Ceiling((double)nextVipCfg.Exp - curExp);
				view.vipExpDescLabel.text = "再充值" + needRecharge + "元即可成为贵族" + (vipInfo.Level + 1);
			}
			else
			{
				//已达最大等级
				var nowVipCfg = ConfigMgr.Instance.GetConfigVoById<VipVo>(vipInfo.Level);
				if (nowVipCfg != null)
				{
					view.vipExpLabel.text = curExp + "/" + nowVipCfg.Exp;
					view.vipExpProgress.value = 100;
					view.vipExpDescLabel.text = "贵族已到达当前最大等级";
				}
				else
				{
					//没找到配置表
					Logger.PrintError("vip config not found ===》 " + vipInfo.Level);
				}
			}
		}
	}

	/// <summary>
	/// 显示贵族内容
	/// </summary>
	private void LoadVipContent()
	{
		view.leftArow.visible = true;
		view.rightArow.visible = true;
		//判断最小等级
		if (showVipLevel == 1)
		{
			view.leftArow.visible = false;
		}

		//判断最大等级
		var nextVipCfg = ConfigMgr.Instance.GetConfigVoById<VipVo>(showVipLevel + 1);
		if (nextVipCfg == null)
		{
			//已显示最大
			view.rightArow.visible = false;
		}

		//贵族特权
		LoadPrivilege();
		//贵族礼包
		LoadGift();
	}

	/// <summary>
	/// 加载特权
	/// </summary>
	private void LoadPrivilege()
	{
		var showVipCfg = ConfigMgr.Instance.GetConfigVoById<VipVo>(showVipLevel);
		if (showVipCfg != null)
		{
			view.vipDescTitle.text = "贵族" + showVipLevel + "特权";
			//特权列表
			var privileges = showVipCfg.Privileges;
			mPrivilegeList.setDatas(privileges);
			mPrivilegeList.ScrollTop();
			//特权购买奖励列表
			var rewards = showVipCfg.GiftRewards;
			mVipBuyRewardList.setDatas(rewards);
			mVipBuyRewardList.ScrollTop();
			//购买礼包价格
			//原价
			var originalPrice = showVipCfg.OriginalPrice[0];
			view.discountIcon.url = ItemTools.GetItemIcon(originalPrice[0]);
			view.discountLabel.text = originalPrice[1] + "";
			//现价
			var currentPrice = showVipCfg.GiftCost[0];
			view.normalIcon.url = ItemTools.GetItemIcon(currentPrice[0]);
			view.normalLabel.text = currentPrice[1] + "";
			//更新购买按钮
			UpdateBuyBtn();
		}
		else
		{
			//没找到配置表
			Logger.PrintError("LoadPrivilege vip config not found ===》 " + showVipLevel);
		}
	}

	/// <summary>
	/// 更新购买按钮
	/// </summary>
	private void UpdateBuyBtn()
	{
		//是否可以购买
		var vipInfo = VipControl.Instance.Model.GetVipInfo();
		if (vipInfo != null)
		{
			view.readyBuy.visible = false;
			view.buyBtn.visible = true;
			if (vipInfo.Level < showVipLevel)
			{
				//未达到购买等级
				view.buyBtn.enabled = false;
				view.buyBtn.grayed = true;
			}
			else
			{
				//可以购买
				var buyList = VipControl.Instance.Model.GetVipGiftLevels();
				if (buyList != null && buyList.Contains(showVipLevel))
				{
					//已购买
					view.buyBtn.visible = false;
					view.readyBuy.visible = true;
				}
				else
				{
					//未购买	
					view.buyBtn.enabled = true;
					view.buyBtn.grayed = false;
				}
			}
		}
	}

	/// <summary>
	/// 加载礼包
	/// </summary>
	/// 
	private void LoadGift()
	{
		var vipInfo = VipControl.Instance.Model.GetVipInfo();
		var showVipCfg = ConfigMgr.Instance.GetConfigVoById<VipVo>(showVipLevel);
		if (showVipCfg != null && vipInfo != null)
		{
			//豪华礼包
			mLuxuryGiftList.setDatas(showVipCfg.LuxuryGift);
			mLuxuryGiftList.ScrollTop();
			//消耗
			var luxuryGiftCost = showVipCfg.LuxuryGiftCost[0];
			view.luxuryBtn.icon.url = ItemTools.GetItemIcon(luxuryGiftCost[0]);
			view.luxuryBtn.title.text = luxuryGiftCost[1] + "";
			//是否购买过豪华礼包
			UpdateLuxuryBtn();

			//专享礼包
			var payId = showVipCfg.ExclusiveGiftPayId;
			var payCfg = ConfigMgr.Instance.GetConfigVoById<PayVo>(payId);
			if (payCfg != null)
			{
				//支付id
				mExclusiveGiftList.setDatas(payCfg.Rewards);
				mExclusiveGiftList.ScrollTop();
			}

			//专享礼包是否购买
			UpdateExclusiveBtn();

			//月卡礼包
			// view.vipGetTips.text = "贵族"++"特惠礼包获得"
			mDailyGiftList.setDatas(showVipCfg.DailyRewards);
			mDailyGiftList.ResizeToFit(4);
			//更新月卡礼包
			UpdateMonthCardBtn();
		}
		else
		{
			//没找到配置表
			Logger.PrintError("LoadGift vip config not found ===》 " + showVipLevel);
		}
	}

	/// <summary>
	/// 更新豪华礼包按钮
	/// </summary>
	private void UpdateLuxuryBtn()
	{
		var vipInfo = VipControl.Instance.Model.GetVipInfo();
		if (vipInfo != null)
		{
			var luxuryInfo = VipControl.Instance.Model.GetLuxuryGiftLevels();
			if (luxuryInfo != null && luxuryInfo.Contains(showVipLevel))
			{
				//已购买
				view.luxuryReadyBuy.visible = true;
				view.n8.visible = true;
				view.luxuryBtn.enabled = false;
			}
			else
			{
				//未购买
				//判断是否可以购买
				if (vipInfo.Level >= showVipLevel)
				{
					//可以购买
					view.luxuryReadyBuy.visible = false;
					view.luxuryBtn.enabled = true;
				}
				else
				{
					//未完成
					view.luxuryReadyBuy.visible = true;
					view.n8.visible = false;
					view.luxuryBtn.enabled = false;
				}
			}
		}
	}

	/// <summary>
	/// 更新专享礼包按钮状态
	/// </summary>
	private void UpdateExclusiveBtn()
	{
		var vipInfo = VipControl.Instance.Model.GetVipInfo();
		if (vipInfo != null)
		{
			var exclusiveInfo = VipControl.Instance.Model.GetExclusiveGiftLevels();
			if (exclusiveInfo != null && exclusiveInfo.Contains(showVipLevel))
			{
				//已购买
				view.exclusiveReadyBuy.visible = true;
				view.n157.visible = true;
				view.exclusiveBtn.enabled = false;
			}
			else
			{
				//未购买
				//判断是否可以购买
				if (vipInfo.Level >= showVipLevel)
				{
					//可以购买
					view.exclusiveReadyBuy.visible = false;
					view.exclusiveBtn.enabled = true;
				}
				else
				{
					//未完成
					view.exclusiveReadyBuy.visible = true;
					view.n157.visible = false;
					view.exclusiveBtn.enabled = false;
				}
			}
		}
	}

	/// <summary>
	/// 加载月卡礼包
	/// </summary>
	private void UpdateMonthCardBtn()
	{
		var vipInfo = VipControl.Instance.Model.GetVipInfo();
		if (vipInfo != null)
		{
			var btnName = "领取";
			var haveMonthCard = true;//VipControl.Instance.Model.GetMonthCard(); //是否拥有月卡
			if (!haveMonthCard)
			{
				btnName = "前往";
			}
			view.getRewardBtn.GetTextField().SetVar("title", btnName);
			var monthCardInfo = VipControl.Instance.Model.GetDailyRewardsLevels();
			if (showVipLevel > vipInfo.Level)
			{
				//未达到领取等级
				view.getRewardBtn.visible = true;
				view.getRewardBtn.grayed = true;
				view.monthRewardReadyGet.visible = false;
			}
			else if (monthCardInfo != null && monthCardInfo.Contains(showVipLevel))
			{
				//可领取
				view.getRewardBtn.visible = true;
				view.getRewardBtn.grayed = false;
				view.monthRewardReadyGet.visible = false;
			}
			else
			{
				//已领取
				view.getRewardBtn.visible = false;
				view.monthRewardReadyGet.visible = true;
			}
		}
	}

	/// <summary>
	/// 特权列表购买更新
	/// </summary>
	private void OnEventVipGiftUpdate()
	{
		//更新购买按钮
		UpdateBuyBtn();
	}

	/// <summary>
	/// 豪华礼包更新
	/// </summary>
	private void OnEventVipLuxuryGiftUpdate()
	{
		UpdateLuxuryBtn();
	}

	/// <summary>
	/// 每日礼包领取
	/// </summary>
	private void OnEventVipDailyRewardsUpdate()
	{
		UpdateMonthCardBtn();
	}
}