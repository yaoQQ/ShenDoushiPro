using FairyGUI;
using Rank;
using System.Collections.Generic;

public class RankProgressItem : BaseRender
{
    public new G_RankProgressItem mRoot
    {
        get { return (G_RankProgressItem)base.mRoot; }
    }


    public override string mPackageName => G_RankProgressItem.PACKAGE_NAME;
    public override string mComponentName => G_RankProgressItem.COMPONENT_NAME;

    private TableView<ItemRender> List_reward;

    private RankHeadItem mRankHeadItem;

    protected override void onCreate()
    {
        List_reward = new TableView<ItemRender>(mRoot.list_reward);
        List_reward.setClickCallBack(OnItemClick);
        mRankHeadItem = Create<RankHeadItem>(mRoot.headItem);
    }

    private void OnItemClick(object arg1, int arg2)
    {
        var data = mData as RankProgressVo;
        var rankId = data.RankId;
        var progressId = data.Id;
        var isCanDraw = RankControl.Instance.Model.GetRankProgressRedPointState(rankId, progressId);
        if (isCanDraw)
        {
            RankControl.Instance.ReqRankProgressReward(rankId, new int[] { progressId });
            return;
        }
        var itemData = (CommonItemData)arg1;
        if (itemData == null) return;
        TipsHelper.ShowTips(itemData.ItemId);
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == mRoot.Button_look)
        {
            var data = mData as RankProgressVo;
            UIViewManager.Instance.Show(UIViewEnum.RankTopFiveView, data.Id);
        }
    }

    protected override void dataChanged()
    {
        var data = mData as RankProgressVo;
        var rankId = data.RankId;
        var progressId = data.Id;
        var isChapter = rankId == 101;
        var descStr = string.Empty;
        if (isChapter)
        {
            ParseChapterStage(data.Value, out var chapter, out var stage);
            descStr = Utility.Text.Format("{0}\n[size=35]{1}-{2}[/size]", data.Desc, chapter, stage);
        }
        else
        {
            descStr = Utility.Text.Format("{0}\n[size=35]{1}[/size]", data.Desc, data.Value);
        }
        mRoot.Text_desc.text = descStr;
        var info = RankControl.Instance.Model.GetRankProgressInfo(data.RankId, data.Id);
        var isEmpty = info == null;
        mRoot.Text_empty.visible = isEmpty;
        mRoot.group_info.visible = !isEmpty;
        var isHaveReward = data.Rewards != null;
        var isDraw = RankControl.Instance.Model.GetIsDrawProgressId(progressId);
        var isCanDraw = false;
        if (!isEmpty)
        {
            isCanDraw = !isDraw;
            mRoot.Text_name.text = info.roleInfo.Name;
            mRankHeadItem.setData(info.roleInfo);
        }
        mRoot.group_get.visible = isDraw;
        mRoot.list_reward.visible = isHaveReward && !isDraw;
        if (isHaveReward)
        {
            var itemDatas = new List<CommonItemData>();
            for (var i = 0; i < data.Rewards.Count; i++)
            {
                var reward = data.Rewards[i];
                itemDatas.Add(new CommonItemData(reward[0], reward[1]) { ClientIsLock = isEmpty, ClientShowRedPoint = isCanDraw, IsEnableClick = false });
            }
            List_reward.setDatas(itemDatas);
        }
    }

    private void ParseChapterStage(int value, out int chapter, out int stage)
    {
        if (value >= 10)
        {
            chapter = value / 10;  // 章节数
            stage = value % 10;    // 关卡数
        }
        else
        {
            chapter = 0;
            stage = value;
        }
    }
}


