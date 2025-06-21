using UnityEngine;

using System.Collections.Generic;

public class BaseView
{
    protected string viewName = "";
    private UIViewType viewType = UIViewType.None;//界面类型（层级）
    private UIViewEnum viewEnum = UIViewEnum.None;//--界面枚举
    private bool isStackView = false;//--是否是栈界面
    private bool isStateBarWhiteColor = false;//--是否是白色状态栏
    protected List<string> loadOrders;//--加载列表
    protected GameObject container;//--容器（层级）GameObject
    private bool isLoading = false;//--是否正在加载，加载完成后设置为false
    private bool isLoaded = false;//--是否已经加载，加载完成后设置为true
    private bool isOpening = false;//--是否正在打开过程中（加载并显示）
    private bool isOpen = false;//--是否已经打开（显示）

    private IMiddleware main_mid;
    private int loadedNum = 0;
    private List<string> loadedNames;
    public BaseView()
    {
        loadedNames = new List<string>();
      //  Logger.PrintDebug("@@@@ BaseView()");
    }
    /// <summary>
    /// --获取界面类型（层级）
    /// </summary>
    /// <returns></returns>
    public UIViewType getViewType()
    {
        if (this.viewType == UIViewType.None)
        {
            Loger.PrintError(this.viewName, " 没有设置 viewType");
        }
        return this.viewType;
    }

    //--获取界面枚举
    public UIViewEnum getViewEnum()
    {
        if (this.viewEnum == UIViewEnum.None) {
            Loger.PrintError(this.viewName, " 没有设置 viewEnum");
           
         }
        return this.viewEnum;
    }

    //--获取是否是栈界面
    public bool getIsStackView()
    {
        return this.isStackView;
    }

    public bool getStateBarWhiteColor()
    {
        return this.isStateBarWhiteColor;
    }
    protected void setViewAttribute(UIViewType _viewType, UIViewEnum _viewEnum, bool _isStackView, bool _isStateBarWhiteColor=true)
    {
        this.viewType = _viewType;
        this.viewEnum = _viewEnum;

       //Logger.PrintDebug(" viewType=" + (_viewType));
       // Logger.PrintDebug(" _viewEnum=" + (_viewEnum));
       
        //if (_isStackView == false && (int)_viewEnum > 1000 && (int)_viewEnum< 5000)
        //{
        //    Logger.PrintError("界面(" + this.viewName + ")没有设置是否参与界面堆栈");
        
        //}
        if (_isStackView)
        {
            this.isStackView = true; 
        }
       
           this.isStateBarWhiteColor = _isStateBarWhiteColor;

    }
    public List<string> getLoadOrders()
    {
        return this.loadOrders;
    }

    public GameObject getViewGO()
    {
        if (this.main_mid != null)
        {
            return this.main_mid.go;
        }
        else
        {
            Logger.PrintError(this.viewName, "main_mid 不能为空");
        }
        return null;
    }

    //--设置容器（层级）GameObject
    public void setContainerGO(GameObject ContainerGO)
    {
        this.container = ContainerGO;
    }

    //--获取是否正在加载
    public bool getIsLoading()
    {
       return this.isLoading;
    }

    //--获取是否已加载
    public bool getIsLoaded()
    {
        return this.isLoaded;
    }

    public string getPackage()
    {

        string[] oriStr = this.loadOrders[0].Split(':');
        return oriStr[0];
        
    }
    public string getBoundleName()
    {

        string[] oriStr = this.loadOrders[0].Split(':');
        return oriStr[1];

    }

    //--设置是否正在打开过程中（加载并显示）
    public void setOpening(bool value)
    {
        this.isOpening = value;
    }

    //--获取是否正在打开过程中（加载并显示）
    public bool getOpening()
    {
        return this.isOpening;
    }

    //--获取是否正在打开过程中（加载并显示）
    public bool getIsOpen()
    {
        return this.isOpen;
    }

    //--开始加载
    public void startLoad()
    {
        this.isLoading = true;
        this.isLoaded = false;
       
        load();
    }

