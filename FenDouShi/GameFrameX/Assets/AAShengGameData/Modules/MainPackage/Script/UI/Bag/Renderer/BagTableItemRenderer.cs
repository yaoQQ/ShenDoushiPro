using Bag;
using FairyGUI;

public class BagTableItemRenderer: BaseItemRenderer
{

    public BagTableItemRenderer()
    {
    }

    public BagTableItemRenderer(GList list) : base(list)
    {
    }

    private G_BagTabItem item;

    private long remianTime;
    public override void Renderer(int index, GObject obj)
    {
        base.Renderer(index, obj);
        item = obj as G_BagTabItem;
        var data = GetData<BagTableItemData>(index);
        item.Text_name.text = data.name;
        item.Image_icon.url = data.icon;
        Clear();
        RefreshTime();
    }

    private void Clear()
    {
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(this);
    }

    private void RefreshTime()
    {
        var data = GetData<BagTableItemData>(index);
        var renderType = data.renderType;
        remianTime = BagControl.Instance.Model.GetItemMinRemianTimeByRenderType(renderType);
        item.group_time.visible = remianTime > 0;
        GlobalTimeManager.Instance.timerController.AddTimer(this, 1000, (int)remianTime, OnRefreshTime);
    }

    private void OnRefreshTime(int time)
    {
        remianTime--;
        if (remianTime > 0)
        {
            item.Text_time.text = DateFormatUtil.FormatLeftTime2(remianTime);
        }
    }
}

public class BagTableItemData : ChildTabItemData
{
    public string name;
    public string icon;
    public eItemRenderType renderType;
}

