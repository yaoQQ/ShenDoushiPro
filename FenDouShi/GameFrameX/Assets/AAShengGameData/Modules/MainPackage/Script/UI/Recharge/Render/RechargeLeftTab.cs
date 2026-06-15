//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------

using common;
using Recharge;

public class RechargeLeftTab : BaseRender
{
    public new G_RechargeLeftTab mRoot
    {
        get { return (G_RechargeLeftTab)base.mRoot; }
    }

    public override string mPackageName => G_RechargeLeftTab.PACKAGE_NAME;
    public override string mComponentName => G_RechargeLeftTab.COMPONENT_NAME;

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
    }


    /// <summary>
    /// 刷新数据
    /// </summary>
    protected override void DataChanged()
    {
        // 数据变化时刷新
        var data = mData as LeftTabData;
        if (data != null)
        {
            mRoot.tabname.text = data.tabName;
            mRoot.tabname2.text = data.tabName;
            mRoot.tabIcon.url = data.icon;
        }
    }

}