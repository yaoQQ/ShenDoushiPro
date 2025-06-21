using FairyGUI;
using UnityEngine;

public class LoginView : Window
{
    UIPanel uipanel;
    public LoginView():base()
    {

        this.uipanel= uipanel;
        uipanel.packageName = "login";
        uipanel.componentName = "LoginPage";
        this.gameObjectName = "LoginPage";
        //下面这是设置选项非必须，注意很多属性都要在container上设置，而不是UIPanel

        //设置renderMode的方式
        uipanel.container.renderMode = RenderMode.ScreenSpaceOverlay;

        //设置renderCamera的方式
        uipanel.container.renderCamera = CameraManager.Instance.StageCamera;

        //设置fairyBatching的方式
        uipanel.container.fairyBatching = true;

        //设置sortingOrder的方式
        uipanel.SetSortingOrder(3, true);

        //设置hitTestMode的方式
        uipanel.SetHitTestMode(HitTestMode.Default);
        GComponent view = uipanel.ui;
        //最后，创建出UI
     //   uipanel.CreateUI(); //已有Uipanel组件不用调用  Start会调用
        Logger.PrintDebug("LoginView uipanel.CreateUI()");
    }

    protected override void OnInit()
    {
        Debug.Log("LoginView OnInit()");
        this.contentPane = UIPackage.CreateObject("login", "LoginPage").asCom;
        Debug.Log("LoginView OnInit() this.contentPane=" + this.contentPane);
        this.Center();
        this.modal = true;
    }
    override protected void OnShown()
    {
        Debug.Log("LoginView OnShown()");
    }
    void RenderListItem(int index, GObject obj)
    {
        GButton button = (GButton)obj;
        button.icon = "i" + UnityEngine.Random.Range(0, 10);
        button.title = "" + UnityEngine.Random.Range(0, 100);
    }

    override protected void DoShowAnimation()
    {
        this.SetScale(0.1f, 0.1f);
        this.SetPivot(0.5f, 0.5f);
        this.TweenScale(new Vector2(1, 1), 0.3f).OnComplete(this.OnShown);
    }

    override protected void DoHideAnimation()
    {
        this.TweenScale(new Vector2(0.1f, 0.1f), 0.3f).OnComplete(this.HideImmediately);
    }

    void __clickItem(EventContext context)
    {
        GButton item = (GButton)context.data;
        this.contentPane.GetChild("n11").asLoader.url = item.icon;
        this.contentPane.GetChild("n13").text = item.icon;
    }
}
