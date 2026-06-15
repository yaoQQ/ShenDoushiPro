using Rank;

public class RankProgressTabItem : BaseRender

{
    public new G_RankProgressTabItem mRoot
    {
        get { return (G_RankProgressTabItem)base.mRoot; }
    }

    public override string mPackageName => G_RankProgressTabItem.PACKAGE_NAME;
    public override string mComponentName => G_RankProgressTabItem.COMPONENT_NAME;


    protected override void dataChanged()
    {
        var rankId = (int)mData;
        var rankCfg = RankControl.Instance.Model.GetRankCfg(rankId);
        if (rankCfg == null)
        {
            return;
        }
        var rankName = rankCfg.Ranktag;
        mRoot.Text_name.text = rankName;
        mRoot.Text_nameSelect.text = rankName;
        mRoot.redPoint.visible = RankControl.Instance.Model.GetRankRedPointState(rankId);
    }
}


