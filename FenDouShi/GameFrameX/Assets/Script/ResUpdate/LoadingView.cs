using System;
using UnityEngine;
using FairyGUI;

public class LoadingView : Window
{
    private GameObject gameObject;

    private GGroup group_progress;
    private GTextField text_progress;
    private GGroup group_alert;
    private GTextField text_alert;
    private GButton btn_1;
    private GButton btn_2;

    private float progressValue = 0f;

    private Action onAlertBtn1;
    private Action onAlertBtn2;

    protected override void OnInit()
    {
        //加载资源包
        UIPackage.AddPackage("UILoading/Loading");
        //创建UI面板对象
        contentPane = UIPackage.CreateObject("Loading", "LoadingView").asCom;
        //屏幕自适应
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);

        //初始化组件，添加UI事件监听
        group_progress = contentPane.GetChild("group_progress").asGroup;
        group_progress.visible = false;
        text_progress = contentPane.GetChild("text_progress").asTextField;
        SetProgress(0f);

        group_alert = contentPane.GetChild("group_alert").asGroup;
        group_alert.visible = false;
        text_alert = contentPane.GetChild("text_alert").asTextField;
        text_alert.text = "";
        btn_1 = contentPane.GetChild("btn_1").asButton;
        btn_1.onClick.Add(() =>
        {
            if (onAlertBtn1 != null)
                onAlertBtn1();
            HideAlert();
        });
        btn_2 = contentPane.GetChild("btn_2").asButton;
        btn_2.onClick.Add(() =>
        {
            if (onAlertBtn2 != null)
                onAlertBtn2();
            HideAlert();
        });
    }

    public void SetVersions(string str)
    {
        //versionsTxt.text = str;
    }

    public void SetLoadContent(string content)
    {
        //contentTxt.text = content;
    }

    public void ShowProgressWindow()
    {
        group_progress.visible = true;
    }

    public void HideProgressWindow()
    {
        group_progress.visible = false;
    }

    public void SetProgress(float value)
    {
        progressValue = value;
        text_progress.text = CommonUtils.ConnectStrs(((int)(value * 100)).ToString(), "%");
    }

    public float GetProgressValue()
    {
        return progressValue;
    }

    public void Show(bool sign)
    {
        contentPane.visible = sign;
    }

    public void ShowAlert1(string msg, string btnName, Action onBtnfunc)
    {
        text_alert.text = msg;
        btn_1.x = 540;
        btn_1.text = btnName;
        onAlertBtn1 = onBtnfunc;
        btn_2.visible = false;
        group_alert.visible = true;
    }

    public void ShowAlert2(string msg, string btnName1, Action onBtnfunc1, string btnName2, Action onBtnfunc2)
    {
        text_alert.text = msg;
        btn_1.x = 250;
        btn_1.text = btnName1;
        onAlertBtn1 = onBtnfunc1;
        btn_2.text = btnName2;
        onAlertBtn2 = onBtnfunc2;
        btn_2.visible = true;
        group_alert.visible = true;
    }

    private void HideAlert()
    {
        group_alert.visible = false;
        onAlertBtn1 = null;
        onAlertBtn2 = null;
    }
}