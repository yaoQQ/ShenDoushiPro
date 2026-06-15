using FairyGUI;
using msg.rank;
using Rank;

public class RankListItem : BaseRender
{

    public new G_RankListItem mRoot
    {
        get { return (G_RankListItem)base.mRoot; }
    }

    public override string mPackageName => G_RankListItem.PACKAGE_NAME;
    public override string mComponentName => G_RankListItem.COMPONENT_NAME;

    private RankHeadItem headItem;

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == mRoot.Button_like)
        {
            var data = mData as RankListData;
            RankControl.Instance.ReqRankLike(data.RankId, data.RankItem.roleInfo.roleId);
        }
    }

    protected override void onCreate()
    {
        headItem = Create<RankHeadItem>(mRoot.headItem);
        SetIsShowLike(true);
    }

    protected override void dataChanged()
    {
        var tempData = mData as RankListData;
        var data = tempData.RankItem;
        var rank = data.Rank;
        var showRankImg = rank is > 0 and <= 3;
        mRoot.Image_rank.visible = showRankImg;
        var rankStr = rank <= 0 ? "[size=27]未上榜[/size]" : rank.ToString();
        mRoot.Image_rankbg.visible = rank > 0 && !showRankImg;
        if (showRankImg)
        {
            mRoot.Image_rank.url = RankControl.Instance.Model.GetImageRankUrl(rank);
        }
        mRoot.Text_rank.text = showRankImg ? string.Empty : rankStr;
        var isLike = RankControl.Instance.Model.GetIsLike(tempData.RankId, data.Key);
        var res = isLike ? "paihangbang_icon_dianzan" : "paihangbang_icon_weidianzan";
        mRoot.Button_like.icon = UIHelper.GetFguiUrl(mPackageName, res);
        headItem.setData(data.roleInfo);
        mRoot.Text_name.text = data.roleInfo.Name;
        mRoot.Text_likeNum.text = data.Like.ToString();
        mRoot.Text_fight.text = data.Score.ToString();
    }

    public void SetIsShowLike(bool isShow)
    {
        mRoot.group_like.visible = isShow;
    }

}

public class RankListData
{
    public RankItem RankItem;
    public int RankId;
}


