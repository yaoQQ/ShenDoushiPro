using common;
using FairyGUI;

public class GoodItemRenderer2 : BaseRender
{

    public new G_CommonGoodItem2 mRoot
    {
        get { return (G_CommonGoodItem2)base.mRoot; }
    }
    public override string mPackageName => G_CommonGoodItem2.PACKAGE_NAME;
    public override string mComponentName => G_CommonGoodItem2.COMPONENT_NAME;

    private GoodItemRenderer mGoodItemRender = null;


    protected override void onCreate()
    {
        mGoodItemRender = Create<GoodItemRenderer>(mRoot.commonGoodItem);
    }

    protected override void dataChanged()
    {
        var data = (CommonItemData)mData;
        mGoodItemRender.setData(data);
        mRoot.Text_name.text = data.NameColorStr;
    }
}
