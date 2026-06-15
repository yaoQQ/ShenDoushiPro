//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-08-15 16:55:46.831
//------------------------------------------------------------

using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using login;

/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: ReconnectView
///	定义窗口标识UIViewEnum.ReconnectView为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
[FGUIViewAttribute(UIViewEnum.ReconnectView,typeof(ReconnectView))]
public class ReconnectView : BaseView
{
	//G_ReconnectView 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
	public override string PackageName => G_ReconnectView.PACKAGE_NAME;
	public override string ComponentName =>G_ReconnectView.COMPONENT_NAME;
	G_ReconnectView view;
    private string topTipsTimer = "TopTipsTimer";
    //注册界面事件
    protected override Dictionary<EEventType, OnEventLister> EventList => new()
	{


	};

	
	public ReconnectView()
	{
		//（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
		setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.ReconnectView, false);
		Logger.PrintColor("yellow", "ReconnectView()");
	}
	/// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
	    Logger.PrintColor("yellow", $"ReconnectView onLoadUIEnd complte!! gameObject={gameObject}");
		this.contentPane = gameObject.asCom;
		contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
		view = contentPane as G_ReconnectView;
		this.modal = true;

        GlobalTimeManager.Instance.timerController.AddTimer(this.topTipsTimer, 1500, 1, OnHideView);
        Logger.PrintColor("red", $"ReconnectView()");
    }
    private void OnHideView(int time)
    {
        if (GlobalTimeManager.Instance.timerController.CheckExistByKey(this.topTipsTimer))
        {
            GlobalTimeManager.Instance.timerController.RemoveTimerByKey(this.topTipsTimer);
        }
        Logger.PrintColor("red", $"OnHideView() time={time}");
        ToHide();
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