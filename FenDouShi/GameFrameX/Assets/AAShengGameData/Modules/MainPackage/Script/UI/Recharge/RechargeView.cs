//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-08-31 17:12:40.083
//------------------------------------------------------------

using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using Recharge;
using System.Linq;

/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: RechargeView
///	定义窗口标识UIViewEnum.RechargeView为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
[FGUIViewAttribute(UIViewEnum.RechargeView, typeof(RechargeView))]
public class RechargeView : BaseView
{
	//G_RechargeView 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
	public override string PackageName => G_RechargeView.PACKAGE_NAME;
	public override string ComponentName => G_RechargeView.COMPONENT_NAME;
	//全屏界面
	protected override bool IsFullScreen => true;
	G_RechargeView view;

	//左侧页签
	private TableView<RechargeLeftTab> leftTabView;
	private TableView<RechargeListItemRender> mRechargeTableView;

	//注册界面事件
	protected override Dictionary<EEventType, OnEventLister> EventList => new()
	{


	};

	protected override TopRenderData mTopRenderData => new TopRenderData
	{
		titleName = "充值",
		helpId = 10000,
		currencyId = 1,
	};

	public RechargeView()
	{
		//（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
		setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.RechargeView, false);
		Logger.PrintColor("yellow", "RechargeView()");
	}
	/// <summary>
	/// 界面加载完成,触发函数
	/// </summary>
	/// <param name="gameObject">当前界面的fairyGUI对象</param>
	protected override void OnFinishLoad(GComponent gameObject)
	{
		Logger.PrintColor("yellow", $"RechargeView onLoadUIEnd complte!! gameObject={gameObject}");
		this.contentPane = gameObject.asCom;
		contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
		contentPane.AddRelation(GRoot.inst, RelationType.Size);
		view = contentPane as G_RechargeView;
		//初始化左侧页签
		InitLeftTab();
		//充值列表
		mRechargeTableView = new TableView<RechargeListItemRender>(view.rechargeList);
		mRechargeTableView.setClickCallBack(OnRechargeItemClick);
	}

	//统一按钮回调
	protected override void OnButtonClick(GButton clickedButton)
	{

	}

	//打开界面,fairyGUI打开动画播放完触发
	protected override void OnShow(object args)
	{
		leftTabView.setSelectIndex(0);
		ChangePage(0);
	}

	/// <summary>
	/// 初始化左侧页签
	/// </summary>
	protected void InitLeftTab()
	{
		var leftdatas = new List<LeftTabData>
		{
			new LeftTabData() { tabName = "充值", icon = "ui_icon_charge", data = 1, redType = 0},
			new LeftTabData() { tabName = "商城", icon = "ui_icon_charge", data = 1, redType = 0}
		};
		leftTabView = new TableView<RechargeLeftTab>(view.leftTabList);
		leftTabView.setDatas(leftdatas);
		leftTabView.setClickCallBack(OnLeftTabClick);
		leftTabView.setSelectIndex(0);
	}

	/// <summary>
	/// 左侧页签点击回调
	/// </summary>
	/// <param name="data">数据</param>
	/// <param name="index">索引</param>
	protected void OnLeftTabClick(object data, int index)
	{
		ChangePage(index);
	}

	protected void ChangePage(int index)
	{
		if (index == 0)
		{
			//充值页签
			view.rechargeList.visible = true;
			LoadPayPanel();
		}
		else if (index == 1)
		{
			//商城页签
			view.rechargeList.visible = false;
		}
	}

	/// <summary>
	/// 加载充值面板
	/// </summary>
	private void LoadPayPanel()
	{
		var payConfigs = ConfigMgr.Instance.GetConfig<PayVo>();
		mRechargeTableView.setDatas(payConfigs.Values.ToList());
	}

	/// <summary>
	/// 充值列表点击回调
	/// </summary>
	/// <param name="data"></param>
	/// <param name="index"></param>
	private void OnRechargeItemClick(object data, int index)
	{
		UIViewManager.Instance.Show(UIViewEnum.RechargeTipsView, data);
	}
}