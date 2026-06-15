//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-08-21 20:45:56.503
//------------------------------------------------------------

using FairyGUI;
using msg.role;
using roleLevel;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: RoleLevelPlayerInfoView
///	定义窗口标识UIViewEnum.RoleLevelPlayerInfoView为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
[FGUIViewAttribute(UIViewEnum.RoleLevelPlayerInfoView,typeof(RoleLevelPlayerInfoView))]
public class RoleLevelPlayerInfoView : BaseView
{
	//G_RoleLevelPlayerInfoView 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
	public override string PackageName => G_RoleLevelPlayerInfoView.PACKAGE_NAME;
	public override string ComponentName =>G_RoleLevelPlayerInfoView.COMPONENT_NAME;
	G_RoleLevelPlayerInfoView view;
	
	//注册界面事件
	protected override Dictionary<EEventType, OnEventLister> EventList => new()
	{
         { EEventType.EventRoleInfoUpdate, Role_RoleInfoResp },
          { EEventType.RoleRenameResp, Role_RoleRenameResp },

    };

    #region 网络事件
    void Role_RoleInfoResp(EventSysArgsBase argsBase)
    {
		Logger.PrintDebug("更新角色信息 view=" + view);
        if (view == null)
            return;
        if (argsBase is EventSysArgs<RoleInfoResp> args)
        {
            Logger.PrintDebug("更新角色信息 args=" + args);
            view.playerLevel.text = args.args1.Level.ToString()+"级";
            view.SetRoleNameBtn.userName.text = args.args1.Name;
			view.guildName.text = "无公会";
            LocationVo locationVo = LocationDataManager.Instance.GetLocation(args.args1.provinceCode, args.args1.cityCode);
            if (locationVo != null)
            {
                view.SetLocationBtn.locationText.text = $"{locationVo.ProvinceName},{locationVo.CityName}";
            }
            else
            {
                view.SetLocationBtn.locationText.text = "未知";
            }
            view.campText.text = "配置官职:" + args.args1.Official;
            view.finghtPowerText.text= args.args1.fightPower.ToString();
			view.roleId.text = args.args1.roleId.ToString();
        }
    }

