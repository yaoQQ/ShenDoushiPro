using msg.rank;
using msg.trial;
using Rank;

public class RankHeadItem : BaseRender
{
    public new G_RankHeadItem mRoot
    {
        get { return (G_RankHeadItem)base.mRoot; }
    }

    public override string mPackageName => G_RankHeadItem.PACKAGE_NAME;
    public override string mComponentName => G_RankBannerItem.COMPONENT_NAME;

    private bool isShowlevel = true;

    protected override void dataChanged()
    {
        var level = 0;
        if (mData is RankRole rankRole)
        {
            level = rankRole.Level;
        }
        else if (mData is StrategyUserInfo userInfo)
        {
            level = userInfo.Level;
        }
        mRoot.Text_level.text = Utility.Text.Format("{0}级", level);
        SetIsShowLevel(isShowlevel);
    }

    public void SetIsShowLevel(bool isShow)
    {
        isShowlevel = isShow;
        if (mRoot != null)
        {
            mRoot.group_level.visible = isShow;
        }
    }
}