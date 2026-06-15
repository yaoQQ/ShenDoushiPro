using GM;
using static GM.GmDefine;
public class GmCustomPageView : BaseRender
{
    public new G_GmCustomPageView mRoot
    {
        get { return (G_GmCustomPageView)base.mRoot; }
    }

    public override string mPackageName => G_GmCustomPageView.PACKAGE_NAME;
    public override string mComponentName => G_GmCustomPageView.COMPONENT_NAME;

    private TableView<GmClientItemRenderer> mTableView;

    protected override void onCreate()
    {
        mTableView = new TableView<GmClientItemRenderer>(mRoot.List_content);
        mTableView.setClickCallBack(onClickItem);
    }

    protected override void dataChanged()
    {
        var data = mData as GMTabItemData;
        mTableView.setDatas(data.clientGMs);
    }

    private void onClickItem(object data, int index)
    { 
        
    }
}

