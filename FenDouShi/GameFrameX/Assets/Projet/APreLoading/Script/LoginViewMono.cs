using FairyGUI;
using UnityEngine;

public class LoginViewMono : MonoBehaviour
{
    private UIPanel uiPanel;
    private GComponent _mainView;
    private GTextField gTextField;
    private void Awake()
    {
        this.gameObject.name = "LoginView";
    }
    private void Start()
    {
        Logger.PrintDebug("LoginView Start");
         uiPanel = this.GetComponent<UIPanel>();
        // 获取当前游戏对象上的UIPanel组件，并获取其UI作为主视图
        _mainView = this.GetComponent<UIPanel>().ui;
       
        if (_mainView == null)
        {
            Logger.PrintColor("red",$"{this.gameObject.name} 当前UIPanel没有配置数据");
        }
        if (_mainView == null)
        {
         //   Logger.PrintError("LoginViewMono _mainView is null");
            return;
        }
        //gTextField = _mainView.GetChild("updateTxt").asTextField;
       
        Logger.PrintColor("yellow","LoginView uipanel.CreateUI() Success");
    }
    private void Init()
    {
        Logger.PrintDebug("LoginView Start");
        uiPanel.packageName = "login";
        uiPanel.componentName = "LoginPage";
        //下面这是设置选项非必须，注意很多属性都要在container上设置，而不是UIPanel
        //设置renderMode的方式
        uiPanel.container.renderMode = RenderMode.ScreenSpaceOverlay;
        //设置renderCamera的方式
        uiPanel.container.renderCamera = CameraManager.Instance.StageCamera;
        //设置fairyBatching的方式
        uiPanel.container.fairyBatching = true;
        //设置sortingOrder的方式
        uiPanel.SetSortingOrder(3, true);
        //设置hitTestMode的方式
        uiPanel.SetHitTestMode(HitTestMode.Default);
       // _mainView = this.GetComponent<UIPanel>().ui;
         uiPanel.CreateUI(); //已有Uipanel组件不用调用 Unity默认Start会调用

    }
    public void Show()
    {
     
        if (_mainView == null)
        {
            Init();
        }
        //Window window = new Window();
        //window.contentPane = _mainView;
    }
}
