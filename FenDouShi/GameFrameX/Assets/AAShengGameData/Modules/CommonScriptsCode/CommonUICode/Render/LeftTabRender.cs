
using common;
using FairyGUI;

/// <summary>
/// 左侧Tab渲染器
/// </summary>
public class LeftTabRender : BaseRender
{
    protected new G_LeftTabRender mRoot
    {
        get { return (G_LeftTabRender)base.mRoot; }
    }

    public override string mPackageName => G_LeftTabRender.PACKAGE_NAME;
    public override string mComponentName => G_LeftTabRender.COMPONENT_NAME;

    /// <summary>
    /// 红点组件
    /// </summary>
    private RedComponent mRedComponent;

    protected override void onCreate()
    {
        mRedComponent = mRoot.AddComponent<RedComponent>();
        var redData = new RedData();
        redData.OffsetX = -15;
        redData.OffsetY = 0;
        mRedComponent.SetRedData(redData);
    }

    protected override void dataChanged()
    {
        // 数据变化时刷新左侧Tab
        LeftTabData itemData = (LeftTabData)mData;
        mRoot.n6.text = itemData.tabName;
        mRoot.tabIcon.url = itemData.icon;
        //设置红点类型
        mRedComponent.SetRedType(itemData.redType);
    }
}