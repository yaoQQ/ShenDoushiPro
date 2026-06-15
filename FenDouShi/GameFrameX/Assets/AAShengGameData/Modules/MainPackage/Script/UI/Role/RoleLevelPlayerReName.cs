//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-08-21 20:46:46.304
//------------------------------------------------------------

using FairyGUI;
using msg.role;
using msg.system;
using roleLevel;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: RoleLevelPlayerReName
///	定义窗口标识UIViewEnum.RoleLevelPlayerReName为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
[FGUIViewAttribute(UIViewEnum.RoleLevelPlayerReName,typeof(RoleLevelPlayerReName))]
public class RoleLevelPlayerReName : BaseView
{
	//G_RoleLevelPlayerReName 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
	public override string PackageName => G_RoleLevelPlayerReName.PACKAGE_NAME;
	public override string ComponentName =>G_RoleLevelPlayerReName.COMPONENT_NAME;
	G_RoleLevelPlayerReName view;
	
	//注册界面事件
	protected override Dictionary<EEventType, OnEventLister> EventList => new()
	{
         { EEventType.RoleRenameResp, Role_RoleRenameResp },

    };

    #region 网络事件
    void Role_RoleRenameResp(EventSysArgsBase argsBase)
    {
        if (view == null)
            return;
        if (argsBase is EventSysArgs<RoleRenameResp> args)
        {
            MessageBoxVo msgVo = new MessageBoxVo();
            msgVo.title = "提示";
            msgVo.msg = $"修改名字成功{args.args1.Name}";
            msgVo.OkBtnfunc = () =>
            {
                ToHide();
            };
            msgVo.CancelBtnfunc = () =>
            {
            };
            CommonViewUtils.ShowMessageBox(msgVo);
        }
    }
    #endregion
    public RoleLevelPlayerReName()
	{
		//（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
		setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.RoleLevelPlayerReName, false);
		Logger.PrintColor("yellow", "RoleLevelPlayerReName()");
	}
	/// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
	    Logger.PrintColor("yellow", $"RoleLevelPlayerReName onLoadUIEnd complte!! gameObject={gameObject}");
		this.contentPane = gameObject.asCom;
		contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
		view = contentPane as G_RoleLevelPlayerReName;

		view.InputText.text = "修改名字";
        view.coinContent.visible = false;

    }
	
	//统一按钮回调
	protected override void OnButtonClick(GButton clickedButton)
	{
        if (view.closeBtn == clickedButton)
        {
            ToHide();
        }
		else if (view.okBtn == clickedButton)
		{
			string reName = view.InputText.text;
			if (string.IsNullOrEmpty(reName))
			{
                MessageBoxVo msgVo = new MessageBoxVo();
                msgVo.title = "提示";
                msgVo.msg ="名字不合符合要求!";
                msgVo.OkBtnfunc = () =>
                {
                };
                CommonViewUtils.ShowMessageBox(msgVo);
                return;
			}
			RoleControl.Instance.ReqRoleRenameReq(reName);

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