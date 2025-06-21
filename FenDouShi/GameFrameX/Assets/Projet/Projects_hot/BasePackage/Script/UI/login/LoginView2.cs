using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UIWidget;
using static UnityEngine.UI.InputField;

public class LoginView2 : BaseView
{
    [SerializeField]

    private Mid_login_panel2 main_mid;
    public LoginView2()
    {
        
        this.viewName = "LoginView2";
        this.loadOrders = new List<string>() { "BasePackage:login_panel2" };
        //test@@@
        setViewAttribute(UIViewType.Platform_Second_View, UIViewEnum.LoginView2, false);
        Debug.Log("@@@@ LoginView2()");
    }
    public static void TestHuaTuo()
    {
        Debug.Log("@@@@yanmei LoginView2  TestHuaTuo  getAuthcodeCount=");
    }
    public void TestOnLoadUIEnd(string uiName, GameObject gameObject)
    {
        onLoadUIEnd(uiName, gameObject);
    }
    protected override void onShowHandler(object msg)
    {
        Loger.PrintDebug(this.viewName + ": onShowHandler");
    }

    protected override void onLoadUIEnd(string uiName, GameObject gameObject)
    {
        // main_mid = gameObject.GetComponent<UIBaseMono>();
        Logger.PrintDebug("onLoadUIEnd complte!! uiName="+ uiName);
       
        main_mid=gameObject.AddComponent<Mid_login_panel2>();
        this.BindMonoTable(gameObject, main_mid);
        UITools.SetParentAndAlign(gameObject, this.container);

        this.main_mid.BtnLoginSee.AddEventListener(UIEvent.PointerClick, onBtnLoginSee);
        this.main_mid.BtnLoginReset.AddEventListener(UIEvent.PointerClick, onBtnLoginReset);
        this.main_mid.BtnLoginLogin.AddEventListener(UIEvent.PointerClick, onBtnLoginLogin);
        this.main_mid.BtnLoginDirectly.AddEventListener(UIEvent.PointerClick, LoginDirectly);

        this.main_mid.BtnLoginDirectly.Txt.text = "开始游戏";
    }

    private void onBtnLoginSee(PointerEventData point)
    {
        InputField inputField = this.main_mid.InputFieldLoginPassword.inputField;
        IconWidget icon = this.main_mid.IconLoginSee;
        if (icon.initIndex == 1)
        {
            inputField.contentType = 0;

            inputField.ForceLabelUpdate();

            icon.ChangeIcon(0);
        }
        else
        {
            inputField.contentType = ContentType.Password;
            inputField.ForceLabelUpdate();
            icon.ChangeIcon(1);
        }
    }

  
    private void onBtnLoginLogin(PointerEventData point)
    {
        Debug.Log("onBtnLoginLogin()");
        //测试按钮
        // PlayFabUserModule.getStoreCatalogData();
        //test@@@
         UIViewManager.Instance.Open(UIViewEnum.Platform_Global_Game_View);
      //  UIViewManager.Instance.Close(UIViewEnum.LoginView2);
       
        return;
        //string account = this.main_mid.InputFieldLoginAccount.text;
        //if (checkEmail(account) == false)
        //{
        //    // Alert.showAlertMsg("无效邮件格式", "请输入正确的邮件号码", "好的");
        //    CommonView.showAlertMsg("无效邮件格式", "请输入正确的邮件号码", "好的",null);
        //}
        //string password = this.main_mid.InputFieldLoginPassword.text;
        //this.main_mid.LoginDecribe.text = "账号登入中...";

        //// ShowWaiting(true, "login")
        //// --邮件注册
        ////test@@@@
        //LoginModule.LoginPlatform(Authtypes.EmailAndPassword, account, password);
    }

    //直接登入
    private void LoginDirectly(PointerEventData point)
    {
        this.main_mid.LoginDecribe.text = "账号登入中...";
        //ShowWaiting(true, "login");
        //test@@@
        LoginModule.LoginPlatform(Authtypes.Silent);
        Logger.PrintDebug("click!");

        //test@@@
        CommonView.showTopTips("开始登入");
       
    }
        private bool checkEmail(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return false;
        }
        return true;
    }
    private void onBtnLoginReset(PointerEventData point)
    {
        this.main_mid.PageLogin.gameObject.SetActive(false);
        this.main_mid.PageMain.gameObject.SetActive(false);
        this.main_mid.PageReset.gameObject.SetActive(true);
        this.main_mid.BtnResetBack.gameObject.SetActive(true);
        this.initPageReset();
    }
    //--重置密码界面初始化
    private void initPageReset(){
        this.main_mid.InputFieldResetAccount.text = "";
        this.main_mid.InputFieldResetAuth.text = "";
        this.main_mid.InputFieldResetPassword.text = "";
        this.main_mid.top_Text.text = "找回密码";
    }
}