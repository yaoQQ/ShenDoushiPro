using FairyGUI;
using UnityEngine;

public class FGUILayoutManager : Singleton<FGUILayoutManager>
{
    public Transform Global;
    public Transform Unit { get; set; }
    public GRoot GRoot { get; set; }
    private GComponent _newGuidGRoot;
    private GComponent _loadingGRoot;
    private GComponent _alertBoxGRoot;
    private GComponent _maskGRoot;

    private GComponent _popUpGRoot;
    private GComponent _midGRoot;
    private GComponent _bottomGRoot;
    private GComponent _windowContentGRoot;
    public Transform Sounds { get; set; }
    
    private GGraph _modalLayer;
    public GComponent GetGRootLayer(UIViewLayerType uiPanelType)
    {
        if (this.GRoot == null)
        {
            Init();
        }
        switch (uiPanelType)
        {
            case UIViewLayerType.windowContent:
                return _windowContentGRoot;
            case UIViewLayerType.Buttom:
                return _bottomGRoot;
            case UIViewLayerType.Second_View:
                return _midGRoot;
            case UIViewLayerType.Pop_view:
                return _popUpGRoot;
            case UIViewLayerType.Loading_View:
                return _loadingGRoot;

            case UIViewLayerType.Alert_box:
                return _alertBoxGRoot;
            case UIViewLayerType.Mask_View:
                return _maskGRoot;
            case UIViewLayerType.NoviceGuide_View:
                return _newGuidGRoot;
            default:
                return null;
        }
    }
    public void AddChild(UIViewLayerType layerType, GComponent child)
    {
        GetGRootLayer(layerType).AddChild(child);
        if(child is BaseView baseView)
        {
            //添加黑底模板
            if (baseView.modal)
            {
                AdjustModalLayer(baseView, layerType);
            }
        }
    }
    public void RemoveChild(UIViewLayerType layerType, GComponent child)
    {
        GetGRootLayer(layerType).RemoveChild(child);
        if (child is BaseView baseView)
        {
            //添加黑底模板
            if (baseView.modal)
            {
                if (_modalLayer.parent != null)
                    _modalLayer.parent.RemoveChild(_modalLayer);
            }
        }
    }
    public void Init()
    {

        this.GRoot = GRoot.inst;

        this._windowContentGRoot = CreateRoot("WindowContentGRoot");
        this._bottomGRoot = CreateRoot("BottonGRoot");
        this._midGRoot = CreateRoot("MidGRoot");
        this._popUpGRoot = CreateRoot("PopGRoot");
        this._loadingGRoot = CreateRoot("LoadingGRoot");
        this._alertBoxGRoot = CreateRoot("alertGRoot");
        this._maskGRoot = CreateRoot("MaskGRoot");
        this._newGuidGRoot = CreateRoot("NewGuidGRoot");


        this.Sounds = CreateSoundRoot();
    }


    private GComponent CreateRoot(string name)
    {
        GComponent gComponent = new GComponent();
        gComponent.gameObjectName = name;
        gComponent.opaque = false;
        GRoot.inst.AddChild(gComponent);
        return gComponent;
    }

    private Transform CreateSoundRoot()
    {
        GameObject gameObject = new GameObject("SoundsRoot");
        gameObject.transform.SetParent(Global, false);
        return gameObject.transform;
    }
    /// <summary>
    /// 调整模态层，与GRoot中的AdjustModalLayer功能类似
    /// </summary>
    private void AdjustModalLayer(BaseView baseView,UIViewLayerType layType)
    {
        if (_modalLayer == null || _modalLayer.isDisposed)
            CreateModalLayer();
        _modalLayer.visible = true;
        ProcessModalLayerForContainer(baseView,GetGRootLayer(layType));
        // 遍历所有层级容器，处理模态窗口
    }

    /// <summary>
    /// 为指定容器处理模态层
    /// </summary>
    /// <param name="container"></param>
    private void ProcessModalLayerForContainer(BaseView baseView, GComponent container)
    {
        if (container == null || container.isDisposed)
            return;
        _modalLayer.data = baseView;//哪个界面的黑底
        int cnt = container.numChildren;

        for (int i = cnt - 1; i >= 0; i--)
        {
            GObject g = container.GetChildAt(i);
            if ((g is Window window) && (g as Window).modal)
            {
                // 如果模态层不在任何容器中，则添加到当前容器中
                if (_modalLayer.parent != container)
                {
                    container.AddChildAt(_modalLayer, i);
                    _modalLayer.visible=true;
                    _modalLayer.gameObjectName = "BlackModalLayer-" + g.gameObjectName;
                    return;
                }
                // 如果模态层已经在当前容器中，则调整位置
                else if (_modalLayer.parent == container)
                {
                    container.SetChildIndexBefore(_modalLayer, i);
                    _modalLayer.visible = true;
                    _modalLayer.gameObjectName = "BlackModalLayer-" + g.gameObjectName;
                    return;
                }
                // 如果模态层在其他容器中，说明有其他更高优先级的模态窗口存在
                else
                {
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 创建模态层
    /// </summary>
    private void CreateModalLayer()
    {
        _modalLayer = new GGraph();
        _modalLayer.DrawRect(GRoot.width, GRoot.height, 0, Color.white, UIConfig.modalLayerColor);
        _modalLayer.AddRelation(GRoot, RelationType.Size);
        _modalLayer.name = _modalLayer.gameObjectName = "BlackModalLayer";
        _modalLayer.SetHome(GRoot);
        // 添加点击事件，点击时调用模态层点击处理方法
        _modalLayer.touchable = true; // 允许接收点击事件
        _modalLayer.onClick.Add(OnModalLayerClick);

    }


    /// <summary>
    /// 模态层点击处理方法
    /// </summary>
    /// <param name="context">事件上下文</param>
    private void OnModalLayerClick()
    {
        Logger.PrintDebug("ModalLayer clicked!");
        if (_modalLayer != null && _modalLayer.data is BaseView baseView)
        {
            // 从对应的层级移除子对象
            UIViewManager.Instance.Hide(baseView.getViewEnum());
            _modalLayer.data = null;
        }
    }

    public void Dispose()
    {
        if (_modalLayer != null && !_modalLayer.isDisposed)
        {
            _modalLayer.onClick.Remove(OnModalLayerClick);
            _modalLayer.Dispose();
        }
            
        _windowContentGRoot.Dispose();
        _bottomGRoot.Dispose();
        _midGRoot.Dispose();
        _popUpGRoot.Dispose();
        _loadingGRoot.Dispose();
        _alertBoxGRoot.Dispose();
        _maskGRoot.Dispose();
        _newGuidGRoot.Dispose();

        _windowContentGRoot = null;
        _bottomGRoot = null;
        _midGRoot = null;
        _popUpGRoot = null;
        _loadingGRoot = null;
        _alertBoxGRoot = null;
        _maskGRoot = null;
        _newGuidGRoot = null;
        _modalLayer = null;
        Sounds = null;
        GRoot = null;
    }
}
