using FairyGUI;

//普通
public class BagItemRenderer : BaseItemRenderer
{
    public override void Renderer(int index, GObject obj)
    {
        base.Renderer(index, obj);
    }
}

public class BagItemData: BaseItemData
{
    public CommonItemData itemData;
}


