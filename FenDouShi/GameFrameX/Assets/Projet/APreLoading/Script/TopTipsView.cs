// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TopTipsView : BaseView
// {

    // // private string topTipsTimer = "TopTipsTimer";

    // // [SerializeField]
    // // private Mid_common_top_tips_panel main_mid;

    // // public TopTipsView()
    // // {

        // // this.viewName = "TopTipsView";
        // // this.loadOrders = new List<string>() { "BasePackage:common_top_tips_panel" };

        // // setViewAttribute(UIViewType.Feedback_Tip, UIViewEnum.TopTips_View, false);
        // // Logger.PrintDebug("@@@@ TopTipsView()");
    // // }
    // // protected override void onLoadUIEnd(string uiName, GameObject gameObject)
    // // {
        // // // main_mid = gameObject.GetComponent<UIBaseMono>();
        // // Logger.PrintDebug("onLoadUIEnd complte!! uiName=" + uiName);

        // // main_mid = gameObject.AddComponent<Mid_common_top_tips_panel>();
        // // this.BindMonoTable(gameObject, main_mid);
        // // UITools.SetParentAndAlign(gameObject, this.container);

        // // //this.main_mid.BtnLoginSee.AddEventListener(UIEvent.PointerClick, onBtnLoginSee);
        // // //this.main_mid.BtnLoginReset.AddEventListener(UIEvent.PointerClick, onBtnLoginReset);
        // // //this.main_mid.BtnLoginLogin.AddEventListener(UIEvent.PointerClick, onBtnLoginLogin);
        // // //this.main_mid.BtnLoginDirectly.AddEventListener(UIEvent.PointerClick, LoginDirectly);
        // // addEvent();

        // // //showTopTips("选中倒计时 this.scanTime="..tostring(this.scanTime))
    // // }
    // // protected override void onShowHandler(object msg)
    // // {
        // // // Logger.PrintDebug("onShowHandler() 显示界面：msg=" + msg);
        // // GameObject go = this.getViewGO();
        // // if (go == null)
        // // {
            // // Logger.PrintError("go == null");
            // // return;
        // // }
        // // go.transform.SetAsLastSibling();
        // // string textStr = msg as string;
        // // onShowTopTips(textStr);
    // // }
    // // private void addEvent()
    // // {

    // // }


    // // private void onShowTopTips(string msg)
    // // {

        // // //if (GlobalTimeManager.Instance.timerController.CheckExistByKey(this.topTipsTimer))
        // // //{
        // // //    GlobalTimeManager.Instance.timerController.RemoveTimerByKey(this.topTipsTimer);
        // // //    this.onHideView(0);
        // // //}
        // // OpenCallBack(msg);
    // // }
    // // private void OpenCallBack(string msg)
    // // {
        // // //if (!string.IsNullOrEmpty(msg))
        // // //{
        // // //    RectTransform tempImageRect = this.main_mid.topTips_bg_Image.transform.GetComponent<RectTransform>();
        // // //    this.main_mid.topTips_Text.text = msg;
        // // //    float tempwidth = this.main_mid.topTips_Text.Txt.preferredWidth + 150;
        // // //    float tempHeight = 165;
        // // //    tempImageRect.sizeDelta = new Vector2(tempwidth, tempHeight);
        // // //}
        // // //GlobalTimeManager.Instance.timerController.AddTimer(this.topTipsTimer, 1500, 1, onHideView);
    // // }

    // // private void onHideView(int countNum)
    // // {
        // // if (this.main_mid)
        // // {

            // // UIViewManager.Instance.Close(UIViewEnum.TopTips_View);
        // // }
    // // }

    // // protected override void onClose()
    // // {
        // // base.onClose();
    // // }


// }
