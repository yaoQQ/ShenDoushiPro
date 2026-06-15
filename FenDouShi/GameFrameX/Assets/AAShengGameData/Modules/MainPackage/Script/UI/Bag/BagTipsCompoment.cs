using Bag;
using FairyGUI;


public class BagTipsCompoment : BaseRender
{
    protected new G_BagTipsCompoment mRoot
    {
        get { return (G_BagTipsCompoment)base.mRoot; }
    }
    private TableView<ItemRender> rewardItemRender;

    public override string mPackageName => G_BagTipsCompoment.PACKAGE_NAME;

    public override string mComponentName => G_BagTipsCompoment.COMPONENT_NAME;

    private CommonItemData mCommonItemData;

    protected override void dataChanged()
    {
        RefreshView();
    }


    protected override void onCreate()
    {
        rewardItemRender = new TableView<ItemRender>(mRoot.List_reward);
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        base.OnButtonClick(clickedButton);
        var mItemId = mCommonItemData.ItemId;
        if (clickedButton == mRoot.Button_blue)
        {
            ItemUseHelper.UseItem(mCommonItemData);
        }
        else if (clickedButton == mRoot.Button_yellow)
        {
            TipsHelper.ShowGetWay(mItemId);
        }
    }

    public void RefreshView()
    {
        var itemData = (CommonItemData)mData;
        mCommonItemData = itemData;
        if (mCommonItemData.IsEmptyItem)
        {
            return;
        }
        mRoot.Text_desc.text = itemData.Desc;
        mRoot.Text_name.text = itemData.Name;
        mRoot.Text_num.text = Utility.Text.Format("拥有:{0}", itemData.Count);
        mRoot.Image_icon.url = itemData.IconUrl;
        var isLock = itemData.GetIsLock;
        mRoot.Text_limit.visible = isLock;
        if (isLock)
        {
            mRoot.Text_limit.text = itemData.GetLockStr;
        }
        var isLimitItem = itemData.GetExpireTime > 0;
        if (isLimitItem)
        {
            mRoot.Text_expireTime.text = itemData.GetIsOutOfDate ? "已过期" : Utility.Text.Format("有效期：{0}", DateFormatUtil.GetDateTimeStr(itemData.GetExpireTime));
        }
        else
        {
            mRoot.Text_expireTime.text = "";
        }
        mRoot.Imgae_fragment.visible = itemData.GetIsFragmentItem;
        var isBoxItem = itemData.GetIsBoxItem;
        var isUse = isBoxItem;
        var isShowGetWay = itemData.GetGetWay?.Count > 0;
        var rightStr = "预览";
        if (isBoxItem)
        {
            rightStr = "预览";
        }
        else if (isShowGetWay)
        {
            rightStr = "获取";
        }
        mRoot.Button_yellow.title = rightStr;
        var itemDatas = itemData.GetUseRewardDatas ?? new System.Collections.Generic.List<CommonItemData>();
        rewardItemRender.setDatas(itemDatas);
        var isShowRightBtn = true;
        mRoot.Button_yellow.visible = isShowRightBtn;
        mRoot.Button_blue.visible = isUse;
        mRoot.group_button.visible = isShowRightBtn || isUse;
    }
}
