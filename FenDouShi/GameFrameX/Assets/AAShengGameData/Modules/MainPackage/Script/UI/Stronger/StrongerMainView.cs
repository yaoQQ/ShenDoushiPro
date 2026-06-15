//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-08-19 20:47:22.798
//------------------------------------------------------------

using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using common;
using Stronger;

/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: StrongerMainView
///	定义窗口标识UIViewEnum.StrongerMainView为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
[FGUIViewAttribute(UIViewEnum.StrongerMainView,typeof(StrongerMainView))]
public class StrongerMainView : BaseView
{
	//G_StrongerMainView 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
	public override string PackageName => G_StrongerMainView.PACKAGE_NAME;
	public override string ComponentName =>G_StrongerMainView.COMPONENT_NAME;
	G_StrongerMainView view;

    protected override TopRenderData mTopRenderData => new TopRenderData
    {
        titleName = "我要变强",
        helpId = 10000,
        currencyId = 10001,
    };
    protected override LeftTabData[] mleftTabDatas => new LeftTabData[]
    {
        new LeftTabData() { tabName = "英雄养成", icon = UIHelper.GetFguiUrl("Stronger","bianqiang_icon_yangcheng"), select = true,data = 1 },
        new LeftTabData() { tabName = "获取资源", icon = UIHelper.GetFguiUrl("Stronger","bianqiang_icon_ziyuan"), select = false,data = 2 },
        new LeftTabData() { tabName = "推荐阵容", icon = UIHelper.GetFguiUrl("Stronger","bianqiang_icon_zhenrong"), select = false,data = 3 },
        new LeftTabData() { tabName = "常见问题", icon = UIHelper.GetFguiUrl("Stronger","bianqiang_icon_wenti"), select = false,data = 4 },
    };

    //注册界面事件
    protected override Dictionary<EEventType, OnEventLister> EventList => new()
	{


	};

	
	public StrongerMainView()
	{
		//（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
		setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.StrongerMainView, false);
		Logger.PrintColor("yellow", "StrongerMainView()");
	}
	/// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
	    Logger.PrintColor("yellow", $"StrongerMainView onLoadUIEnd complte!! gameObject={gameObject}");
		this.contentPane = gameObject.asCom;
		contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
		view = contentPane as G_StrongerMainView;
	}

    //统一按钮回调
    protected override void OnButtonClick(GButton clickedButton)
	{
	
	}
	
	//打开界面,fairyGUI打开动画播放完触发
	protected override void OnShown()
	{
		base.OnShown();
	}
	
	//关闭界面,fairyGUI关闭动画播放完触发
	protected override void OnHide()
	{
		base.OnHide();
	}
	
	//框架和fairyGUI底层销毁界面时触发
	protected override void OnDestroy() { 
	
	}
}