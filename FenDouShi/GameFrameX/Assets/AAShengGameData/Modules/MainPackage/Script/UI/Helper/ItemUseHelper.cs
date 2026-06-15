


public static class ItemUseHelper
{
    public static void UseItem(CommonItemData itemData)
    {
        if (itemData == null || itemData.IsEmptyItem)
        {
            return;
        }
        if (!itemData.GetIsUseTypeItem) return;
        if (itemData.GetIsUseItem)
        {
            if (itemData.CountImt == 1)
            {
                BagControl.Instance.OnUseItemReq(itemData.UniqueId, itemData.ItemId, 1);
                return;
            }
        }
        var view = UIViewEnum.ItemUseView;
        if (itemData.GetIsOptionalItemGiftType)
        {
            view = UIViewEnum.ItemSelectRewardView;
        }
        //else if (itemData.GetIsRandomBoxGiftType ||itemData.GetIsAllGetBoxType)
        //{
        //    view = UIViewEnum.ItemBoxRandomTipsView;
        //}
        UIViewManager.Instance.Show(view, itemData);
    }
}
