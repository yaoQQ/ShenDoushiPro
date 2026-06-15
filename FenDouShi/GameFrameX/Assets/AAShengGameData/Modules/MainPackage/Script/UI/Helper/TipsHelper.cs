public static class TipsHelper
{
    private static TipsOpenParam mTipsOpenParam;

    private static TipsOpenParam GetTipsOpenParam()
    {
        mTipsOpenParam ??= new TipsOpenParam();
        mTipsOpenParam.IsShowGetWay = false;
        return mTipsOpenParam;
    }

    private static CommonItemData tempCommonItem;

    private static void ShowTipsInner(TipsOpenParam param)
    {
        var view  = UIViewEnum.ItemTipsView;
        tempCommonItem ??= new CommonItemData(param.ItemId);
        tempCommonItem.SetItemId(param.ItemId);
        //if (!param.IsShowGetWay)
        //{
            if (tempCommonItem.GetIsBoxItem)
            {
                view = UIViewEnum.ItemBoxRandomTipsView;
            }
        //}
        UIViewManager.Instance.Show(view, param);
        Logger.PrintLog("点击Tips" + param.ItemId);
    }

    /// <summary>
    /// 展示Tips
    /// </summary>
    /// <param name="ItemId"></param>
    public static void ShowTips(int ItemId)
    {
        mTipsOpenParam = GetTipsOpenParam();
        mTipsOpenParam.ItemId = ItemId;
        mTipsOpenParam.IsShowGetWay = false;
        ShowTipsInner(mTipsOpenParam);
    }

    public static void ShowGetWay(int ItemId)
    {
        mTipsOpenParam = GetTipsOpenParam();
        mTipsOpenParam.ItemId = ItemId;
        mTipsOpenParam.IsShowGetWay = true;
        ShowTipsInner(mTipsOpenParam);
    }
}

public class TipsOpenParam
{
    public int ItemId;
    public bool IsShowGetWay;
}
