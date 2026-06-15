using Rank;

public class RankTabItem : BaseRender

{
    public new G_RankTabItem mRoot
    {
        get { return (G_RankTabItem)base.mRoot; }
    }

    public override string mPackageName => G_RankTabItem.PACKAGE_NAME;
    public override string mComponentName => G_RankTabItem.COMPONENT_NAME;

    public RedComponent mRedComponent;
    protected override void onCreate()
    {
        //mRedComponent =  mRoot.AddComponent<RedComponent>();
    }

    protected override void dataChanged()
    {
        var data = (RankTabItemData)mData;
        mRoot.Image_bg.url = data.ImageBgUrl;
        mRoot.Image_icon.url = data.ImageIconUrl;
        mRoot.Text_name.text = data.Name;
        //mRedComponent.SetRedType((int)ERedPointType.Rank + data.RankGroupCfg.Id);
        mRoot.redPoint.visible = RankControl.Instance.Model.GetTabRedPointState(data.RankGroupCfg.Id);
    }
}

public class RankTabItemData
{
    public string ImageBgUrl;

    public string ImageIconUrl;

    public string Name;

    public ERedPointType RedPoint;

    public RankGroupVo RankGroupCfg;
}


