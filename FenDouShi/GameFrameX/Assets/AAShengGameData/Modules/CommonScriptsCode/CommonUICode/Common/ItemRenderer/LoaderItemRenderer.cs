
using System;
using FairyGUI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;


public interface ILoaderItemRenderer
{
    LoaderItemRenderer CreateRender(Type renderer);
    void Init();
    void Load(GComponent parent);
    void LoadFinish(GObject obj);
    void OnCreate();
    void SetData(BaseItemData data);
    void OnDestroy();
}

public abstract class LoaderItemRenderer : BaseItemRenderer, ILoaderItemRenderer
{
    public virtual LoaderItemRenderer mItemRenderer { get; private set; } //渲染器

    public GComponent mRootNode { get; private set; }

    private bool needRefresh = true;

    public bool isLoaded { get; private set; }


    protected LoaderItemRenderer()
    {
    }

    protected LoaderItemRenderer(GList list)
    {
        base.OnInit(list);
    }

    public virtual LoaderItemRenderer CreateRender(Type renderer)
    {
        isLoaded = false;
        needRefresh = false;
        mItemRenderer?.OnDestroy();
        mItemRenderer = Activator.CreateInstance(renderer) as LoaderItemRenderer;
#if UNITY_EDITOR
        UnityEngine.Debug.Assert(mItemRenderer != null, Utility.Text.Format("$load类型必须是{0},当前为{1}", nameof(LoaderItemRenderer), renderer.Name));
#endif
        Init();
        return mItemRenderer;
    }

    public virtual void Init()
    {
    }

    private void HideOtherRender()
    {
        //TODO隐藏或者对象池 先暴力回收了
        mItemRenderer?.mRootNode?.RemoveChildren();
    }

    /// <summary>
    /// 有对象池的Load
    /// </summary>
    /// <param name="parent"></param>
    public void Load(GComponent parent)
    {
        HideOtherRender();
        mRootNode = parent;
        LoadUI(mPackageName, mComponentName);
    }

    public async UniTask LoadAsync(GComponent parent)
    {
        HideOtherRender();
        mRootNode = parent;
        await LoadUIAsync(mPackageName, mComponentName);
    }

    public virtual void LoadFinish(GObject obj)
    {
        Logger.PrintLog("LoadFinish");
        OnCreate();
        RefreshItem();
    }

    public virtual void OnCreate()
    {
    }

    public override void SetData(List<BaseItemData> itemDatas)
    {
        base.SetData(itemDatas);
        RefreshItem();
    }

    private void RefreshItem()
    {
        if (!isLoaded)
        {
            needRefresh = true;
        }
        else if (mItemDatas != null)
        {
            if (mObject == null) return;
            Renderer(0, mObject);
            needRefresh = false;
        }
    }

    public override void SetData(BaseItemData itemDatas)
    {
        base.SetData(itemDatas);
        RefreshItem();
    }
    public void OnDestroy()
    {
        base.Destroy();
    }

    private void LoadUI(string packageName, string viewName)
    {
        var op = UIObjPool.Instance.GetObject(packageName, viewName);
        op.displayObject.cachedTransform.SetParent(mRootNode.displayObject.cachedTransform, false);
        InnerLoadUIFinish(viewName, op);
    }

    private async UniTask LoadUIAsync(string packageName, string viewName)
    {
        var op = await FGUIAssetManager.Instance.CreateObjectAsync(packageName, viewName);
        InnerLoadUIFinish(viewName, op);
    }

    private void InnerLoadUIFinish(string viewName, GObject op)
    {
        if (op == null)
        {
            return;
        }
        op.name = viewName;
        mObject = op;
        if (op is { asCom: not null })
        {
            mRootNode.AddChild(mObject);
        }
        isLoaded = true;
        LoadFinish(mObject);
    }
}






