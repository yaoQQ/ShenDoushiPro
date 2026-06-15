using AthenaTrial;
using FairyGUI;
using msg.trial;

public class AthenaTrialPlayBackItem : BaseRender
{
    public override string mPackageName => G_AthenaTrialPlayBackItem.PACKAGE_NAME;
    public override string mComponentName => G_AthenaTrialPlayBackItem.COMPONENT_NAME;
    public G_AthenaTrialPlayBackItem mRoot => base.mRoot as G_AthenaTrialPlayBackItem;

    private RankHeadItem headItem;

    protected override void OnCreate()
    {
        headItem = Create<RankHeadItem>(mRoot.headItem);
    }

    protected override void DataChanged()
    {
        var data = mData as StrategyUserInfo;
        headItem.setData(data);
        var str1 = "最近通过";
        if (mIndex == 0)
        {
            str1 = "最速通过";
        }
        else if (mIndex == 1)
        {
            str1 = "最低战力";
        }
        var flagStr = "";
        var isFriend = false;
        var isGuild = false;
        mRoot.group_flag.visible = isFriend || isGuild;
        if (isFriend)
        {
            flagStr = "好友";
        }
        else if (isGuild)
        {

            flagStr = "公会成员";
        }
        mRoot.Text_flag.text = flagStr;
        mRoot.Text_name.text = Utility.Text.Format("{0}[size=25]({1})[/size]", data.Name, str1);
        mRoot.Text_fight.text = data.fightPower.ToString();
    }
}



