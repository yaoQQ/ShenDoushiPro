using FairyGUI;
using Rank;
using System.Collections.Generic;

[FGUIViewAttribute(UIViewEnum.RankProgressView, typeof(RankProgressView))]
public class RankProgressView : BaseView
{
    public override string PackageName => G_RankProgressView.PACKAGE_NAME;
    public override string ComponentName => G_RankProgressView.COMPONENT_NAME;

    private TableView<RankProgressTabItem> list_tab;

    private TableView<RankProgressItem> list_content;


    private G_RankProgressView mRoot;

    private List<int> _ranks;

    private int _rankId;


    public RankProgressView()
    {
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.RankProgressView, false);
    }

    protected override Dictionary<EEventType, OnEventLister> EventList => new Dictionary<EEventType, OnEventLister>()
    {
        { EEventType.RankProgressEvent, OnRankProgressEvent },
        { EEventType.RankEvent_RedPoint, OnRankEvent_RedPoint },
    };
  
    private void OnRankEvent_RedPoint(EventSysArgsBase argsBase)
    {
        ShowView();
    }

    private void OnRankProgressEvent(EventSysArgsBase argsBase)
    {
        ShowView();
    }

    private void ShowView()
    {
        if (_ranks != null)
        {
            list_tab.setDatas(_ranks);
            var index = _ranks.IndexOf(_rankId);
            list_tab.setSelectIndex(index);
            OnShowContent();
        }
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mRoot = contentPane as G_RankProgressView;
        closeButton = mRoot.Button_close;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        list_tab = new(mRoot.List_tabs);
        list_tab.setClickCallBack(OnTabClick);
        list_content = new TableView<RankProgressItem>(mRoot.list_reward);
    }

    private void OnTabClick(object arg1, int arg2)
    {
        _rankId = (int)arg1;
        OnShowContent();
    }

    private void OnShowContent()
    {
        var progressCfg = RankControl.Instance.Model.GetRankProgressListCfg(_rankId);
        list_content.setDatas(progressCfg);
    }

    private void Clear()
    {
    }

    protected override void OnShown()
    {
        base.OnShown();
        _ranks = null;
        _rankId = 0;
        if (showArgs is int rankId)
        {
            _rankId = rankId;
            var cfg = RankControl.Instance.Model.GetRankGroupCfgByRankId(rankId);
            if (cfg != null)
            {
                _ranks = cfg.Ranks;
            }
            RankControl.Instance.ReqRankProgress(rankId);
        }
        Clear();
    }
}



