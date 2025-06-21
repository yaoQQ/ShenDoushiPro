using FairyGUI;
using UnityEngine;

public class LoginViewOnInit : Window
{
    public LoginViewOnInit()
    {
        Logger.PrintDebug("LoginViewOnInit()");
        this.gameObjectName = "LoginViewOnInit";
        //最后，创建出UI
        //   uipanel.CreateUI(); //已有Uipanel组件不用调用  Start会调用
        Logger.PrintDebug("LoginView uipanel.CreateUI()");
    }

    protected override void OnInit()
    {
        Debug.Log("LoginView OnInit()");
        this.contentPane = UIPackage.CreateObject("login", "LoginPage").asCom;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        Debug.Log("LoginView OnInit() this.contentPane=" + this.contentPane);
        this.Center();
        this.modal = true;

        ////加载资源包
        //UIPackage.AddPackage("UILoading/Loading");
        ////创建UI面板对象
        //contentPane = UIPackage.CreateObject("Loading", "LoadingView").asCom;
        ////屏幕自适应
        //contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        //contentPane.AddRelation(GRoot.inst, RelationType.Size);

        ////初始化组件，添加UI事件监听
        //group_progress = contentPane.GetChild("group_progress").asGroup;
        //group_progress.visible = false;
        //text_progress = contentPane.GetChild("text_progress").asTextField;
        //SetProgress(0f);

        //group_alert = contentPane.GetChild("group_alert").asGroup;
        //group_alert.visible = false;
        //text_alert = contentPane.GetChild("text_alert").asTextField;
        //text_alert.text = "";
        //btn_1 = contentPane.GetChild("btn_1").asButton;
        //btn_1.onClick.Add(() =>
        //{
        //    if (onAlertBtn1 != null)
        //        onAlertBtn1();
        //    HideAlert();
        //});
        //btn_2 = contentPane.GetChild("btn_2").asButton;
        //btn_2.onClick.Add(() =>
        //{
        //    if (onAlertBtn2 != null)
        //        onAlertBtn2();
        //    HideAlert();
        //});
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
