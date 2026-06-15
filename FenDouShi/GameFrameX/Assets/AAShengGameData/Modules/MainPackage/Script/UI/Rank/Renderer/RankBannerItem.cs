using Rank;

public class RankBannerItem : BaseRender
{
    public new G_RankBannerItem mRoot
    {
        get { return (G_RankBannerItem)base.mRoot; }
    }

    public override string mPackageName => G_RankBannerItem.PACKAGE_NAME;
    public override string mComponentName => G_RankBannerItem.COMPONENT_NAME;

    private RankHeadItem headItem;

    protected override void onCreate()
    {
        headItem = Create<RankHeadItem>(mRoot.headItem);
    }

    protected override void dataChanged()
    {
        var rankId = (int)mData;
        var rankCfg = RankControl.Instance.Model.GetRankCfg(rankId);
        var rankInfo = RankControl.Instance.Model.GetRankBaseInfoById(rankId);
        var isNull = rankInfo?.roleInfo == null;
        mRoot.group_info.visible = !isNull;
        mRoot.Text_empty.visible = isNull;
        if (!isNull)
        {
            mRoot.Text_name.text = rankInfo.roleInfo.Name;
            var descStr = rankCfg.Scoretag;
            if (descStr.Contains("{score}"))
            {
                descStr = descStr.Replace("{score}", "{0}");
            }
            mRoot.Text_fight.text = Utility.Text.Format(descStr, rankInfo.topScore);
            headItem.setData(rankInfo.roleInfo);
            headItem.SetIsShowLevel(false);
        }
        mRoot.Image_quality.url = UIHelper.GetFguiUrl(mPackageName, rankCfg.RankBg);
        mRoot.Image_type.url = UIHelper.GetFguiUrl("Rank", rankCfg.RankIcon);
        mRoot.Image_strType.url = UIHelper.GetFguiUrl(mPackageName, rankCfg.RankTitle);
        var imageInfo = rankCfg.Icon;
        var isTypeSmallNull = string.IsNullOrEmpty(imageInfo);
        mRoot.Image_typeSmall.visible = !isTypeSmallNull;
        if (!isTypeSmallNull)
        {
            mRoot.Image_typeSmall.url = UIHelper.GetCommonUrl(imageInfo);
        }
    }
}


