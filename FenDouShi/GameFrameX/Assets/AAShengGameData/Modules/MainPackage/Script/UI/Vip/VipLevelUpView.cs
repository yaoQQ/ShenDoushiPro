//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-09-02 20:20:48.988
//------------------------------------------------------------

using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using common;
using vip;

/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: VipLevelUpView
///	定义窗口标识UIViewEnum.VipLevelUpView为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
[FGUIViewAttribute(UIViewEnum.VipLevelUpView, typeof(VipLevelUpView))]
public class VipLevelUpView : BaseView
{
	//G_VipLevelUpView 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
	public override string PackageName => G_VipLevelUpView.PACKAGE_NAME;
	public override string ComponentName => G_VipLevelUpView.COMPONENT_NAME;
	G_VipLevelUpView view;

	private TableView<VipLevelUpDescListRender> privilegeList; //特权列表
	private TableView<ItemRender> rewardList;  //奖励列表

	protected override bool ClickMaskHide => true;

	//注册界面事件
	protected override Dictionary<EEventType, OnEventLister> EventList => new()
	{


	};


	public VipLevelUpView()
	{
		//（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
		setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.VipLevelUpView, false);
		Logger.PrintColor("yellow", "VipLevelUpView()");
	}

    protected override void OnEventListener()
    {
		
    }

	/// <summary>
	/// 界面加载完成,触发函数
	/// </summary>
	/// <param name="gameObject">当前界面的fairyGUI对象</param>
	protected override void OnFinishLoad(GComponent gameObject)
	{
		Logger.PrintColor("yellow", $"VipLevelUpView onLoadUIEnd complte!! gameObject={gameObject}");
		this.contentPane = gameObject.asCom;
		contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
		contentPane.AddRelation(GRoot.inst, RelationType.Size);
		view = contentPane as G_VipLevelUpView;
		modal = true;
		//特权列表
		privilegeList = new TableView<VipLevelUpDescListRender>(view.descList);
		//奖励列表
		rewardList = new TableView<ItemRender>(view.rewardList);
	}

	//统一按钮回调
	protected override void OnButtonClick(GButton clickedButton)
	{
		if (view.goBuyBtn == clickedButton)
		{
			//前往购买按钮
			Hide();
		}
	}

	//打开界面,fairyGUI打开动画播放完触发
	protected override void OnShow(object args)
	{
		int level = (int)args;
		if (level != 0)
		{	
			//等级
			view.vip1.text = "v" + (level - 1);
			view.vip2.text = "v" + level;
		
			var vipCfg = ConfigMgr.Instance.GetConfigVoById<VipVo>(level);
			if (vipCfg != null)
			{
				//特权列表
				privilegeList.setDatas(vipCfg.Privileges);
				//特权礼包奖励
				rewardList.setDatas(vipCfg.GiftRewards);
			}
		}
	}

	//关闭界面,fairyGUI关闭动画播放完触发
	protected override void OnHide()
	{
		base.OnHide();
	}

	//框架和fairyGUI底层销毁界面时触发
	protected override void OnDestroy()
	{

	}
}