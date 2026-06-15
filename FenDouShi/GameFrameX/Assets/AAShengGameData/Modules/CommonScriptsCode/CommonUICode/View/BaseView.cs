using FairyGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class LeftTabData
{
    public string tabName { get; set; } = "";
    public string icon { get; set; } = "";
    public object data { get; set; } = null;
    public int redType { get; set; } = 0;
    public bool select { get; set; } = false;
}

/// <summary>
/// 顶部渲染数据
/// </summary>
public class TopRenderData
{
    public string titleName { get; set; } = ""; //标题名称
    public string icon { get; set; } = ""; //图标
    public int helpId = 0; //帮助id
    public int currencyId = 0; //货币id
}

public abstract class BaseView : Window, IBaseView
{
    private UIViewLayerType viewLayerType = UIViewLayerType.None;//界面类型（层级）
    private UIViewEnum viewEnum = UIViewEnum.None;//--界面枚举

    public abstract string PackageName { get; }      // FGUI包名
    public abstract string ComponentName { get; }    // FGUI窗口组件名字
    public virtual bool IsPreload { get; } = false;     // 是否预加载界面

    RedPointUIRegister redPointRegister = new();    // 红点,TODO:类对象池   
    EventRegister eventRegister = new();            // 事件
    protected virtual Dictionary<EEventType, OnEventLister> EventList { get; }

    protected object showArgs;
    private bool isStackView = false;//--是否是栈界面
    private bool isLoading = false;//--是否正在加载，加载完成后设置为false
    private bool isLoaded = false;//--是否已经加载，加载完成后设置为true
    private bool isOpening = false;//--是否正在打开过程中（加载并显示）
    private bool isOpen = false;//--是否已经打开（显示）

    /// <summary>
    /// 左侧页签渲染
    /// </summary>
    /// 
    protected virtual LeftTabData[] mleftTabDatas { get; }
    protected LeftTabCompoment mleftTabCompoment;
    //顶部渲染数据
    protected virtual TopRenderData mTopRenderData { get; }
    //顶部渲染
    protected TopContentCompoment mTopContentRender;
    //是否全屏界面
    protected virtual bool IsFullScreen => false;
    protected virtual bool ClickMaskHide => false;

    public BaseView()
    {
        gameObjectName = ComponentName;
    }

    // 生命周期
    // 这个是FGUI的生命周期，用在加载资源结束之后，但太容易混淆概念了不允许继承
    protected sealed override void OnInit() { }

    // 初始化,注册到界面管理器
    // 开始加载资源
    public void StartLoad()
    {
        //Logger.PrintLog($"StartLoad:{mComponentName}");
        isLoading = true;
        isLoaded = false;

        // 异步加载
        FGUIAssetManager.Instance.CreateObjectAsync(PackageName, ComponentName, this);
        OnStartLoad();
    }

    protected virtual void OnStartLoad() { }

    private GComponent _background;
    // 结束加载资源
    public void FinishLoad(GComponent gComponent)
    {
        //Logger.PrintLog($"FinishLoad:{mComponentName}");
        // 创建透明背景这个后面要改一下先暂时用可以删除因为模态窗口目前有问题，先用这个点击关闭
        if (ClickMaskHide)
        {
            _background = new GComponent();
            _background.displayObject.touchable = true; // 允许接收点击
            _background.opaque = true; // 不透明
            _background.SetSize(GRoot.inst.width, GRoot.inst.height);
            _background.AddRelation(GRoot.inst, RelationType.Size);
            _background.onClick.Add(OnBackgroundClick);
            AddChild(_background);
        }

        SetHome(FGUILayoutManager.Instance.GetGRootLayer(this.viewLayerType));
        OnFinishLoad(gComponent);
        RegisterRedPoint();
        OnEventListener();
        TraverseAndAddListeners(gComponent);
        _ = loadOtherComponents();

    }

    private void OnBackgroundClick(EventContext context)
    {
        context.StopPropagation();
        HideView();
    }

    private async Task loadOtherComponents()
    {
        try
        {
            await loadleftAsync();
            await loadTopRenderAsync();
            OnCreate();
            isLoading = false;
            isLoaded = true;
        }
        catch (Exception ex)
        {
            Logger.PrintError($"loadOtherComponents:{ex}");
        }
    }

