using Bag;

public class BagSelectionItem : BaseRender
{
    public new G_BagSelectionItem mRoot
    {
        get { return (G_BagSelectionItem)base.mRoot; }
    }

    public override string mPackageName => G_BagSelectionItem.PACKAGE_NAME;
    public override string mComponentName => G_BagSelectionItem.COMPONENT_NAME;


    protected override void dataChanged()
    {
        var data = (BagSelectionItemData)mData;
        var isImgNull = string.IsNullOrEmpty(data.IconUrl);
        mRoot.Image_icon.visible = !isImgNull;
        if (!isImgNull)
        {
            mRoot.Image_icon.url = data.IconUrl;
        }
        mRoot.Text_All.text = data.Desc;
    }
}
public class BagSelectionItemData
{
    public string IconUrl { get; set; }
    public string Desc { get; set; }

    public eBagSelectType Type;

    public BagSelectionItemData(string iconUrl, string Desc)
    {
        IconUrl = iconUrl;
        this.Desc = Desc;
    }
    public BagSelectionItemData()
    {

    }
}

public enum eBagSelectType
{

    All = -1,

    /// <summary>
    /// 非宝箱
    /// </summary>
    NonBox = 1,

    /// <summary>
    /// 宝箱
    /// </summary>
    Box = 2,
}




