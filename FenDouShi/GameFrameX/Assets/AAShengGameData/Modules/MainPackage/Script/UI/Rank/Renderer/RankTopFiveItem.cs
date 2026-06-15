using msg.rank;
using Rank;
using static iConGLoader;

public class RankTopFiveItem : BaseRender
{
    public new G_RankTopFiveItem mRoot
    {
        get { return (G_RankTopFiveItem)base.mRoot; }
    }

    public override string mPackageName => G_RankTopFiveItem.PACKAGE_NAME;
    public override string mComponentName => G_RankTopFiveItem.COMPONENT_NAME;

    private RankHeadItem headItem;

    protected override void onCreate()
    {
        headItem = Create<RankHeadItem>(mRoot.headItem);
    }

    protected override void dataChanged()
    {
        var data = mData as ProgressInfo;
        var rank = mIndex + 1;
        mRoot.Image_rank.visible = rank <= 3;
        if (rank <= 3)
        {
            mRoot.Image_rank.url = RankControl.Instance.Model.GetImageRankUrl(rank);
        }
        headItem.setData(data.roleInfo);
        mRoot.Text_rank.text = rank <= 3 ? string.Empty : rank.ToString();
        mRoot.Text_name.text = data.roleInfo.Name;
        mRoot.Text_desc.text = Utility.Text.Format("达成时间：[color=##199070]{0}[/color]", DateFormatUtil.GetDateTimeStr2(data.Time));
    }
}


