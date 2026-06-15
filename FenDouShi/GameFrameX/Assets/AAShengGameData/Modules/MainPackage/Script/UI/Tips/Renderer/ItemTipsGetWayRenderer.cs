using Tips;

public class ItemTipsGetWayRenderer : BaseRender
{
    public new G_ItemGetWayItem mRoot
    {
        get { return (G_ItemGetWayItem)base.mRoot; }
    }

    public override string mPackageName => G_ItemGetWayItem.PACKAGE_NAME;
    public override string mComponentName => G_ItemGetWayItem.COMPONENT_NAME;
    public ItemTipsGetWayRenderer()
    {
    }

    protected override void dataChanged()
    {
        var data = (ItemTipsGetWayData)mData;
        if (data.type == eGetWayType.System)
        {
            var id = data.id;
            var jumpCfg = BagControl.Instance.Model.GetJumpCfgById(id);
            var isOpen = SystemOpenControl.Instance.Model.GetIsSystemOpen(id);
            mRoot.Text_desc.visible = !isOpen;
            mRoot.height = isOpen ? 52f : 91f;
            mRoot.Text_desc.visible = !isOpen;
            mRoot.Text_name.text = jumpCfg.Name;
            if (!isOpen)
            {
                mRoot.Text_desc.text = SystemOpenControl.Instance.Model.GetSystemOpenTipDes(jumpCfg.FunOpen);
            }
        }
    }
}

public enum eGetWayType
{
    System = 1,
}

public class ItemTipsGetWayData : BaseItemData
{
    public eGetWayType type;
    public int id;
}


