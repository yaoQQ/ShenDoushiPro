using FairyGUI;
using msg.rank;
using Rank;
using System.Collections.Generic;

[FGUIViewAttribute(UIViewEnum.RankListView, typeof(RankListView))]
public class RankListView : BaseView
{
    public override string PackageName => G_RankListView.PACKAGE_NAME;
    public override string ComponentName => G_RankListView.COMPONENT_NAME;

    private TableView<RankListItem> list_rank;

    private G_RankListView mRoot;

    private RankListResp rankListResp;

    private string _rankTimer2 = "_rankTimer2";

    private string _rankListReqTimer = "_rankListReqTimer";


    private int _rankId;

    private RankListItem mRankListItem;


    public RankListView()
    {
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.RankListView, false);
    }

    protected override Dictionary<EEventType, OnEventLister> EventList => new Dictionary<EEventType, OnEventLister>()
    {
        { EEventType.RankListEvent, OnRankListEvent },
        { EEventType.RankEvent_RedPoint, OnRankEvent_RedPoint },
    };

    private void OnRankEvent_RedPoint(EventSysArgsBase argsBase)
    {
        RefreshRedPoint();
    }

    private void OnRankListEvent(EventSysArgsBase argsBase)
    {
        if (argsBase is EventSysArgs<RankListResp> resp)
        {
            rankListResp = resp.args1;
            ShowView();
        }
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mRoot = contentPane as G_RankListView;
        closeButton = mRoot.Button_close;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        list_rank = new TableView<RankListItem>(mRoot.List_rank);
        mRankListItem = BaseRender.Create<RankListItem>(mRoot.mRank);
    }

    private int _remainTime;
    private RankListData mRankData;

    private void ShowView()
    {
        var isNull = rankListResp?.Self == null || rankListResp?.Lists.Count <= 0;
        mRoot.root.visible = !isNull;
        mRoot.group_empty.visible = isNull;
        var showTime = false;
        mRoot.group_time.visible = showTime;
        if (!isNull)
        {
            var rankId = rankListResp.Id;
            var rankList = new List<RankListData>();
            var mCount = rankListResp.Lists.Count;
            for (var i = 0; i < mCount; i++)
            {
                var list = rankListResp.Lists[i];
                rankList.Add(new RankListData() { RankId = rankId, RankItem = list });
            }
            list_rank.setDatas(rankList);
            mRankData ??= new RankListData();
            mRankData.RankId = rankListResp.Id;
            mRankData.RankItem = rankListResp.Self;
            mRankListItem.setData(mRankData);
            mRankListItem.SetIsShowLike(false);
            var firstInfo = rankListResp.Lists[0];
            if (firstInfo != null)
            {
                var rankCfg = RankControl.Instance.Model.GetRankCfg(rankId);
                var imageInfo = rankCfg.Icon;
                var isTypeSmallNull = string.IsNullOrEmpty(imageInfo);
                mRoot.Image_typeSmall.visible = !isTypeSmallNull;
                if (!isTypeSmallNull)
                {
                    mRoot.Image_typeSmall.url = UIHelper.GetFguiUrl(PackageName, imageInfo);
                }
                var descStr = rankCfg.Scoretag;
                if (descStr.Contains("{score}"))
                {
                    descStr = descStr.Replace("{score}", "{0}");
                }
                mRoot.Text_fight.text = Utility.Text.Format(descStr, firstInfo.Score);
                var progressCfg = RankControl.Instance.Model.GetRankProgressListCfg(_rankId);
                var isHaveReward = progressCfg != null;
                mRoot.Button_reward.visible = isHaveReward;
                mRoot.group_info.y = isHaveReward ? 189 : 263;
                if (isHaveReward)
                {
                    mRoot.Button_reward.redPoint.visible = RankControl.Instance.Model.GetRankRedPointState(_rankId);
                }
            }
        }
        if (showTime)
        {
            _remainTime = 3600;
            GlobalTimeManager.Instance.timerController.AddTimer(_rankTimer2, 1000, _remainTime, CountDownLogic);
        }
    }

    private void RefreshRedPoint()
    {
        var progressCfg = RankControl.Instance.Model.GetRankProgressListCfg(_rankId);
        var isHaveReward = progressCfg != null;
        if (isHaveReward)
        {
            mRoot.Button_reward.redPoint.visible = RankControl.Instance.Model.GetRankRedPointState(_rankId);
        }
    }

    private void StopTimer()
    {
        StopRankTimer();
        StopRankTimer2();
    }

    private void StopRankTimer()
    {
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(_rankListReqTimer);
    }
    private void StopRankTimer2()
    {
        rankListResp = null;
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(_rankTimer2);
    }


    private void CountDownLogic(int count)
    {
        _remainTime--;
        if (_remainTime <= 0)
        {
            StopRankTimer2();
            mRoot.Text_time.text = "排行榜已结束";
            return;
        }
        mRoot.Text_time.text = Utility.Text.Format("排行榜将于{0}结束", DateFormatUtil.FormatLeftTime(_remainTime));
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == mRoot.Button_reward)
        {
            UIViewManager.Instance.Show(UIViewEnum.RankProgressView, _rankId);
        }
    }


    protected override void OnShown()
    {
        base.OnShown();
        StopTimer();
        if (showArgs is int rankId)
        {
            _rankId = rankId;
            var rankCfg = RankControl.Instance.Model.GetRankCfg(rankId);
            mRoot.Text_title.text = rankCfg?.Name;
            ReqRankMsg();
            GlobalTimeManager.Instance.timerController.AddTimer(_rankListReqTimer, 1000 * 60, -1, ReqRankMsg);
        }
    }


    protected override void OnHide()
    {
        base.OnHide();
        StopTimer();
    }

    private void ReqRankMsg(int count = 0)
    {
        if (_rankId <= 0)
        {
            StopRankTimer();
            return;
        }
        RankControl.Instance.ReqRankList(_rankId);
    }
}



