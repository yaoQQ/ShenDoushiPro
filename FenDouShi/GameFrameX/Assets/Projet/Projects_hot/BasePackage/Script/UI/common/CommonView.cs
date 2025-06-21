
using System;

public class CommonView 
{
    public static void showTopTips(string str)
    {

        UIViewManager.Instance.Open(UIViewEnum.TopTips_View, str);
    }
    public static void showAlertMsg(string title, string msg, string btnName, Action<int> onBtnfunc)
    {

        AlertWindowInfo alertInfo = new AlertWindowInfo();
        alertInfo.msgType = AlertWindowType.Alert;
        alertInfo.title = title;
        alertInfo.msg = msg;
        alertInfo.btnName = btnName;
        alertInfo.onBtnfunc = onBtnfunc;
        //test@@@
        UIViewManager.Instance.Open(UIViewEnum.AlertWindow, alertInfo);
    }

    public static void showVerifyMsg(string title, string msg, string btnName1, Action<int> onBtnfunc1, string btnName2, Action<int> onBtnfunc2, bool isAllowClose = false)
    {

        AlertWindowInfo alertInfo = new AlertWindowInfo();
        alertInfo.msgType = AlertWindowType.Verify;
        alertInfo.title = title;
        alertInfo.msg = msg;
        alertInfo.btnName1 = btnName1;
        alertInfo.onBtnfunc1 = onBtnfunc1;
        alertInfo.btnName2 = btnName2;
        alertInfo.onBtnfunc2 = onBtnfunc2;

        UIViewManager.Instance.Open(UIViewEnum.AlertWindow, alertInfo);
    }
    public static void showAlertVerifyWindow(string title, string msg, string btnName, Action<int> onBtnfunc, bool isAllowClose)
    {

        AlertWindowInfo alertInfo = new AlertWindowInfo();
        alertInfo.msgType = AlertWindowType.AlertVerify;
        alertInfo.title = title;
        alertInfo.msg = msg;
        alertInfo.btnName = btnName;
        alertInfo.onBtnfunc = onBtnfunc;
        alertInfo.isAllowClose = isAllowClose;

        UIViewManager.Instance.Open(UIViewEnum.AlertWindow, alertInfo);
    }
    public static void OpenShop(ShopType mallType, ShopFromType from, Action succeedCallback)
    {
        MailInfo info = new MailInfo();
        info.shopType = mallType;
        info.fromType = from;
        info.succeedCallback = succeedCallback;

        UIViewManager.Instance.Open(UIViewEnum.Platform_Mall_View, info, succeedCallback);
    }
    public static string NumberToShow(int number)
    {
        //if ((number / (10 ^ 6)) >= 1)
        //{
        //    int num = number / (10 ^ 4);
        //    number = (int)UnityEngine.Mathf.Floor(num);
        //    return (number + "万");
        //}

        return "" + number;
    }
}