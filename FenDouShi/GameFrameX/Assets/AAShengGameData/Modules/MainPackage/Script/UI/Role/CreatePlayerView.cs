//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-08-15 16:32:07.048
//------------------------------------------------------------

using FairyGUI;
using login;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: CreatePlayerView
///	定义窗口标识UIViewEnum.CreatePlayerView为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
[FGUIViewAttribute(UIViewEnum.CreatePlayerView,typeof(CreatePlayerView))]
public class CreatePlayerView : BaseView
{
	//G_CreatePlayerView 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
	public override string PackageName => G_CreatePlayerView.PACKAGE_NAME;
	public override string ComponentName =>G_CreatePlayerView.COMPONENT_NAME;
	G_CreatePlayerView view;
	
	//注册界面事件
	protected override Dictionary<EEventType, OnEventLister> EventList => new()
	{


	};

	
	public CreatePlayerView()
	{
		//（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
		setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.CreatePlayerView, false);
		Logger.PrintColor("yellow", "CreatePlayerView()");
	}
	/// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
	    Logger.PrintColor("yellow", $"CreatePlayerView onLoadUIEnd complte!! gameObject={gameObject}");
		this.contentPane = gameObject.asCom;
		contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
		view = contentPane as G_CreatePlayerView;
	}
	
	//统一按钮回调
	protected override void OnButtonClick(GButton clickedButton)
	{
		if(view.okBtn== clickedButton)
		{
			string roleName = view.userNameInput.text;
            if (string.IsNullOrEmpty(roleName))
            {
				CommonViewUtils.ShowTopTips("请输入昵称");
                return;
            }
            // else if (ConfigManager.Instance.checkShieldFont(roleName))
            //{
            //    CommonViewUtils.ShowTopTips("包含不恰当的用词，请尝试其它昵称");
            //}
            //else if (!UITools.validateNickname(roleName))
            //{
            //    CommonViewUtils.ShowTopTips("昵称仅可使用中文、数字、非特殊符号");
            //}
            //else if (!UITools.needChineseNickName(roleName))
            //{
            //    CommonViewUtils.ShowTopTips("昵称最小字数为1个中文字");
            //}
            //else
            //{
            //    Logger.PrintDebug("创建角色："+roleName);
            //    RoleCtrl.ins().reqRoleRenameReq(roleName);
            //}

            RoleControl.Instance.ReqRoleRenameReq(roleName);
            ToHide();
        }
		else if (view.closeBtn == clickedButton)
		{
			ToHide();
		}
        else if (view.RandomBtn == clickedButton)
        {
			
        }
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