using Tips;

public class ItemConsumeRender : BaseRender
{
    public new G_ItemConsumeRender mRoot
    {
        get { return (G_ItemConsumeRender)base.mRoot; }
    }

    public override string mPackageName => G_ItemConsumeRender.PACKAGE_NAME;
    public override string mComponentName => G_ItemConsumeRender.COMPONENT_NAME;
    public ItemConsumeRender()
    {
    }

    protected override void dataChanged()
    {
        var data = (CommonItemData)mData;
        mRoot.Image_icon.url = data.IconUrl;
        mRoot.Text_consume.text = data.Count.ToString();
    }
}



