using Cysharp.Threading.Tasks;
using FairyGUI;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

/**
    渲染基础类
*/
public abstract class BaseRender
{
    protected object mData;

    //下标
    public int mIndex;

    // 组件FGUI节点
    public GComponent mRoot;
    public abstract string mPackageName { get; }      // FGUI包名
    public abstract string mComponentName { get; }    // FGUI窗口组件名字

    private Dictionary<EEventType, OnEventLister> eventList = new();
    // 全屏显示适配
    protected virtual bool mIsFullScreen { get; set; } = false;
    /// <summary>
    ///  视图事件列表
    /// </summary>
    private Dictionary<int, IViewEvent> mEventList = new Dictionary<int, IViewEvent>();

    public BaseRender()
    {

    }

    /// <summary>
    /// /// 创建新的渲染对象
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <returns></returns>
    public static async Task<T1> Create<T1>() where T1 : BaseRender, new()
    {
        var render = new T1();
        // 异步加载节点
        var te = await FGUIAssetManager.Instance.CreateObjectAsync(render.mPackageName, render.mComponentName);
        render.mRoot = (GComponent)te;
        //初始化
        render.init();
        return render;
    }

    /// <summary>
    /// 对象池加载测试
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="parent"></param>
    /// <param name="usePool"></param>
    /// <returns></returns>
    public static T1 CreateTest<T1>(GComponent parent) where T1 : BaseRender, new()
    {
        var render = new T1();
        var te = UIObjPool.Instance.GetObject(render.mPackageName, render.mComponentName);
        te.displayObject.cachedTransform.SetParent(parent.displayObject.cachedTransform, false);
        parent.AddChild(te);
        render.mRoot = (GComponent)te;
        //初始化
        render.init();
        return render;
    }