    void Role_RoleRenameResp(EventSysArgsBase argsBase)
    {
        if (view == null)
            return;
        if (argsBase is EventSysArgs<RoleRenameResp> args)
        {
            view.SetRoleNameBtn.userName.text = args.args1.Name;
        }
    }
    #endregion
    public RoleLevelPlayerInfoView()
	{
		//（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
		setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.RoleLevelPlayerInfoView, false);
		Logger.PrintColor("yellow", "RoleLevelPlayerInfoView()");
	}
	/// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
	    Logger.PrintColor("yellow", $"RoleLevelPlayerInfoView onLoadUIEnd complte!! gameObject={gameObject}");
		this.contentPane = gameObject.asCom;
		contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
		view = contentPane as G_RoleLevelPlayerInfoView;

        view.heroTabList.selectedIndex = 0;
        view.heroTabList.onClickItem.Add(OnTabSelect);
 
        for (int i = 0; i < view.heroTabList.numItems; i++)
        {
            G_heroItemList g_HeroItemList = view.heroTabList.GetChildAt(i) as G_heroItemList;
            g_HeroItemList.data = i;
        }
        view.heroIconList.onClickItem.Add(OnIconListSelect);

        for (int i = 0; i < view.heroIconList.numItems; i++)
        {
            G_RoleTypeIcon g_RoleTypeIcon = view.heroIconList.GetChildAt(i) as G_RoleTypeIcon;
            g_RoleTypeIcon.data = i;
            g_RoleTypeIcon.c1.selectedIndex = view.c1.selectedIndex;
        }
        //item的类型背景
        G_RoleTypeIcon itemBg = view.heroIconList.AddItemFromPool() as G_RoleTypeIcon;
        itemBg.data = -1;//代表背景
        itemBg.heroIcon.visible = false;
        itemBg.c1.selectedIndex = view.c1.selectedIndex;
        //view.areaList.RemoveChildrenToPool();
        //view.serverList.RemoveChildrenToPool();
    }
    void OnTabSelect(EventContext context)
    {

        G_heroItemList itemBtn = context.data as G_heroItemList;
       // int zoneNum = (int)itemBtn.data;//所点击的区按钮
       Logger.PrintGreen("heroList data="+itemBtn.data);
        view.c1.SetSelectedIndex((int)itemBtn.data);
        for (int i = 0; i < view.heroIconList.numItems; i++)
        {
            G_RoleTypeIcon g_RoleTypeIcon = view.heroIconList.GetChildAt(i) as G_RoleTypeIcon;
            g_RoleTypeIcon.heroIcon.c1.selectedIndex = view.c1.selectedIndex;
            g_RoleTypeIcon.c1.selectedIndex = view.c1.selectedIndex;
        }
    }
    void OnIconListSelect(EventContext context)
    {
        G_RoleTypeIcon itemBtn = context.data as G_RoleTypeIcon;
        Logger.PrintGreen("heroList data=" + itemBtn.data);
    }
    //统一按钮回调
    protected override void OnButtonClick(GButton clickedButton)
	{
        if (view.closeBtn == clickedButton)
        {
            model.gameObject.SetActive(false);
            ToHide();
        }
        else if (view.SetRoleNameBtn== clickedButton)//修改昵称
        {
            UIViewManager.Instance.Show(UIViewEnum.RoleLevelPlayerReName);
        }
        else if(view.SetLocationBtn== clickedButton)//修改位置
        {
            UIViewManager.Instance.Show(UIViewEnum.RoleLocationView);

        }
        else if (view.fightPowerBtn == clickedButton)//战力对比
        {
            UIViewManager.Instance.Show(UIViewEnum.RoleLevelFightPowerView);
        }
        else if (view.chatBtn == clickedButton)//私聊
        {
            UIViewManager.Instance.Show(UIViewEnum.ChatView);
        }
        else if (view.delectFriendBtn == clickedButton)//删除好友	
        {
            CommonViewUtils.ShowTopTips("功能未开放！");
        }
        else if (view.reportBtn == clickedButton)//举报
        {
            UIViewManager.Instance.Show(UIViewEnum.RoleLevelReportView);
        }
        else if (view.blackBtn == clickedButton)//拉黑
        {
            CommonViewUtils.ShowTopTips("功能未开放！"); 
        }
        else if (view.roleInfoBtnContent.adviceBtn == clickedButton)//公告
        {
            CommonViewUtils.ShowTopTips("功能未开放！");
        }
        else if (view.roleInfoBtnContent.setBtn == clickedButton)//设置
        {
            CommonViewUtils.ShowTopTips("功能未开放！");
        }
        else if (view.roleInfoBtnContent.roleStyleBtn == clickedButton)//形象
        {
            CommonViewUtils.ShowTopTips("功能未开放！");
        }
    }
	
	//打开界面,fairyGUI打开动画播放完触发
	protected override void OnShown()
	{
        GetModel();
        base.OnShown();
        RoleControl.Instance.ReqRoleInfoReq();


    }
    RoleStageContent model;

    private async void GetModel()
	{
        if (model == null)
        {
            model = await ModelManager.Instance.GetRoleStageContent(view.model);
        }
        if (model == null)
		{
			Logger.PrintError("获取角色模型失败");
            return;
		}
        model.gameObject.SetActive(true);
    }
	
	//关闭界面,fairyGUI关闭动画播放完触发
	protected override void OnHide()
	{

        base.OnHide();
        UIViewManager.Instance.Show(UIViewEnum.GameMainView);
    }
	
	//框架和fairyGUI底层销毁界面时触发
	protected override void OnDestroy() { 
	
	}
}