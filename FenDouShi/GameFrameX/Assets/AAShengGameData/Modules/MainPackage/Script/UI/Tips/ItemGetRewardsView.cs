using common;
using FairyGUI;
using System.Collections.Generic;
using Tips;


[FGUIViewAttribute(UIViewEnum.ItemGetRewardsView, typeof(ItemGetRewardsView))]
public class ItemGetRewardsView : BaseView
{
    public override string PackageName => G_ItemGetRewardView.PACKAGE_NAME;
    public override string ComponentName => G_ItemGetRewardView.COMPONENT_NAME;

    private G_ItemGetRewardView mainView;

    private List<CommonItemData> mItemDatas;

    private TableView<ItemRender> mItemRender;


    public ItemGetRewardsView()
    {
        setViewAttribute(UIViewLayerType.Alert_box, UIViewEnum.ItemGetRewardsView, false);
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mainView = contentPane as G_ItemGetRewardView;
        closeButton = mainView.viewMask;
        mainView.list_reward.defaultItem = G_ItemRender.URL;
        mItemRender = new TableView<ItemRender>(mainView.list_reward);
    }

    protected override void OnShown()
    {
        base.OnShown();
        if (showArgs is List<CommonItemData> itemDatas)
        {
            mItemDatas = itemDatas;
        }
        TryShowView();
    }

    private void TryShowView()
    {
        var mCount = mItemDatas?.Count ?? 0;
        mItemRender.SetAlign(mCount >= 5 ? AlignType.Left : AlignType.Center);
        mItemDatas.ForEach(itemData => { itemData.mItemSource = eItemSource.RewardShow; });
        mItemRender.setDatas(mItemDatas);
    }

    protected override void DoShowAnimation() => OnShown();

    protected override void DoHideAnimation() => this.HideWindowImmediately();
}

