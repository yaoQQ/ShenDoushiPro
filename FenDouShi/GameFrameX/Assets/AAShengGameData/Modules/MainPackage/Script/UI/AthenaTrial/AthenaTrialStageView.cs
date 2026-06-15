using System.Collections.Generic;
using System.Linq;
using AthenaTrial;
using FairyGUI;

[FGUIViewAttribute(UIViewEnum.AthenaTrialStageView, typeof(AthenaTrialStageView))]
public class AthenaTrialStageView : BaseView
{
    public override string PackageName => G_AthenaTrialStageView.PACKAGE_NAME;
    public override string ComponentName => G_AthenaTrialStageView.COMPONENT_NAME;

    private int mType = 0;

    private List<TrialDegreeVo> stageCfg;

    protected override Dictionary<EEventType, OnEventLister> EventList => new Dictionary<EEventType, OnEventLister>()
    {
        { EEventType.AthenaTrialInfoUpdate,OnAthenaTrialEvent}
    };

    private void OnAthenaTrialEvent(EventSysArgsBase argsBase)
    {
        RefreshView();
    }

    private G_AthenaTrialStageView mRoot;

    private TableView<AthenaTrialStageItem> List_reward;

    public AthenaTrialStageView()
    {
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.AthenaTrialStageView, false);
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mRoot = contentPane as G_AthenaTrialStageView;
        closeButton = mRoot.closeButton;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        List_reward = new TableView<AthenaTrialStageItem>(mRoot.List_reward);
        modal = true;
    }

    protected override void OnShown()
    {
        base.OnShown();
        if (showArgs is int type)
        {
            stageCfg = null;
            mType = type;
            RefreshView();
            List_reward.ScrollTop();
        }
    }


    private void RefreshView()
    {
        if (mType <= 0) { return; }
        stageCfg ??= AthenaTrialControl.Instance.Model.GetStageCfgByType(mType).Values.OrderBy(s => s.Level).ToList();
        stageCfg = OnSort(stageCfg);
        List_reward.setDatas(stageCfg);
    }

    private List<TrialDegreeVo> OnSort(List<TrialDegreeVo> degreeVos)
    {
        if (degreeVos?.Count <= 0)
        {
            return degreeVos;
        }
        return degreeVos.OrderBy(s => AthenaTrialControl.Instance.Model.GetIsDrawStageReward(s.Id)).ThenBy(s => s.Id).ToList();
    }
}

