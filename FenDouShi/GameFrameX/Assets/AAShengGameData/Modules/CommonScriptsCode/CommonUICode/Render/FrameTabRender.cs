//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------

using common;

public class FrameTabRender : BaseRender
{
    public new G_FrameTabRender mRoot
    {
        get { return (G_FrameTabRender)base.mRoot; }
    }

    public override string mPackageName => G_FrameTabRender.PACKAGE_NAME;
    public override string mComponentName => G_FrameTabRender.COMPONENT_NAME;

    /// <summary>
    /// //节点创建子
    /// </summary>
    protected override void onCreate()
    {

    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    protected override void onEventLister()
    {
    }


    /// <summary>
    /// 刷新数据
    /// </summary>
    protected override void dataChanged()
    {
        // 数据变化时刷新
        var data = mData as LeftTabData;
        mRoot.title.text = data.tabName;
        mRoot.icon.url = data.icon;
    }
}