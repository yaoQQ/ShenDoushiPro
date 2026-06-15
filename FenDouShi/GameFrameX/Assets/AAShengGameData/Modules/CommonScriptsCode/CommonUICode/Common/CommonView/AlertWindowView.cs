using FairyGUI;
using login;
using System;
using System.Collections.Generic;

//确认框类型
public enum AlertWindowType
{
    None = 0,
    //	--警告
    Alert = 1,
    Verify,
    goodCostTips = 4,//字符串带花费的确认框
    goodCostContent =5,//花费提示
}
public class AlertWindowInfo//传入的信息类
{
    public AlertWindowType msgType = AlertWindowType.None;
    public string title;
    public string msg;
    public string btnName;
    public string btnName1;
    public string btnName2;


    public bool isAllowClose = false;
    public System.Action<int> onBtnfunc;
    public System.Action<int> onBtnfunc1;
    public System.Action<int> onBtnfunc2;
}

[FGUIViewAttribute(G_AlertWindow.COMPONENT_NAME, typeof(AlertWindowView))]
public class AlertWindowView : BaseView
{
    public override string PackageName => G_AlertWindow.PACKAGE_NAME;
    public override string ComponentName => G_AlertWindow.COMPONENT_NAME;
    public override bool IsPreload => true;
    G_AlertWindow view;

   
    private Action<int> confirmFun;
    private Action<int> cancelFun;
    private bool isAllowClose = false;

    public AlertWindowView()
    {
        //声明界面{fairyGUI所在包:包里面Componet名字}
        //（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.AlertWindowView, false);
        Logger.PrintColor("yellow", "AlertWindowView()");
    }
    /// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="uiName">界面名</param>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
        Logger.PrintColor("yellow", $"onLoadUIEnd complte!! gameObject={gameObject}");
        this.contentPane = gameObject.asCom;;
        this.Center();
        view = this.contentPane as G_AlertWindow;
        this.modal = true;//@@@@待添加，点击黑屏自动取消弹窗功能
        init();
        ////调用fairyGUI的Window方法展示界面,加载完直接展示
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
       if(clickedButton== view.okBtn)
        {
            OnConfirmFun();
        }
        else if (clickedButton== view.closeBtn)
        {
            OnCancelFun();
        }
        else if (clickedButton == view.backBtn)
        {
            OnCancelFun();
        }
    }
    protected override void OnShown()
    {
        base.OnShown();
        Logger.PrintDebug("AlertWindowView OnShowHandler() 显示界面：msg=" + showArgs);
        AlertWindowInfo info = (AlertWindowInfo)showArgs;
        showArgs = null;
        OpenMsgView(info);
        Logger.PrintColor("red", $"OnShowHandler() ");
    }
    private void OpenMsgView(AlertWindowInfo msg)
    {
        switch (msg.msgType)
        {
            case AlertWindowType.Alert:
                updateAlertWindow(msg);
                break;
            case AlertWindowType.Verify:
                updateVerifyWindow(msg);
                break;
            case AlertWindowType.goodCostTips:
                updateAlertVerifyWindow(msg);
                break;
            case AlertWindowType.goodCostContent:
                updateAlertVerifyWindow(msg);
                break;
        }
    }
    private void init()
    {
        view.title.text ="";
        view.noticeText.text = "";
        view.okBtn.title ="";
        view.okBtn.visible = true;
        view.closeBtn.title ="";
        view.closeBtn.visible = true;
        this.confirmFun = null;
        this.cancelFun = null;
        this.isAllowClose =false;
    }
    private void updateAlertWindow(AlertWindowInfo msg)
    {
        view.title.text = msg.title;
        view.noticeText.text = msg.msg;
        //bgBestView(msg.title);
        view.okBtn.title = msg.btnName;
        view.okBtn.visible = true;
        view.closeBtn.visible = false;
        this.confirmFun = msg.onBtnfunc;
        this.isAllowClose = false;
    }
    private void updateVerifyWindow(AlertWindowInfo msg)
    {
        view.title.text = msg.title;
        view.noticeText.text = msg.msg;
        view.okBtn.title = msg.btnName1;
        view.okBtn.visible=true;
        view.closeBtn.title = msg.btnName2;
        view.closeBtn.visible=true;
        this.confirmFun = msg.onBtnfunc1;
        this.cancelFun = msg.onBtnfunc2;
        this.isAllowClose = msg.isAllowClose;
    }
    private void updateAlertVerifyWindow(AlertWindowInfo msg)
    {
        view.title.text = msg.title;
        view.noticeText.text = msg.msg;

        view.closeBtn.title = msg.btnName;
        view.okBtn.visible=false;
        view.closeBtn.visible = true;
        this.cancelFun = msg.onBtnfunc;
        this.isAllowClose = msg.isAllowClose;
    }
    private void OnConfirmFun()
    {
        if (this.confirmFun != null)
        {
            GlobalTimeManager.Instance.timerController.AddTimer("AlertWindowView", 100, 1, confirmFun);
        }
        UIViewManager.Instance.Hide(UIViewEnum.AlertWindowView);
    }
    private void OnCancelFun()
    {
        if (this.cancelFun != null)
        {
            GlobalTimeManager.Instance.timerController.AddTimer("AlertWindowView", 100, 1, cancelFun);
        }
        UIViewManager.Instance.Hide(UIViewEnum.AlertWindowView);
    }

}
