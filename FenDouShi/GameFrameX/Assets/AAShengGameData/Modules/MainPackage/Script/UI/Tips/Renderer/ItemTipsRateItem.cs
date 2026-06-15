using Tips;

public class ItemTipsRateItem : BaseRender
{
    public new G_ItemTipsRateItem mRoot
    {
        get { return (G_ItemTipsRateItem)base.mRoot; }
    }

    public override string mPackageName => G_ItemTipsRateItem.PACKAGE_NAME;
    public override string mComponentName => G_ItemTipsRateItem.COMPONENT_NAME;
    public ItemTipsRateItem()
    {
    }

    protected override void dataChanged()
    {
        var data = (ItemTipsRateData)mData;
        mRoot.Text_name.text = Utility.Text.Format("{0}*{1}", data.Name, data.ItemCount);
        //mRoot.Text_rate.text = Utility.Text.Format("{0:P2}", data.Weight / data.WeightMax);
        mRoot.Text_rate.text = string.Format("{0:P2}", data.Weight / data.WeightMax);
    }
}

public class ItemTipsRateData
{
    public string Name;
    public float Weight;
    public float WeightMax;
    public int ItemId;
    public int ItemCount;
}


