using AthenaTrial;
using FairyGUI;
using msg.trial;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.TextCore.Text;

[FGUIViewAttribute(UIViewEnum.AthenaTrialPlayBackView, typeof(AthenaTrialPlayBackView))]
public class AthenaTrialPlayBackView : BaseView
{
    public override string PackageName => G_AthenaTrialPlayBackView.PACKAGE_NAME;
    public override string ComponentName => G_AthenaTrialPlayBackView.COMPONENT_NAME;

    private G_AthenaTrialPlayBackView mRoot;

    private TableView<AthenaTrialPlayBackItem> List_note;

    private TrialLevelVo levelCfg;

    private StrategyUserInfo selectData;

    public AthenaTrialPlayBackView()
    {
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.AthenaTrialPlayBackView, false);
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mRoot = contentPane as G_AthenaTrialPlayBackView;
        closeButton = mRoot.closeButton;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        List_note = new TableView<AthenaTrialPlayBackItem>(mRoot.List_note);
        List_note.setClickCallBack(OnClickTab);
        modal = true;
    }

    private void OnClickTab(object arg1, int arg2)
    {
        var data = arg1 as StrategyUserInfo;
        selectData = data;
        RefreshContent();
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == mRoot.Button_vedio)
        {
            Logger.PrintDebug(selectData?.Vid.ToString());
        }
    }

    private List<StrategyUserInfo> recordInfos;


    protected override void OnShown()
    {
        base.OnShown();
        if (showArgs is List<StrategyUserInfo> strategyUserInfos)
        {
            recordInfos = OnSort(strategyUserInfos);
            selectData = recordInfos[0];
        }
        RefreshView();
        List_note.setSelectIndex(0);
        RefreshContent();
    }

    private List<StrategyUserInfo> OnSort(List<StrategyUserInfo> userInfoList)
    {
        if (userInfoList is not { Count: > 1 })
            return userInfoList?.ToList() ?? new List<StrategyUserInfo>();
        var maxPower = userInfoList.Max(c => c.fightPower);
        var minPower = userInfoList.Min(c => c.fightPower);

        var maxPowerChars = userInfoList.Where(c => c.fightPower == maxPower).ToList();
        var minPowerChars = userInfoList.Where(c => c.fightPower == minPower).ToList();
        var middleChars = userInfoList
            .Where(c => c.fightPower != maxPower && c.fightPower != minPower)
            .OrderByDescending(c => c.fightPower)
            .ToList();
        var result = new List<StrategyUserInfo>();
        result.AddRange(maxPowerChars);
        result.AddRange(minPowerChars);
        result.AddRange(middleChars);
        return result;
    }


    private void RefreshView()
    {
        if (recordInfos == null) return;
        List_note.setDatas(recordInfos);


    }

    private void RefreshContent()
    {
        mRoot.Text_fight.text = selectData.fightPower.ToString();
    }
}

