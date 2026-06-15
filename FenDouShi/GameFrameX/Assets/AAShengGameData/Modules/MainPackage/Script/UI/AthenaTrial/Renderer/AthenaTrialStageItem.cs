using AthenaTrial;
using FairyGUI;

public class AthenaTrialStageItem : BaseRender
{
    public override string mPackageName => G_AthenaTrialStageItem.PACKAGE_NAME;
    public override string mComponentName => G_AthenaTrialStageItem.COMPONENT_NAME;
    public G_AthenaTrialStageItem mRoot => base.mRoot as G_AthenaTrialStageItem;

    private TableView<ItemRender> List_reward;

    private RedComponent mRedComponent;

    protected override void OnCreate()
    {
        List_reward = new TableView<ItemRender>(mRoot.List_reward);
        mRedComponent = mRoot.Button_get.AddComponent<RedComponent>();
        var redData = new RedData();
        redData.OffsetX = -10;
        redData.OffsetY = 0;
        mRedComponent.SetRedData(redData);
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == mRoot.Button_get)
        {
            var data = mData as TrialDegreeVo;
            AthenaTrialControl.Instance.ReqTrialReceive(data.Id);
        }
        else if (clickedButton == mRoot.Button_jump)
        {
        }
    }


    protected override void DataChanged()
    {
        var data = mData as TrialDegreeVo;
        var level = data.Level;
        var mPassLevel = AthenaTrialControl.Instance.Model.GetPassMaxLevel(data.TypeId);
        var greenColor = "#119e7b";
        var redColor = "#F13E4D";
        var mColor = mPassLevel >= level ? greenColor : redColor;
        mRoot.Text_desc.text = Utility.Text.Format("通关试炼塔{0}层([color={1}]{2}[/color]/{3})", level, mColor, mPassLevel, level);
        List_reward.setDatas(data.DegreeReward);
        List_reward.ResizeToFit();
        var isDraw = AthenaTrialControl.Instance.Model.GetIsDrawStageReward(data.Id);
        var isCanDraw = AthenaTrialControl.Instance.Model.GetIsCanDrawStageReward(data.Id);
        mRoot.group_get.visible = isDraw;
        mRoot.Button_get.visible = isCanDraw;
        mRedComponent.SetRedState(isCanDraw);
        mRoot.Button_jump.visible = mPassLevel< level;
    }
}



