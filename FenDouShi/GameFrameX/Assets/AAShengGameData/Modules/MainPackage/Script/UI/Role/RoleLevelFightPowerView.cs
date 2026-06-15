//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-08-21 20:42:58.600
//------------------------------------------------------------

using FairyGUI;
using msg.role;
using roleLevel;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: RoleLevelFightPowerView
///	定义窗口标识UIViewEnum.RoleLevelFightPowerView为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
[FGUIViewAttribute(UIViewEnum.RoleLevelFightPowerView, typeof(RoleLevelFightPowerView))]
public class RoleLevelFightPowerView : BaseView
{
    //G_RoleLevelFightPowerView 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
    public override string PackageName => G_RoleLevelFightPowerView.PACKAGE_NAME;
    public override string ComponentName => G_RoleLevelFightPowerView.COMPONENT_NAME;
    G_RoleLevelFightPowerView view;

    //注册界面事件
    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {
          { EEventType.RolePowerCompareResp, Role_RolePowerCompareResp },
    };

    #region 网络事件
    void Role_RolePowerCompareResp(EventSysArgsBase argsBase)
    {
        if (view == null)
            return;
        if (argsBase is EventSysArgs<RolePowerCompareResp> args)
        {
            view.HeadIconLeft.fightPowerTotalText.text = args.args1.Left.roleInfo.fightPower.ToString();
            view.rightHeadIcon.fightPowerTotalText.text = args.args1.Right.roleInfo.fightPower.ToString();
        }
    }
    #endregion
    public RoleLevelFightPowerView()
    {
        //（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.RoleLevelFightPowerView, false);
        Logger.PrintColor("yellow", "RoleLevelFightPowerView()");
    }
    /// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
        Logger.PrintColor("yellow", $"RoleLevelFightPowerView onLoadUIEnd complte!! gameObject={gameObject}");
        this.contentPane = gameObject.asCom;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        view = contentPane as G_RoleLevelFightPowerView;
    }

    //统一按钮回调
    protected override void OnButtonClick(GButton clickedButton)
    {
        if (view.closeBtn == clickedButton)
        {
            ToHide();
        }
    }

    //打开界面,fairyGUI打开动画播放完触发
    protected override void OnShown()
    {
        base.OnShown();;
        Logger.PrintDebug("RoleLevelFightPowerView OnShown ");
        var roleInfo = RoleData.Instance.getRoleInfo();
        if (roleInfo == null)
        {
            CommonViewUtils.ShowTopTips("角色信息为空");
        }
        else
        {
            RoleControl.Instance.ReqRolePowerCompareReq(RoleData.Instance.getRoleInfo().roleId);
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