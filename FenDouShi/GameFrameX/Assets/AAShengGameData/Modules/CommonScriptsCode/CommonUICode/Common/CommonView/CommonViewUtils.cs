
using System;
using System.Collections.Generic;

public class CommonViewUtils
{

    public static void ShowTopTips(string str)
    {
        UIViewManager.Instance.Show(UIViewEnum.TopTips_View, str);
    }

    //本次登录不提示列表
    public static List<string> NoTipsList = new List<string>();
    /// <summary>
    /// 显示一个提示框
    /**
        MessageBoxVo msgVo = new MessageBoxVo();
        msgVo.title = "提示";
        msgVo.msg = "是否从好友列表删除该好友？";
        msgVo.isCheckNoShowTodayKey = "friend_delete_no_show_today";
        msgVo.OkBtnfunc = () =>
        {
            
        };
        CommonViewUtils.ShowMessageBox(msgVo);
    
    */
    /// </summary>
    /// <param name="alertInfo"></param>
    public static void ShowMessageBox(MessageBoxVo messageVo)
    {
        if (messageVo.isCheckNoShowTodayKey != "")
        {
            var isNoShowTips = false;
            if (NoTipsList.Contains(messageVo.isCheckNoShowTodayKey))
            {
                isNoShowTips = true;
            }
            else if (messageVo.CheckNoShowState == ECheckNoShowState.Today)
            {
                isNoShowTips = PlayerPrefsTools.GetIsSameDay(messageVo.isCheckNoShowTodayKey);
            }
            if (isNoShowTips)
            {
                //本次登录不提示了
                if (messageVo.OkBtnfunc != null)
                {
                    messageVo.OkBtnfunc();
                }
                return;
            }
        }
        UIViewManager.Instance.Show(UIViewEnum.MessageBoxView, messageVo);
    }

    //一个按钮框 已弃用
    [Obsolete("通用提示框已弃用, 请使用 ShowMessageBox")]
    public static void ShowAlertMsg(string title, string msg, string btnName, Action<int> onBtnfunc)
    {

        AlertWindowInfo alertInfo = new AlertWindowInfo();
        alertInfo.msgType = AlertWindowType.Alert;
        alertInfo.title = title;
        alertInfo.msg = msg;
        alertInfo.btnName = btnName;
        alertInfo.onBtnfunc = onBtnfunc;

        UIViewManager.Instance.Show(UIViewEnum.AlertWindowView, alertInfo);
    }

    //两个按钮框
    [Obsolete("通用提示框已弃用, 请使用 ShowMessageBox")]
    public static void ShowAlertMsgTwoBtn(string title, string msg, string btnName1, Action<int> onBtnfunc1, string btnName2, Action<int> onBtnfunc2, bool isAllowClose = false)
    {

        AlertWindowInfo alertInfo = new AlertWindowInfo();
        alertInfo.msgType = AlertWindowType.Verify;
        alertInfo.title = title;
        alertInfo.msg = msg;
        alertInfo.btnName1 = btnName1;
        alertInfo.onBtnfunc1 = onBtnfunc1;
        alertInfo.btnName2 = btnName2;
        alertInfo.onBtnfunc2 = onBtnfunc2;

        UIViewManager.Instance.Show(UIViewEnum.AlertWindowView, alertInfo);
    }

    //两个按钮框
    public static void ShowCostContentAlertTwoBtn(string title, string msg, string btnName1, Action<int> onBtnfunc1, string btnName2, Action<int> onBtnfunc2, bool isAllowClose = false)
    {

        AlertWindowInfo alertInfo = new AlertWindowInfo();
        alertInfo.msgType = AlertWindowType.goodCostTips;
        alertInfo.title = title;
        alertInfo.msg = msg;
        alertInfo.btnName1 = btnName1;
        alertInfo.onBtnfunc1 = onBtnfunc1;
        alertInfo.btnName2 = btnName2;
        alertInfo.onBtnfunc2 = onBtnfunc2;

        UIViewManager.Instance.Show(UIViewEnum.AlertWindowView, alertInfo);
    }
    ////两个按钮框
    //public static void ShowCostAlertMsgTwoBtn(string title, string msg, string btnName1, Action<int> onBtnfunc1, string btnName2, Action<int> onBtnfunc2, bool isAllowClose = false)
    //{

    //    AlertWindowInfo alertInfo = new AlertWindowInfo();
    //    alertInfo.msgType = AlertWindowType.goodCostContent;
    //    alertInfo.title = title;
    //    alertInfo.msg = msg;
    //    alertInfo.btnName1 = btnName1;
    //    alertInfo.onBtnfunc1 = onBtnfunc1;
    //    alertInfo.btnName2 = btnName2;
    //    alertInfo.onBtnfunc2 = onBtnfunc2;

    //    UIViewManager.Instance.Show(UIViewEnum.AlertWindowView, alertInfo);
    //}
}