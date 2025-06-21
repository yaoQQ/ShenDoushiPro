using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UIWidget;

//确认框类型
public enum AlertWindowType
{
    None = 0,
    //	--警告
    Alert = 1, 

   // --确认
    Verify = 2,

	//--确认型警告
    AlertVerify = 3,
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
public class AlertWindowView : BaseView
{
   // private string topTipsTimer = "AlertWindowView";

    private Action<int> confirmFun;
    private Action<int> cancelFun;
    private bool isAllowClose=false;

    [SerializeField]
    private Mid_common_popup_window_panel main_mid;

    public AlertWindowView()
    {

        this.viewName = "TopTipsView";
        this.loadOrders = new List<string>() { "BasePackage:common_popup_window_panel" };
        
        setViewAttribute(UIViewType.Alert_box, UIViewEnum.AlertWindow, false);
    }
    protected override void onLoadUIEnd(string uiName, GameObject gameObject)
    {
        Logger.PrintDebug("onLoadUIEnd complte!! uiName=" + uiName);

        main_mid = gameObject.AddComponent<Mid_common_popup_window_panel>();
        this.BindMonoTable(gameObject, main_mid);
        UITools.SetParentAndAlign(gameObject, this.container);

         addEvent();

    }
    private void addEvent()
    {
        this.main_mid.btn_1.AddEventListener(UIEvent.PointerClick, this.onConfirmFun);
        this.main_mid.btn_2.AddEventListener(UIEvent.PointerClick, this.onCancelFun);
        this.main_mid.mask.AddEventListener(UIEvent.PointerClick, this.onMaskClose);
    }
    protected override void onShowHandler(object msg)
    {
        Logger.PrintDebug("onShowHandler() 显示界面：msg=" + msg);
        GameObject go = this.getViewGO();
        if (go == null)
        {
            Logger.PrintError("go == null");
            return;
        }
        AlertWindowInfo info = (AlertWindowInfo)msg;
        OpenMsgView(info);
    }
    protected override void onClose()
    {
        this.confirmFun = null;
        this.cancelFun = null;
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
            case AlertWindowType.AlertVerify:
                updateAlertVerifyWindow(msg);
                break;
          
        }
    }
    private void updateAlertWindow(AlertWindowInfo msg)
    {
        this.main_mid.title_text.text = msg.title;
        this.main_mid.info_text.text = msg.msg;
        bgBestView(msg.title);
        this.main_mid.btn_1.Txt.text = msg.btnName;
        this.main_mid.btn_1.gameObject.SetActive(true);
        this.main_mid.btn_2.gameObject.SetActive(false);
        this.confirmFun = msg.onBtnfunc;
        this.isAllowClose = false;
        this.main_mid.go.SetActive(true);
    }
    private void updateVerifyWindow(AlertWindowInfo msg)
    {
        this.main_mid.title_text.text = msg.title;
        this.main_mid.info_text.text = msg.msg;
        bgBestView(msg.title);
        this.main_mid.btn_1.Txt.text = msg.btnName1;
        this.main_mid.btn_1.gameObject.SetActive(true);
        this.main_mid.btn_2.Txt.text = msg.btnName2;
        this.main_mid.btn_2.gameObject.SetActive(true);
        this.confirmFun = msg.onBtnfunc1;
        this.cancelFun = msg.onBtnfunc2;
        this.isAllowClose = msg.isAllowClose;
        this.main_mid.go.SetActive(true);
    }
    private void updateAlertVerifyWindow(AlertWindowInfo msg)
    {
        this.main_mid.title_text.text = msg.title;
        this.main_mid.info_text.text = msg.msg;
        bgBestView(msg.title);
        this.main_mid.btn_2.Txt.text = msg.btnName;
        this.main_mid.btn_1.gameObject.SetActive(false);
        this.main_mid.btn_2.gameObject.SetActive(true);
        this.cancelFun = msg.onBtnfunc;
        this.isAllowClose = msg.isAllowClose;
        this.main_mid.go.SetActive(true);
    }
    private void bgBestView(string isTitleNil)
    {
        RectTransform tempImageRect = this.main_mid.bg_image.transform.GetComponent<RectTransform>();
        float tempwidth = 710;
        float tempHeight = this.main_mid.info_text.Txt.preferredHeight + 430;
        if (string.IsNullOrEmpty(isTitleNil))
        {
            tempHeight = tempHeight - 88;
        }
        tempImageRect.sizeDelta =new Vector2(tempwidth, tempHeight);
    }
    private void onConfirmFun(PointerEventData eventData)
    {
        if (this.confirmFun != null)
        {
            GlobalTimeManager.Instance.timerController.AddTimer("AlertWindowView", 100, 1, confirmFun);
        }
        //test@@@
          UIViewManager.Instance.Close(UIViewEnum.AlertWindow);
    }
    private void onCancelFun(PointerEventData eventData)
    {
        if (this.cancelFun != null)
        {
            GlobalTimeManager.Instance.timerController.AddTimer("AlertWindowView", 100, 1, cancelFun);
        }
        //test@@@
         UIViewManager.Instance.Close(UIViewEnum.AlertWindow);
    }
    private void onMaskClose(PointerEventData eventData)
    {
        Logger.PrintDebug("尝试关闭警告窗口");
        if (this.isAllowClose)
        {
            //test@@@
             UIViewManager.Instance.Close(UIViewEnum.AlertWindow);
        }
    }


    

}