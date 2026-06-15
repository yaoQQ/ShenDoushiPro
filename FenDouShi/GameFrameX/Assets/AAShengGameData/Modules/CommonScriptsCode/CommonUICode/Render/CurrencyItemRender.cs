using common;
using msg.common;
using UnityEngine;

public class CurrencyItemRender : BaseRender
{
    public new G_CurrencyItemRender mRoot
    {
        get { return (G_CurrencyItemRender)base.mRoot; }
    }

    public override string mPackageName => G_CurrencyItemRender.PACKAGE_NAME;
    public override string mComponentName => G_CurrencyItemRender.COMPONENT_NAME;

    private int itemId = 0;

    /// <summary>
    /// //节点创建子
    /// </summary>
    protected override void OnCreate()
    {

    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    protected override void InitEventLister()
    {
        On<ResourceInfo>(EEventType.EventItemCountChange, UpdateItemCount);
    }


    /// <summary>
    /// 刷新数据
    /// </summary>
    protected override void DataChanged()
    {
        // 数据变化时刷新
        var data = mData as CurrencySystemItemsLinkVo;
        if (data != null)
        {
            mRoot.itemIcon.url = ItemTools.GetItemIcon(data.ItemId);
            itemId = data.ItemId;
            //道具数量
            if (InterFaceManager.BagModel != null)
            {
                mRoot.number.text = InterFaceManager.BagModel.GetItemCountByItemId(itemId) + "";
            }
            //是否可以购买
            mRoot.addBtn.visible = data.Buy == 1;
        }
    }

    protected void UpdateItemCount(ResourceInfo itemInfo)
    {
        if (itemInfo.Id == itemId)
        {
            mRoot.number.text = itemInfo.Num.ToString();
        }
    }

}