    protected virtual void OnCreate()
    {

    }

    private async Task loadleftAsync()
    {

        if (mleftTabDatas != null && mleftTabDatas.Length > 0)
        {
            mleftTabCompoment = await BaseRender.Create<LeftTabCompoment>();
            AddChild(mleftTabCompoment.mRoot);

            //设置左侧页签选择回调
            mleftTabCompoment.setSelectCallBack(selectLeftTab);
            mleftTabCompoment.setData(mleftTabDatas);
        }
    }


    /// <summary>
    /// 加载顶部渲染
    /// </summary>
    /// <returns></returns>
    private async Task loadTopRenderAsync()
    {
        if (mTopRenderData != null)
        {
            mTopContentRender = await BaseRender.Create<TopContentCompoment>();
            AddChild(mTopContentRender.mRoot);
            //设置左侧页签选择回调
            mTopContentRender.setData(mTopRenderData);
            mTopContentRender.setView(this);
        }

    }

    protected virtual void OnFinishLoad(GComponent gComponent) { }

    //（不提倡调用，只开放给UIViewManager）UIViewManager界面管理展示调用方法
    //展示界面请调用  UIViewManager.Instance.Show(UIViewEnum);
    public void Show(object msg)
    {
        showArgs = msg;

        isOpen = true;
        getViewGO().visible = true;
        FGUILayoutManager.Instance.AddChild(this.viewLayerType, this);
        //显示界面生命周期
        OnShow(msg);
        if (_background != null)
        {
            SetChildIndex(_background, 0);
        }
    }

    /// <summary>
    /// 显示界面生命周期
    /// </summary>
    /// <param name="msg"></param>
    protected virtual void OnShow(object msg)
    {

    }

    // 开始播放显示动画,不要动画则重写这个接口
    protected override void DoShowAnimation()
    {
        if (!IsFullScreen)
        {
            this.SetScale(0.1f, 0.1f);
            this.SetPivot(0.5f, 0.5f);
            this.TweenScale(new Vector2(1, 1), 0.3f).OnComplete(this.OnShown);
        }
        else
        {
            OnShown();
        }
    }

    // 显示时
    protected override void OnShown()
    {
        base.OnShown();
        // 注册事件
        GetEventRegister().RegisterListener();
    }

    // 开始播放隐藏动画,不要动画则重写这个接口
    protected override void DoHideAnimation()
    {
        //没有自定义层级之前默，认调用windows的HideImmediately
        //  this.HideImmediately();
        if (!IsFullScreen)
        {
            this.TweenScale(new Vector2(0.1f, 0.1f), 0.3f).OnComplete(HideWindowImmediately);
        }
        else
        {
            HideWindowImmediately();
        }
    }

    // 走隐藏流程
    public void HideView()
    {
        UIViewManager.Instance.Hide(viewEnum);
    }

    protected override void closeEventHandler(EventContext context)
    {
        HideView();
    }

    // 隐藏时
    protected override void OnHide()
    {
        base.OnHide();
        // 注销事件
        GetEventRegister().DeregisterListener();
    }

    //事件监听重写
    protected virtual void OnEventListener() { }

    // 递归遍历子对象并添加事件
    void TraverseAndAddListeners(GComponent parent)
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

    // 统一回调函数（接收按钮对象参数）
    protected virtual void OnButtonClick(GButton clickedButton)
    {
        Logger.PrintDebug("按钮被点击：" + clickedButton.name);
    }
    // 卸载资源,注销界面
    public void Destroy()
    {
        //Logger.PrintLog($"Destroy:{mComponentName}");
        // 注销红点
        redPointRegister.DeregisterAll();
        OnDestroy();
        isLoading = false;
        isLoaded = false;
        isOpen = false;
        UIViewManager.Instance.DestroyUIRes(PackageName, ComponentName, viewEnum);
        Dispose();
        foreach (var item in mEventList)
        {
            Off(item.Key);
        }
        mEventList.Clear();
    }

    protected virtual void OnDestroy() { }

    /// <summary>
    /// --获取界面类型（层级）
    /// </summary>
    /// <returns></returns>
    public UIViewLayerType getLayerType()
    {
        if (viewLayerType == UIViewLayerType.None)
        {
            Logger.PrintError($"{ComponentName} 没有设置 viewLayerType");
        }
        return viewLayerType;
    }

