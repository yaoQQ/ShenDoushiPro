using FairyGUI;
using Rank;
using System.Collections.Generic;

[FGUIViewAttribute(UIViewEnum.RankView, typeof(RankView))]
public class RankView : BaseView
{
    public override string PackageName => G_RankView.PACKAGE_NAME;
    public override string ComponentName => G_RankView.COMPONENT_NAME;

    private TableView<RankTabItem> list_tab;
    private TableView<RankBannerItem> list_banner;

    private G_RankView mRoot;

    private string _rankReqTimer = "_rankReqTimer";

    protected override TopRenderData mTopRenderData => new TopRenderData
    {
        titleName = "排行榜",
        //icon = "ui://Common/Gm/icon_gm",
        helpId = 10000,
        //currencyId = 10001,
    };

    public RankView()
    {
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.RankView, false);
    }

    private List<RankTabItemData> mleftTabDatas;

    protected override Dictionary<EEventType, OnEventLister> EventList => new Dictionary<EEventType, OnEventLister>()
    {
        { EEventType.RankEvent_RedPoint, OnRankEvent_RedPoint },
    };

    private void OnRankEvent_RedPoint(EventSysArgsBase argsBase)
    {
        list_tab?.setDatas(mleftTabDatas);
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mRoot = contentPane as G_RankView;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        list_tab = new TableView<RankTabItem>(mRoot.List_tabs);
        list_banner = new TableView<RankBannerItem>(mRoot.List_banner);
        list_banner.setClickCallBack(OnClickBanner);
        mleftTabDatas = new List<RankTabItemData>();
        var cfgs = RankControl.Instance.Model.GetRankGroupCfg();
        for (var index = 0; index < cfgs.Count; index++)
        {
            var cfg = cfgs[index];
            //var Image_bg = Utility.Text.Format("paihangbang_btn_yeqian0{0}", index + 1);
            var Image_bg = Utility.Text.Format("paihangbang_btn_yeqian0{0}", 2);
            mleftTabDatas.Add(new RankTabItemData()
            {
                ImageBgUrl = UIHelper.GetFguiUrl("Rank", Image_bg),
                ImageIconUrl = UIHelper.GetFguiUrl("Rank", cfg.Icon),
                Name = cfg.Name,
                RankGroupCfg = cfg,
            });
        }
        list_tab.setClickCallBack(OnClickTable);
    }


    private void OnClickTable(object arg1, int arg2)
    {
        var data = arg1 as RankTabItemData;
        ShowContent(data.RankGroupCfg);
    }

    List<int> datas = new List<int>();

    private void ShowContent(RankGroupVo rankGroup)
    {
        datas ??= new List<int>();
        datas.Clear();
        var mCnt = rankGroup.Ranks.Count;
        for (int i = 0; i < mCnt; i++)
        {
            var rankId = rankGroup.Ranks[i];
            var cfg = RankControl.Instance.Model.GetRankCfg(rankId);
            if (cfg is { ShowInPanel: 1 })
            {

                datas.Add(rankId);
            }
        }
        list_banner.setDatas(datas);
    }

    private void OnClickBanner(object data, int index)
    {
        var rankId = (int)data;
        var rankInfo = RankControl.Instance.Model.GetRankBaseInfoById(rankId);
        if (rankInfo?.roleInfo == null)
        {
            CommonViewUtils.ShowTopTips("暂无人上榜");
        }
        UIViewManager.Instance.Show(UIViewEnum.RankListView, rankId);
    }
    protected override void OnShown()
    {
        base.OnShown();
        StopTimer();
        var _selectIdx = 0;
        list_tab.setDatas(mleftTabDatas);
        list_tab.setSelectIndex(_selectIdx);
        ShowContent(mleftTabDatas[_selectIdx].RankGroupCfg);
        GlobalTimeManager.Instance.timerController.AddTimer(_rankReqTimer, 1000 * 60, -1, ReqRankMsg);
        ReqRankMsg();
    }
    protected override void OnHide()
    {
        base.OnHide();
        StopTimer();
    }

    private void StopTimer()
    {
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(_rankReqTimer);
    }

    private void ReqRankMsg(int count = 0)
    {
        RankControl.Instance.ReqInitInfo();
    }
}

