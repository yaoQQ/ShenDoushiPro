
using FairyGUI;
using System.Collections.Generic;
using System.Diagnostics;

public interface IItemRenderer
{
    void OnInit(GList gList);

    void OnInit(GList gList, int itemMaxCnt);

    void Renderer(int index, GObject obj);
    void SetData(List<BaseItemData> obj);
    void Destroy();
}

public abstract class BaseItemRenderer : BaseRender, IItemRenderer
{
    public override string mPackageName => string.Empty;

    public override string mComponentName => string.Empty;

    public List<BaseItemData> mItemDatas { get; private set; }

    public List<object> mItemDatas_object { get; private set; }

    public int index;
    public BaseItemData data { get; private set; }

    protected GObject mObject { get; set; }

    public GList mGlist { get; private set; }

    public object tempData { get; set; }

    public int ItemMaxCnt { get; private set; }

    protected BaseItemRenderer()
    {
    }

    public BaseItemRenderer(GList list)
    {
        OnInit(list);
    }

    public void OnInit(GList list)
    {
        mGlist = list;
        mGlist.itemRenderer = Renderer;
        this.ItemMaxCnt = 0;
    }

    public virtual void OnInit(GList list, int itemMaxCnt)
    {
        mGlist = list;
        mGlist.itemRenderer = Renderer;
        this.ItemMaxCnt = itemMaxCnt;
    }

    public virtual void Renderer(int index, GObject obj)
    {
        mObject = obj;
        this.index = index;
        if (mItemDatas == null) return;
        data = mItemDatas[index];
        obj.data = data;
    }

    public virtual void SetData(List<BaseItemData> itemDatas)
    {
        mItemDatas = itemDatas;
        RefrshListNum();
    }

    public virtual void SetData(BaseItemData itemDatas)
    {
        mItemDatas ??= new List<BaseItemData>();
        if (mItemDatas.Count == 0)
        {
            mItemDatas.Add(itemDatas);
        }
        else
        {
            mItemDatas[0] = itemDatas;
        }
        RefrshListNum();
    }

    public virtual void SetData<T>(List<T> itemDatas)
    {
        if (typeof(BaseItemData).IsAssignableFrom(typeof(T)))
        {
            mItemDatas = UIHelper.TransformDatas(itemDatas);
        }
        else
        {
            SetData_obj(itemDatas);
        }
        RefrshListNum();
    }

    private void SetData_obj<T>(List<T> itemDatas)
    {
        if (mItemDatas_object == null) mItemDatas_object = new List<object>();
        else mItemDatas_object.Clear();
        foreach (var item in itemDatas)
        {
            mItemDatas_object.Add(item);
        }
    }

    public T GetData<T>(int index)
    {
        if (mItemDatas != null)
        {
            dynamic item = GetData_inner<BaseItemData>(index);
            return item;
        }
        if (mItemDatas_object != null)
        {
            dynamic item = mItemDatas_object[index];
            return item;
        }
        Logger.PrintError(Utility.Text.Format("transform mData error {0} ", new StackTrace()));
        return default;
    }

    protected T GetItem<T>() where T : GComponent
    {
        return mObject as T;
    }

    private BaseItemData GetData_inner<BaseItemData>(int index)
    {
        var data = mItemDatas?[index];
#if UNITY_EDITOR
        if (data == null)
        {
            Logger.PrintError(Utility.Text.Format("mItemDatas is Null {0} ", new StackTrace()));
        }
#endif
        if (data is BaseItemData itemData)
        {
            return itemData;
        }
        Logger.PrintError(Utility.Text.Format("transform mData error {0} ", new StackTrace()));
        return default;
    }

    public void ForceRefresh()
    {
        RefrshListNum();
    }

    private void RefrshListNum()
    {
        if (mGlist == null) return;
        if (ItemMaxCnt > 0)
        {
            mGlist.numItems = ItemMaxCnt;
        }
        else if (mItemDatas != null)
        {
            mGlist.numItems = mItemDatas.Count;
        }
        else if (mItemDatas_object != null)
        {
            mGlist.numItems = mItemDatas_object.Count;
        }
    }


    public virtual void Destroy()
    {
        Logger.PrintLog("未实现  OnDestroy");
    }
}

//渲染器数据基类
public abstract class BaseItemData
{
}