    /// <summary>
    /// /// 创建一个包含指定组件的渲染对象
    /// </summary>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public static T1 Create<T1>(GComponent component) where T1 : BaseRender, new()
    {
        var render = new T1();
        //设置节点
        render.mRoot = component;
        //初始化
        render.init();
        return render;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <returns></returns>
    private void init()
    {
        if (mIsFullScreen)
        {
            // 全屏显示适配
            mRoot.SetSize(GRoot.inst.width, GRoot.inst.height);
            mRoot.AddRelation(GRoot.inst, RelationType.Size);
        }
        // 注册当前节点生命周期事件
        // mRoot.onAddedToStage.Add(onShow);
        // mRoot.onRemovedFromStage.Add(onHide);
        var comp = mRoot.GetComponent<LiftCycleComponent>();
        if (comp == null)
        {
            comp = mRoot.AddComponent<LiftCycleComponent>();
        }
        comp.setBaseRender(this);

        TraverseAndAddListeners(mRoot);
        //节点创建成功
        onCreate();
        OnCreate();
        // 注册事件监听
        onEventLister();
        InitEventLister();
    }

    // 递归遍历子对象并添加事件
    void TraverseAndAddListeners(GComponent parent)
    {
        try
        {
            if (parent == null)
            {
                return;
            }
            // 遍历所有直接子对象
            for (int i = 0; i < parent.numChildren; i++)
            {
                GObject child = parent.GetChildAt(i);
                // 检查是否为按钮
                GButton button = child.asButton;
                if (button != null)
                {
                    // 添加点击事件监听器
                    button.onClick.Add(() => OnButtonClick(button));
                }
                // 递归遍历子对象的子对象
                TraverseAndAddListeners(child.asCom);
            }
        }
        catch (Exception ex)
        {
            Logger.PrintError("遍历子对象异常：" + ex.Message);
        }
    }

    /// <summary>
    /// 设置列表选中下标切换
    /// </summary>
    public void setSelectIndex(int index)
    {
        SetSelectState(index == mIndex);
    }

    /// <summary>
    /// 统一回调函数（接收按钮对象参数）
    /// </summary>
    /// <param name="clickedButton"></param>
    protected virtual void OnButtonClick(GButton clickedButton)
    {
        // Logger.PrintDebug("按钮被点击：" + clickedButton.name);
    }

    /// <summary>
    /// 设置数据
    /// </summary>
    /// <param name="datas"></param>
    public virtual void setData(object datas)
    {
        mData = datas;
        dataChanged();
        DataChanged();
    }

    public virtual void setData(object datas, int index)
    {
        mData = datas;
        mIndex = index;
        dataChanged();
        DataChanged();
    }

    /// <summary>
    /// 数据变化
    /// </summary>
    [Obsolete("请使用DataChanged")]
    protected virtual void dataChanged()
    {
    }
    protected virtual void DataChanged()
    { 
        
    }

    /// <summary>
    /// 节点销毁
    /// </summary>
    public void Dispose()
    {
        // 取消所有事件监听
        offAll();
        // 节点销毁生命周期回调
        OnDestroy();
    }

    /// <summary>
    /// //节点创建子类重写
    /// </summary>
    // 弃用
    [Obsolete("请使用OnCreate")]
    protected virtual void onCreate()
    {

    }

    protected virtual void OnCreate()
    {

    }

    /// <summary>
    /// 节点显示
    /// </summary>
    protected virtual void OnShow()
    {
    }

    /// <summary>
    /// 节点隐藏
    /// </summary>
    protected virtual void OnHide()
    {
        // 节点隐藏
    }

    /// <summary>
    /// 节点销毁 子类重写
    /// </summary>
    protected virtual void OnDestroy()
    {
    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    [Obsolete("请使用InitEventLister")]
    protected virtual void onEventLister()
    {
    }
    protected virtual void InitEventLister()
    {
    }


    public virtual void SetSelectState(bool state)
    {
    }

    /// <summary>
    /// 每帧更新
    /// </summary>
    public virtual void OnUpdate()
    {

    }
    /// <summary>
    /// 事件监听
    /// </summary>
    [Obsolete("请使用On")]
    protected void on(EEventType eventType, OnEventLister listener)
    {
        EventManager.Instance.AddEventLister(eventType, listener);
        //判断是否之前监听过，如果有就删除
        if (eventList.ContainsKey(eventType))
        {
            off(eventType, eventList[eventType]);
        }
        //添加到本地累计列表方便统一一起删除
        eventList.Add(eventType, listener);
    }

    /// <summary>
    /// 取消事件监听
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    [Obsolete("请使用Off")]
    protected void off(EEventType eventType, OnEventLister listener)
    {
        EventManager.Instance.RemoveEventLister(eventType, listener);
        eventList.Remove(eventType);
    }

    /// <summary>
    /// 取消所有事件监听
    /// </summary>
    protected void offAll()
    {
        if (eventList != null && eventList.Count > 0)
        {
            // 取消监听事件
            foreach (var i in eventList)
            {
                EventManager.Instance.RemoveEventLister(i.Key, i.Value);
            }
        }

        foreach (var item in mEventList)
        {
            Off(item.Key);
        }
        mEventList.Clear();
    }

    /// <summary>
    /// 添加一个事件监听
    /// </summary>
    /// <typeparam name="T1"></typeparam> 回调类型
    /// <param name="eventType"></param>
    /// <param name="handler"></param>
    protected void On(EEventType eventType, Action handler)
    {
        var ev = new ViewEventData();
        ev.CallBack = handler;
        EventManager.Instance.AddEventLister(eventType, ev.onHandler);
        mEventList.Add((int)eventType, ev);
    }

    /// <summary>
    /// 添加一个事件监听
    /// </summary>
    /// <typeparam name="T1"></typeparam> 回调类型
    /// <param name="eventType"></param>
    /// <param name="handler"></param>
    protected void On<T1>(EEventType eventType, Action<T1> handler)
    {
        var ev = new ViewEventData<T1>();
        ev.CallBack = handler;
        EventManager.Instance.AddEventLister(eventType, ev.onHandler);
        mEventList.Add((int)eventType, ev);
    }

    /// <summary>
    /// 添加一个事件监听
    /// </summary>
    /// <typeparam name="T1"></typeparam> 回调类型
    /// <param name="eventType"></param>
    /// <param name="handler"></param>
    protected void On<T1>(int eventType, Action<T1> handler)
    {
        var ev = new ViewEventData<T1>();
        ev.CallBack = handler;
        EventManager.Instance.AddEventLister(eventType, ev.onHandler);
        mEventList.Add(eventType, ev);
    }

    /// <summary>
    /// 移除一个事件监听
    /// </summary>
    /// <param name="eventType"></param>
    protected void Off(EEventType eventType)
    {
        if (mEventList.TryGetValue((int)eventType, out IViewEvent ev))
        {
            EventManager.Instance.RemoveEventLister(eventType, ev.onHandler);
            mEventList.Remove((int)eventType);
        }
    }

    protected void Off(int eventType)
    {
        if (mEventList.TryGetValue(eventType, out IViewEvent ev))
        {
            EventManager.Instance.RemoveEventLister(eventType, ev.onHandler);
            mEventList.Remove((int)eventType);
        }
    }

    public void Hide()
    {
        if (mRoot != null)
        {
            mRoot.visible = false;
        }
    }
    public void Show()
    {
        if (mRoot != null)
        {
            mRoot.visible = true;
        }
    }

    /// <summary>
    /// 节点被显示调用
    /// </summary>
    public void OnEnable()
    {
        OnShow();
    }

    /// <summary>
    /// 生命周期隐藏回调
    /// </summary>
    public void OnDisable()
    {
        OnHide();
    }

    /// <summary>
    /// 生命周期回调
    /// </summary>
    public void Update()
    {
        OnUpdate();
    }


}