    //--获取界面枚举
    public UIViewEnum getViewEnum()
    {
        if (viewEnum == UIViewEnum.None)
        {
            Logger.PrintError($"{ComponentName} 没有设置 viewEnum");

        }
        return viewEnum;
    }

    //--获取是否是栈界面
    public bool getIsStackView()
    {
        return isStackView;
    }

    protected void setViewAttribute(UIViewLayerType _viewType, UIViewEnum _viewEnum, bool _isStackView)
    {
        viewLayerType = _viewType;
        viewEnum = _viewEnum;
        if (_isStackView)
        {
            isStackView = true;
        }
    }

    public Container getViewGO()
    {
        if (rootContainer != null)
        {
            return rootContainer;
        }
        Logger.PrintError($"{ComponentName} this.contentPane 不能为空");
        return null;
    }
    /// <summary>
    /// 添加自定义层级后，层级关闭一个窗口。之前播放完动画调用this.HideImmediately()。
    /// </summary>
    protected void HideWindowImmediately()
    {
        FGUILayoutManager.Instance.RemoveChild(viewLayerType, this);
        //之前调用模式如下
        //this.HideImmediately();
        // GRoot.inst.AdjustModalLayer();
    }
    public void ToHide()
    {
        UIViewManager.Instance.Hide(viewEnum);

    }
    //--获取是否正在加载
    public bool getIsLoading()
    {
        return isLoading;
    }

    //--获取是否已加载
    public bool getIsLoaded()
    {
        return isLoaded;
    }

    //--设置是否正在打开过程中（加载并显示）
    public void setOpening(bool value)
    {
        isOpening = value;
    }

    //--获取是否正在打开过程中（加载并显示）
    public bool getOpening()
    {
        return isOpening;
    }

    //--获取是否正在打开过程中（加载并显示）
    public bool getIsOpen()
    {
        return isOpen;
    }

    public void setIsOpen(bool value)
    {
        isOpen = value;
    }

    // 事件
    EventRegister GetEventRegister()
    {
        if (!eventRegister.init)
            eventRegister.Init(EventList);
        return eventRegister;
    }

    // 统一注册红点
    protected virtual void RegisterRedPoint() { }

    // 创建红点
    protected void RegisterRedPoint(ERedPointType redPointType, GComponent parent, ERedPointAlignment align = ERedPointAlignment.RightTop, float offsetX = 0, float offsetY = 0)
    {
        RegisterRedPoint((int)redPointType, parent, align, offsetX, offsetY);
    }

    protected void RegisterRedPoint(int redPointType, GComponent parent, ERedPointAlignment align = ERedPointAlignment.RightTop, float offsetX = 0, float offsetY = 0)
    {
        redPointRegister.Register(redPointType, parent, align, offsetX, offsetY);
    }

    // 绑定红点
    protected void AttachRedPoint(ERedPointType redPointType, GComponent parent)
    {
        AttachRedPoint((int)redPointType, parent);
    }

    protected void AttachRedPoint(int redPointType, GComponent parent)
    {
        redPointRegister.Attach(redPointType, parent);
    }



    // 移除红点
    protected void RemoveRedPoint(GComponent parent)
    {
        redPointRegister.Deregister(parent);
    }

    /// <summary>
    /// 左侧页签点击回调
    /// </summary>
    /// <param name="data"></param>
    protected virtual void selectLeftTab(LeftTabData data)
    {
        //TODO: 左侧页签点击回调子类重写
    }


    /// <summary>
    ///  视图事件列表
    /// </summary>
    private Dictionary<int, IViewEvent> mEventList = new Dictionary<int, IViewEvent>();


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
}

public interface IViewEvent
{
    void onHandler(EventSysArgsBase args);
}

public class ViewEventData<T> : IViewEvent
{

    public Action<T> CallBack { get; set; }

    public void onHandler(EventSysArgsBase eventArgs)
    {
        if (eventArgs != null && eventArgs is EventSysArgs<T> args)
        {
            CallBack.Invoke(args.args1);
        }
    }

}

public class ViewEventData : IViewEvent
{

    public Action CallBack { get; set; }

    public void onHandler(EventSysArgsBase eventArgs)
    {
        if (eventArgs != null && eventArgs is EventSysArgs args)
        {
            CallBack.Invoke();
        }
    }

}