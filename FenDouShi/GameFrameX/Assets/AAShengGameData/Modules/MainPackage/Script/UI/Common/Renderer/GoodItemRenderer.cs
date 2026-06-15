using common;
using System;

public class GoodItemRenderer : BaseRender
{
    public new G_CommonGoodItem mRoot
    {
        get { return (G_CommonGoodItem)base.mRoot; }
    }
    public override string mPackageName => G_CommonGoodItem.PACKAGE_NAME;
    public override string mComponentName => G_CommonGoodItem.COMPONENT_NAME;

    private bool mLoadFinish = false;

    protected override void onCreate()
    {
        mLoadFinish = true;
        Init();
    }

    protected override void dataChanged()
    {
        RefreshItemInfo();
    }

    private void RefreshItemInfo()
    {
        var data = (CommonItemData)mData;
        var quality = data.Quality;
        mRoot.Image_icon.url = data.IconUrl;
        var qualit_Res = ItemDefine.GetItemQualityByQuality(quality);
        mRoot.Image_quality.url = UIHelper.GetCommonUrl(qualit_Res);
        mRoot.Text_count.text = data.Count.ToString();
        mRoot.Imgae_fragment.visible = data.GetIsFragmentItem;
        var isLimitItem = data.GetExpireTime > 0;
        mRoot.group_time.visible = isLimitItem;
        var mItemSource = data.mItemSource;
        var isLock = false;
        if (data.ClientIsLock)
        {
            isLock = data.ClientIsLock;
        }
        else if (mItemSource == eItemSource.Bag)
        {
            isLock = data.GetIsLock;
        }
        mRoot.group_lock.visible = isLock;
        if (isLimitItem)
        {
            mRoot.Text_time.text = data.GetIsOutOfDate ? "已过期" : DateFormatUtil.FormatLeftTime2(data.GetExpireTime);
        }
        //SetIsSelect(data.GetIsSelect);
        SetIsShowItemCount(data.IsShowNum);
        RefreshRedPoint();
        mRoot.Image_up.visible = data.IsUp;
    }

    private void Init()
    {
        SetIsGetState(false);
        if (mRoot != null)
        {
            mRoot.redPoint.visible = false;
        }
    }

    private void RefreshRedPoint()
    {
        if (mRoot == null) return;
        var data = (CommonItemData)mData;
        if (data != null)
        {
            if (data.RedPointType != -1)
            {
                //红点系统管理
                return;
            }
        }
        mRoot.redPoint.visible = data.GetIsShowRedPoint;
    }

    /// <summary>
    /// 选中状态
    /// </summary>
    /// <param name="isShow"></param>
    public void SetIsSelect(bool isShow)
    {
        if (mLoadFinish)
            try
            {
                if (mRoot != null && mRoot.Image_select != null)
                {
                    mRoot.Image_select.visible = isShow;
                }
            }
            catch (Exception e)
            {
                Logger.PrintDebug("SetIsSelect Exception: " + e.Message);
            }
    }

    public override void SetSelectState(bool state)
    {
        SetIsSelect(state);
    }

    public void SetIsShowItemCount(bool isShow)
    {
        if (mRoot == null) return;
        mRoot.Image_countBg.visible = isShow;
        mRoot.Text_count.visible = isShow;
    }


    public void SetIsGetState(bool isShow)
    {
        //mRoot.group_gou.visible = isShow;
    }
}


