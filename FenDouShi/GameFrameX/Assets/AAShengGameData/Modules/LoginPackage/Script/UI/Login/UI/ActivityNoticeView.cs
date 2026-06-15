//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：2025-08-15 17:35:10.855
//------------------------------------------------------------

using Bag;
using FairyGUI;
using login;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自定义 FairyGUIView 模板
/// Script Name: ActivityNoticeView
///	定义窗口标识UIViewEnum.ActivityNoticeView为类型名，否则请填入自定义UIViewEnum标识
/// </summary>
[FGUIViewAttribute(UIViewEnum.ActivityNoticeView,typeof(ActivityNoticeView))]
public class ActivityNoticeView : BaseView
{
	//G_ActivityNoticeView 为fairyGUI导出的组件名，默认为与类型同名。否则请修改为自定义的导出组件名
	public override string PackageName => G_ActivityNoticeView.PACKAGE_NAME;
	public override string ComponentName =>G_ActivityNoticeView.COMPONENT_NAME;
	G_ActivityNoticeView view;
    private int currServiceZoneIndex = 0;
    //注册界面事件
    protected override Dictionary<EEventType, OnEventLister> EventList => new()
	{


	};

	
	public ActivityNoticeView()
	{
		//（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
		setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.ActivityNoticeView, false);
		Logger.PrintColor("yellow", "ActivityNoticeView()");
	}
    List<string> dataList = new List<string> { "活动", "系统", "新服咨询", "名称" };
    /// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
	    Logger.PrintColor("yellow", $"ActivityNoticeView onLoadUIEnd complte!! gameObject={gameObject}");
		this.contentPane = gameObject.asCom;
		contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
		view = contentPane as G_ActivityNoticeView;
        this.modal = true;
        initAvitvity();
    }
    private void initAvitvity()
    {
        view.activityTabList.RemoveChildrenToPool();



       
        CentralServerLogin.ReqHostTextByType(RequestHostType.notice_list, (str) =>
        {
            Logger.PrintGreen("RequestHostType.notice_list  str=" + str);
            ActivityHostDataList response = DataTableFrame.CongfigUtility.Json.ToObject<ActivityHostDataList>(str);
            if(response.Data==null|| response.Data.notice_list==null|| response.Data.notice_list.Count == 0)
            {
                view.empetText.visible = true;
                return;
            }
            view.empetText.visible=false;
            Logger.PrintGreen("RequestHostType.notice_list =" + response.Data.notice_list);
            int firstId=0;
            for (int i = 0; i < response.Data.notice_list.Count; i++)
            {
                G_noticeTab Item = view.activityTabList.AddItemFromPool() as G_noticeTab;
                Item.title.text = response.Data.notice_list[i].title;
                int id = response.Data.notice_list[i].id;
                Item.data = id;//设置按钮数据为区Num
                if (i == 0)
                {
                    firstId = id;
                }
            }
            view.activityTabList.selectedIndex = 0;
            selectTab(firstId);

            view.activityTabList.onClickItem.Add(OnClickTabItem);
        });
    }
    void OnClickTabItem(EventContext context)
    {
      
        GButton itemBtn = context.data as GButton;
        int zoneNum = (int)itemBtn.data;//所点击的区按钮
        Logger.PrintDebug("OnClickTabItem zoneNum=" + zoneNum);
        if (currServiceZoneIndex == zoneNum)
        {
            return;
        }
        selectTab(zoneNum);
        Logger.PrintDebug($"点击服务器区 onClickItem.data={zoneNum} ");
        currServiceZoneIndex = zoneNum;

    }
    private void selectTab(int index)
    {
        CentralServerLogin.ReqHostTextByType(RequestHostType.notice, index.ToString(), (str) =>
        {
            ActivityHostData response = DataTableFrame.CongfigUtility.Json.ToObject<ActivityHostData>(str);
            view.contentTitle.text = response.Data.Notice.Title; 
            view.contentText.Info.text = response.Data.Notice.Content; 
            Logger.PrintGreen("RequestHostType.notice str=" + str);
            
            Debug.Log("22RequestHostType.notice response.Data.Notice.Content=" + response.Data.Notice.Content);
            Logger.PrintGreen("RequestHostType.notice response.Data.Notice.Content=" + response.Data.Notice.Content);
        });
    }
    //统一按钮回调
    protected override void OnButtonClick(GButton clickedButton)
	{
	    if(view.closeBtn == clickedButton)
        {
            ToHide();
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