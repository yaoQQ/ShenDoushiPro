//------------------------------------------------------------
//------------------------------------------------------------
// ?????????????????
// ???????2025-08-25 17:23:38.980
//------------------------------------------------------------

using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using common;
using Friend;
using System.Threading.Tasks;

enum ELeftTab
{
	Friend,
	World,
	Add,
	Apply,
	BlackList,
}

/// <summary>
/// ????? FairyGUIView ???
/// Script Name: FriendView
///	???崰????UIViewEnum.FriendView????????????????????????UIViewEnum???
/// </summary>
[FGUIViewAttribute(UIViewEnum.FriendView, typeof(FriendView))]
public class FriendView : BaseView
{
	//G_FriendView ?fairyGUI????????????????????????????????????????????????????
	public override string PackageName => G_FriendView.PACKAGE_NAME;
	public override string ComponentName => G_FriendView.COMPONENT_NAME;
	G_FriendView view;
	//背景框
	private FrameBgComponent bgFrame;
	private Dictionary<ELeftTab, BaseRender> mPanels = new Dictionary<ELeftTab, BaseRender>();

	//?????????
	protected override Dictionary<EEventType, OnEventLister> EventList => new()
	{


	};

	private List<LeftTabData> mLeftData = new List<LeftTabData>()
	{
		new LeftTabData() { tabName = "本服好友", icon = UIHelper.GetFguiUrl("Friend","haoyou_icon_benfuhaoyou"), data = ELeftTab.Friend, select = true },
		new LeftTabData() { tabName = "跨服好友", icon = UIHelper.GetFguiUrl("Friend","haoyou_icon_kuafuhaoyou"), data = ELeftTab.World, select = false },
		new LeftTabData() { tabName = "添加好友", icon = UIHelper.GetFguiUrl("Friend","haoyou_icon_tianjiahaoyou"), data = ELeftTab.Add, select = false },
		new LeftTabData() { tabName = "好友申请", icon = UIHelper.GetFguiUrl("Friend","haoyou_icon_haoyoushenqing"), data = ELeftTab.Apply, select = false },
		new LeftTabData() { tabName = "黑名单", icon = UIHelper.GetFguiUrl("Friend","haoyou_icon_heimingdan"), data = ELeftTab.BlackList, select = false },
	};


	public FriendView()
	{
		//??????????????? ,????????Enum???,???????????????
		setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.FriendView, false);
		Logger.PrintColor("yellow", "FriendView()");
	}
	/// <summary>
	/// ??????????,????????
	/// </summary>
	/// <param name="gameObject">????????fairyGUI????</param>
	protected override void OnFinishLoad(GComponent gameObject)
	{
		Logger.PrintColor("yellow", $"FriendView onLoadUIEnd complte!! gameObject={gameObject}");
		this.contentPane = gameObject.asCom;
		contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
		contentPane.AddRelation(GRoot.inst, RelationType.Size);
		view = contentPane as G_FriendView;
		modal = true;

		//背景框
		bgFrame = BaseRender.Create<FrameBgComponent>(view.frameBg);
		bgFrame.SetTitle("好 友");
		bgFrame.SetView(this);
		bgFrame.SetTabCallback(OnTabClick);
		bgFrame.SetLeftTabData(mLeftData); //设置页签数据
	}

	//????????
	protected override void OnButtonClick(GButton clickedButton)
	{

	}

	//??????,fairyGUI??????????????
	protected override void OnShown()
	{
		base.OnShown();
		FriendControl.Instance.Model.ClearList();
		bgFrame.SetSelectTab(0);
	}

	//??????,fairyGUI??????????????
	protected override void OnHide()
	{
		base.OnHide();
	}

	//????fairyGUI???????????????
	protected override void OnDestroy()
	{

	}

	/// <summary>
	/// 左侧页签点击回调
	/// </summary>
	/// <param name="index"></param>
	private void OnTabClick(LeftTabData data)
	{

		var type = (ELeftTab)data.data;
		_ = ShowPanel(type);
	}

	/// <summary>
	/// 显示面板
	/// </summary>
	/// <param name="type"></param>
	private async Task ShowPanel(ELeftTab type)
	{
		//隐藏其他面板
		foreach (KeyValuePair<ELeftTab, BaseRender> pair in mPanels)
		{
			pair.Value.Hide();
		}

		//显示当前面板
		if (!mPanels.TryGetValue(type, out BaseRender panel))
		{
			//没有面板创建面板
			if (type == ELeftTab.Friend)
			{
				panel = await BaseRender.Create<FriendListPanel>();
			}
			else if (type == ELeftTab.World)
			{
				panel = await BaseRender.Create<FriendListPanel>();
			}
			else if (type == ELeftTab.Apply)
			{
				panel = await BaseRender.Create<FriendApplyPanel>();
			}
			else if (type == ELeftTab.Add)
			{
				panel = await BaseRender.Create<FriendSerachPanel>();
			}
			else if (type == ELeftTab.BlackList)
			{
				panel = await BaseRender.Create<FriendBlackPanel>();
			}
			view.content.AddChild(panel.mRoot);
			mPanels.Add(type, panel);
		}
		panel.setData(type);
		panel.Show();
	}
}