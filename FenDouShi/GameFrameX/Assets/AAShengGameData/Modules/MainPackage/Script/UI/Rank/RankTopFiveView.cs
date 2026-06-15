using FairyGUI;
using msg.rank;
using Rank;
using System.Collections.Generic;

[FGUIViewAttribute(UIViewEnum.RankTopFiveView, typeof(RankTopFiveView))]
public class RankTopFiveView : BaseView
{
    public override string PackageName => G_RankTopFiveView.PACKAGE_NAME;
    public override string ComponentName => G_RankTopFiveView.COMPONENT_NAME;

    private TableView<RankTopFiveItem> list_rank;

    private G_RankTopFiveView mRoot;

    private RankProgressDetailResp rankListResp;

    private int _progressId;


    public RankTopFiveView()
    {
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.RankTopFiveView, false);
    }


    protected override Dictionary<EEventType, OnEventLister> EventList => new Dictionary<EEventType, OnEventLister>()
    {
        { EEventType.RankTopFiveEvent, OnRankTopFiveEvent },
    };

    private void OnRankTopFiveEvent(EventSysArgsBase argsBase)
    {
        if (argsBase is EventSysArgs<RankProgressDetailResp> resp)
        {
            rankListResp = resp.args1;
            if (rankListResp.progressId == _progressId)
            {
                ShowView();
            }
        }
    }
    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mRoot = contentPane as G_RankTopFiveView;
        closeButton = mRoot.Button_click;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        list_rank = new (mRoot.List_banner);
    }

    private void ShowView()
    {
        list_rank.setDatas(rankListResp.progressLists);
    }

    protected override void OnShown()
    {
        base.OnShown();
        if (showArgs is int progressId)
        {
            _progressId = progressId;
            RankControl.Instance.ReqRankProgressTopFive(_progressId);
        }
    }
}



