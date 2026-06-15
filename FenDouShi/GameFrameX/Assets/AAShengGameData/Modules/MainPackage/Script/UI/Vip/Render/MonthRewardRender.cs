//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------

using common;
using vip;

public class MonthRewardRender : BaseRender
{
    public new G_MonthRewardRender mRoot
    {
        get { return (G_MonthRewardRender)base.mRoot; }
    }

    public override string mPackageName => G_MonthRewardRender.PACKAGE_NAME;
    public override string mComponentName => G_MonthRewardRender.COMPONENT_NAME;

    private ItemRender itemRender;

    /// <summary>
    /// //节点创建子
    /// </summary>
    protected override void OnCreate()
    {
        itemRender = Create<ItemRender>(mRoot.itemRender);
    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    protected override void InitEventLister()
    {
    }


    /// <summary>
    /// 刷新数据
    /// </summary>
    protected override void DataChanged()
    {
        // 数据变化时刷新
        itemRender.setData(mData);
    }

}