    //--加载
    private void load()
    {
        if (this.loadOrders.Count<1)
        {
            Logger.PrintError(this.viewName, " loadOrders 加载列表不能为空");
        }
        for(int i = 0; i< this.loadOrders.Count; i++)
        {
            string order = this.loadOrders[i];
            string[] orderArr = order.Split(':');
            if (orderArr.Length != 2)
            {
                Loger.PrintError(this.viewName, " loadOrders 加载列表格式错误");
                return;
            }
            //test@@@
            // UILoadControl.Instance.CreateUI(orderArr[0], orderArr[1], this);
        }
    }

    public void executeLoadUIEnd(string uiName,GameObject obj)
    {
        Loger.PrintColor("blue", "executeLoadUIEnd uiName=" + uiName);
        this.loadedNum = this.loadedNum + 1;
        this.loadedNames.Add(uiName);
        Loger.PrintColor("blue", "executeLoadUIEnd gameObject=" + obj);
        this.onLoadUIEnd(uiName, obj);
        if (this.loadedNum >= this.loadOrders.Count) {
            this.endLoad();
        }
        obj.SetActive(false);
    }

    
      //--override
    protected virtual void onLoadUIEnd(string uiName, GameObject gameObject)
    {
        Loger.PrintWarning(this.viewName, " -- ", uiName, " 需重定义onLoadUIEnd");
    }
    public void show(System.Object msg)
    {

        Logger.PrintDebug("show() 显示界面：" + this.viewName);

        this.isOpen = true;
        this.getViewGO().SetActive(true);

        //if (this.ease_container != null)
        //{
        //    UITools.SetUIScale(this.ease_container, new Vector2(1, 1));
        //    //   this.ease_container.transform.DOScale(Vector3.one).SetEase(Ease.OutBack);
        //}

        //if (this.main_mid != null)
        //{
        //    Image img = this.main_mid.go.GetComponent<Image>();
        //    if (img != null)
        //    {
        //        Material mat= img.material;
        //        if (mat != null)
        //        {

        //        }
        //    }
        //}
        this.onShowHandler(msg);
    }
    protected virtual void onShowHandler(System.Object msg)
    {
        Loger.PrintWarning(this.viewName, " -- ", " 需重定义onShowHandler");
    }
    //--加载结束
    private void endLoad()
    {
        this.isLoading = false;
        this.isLoaded = true;
    }

    // --已废弃，保留在这用于兼容旧代码
    private void endInit()
    { 
    }

    private GameObject ease_container = null;
    private void setEaseContainer(GameObject container)
    {
        this.ease_container = container;
    }

    

    public void hide()
    {
        Debug.Log("隐藏界面：" + this.viewName);
        this.isOpen = false;
        if (this.getViewGO() != null)
        {
            this.getViewGO().SetActive(false);
        }
        this.onClose();
    }
    //--override
    protected virtual void onClose()
    {

    }

    public void closeByEsc()
    {
        //test@@@
       // UIViewManager.Instance.Close(this.viewEnum);
    }

    protected void BindMonoTable(GameObject gameObject, IMiddleware mid)
    {
        
        if (gameObject == null)
        {
            Loger.PrintError(this.viewName + "绑定Mono界面为空");
            return;
        }
        this.main_mid = mid;
        //test@@@@@  绑定
        //  gameObject.GetComponent<UIBaseMono>().BindMonoTable();
        // this.main_mid = gameObject.GetComponent<UIBaseMono>();

        //test@@@
      //  PanelWidget panel= gameObject.GetComponent<PanelWidget>();
      //  panel.UIViewEnum = (int)this.viewEnum;

       // Loger.PrintDebug("=======================this:onLoadUIEnd================panel.UIViewEnum =" + (panel.UIViewEnum));
        
    }

    //--销毁界面
    public void onDestroy()
    {
       // --重置状态
        this.isLoading = false;
        this.isLoaded = false;
        this.isOpen = false;
        this.main_mid = null;
        this.loadedNum = 0;

        for(int i = 0; i < this.loadedNames.Count; i++)
        {
            string te = loadedNames[0];
            string[] strs = te.Split(':');
            Logger.PrintDebug("ViewManager.destroyUIRes：" + strs[0] + " " + strs[1]);
         
            
            UIViewManager.Instance.DestroyUIRes(strs[0], strs[1],this.viewEnum);
        }
       
    }
}