using System.Collections.Generic;
using AthenaTrial;
using FairyGUI;

[FGUIViewAttribute(UIViewEnum.AthenaTrialChapterView, typeof(AthenaTrialChapterView))]
public class AthenaTrialChapterView : BaseView
{
    public override string PackageName => G_AthenaTrialChapterView.PACKAGE_NAME;
    public override string ComponentName => G_AthenaTrialChapterView.COMPONENT_NAME;


    private TopRenderData _mTopRenderData = new TopRenderData()
    {
        titleName = "风之试炼",
        helpId = 10000,
    };

    protected override TopRenderData mTopRenderData => _mTopRenderData;

    private int mType = 0;

    //暂时不写算法了emmm
    private List<int> sliderValueList = new List<int>() {
        0,
        6,
        14,
        21,

        32,
        40,
        50,


        58,
        66,
        75,
        100,
    };

    private List<AthenaTrialChapterProgressItemData> progressDatas = new()
    {
        new(){
            TowerIcon = "yadiannashilian_icon_ta01",
            starList = new List<int>(2){ 0,0},
            Chapter = 0,
        },
        new(){
            TowerIcon = "yadiannashilian_icon_ta01",
            starList = new List<int>(2){ 0,0},
            Chapter = 0,
        },
        new(){
            TowerIcon = "yadiannashilian_icon_ta01",
            starList = new List<int>(2){ 0,0},
            Chapter = 0,
        },
        new(){
            TowerIcon = "yadiannashilian_icon_ta02",
            starList = null,
            Chapter = 0,
        },
    };


    protected override Dictionary<EEventType, OnEventLister> EventList => new Dictionary<EEventType, OnEventLister>()
    {
        { EEventType.AthenaTrialInfoUpdate,OnAthenaTrialEvent}
    };

    private void OnAthenaTrialEvent(EventSysArgsBase argsBase)
    {
        RefreshView();
    }

    private G_AthenaTrialChapterView mRoot;

    private TableView<AthenaTrialChapterProgressItem> List_progress;


    private TableView<ItemRender> List_reward;
    private TrialLevelVo nextCfg;


    private RedComponent mRedPointStage;

    private RedComponent mRedPointFree;


    public AthenaTrialChapterView()
    {
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.AthenaTrialMainView, false);
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mRoot = contentPane as G_AthenaTrialChapterView;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        List_progress = new TableView<AthenaTrialChapterProgressItem>(mRoot.List_progress);
        List_reward = new TableView<ItemRender>(mRoot.List_reward);
        mRedPointStage = mRoot.Button_box.TryGetComponent<RedComponent>();
        mRedPointFree = mRoot.Button_sweep.TryGetComponent<RedComponent>();
        mRedPointStage.SetRedData(new RedData()
        {
            RedPointAlignment = ERedPointAlignment.RightTop,
            OffsetX = -30,
            OffsetY = 0,
        });
        mRedPointFree.SetRedData(new RedData()
        {
            RedPointAlignment = ERedPointAlignment.RightTop,
            OffsetX = -20,
            OffsetY = 0,
        });
    }
    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == mRoot.Button_buy)
        {
            AthenaTrialControl.Instance.OpenBuyChallengeView(mType);
        }
        else if (clickedButton == mRoot.Button_sweep)
        {
            UIViewManager.Instance.Show(UIViewEnum.AthenaTrialSweepView, mType);
        }
        else if (clickedButton == mRoot.Button_box)
        {
            UIViewManager.Instance.Show(UIViewEnum.AthenaTrialStageView, mType);
        }
        else if (clickedButton == mRoot.Button_look)
        {
            AthenaTrialControl.Instance.ReqTrialLevelStrategy(nextCfg.Id);
        }
        else if (clickedButton == mRoot.Button_shop)
        {
        }
        else if (clickedButton == mRoot.Button_rank)
        {
        }
        else if (clickedButton == mRoot.Button_fight)
        {
            BattleControl.Instance.ReqBattleInfo(nextCfg.Id, 6);
        }
    }

    protected override void OnShown()
    {
        base.OnShown();
        if (showArgs is int type)
        {
            mType = type;
            RefreshView();
        }
    }
    private void RefreshView()
    {
        if (mType <= 0) { return; }
        var typeCfg = AthenaTrialControl.Instance.Model.GetTypeCfgByTypeId(mType);
        var showLevel = AthenaTrialControl.Instance.Model.GetShowLevel(mType);
        _mTopRenderData.titleName = typeCfg.Name;
        mTopContentRender.setData(_mTopRenderData);
        var isShowRedStage = AthenaTrialControl.Instance.Model.GetIsShowCopyTypeRedPoint_stage(mType);
        //mRedPointStage.SetRedType(ERedPointType.AthenaTrial + mType);
        mRedPointStage.SetRedState(isShowRedStage);
        var isShowRedFree = AthenaTrialControl.Instance.Model.GetIsShowCopyTypeRedPoint_freeChallenge(mType);
        mRedPointFree.SetRedState(isShowRedFree);
        mRoot.Button_sweep.text = isShowRedFree ? "免费扫荡" : "扫荡";
        var index = showLevel;
        var maxCnt = progressDatas.Count;
        for (int i = 0; i < maxCnt; i++)
        {
            if (i != maxCnt - 1)
            {
                progressDatas[i].starList[0] = ++index;
                progressDatas[i].starList[1] = ++index;
            }
            progressDatas[i].Chapter = ++index;
            progressDatas[i].Type = mType;
        }
        List_progress.setDatas(progressDatas);
        var isOpenSweep = typeCfg.OpenSweep == 1;
        if (isOpenSweep)
        {
            var maxSweepCnt = AthenaTrialControl.Instance.Model.GetShowSweepMaxCnt(mType);
            var remianAllCnt = AthenaTrialControl.Instance.Model.GetSweepRemainAllCnt(mType);
            mRoot.Text_remainCnt.text = Utility.Text.Format("剩余次数:{0}/{1}", remianAllCnt, maxSweepCnt);
        }
        mRoot.group_time.visible = isOpenSweep;
        mRoot.Button_sweep.visible = isOpenSweep;
        var curLevel = AthenaTrialControl.Instance.Model.GetPassMaxLevel(mType);
        nextCfg = AthenaTrialControl.Instance.Model.GetLevelCfg(mType, curLevel + 1);
        var isFullLevel = nextCfg == null;
        var mSliderValue = 0;
        if (isFullLevel)
        {
            nextCfg = AthenaTrialControl.Instance.Model.GetLevelCfg(mType, curLevel);
            mSliderValue = 100;
        }
        else
        {
            mSliderValue = curLevel % 10;
        }
        mRoot.Image_point.visible = mSliderValue > 0;
        mRoot.Image_slider.fillAmount = (float)sliderValueList[mSliderValue] / 100;
        mRoot.Text_fight.text = nextCfg.Power.ToString();
        mRoot.Text_guanqia.text = Utility.Text.Format("第{0}关", nextCfg.Level);
        List_reward.setDatas(nextCfg.Reward);
    }
}

