using common;
using FairyGUI;
using System.Collections.Generic;
using SystemOpen;

[FGUIViewAttribute(UIViewEnum.SystemOpenView, typeof(SystemOpenView))]
public class SystemOpenView : BaseView
{
    private G_SystemOpenView mainView;
    public override string PackageName => G_SystemOpenView.PACKAGE_NAME;
    public override string ComponentName => G_SystemOpenView.COMPONENT_NAME;

    private SystemOpenTabItemRenderer SystemOpenTabItemRenderer;

    private List<FuncVo> tabDtas;

    private SystemOpenTaskItemRenderer taskItemRenderer;

    private int mSelectFucId;

    private RenderImage _renderImage;

    private TableView<ItemRender> mItemRender;


    protected override Dictionary<EEventType, OnEventLister> EventList => new Dictionary<EEventType, OnEventLister>() {

        { EEventType.SystemOpenEvent,OnSystemOpenEvent},
        { EEventType.SystemOpen_InfoUpdate,OnSystemInfoUpdateEvent }
    };

    private void OnSystemInfoUpdateEvent(EventSysArgsBase argsBase)
    {
        OnRefreshView();
    }

    private void OnSystemOpenEvent(EventSysArgsBase argsBase)
    {
        OnRefreshView();
    }

    private void OnRefreshView()
    {
        if (mSelectFucId <= 0) return;
        var cfg = SystemOpenControl.Instance.Model.GetSystemCfgById(mSelectFucId);
        SystemOpenTabItemRenderer.ForceRefresh();
        OnRefreshContent(cfg);
    }

    public SystemOpenView()
    {
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.SystemOpenView, false);
    }


    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mainView = contentPane as G_SystemOpenView;
        contentPane.MakeFullScreen();
        closeButton = mainView.closeButton;
        mainView.SetSize(GRoot.inst.width, GRoot.inst.height);
        mainView.AddRelation(GRoot.inst, RelationType.Size);
        SystemOpenTabItemRenderer = new SystemOpenTabItemRenderer(mainView.List_tab);
        taskItemRenderer = new SystemOpenTaskItemRenderer(mainView.List_task);
        mainView.List_rewardItem.defaultItem = G_ItemRender.URL;
        mItemRender = new TableView<ItemRender>(mainView.List_rewardItem);
        var cfgs = SystemOpenControl.Instance.Model.GetSystemCfgs();
        tabDtas ??= new List<FuncVo>();
        tabDtas.Clear();
        foreach (var item in cfgs)
        {
            if (item.IsNotice == 1)
            {
                tabDtas.Add(item);
            }
        }
        tabDtas.Sort(SortLogic);
        SystemOpenTabItemRenderer.SetData(tabDtas);
        mainView.List_tab.onClickItem.Add(OnTableClick);
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == mainView.Button_draw)
        {
            OnDrawClick();
        }
    }

    private void OnDrawClick()
    {
        SystemOpenControl.Instance.OnDrawSystemOpenReward(mSelectFucId);
    }

    private int SortLogic(FuncVo a, FuncVo b)
    {
        var isDraw_a = SystemOpenControl.Instance.Model.GetIsGetSystemOpenReward(a.Id);
        var isDraw_b = SystemOpenControl.Instance.Model.GetIsGetSystemOpenReward(b.Id);
        if (isDraw_a != isDraw_b)
        {
            return isDraw_a.CompareTo(isDraw_b);
        }
        if (a.Sort != b.Sort)
        {
            return b.Sort.CompareTo(a.Sort);
        }
        return a.Id.CompareTo(b.Id);
    }

    private void OnTableClick(EventContext context)
    {
        var clickItem = context.data as GComponent;
        var cfg = (FuncVo)clickItem.data;
        if (cfg.Id == mSelectFucId)
        {
            return;
        }
        OnRefreshContent(cfg);
    }

    private void OnRefreshContent(FuncVo cfg)
    {
        mSelectFucId = cfg.Id;
        var taskList = SystemOpenControl.Instance.Model.GetTaskCfgs(mSelectFucId);
        taskItemRenderer.tempData = mSelectFucId;
        taskItemRenderer.SetData(taskList);
        mainView.Text_systemDesc.text = cfg.Desc;
        mainView.Text_systemName.text = cfg.Name;
        mainView.Image_system.url = UIHelper.GetFguiUrl(G_SystemOpenView.PACKAGE_NAME, cfg.Icon);
        var isDraw = SystemOpenControl.Instance.Model.GetIsGetSystemOpenReward(mSelectFucId);
        var res = !isDraw ? "common_btn_02" : "common_btn_02_hui";
        mainView.Button_draw.Image_button.url = UIHelper.GetFguiUrl(ItemDefine.commonPackage, res);
        mainView.Button_draw.Text_draw.text = isDraw ? "已领取" : "领取";
        var mLent = cfg.Rewards.Count;
        var rewards = new List<CommonItemData>();
        for (int i = 0; i < mLent; i++)
        {
            var item = cfg.Rewards[i];
            rewards.Add(new CommonItemData(item[0], item[1]));
        }
        mItemRender.setDatas(rewards);
        var isRed = SystemOpenControl.Instance.Model.GetIsHaveReward(mSelectFucId);
        mainView.Button_draw.redPoint.visible = isRed;
    }

    protected override void OnShown()
    {
        base.OnShown();
        SystemOpenTabItemRenderer.ForceRefresh();
        mainView.List_tab.selectedIndex = 0;
        mSelectFucId = 0;
        if (tabDtas is { Count: > 0 })
        {
            OnRefreshContent(tabDtas[0]);
        }
        //if (_renderImage == null)
        //{
        //    var img = mainView.n48;
        //    _renderImage = new RenderImage(mainView.Image_model, img);
        //}
        //_renderImage.LoadModel(20.ToString());
    }

    protected override void OnHide()
    {
        base.OnHide();
        //_renderImage.OnShow(false);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        //_renderImage?.Dispose();
    }
}
