using common;
using FairyGUI;
using login;
using System.Collections.Generic;

[FGUIViewAttribute(UIViewEnum.LoginSelectServerView, typeof(LoginSelectServerView))]
public class LoginSelectServerView : BaseView
{
    public override string PackageName => G_SelServerWin.PACKAGE_NAME;
    public override string ComponentName => G_SelServerWin.COMPONENT_NAME;

    G_SelServerWin view;
    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {
        { EEventType.LoadServiceZoneDataComplete, OnLoadServiceZoneDataComplete }
    };

    private int currServiceZoneIndex = -1;
    private ServerInfo currServerInfo;
    public LoginSelectServerView()
    {
        //(声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理)
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.LoginSelectServerView, false);
        Logger.PrintColor("yellow", "LoginSelectServerView()");
    }
    /// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="uiName"></param>
    /// <param name="gameObject"></param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
        Logger.PrintColor("yellow", $"LoginSelectServerView onLoadUIEnd()");
        this.contentPane = gameObject.asCom;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        view = contentPane as G_SelServerWin;
        this.modal = true;
        view.emptyText.visible = false;
        view.areaList.RemoveChildrenToPool();
        view.serverList.RemoveChildrenToPool();
        this.Center();
    }
    protected override void OnShown()
    {
        base.OnShown();
       // GRoot.inst.ShowPopup(contentPane.parent, GRoot.inst, PopupDirection.Auto);
        //this.Center();
        Logger.PrintColor("red", $"@@@@@@@@@@@@@LoginSelectServerView OnShown()");

        view.backBtn.onClick.Add(() =>
        {
            Logger.PrintDebug("backBtn onClick");
            Hide();

        });
        currServiceZoneIndex = -1;
        refresh();

    }
    private void refresh()
    {
        //区的列表
        view.areaList.onClickItem.Add(OnClickServiceZoneItem);
        view.areaList.RemoveChildrenToPool();
        //服务器选择的列表

        view.serverList.RemoveChildrenToPool();
        view.serverList.onClickItem.Add(OnClickServiceInfoItem);

        CentralServerLogin.ReqServicesList(-1);

    }

    private void OnLoadServiceZoneDataComplete(EventSysArgsBase notice)
    {
        Logger.PrintColor("red", $"@@@@@@@@@@OnLoadServiceZoneDataComplete() Loading service zone data complete: {notice}");
        RefreshServiceZone();
        RefreshServiceInfoList();
        int totalPage = GameLoginSessionData.Instance.TotalPages;
        int currentPage = GameLoginSessionData.Instance.CurrentPage;


    }
    private List<G_AreaItem> serverList = new List<G_AreaItem>();
    //刷新服务区的列表
    private void RefreshServiceZone()
    {
        Logger.PrintDebug($"RefreshServiceZone() Loading service zone data complete!");
        view.areaList.RemoveChildrenToPool();
        serverList.Clear();
        ServerListResponse serviceRe = GameLoginSessionData.Instance.ServerListResponse;
        //int pageNum = serviceRe.data.page_num;
        int pageMaxNum = GameLoginSessionData.Instance.TotalPages;
        Logger.PrintDebug($"RefreshServiceZone() Loading service zone data complete!");
        Logger.PrintDebug($"RefreshServiceZone() max_page_num={serviceRe.data.max_page_num}!");
        Logger.PrintDebug($"RefreshServiceZone() page_num={serviceRe.data.page_num}!");
        Logger.PrintDebug($"RefreshServiceZone() page_size={serviceRe.data.page_size}!");
        G_AreaItem playerItem = view.areaList.AddItemFromPool() as G_AreaItem;
        playerItem.title.text = "推荐服务器";
        playerItem.data = -1;//设置按钮数据为区Num
        playerItem.icon.SetSelectedIndex( 0 );
        G_AreaItem playerItem2 = view.areaList.AddItemFromPool() as G_AreaItem;
        playerItem2.title.text = "我的服务器";
        playerItem2.data = 0;//设置按钮数据为区Num
        playerItem2.icon.SetSelectedIndex(1);
        serverList.Add(playerItem);
        serverList.Add(playerItem2);
        for (int i = 0; i < pageMaxNum; i++)
        {
            G_AreaItem Item = view.areaList.AddItemFromPool() as G_AreaItem;
            int pageStartIndex = i * serviceRe.data.page_size + 1;//开始页数
            Item.title.text = $"{pageStartIndex}~{pageStartIndex+GameLoginSessionData.Instance.PageSize}";
            Item.num.text = LoginUtils.GetStrByServicePage(i+1);// //获取罗马把数字页字符
            Item.data = (i + 1);//设置按钮数据为区Num
            Item.icon.SetSelectedIndex(2);//页签图标
            serverList.Add(Item);
        }
        serverList[currServiceZoneIndex + 1].selected = true;
    }

    //刷新当前区的服务器列表
    private void RefreshServiceInfoList()
    {
        view.serverList.RemoveChildrenToPool();
        List<ServerInfo> serverList = GameLoginSessionData.Instance.ServerList;
        for (int i = 0; i < serverList.Count; i++)
        {
            G_SelServerItem itemComp = view.serverList.AddItemFromPool() as G_SelServerItem;
            UpdateItem(itemComp, serverList[i]);
        }
    }
    private void UpdateItem(G_SelServerItem itemComp, ServerInfo serverInfo)
    {
        itemComp.title.text = serverInfo.name;
        //  itemComp.text = serverList[i].GetStatusText();
        itemComp.ServerStatusIcon.c1.selectedIndex = serverInfo.status - 1;
        itemComp.data = serverInfo;//设置按钮数据为当前服务器数据
        if (serverInfo.isUser > 0)
        {
            itemComp.PlayerHeadIcon.visible = true;
            itemComp.PlayerTextItem.visible = true;
            itemComp.PlayerHeadIcon.levelText.text = serverInfo.role_info.level.ToString();
            int headId = serverInfo.role_info.head_id;
            itemComp.PlayerName.text = serverInfo.role_info.name;
            itemComp.LevelText.text = serverInfo.role_info.level.ToString()+"级";
        }
        else
        {
            itemComp.PlayerHeadIcon.visible = false;
            itemComp.PlayerTextItem.visible = false;
        }
    }


    void OnClickServiceZoneItem(EventContext context)
    {

        GButton itemBtn = context.data as GButton;
        int zoneNum = (int)itemBtn.data;//所点击的区按钮
        if (currServiceZoneIndex == zoneNum)
        {
            return;
        }
        CentralServerLogin.ReqServicesList(zoneNum);
        Logger.PrintDebug($"点击服务器区 onClickItem.data={zoneNum} ");
        currServiceZoneIndex = zoneNum;

    }
    void OnClickServiceInfoItem(EventContext context)
    {
        GButton itemBtn = context.data as GButton;
        currServerInfo = (ServerInfo)itemBtn.data;
        Logger.PrintDebug($"选择服务器区 serverInfo.server_id={currServerInfo.server_id} ");
        Logger.PrintDebug($"选择服务器区 serverInfo.name={currServerInfo.name} ");
        Logger.PrintDebug($"选择服务器区 serverInfo.ip={currServerInfo.ip} ");
        if (currServerInfo != null)
        {
            GameLoginSessionData.Instance.ChangeServiceInfo(currServerInfo);
            EventManager.Instance.Dispatch(EEventType.LoginChangeService, currServerInfo);
        }
        Hide();
    }
}