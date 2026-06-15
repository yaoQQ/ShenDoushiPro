using AthenaTrial;
using FairyGUI;
using System.Collections.Generic;

[FGUIViewAttribute(UIViewEnum.AthenaTrialSweepView, typeof(AthenaTrialSweepView))]
public class AthenaTrialSweepView : BaseView
{
    public override string PackageName => G_AthenaTrialSweepView.PACKAGE_NAME;
    public override string ComponentName => G_AthenaTrialSweepView.COMPONENT_NAME;

    private int mlevelId = 0;
    private int mType = 0;

    private G_AthenaTrialSweepView mRoot;

    private TableView<ItemRender> List_reward;

    private TrialLevelVo levelCfg;

    private RedComponent mRedPointFree;

    public AthenaTrialSweepView()
    {
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.AthenaTrialSweepView, false);
    }
    protected override Dictionary<EEventType, OnEventLister> EventList => new Dictionary<EEventType, OnEventLister>()
    {
        { EEventType.AthenaTrialInfoUpdate,OnAthenaTrialEvent}
    };

    private void OnAthenaTrialEvent(EventSysArgsBase argsBase)
    {
        RefreshView();
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mRoot = contentPane as G_AthenaTrialSweepView;
        closeButton = mRoot.closeButton;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        List_reward = new TableView<ItemRender>(mRoot.List_reward);
        modal = true;
        mRedPointFree = mRoot.Button_sure.TryGetComponent<RedComponent>();
        mRedPointFree.SetRedData(new RedData()
        {
            RedPointAlignment = ERedPointAlignment.RightTop,
            OffsetX = -20,
            OffsetY = 0,
        });
    }
    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == mRoot.Button_cancel)
        {
            HideView();
        }
        else if (clickedButton == mRoot.Button_sure)
        {
            var remianAllCnt = AthenaTrialControl.Instance.Model.GetSweepRemainAllCnt(mType);
            if (remianAllCnt <= 0)
            {
                CommonViewUtils.ShowTopTips("…®µ¥¥Œ ˝≤ª◊„");
                return;
            }
            var stage = AthenaTrialControl.Instance.Model.GetStageIdByType(levelCfg.TypeId);
            AthenaTrialControl.Instance.ReqTrialSweep(stage);
        }
    }

    protected override void OnShown()
    {
        base.OnShown();
        if (showArgs is int type)
        {
            mType = type;
            mlevelId = AthenaTrialControl.Instance.Model.GetPassMaxLevel(type);
            mlevelId += 1;
            RefreshView();
        }
    }

    private void RefreshView()
    {
        levelCfg = AthenaTrialControl.Instance.Model.GetLevelCfg(mType, mlevelId) ?? AthenaTrialControl.Instance.Model.GetLevelCfg(mType, mlevelId - 1);
        List_reward.setDatas(levelCfg.Reward);
        var remianAllCnt = AthenaTrialControl.Instance.Model.GetSweepRemainAllCnt(mType);
        var maxSweepCnt = AthenaTrialControl.Instance.Model.GetShowSweepMaxCnt(mType);
        var freeCnt = AthenaTrialControl.Instance.Model.GetFreeChallengeCnt(mType);
        mRoot.Text_desc.text = Utility.Text.Format(" £”‡¥Œ ˝:{0}/{1}", remianAllCnt, maxSweepCnt);
        mRedPointFree.SetRedState(freeCnt > 0);
        mRoot.Button_sure.text = freeCnt > 0 ? "√‚∑—…®µ¥" : "…®µ¥";
    }
}

