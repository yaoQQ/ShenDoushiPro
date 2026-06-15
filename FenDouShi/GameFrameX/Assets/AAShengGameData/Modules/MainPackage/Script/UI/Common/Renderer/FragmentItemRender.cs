using common;

public class FragmentItemRender : BaseRender
{
    public new G_FragmentItemRender mRoot
    {
        get { return (G_FragmentItemRender)base.mRoot; }
    }

    public override string mPackageName => G_FragmentItemRender.PACKAGE_NAME;
    public override string mComponentName => G_FragmentItemRender.COMPONENT_NAME;
    private GoodItemRenderer mGoodItemRender;

    protected override void onCreate()
    {
        mGoodItemRender = GoodItemRenderer.Create<GoodItemRenderer>(mRoot.commonGoodItem);
    }

    protected override void dataChanged()
    {
        var data = (CommonItemData)mData;
        mGoodItemRender.setData(data);
        var maxCnt = 20;
        mRoot.Image_slider.fillAmount = data.Count / maxCnt;
        mRoot.Text_value.text = Utility.Text.Format("{0}/{1}", data.Count, maxCnt);
    }

    public override void SetSelectState(bool state)
    {
        mGoodItemRender?.SetIsSelect(state);
    }
}


