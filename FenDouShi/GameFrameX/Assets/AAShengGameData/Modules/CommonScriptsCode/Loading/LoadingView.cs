using FairyGUI;
using launch;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class LoadingView
{
    // private GButton enterBtn;
    private G_LauchNoticeView noticeWin;


    private Action onAlertBtn1;
    private Action onAlertBtn2;
    private TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

    private GComponent contentPane;
    private float progressValue = 0f;
    public Action LoadCallBakck;
    public  G_LaunchPage view;
  
    public LoadingView(Action loadCallBakck)
    {
        this.LoadCallBakck = loadCallBakck;
        launchBinder.BindAll();
        var op = UIPackage.CreateObject(G_LaunchPage.PACKAGE_NAME, G_LaunchPage.COMPONENT_NAME);
        Logger.PrintColor("yellow", $"CreateObjectAsync login op=" + op);
        contentPane = op.asCom;
        OnInit(contentPane);
        Stage.inst.AddChild(op.displayObject);
        if (LoadCallBakck != null)
        {
            LoadCallBakck();
        }

    }
    protected void OnInit(GComponent contentPane)
    {

        //初始化组件，添加UI事件监听
        view = contentPane as G_LaunchPage;
        noticeWin = view.LauchNoticeView;
        noticeWin.visible = false;
        view.Bg.texture = new NTexture(LoadingBarController.Instance.loadingTexture);
        InitNoticeView();
    }


    private void InitNoticeView()
    {
        var noticeText = noticeWin.noticeText;
        var backBtn = noticeWin.backBtn;
        var okBtn = noticeWin.okBtn;
        noticeText.text = "初始化异常，请尝试重新登录";
        okBtn.onClick.Add(() =>
        {
            if (onAlertBtn1 != null)
                onAlertBtn1();
            this.tcs.SetResult(true);
            HideAlert();
        });

        backBtn.onClick.Add(() =>
        {
            noticeWin.visible = false;
        });
    }
    public void ShowEnterBtn()
    {
        // enterBtn.visible = true;
    }
    public void ShowOneButton(string msg, Action okClick)
    {
        if (noticeWin == null) return;

        noticeWin.visible = true;
        noticeWin.SetXY(350, 200);

        view.LauchNoticeView.noticeText.text = msg;
        view.LauchNoticeView.okBtn.onClick.Add(() =>
        {
            Logger.PrintDebug("noticeWin button click");
            okClick?.Invoke();
        });

    }

    public void SetVersions(string str)
    {
        //  versionsTxt.text = str;
    }

    string contentStr;
    public void SetLoadContent(string content)
    {
        if (view.text_notice != null)
        {
            view.text_notice.text = content;
        }
    }

    public void ShowProgressWindow()
    {
        view.updateProgress.visible = true;
    }

    public void HideProgressWindow()
    {
        view.updateProgress.visible = false;
    }

    public void SetProgress(float percent)
    {
        int IntValue = (int)(percent * 100);
        view.updateProgress.value = IntValue;
        view.updateTxt.text =  Utility.Platform.ConnectStrs(", ", IntValue.ToString(), "%");
        progressValue = percent;
    }

    public float GetProgressValue()
    {
        return progressValue;
    }

    public void Show()
    {
        contentPane.visible = true;
    }
    public void Hide()
    {
        contentPane.visible = false;
        // enterBtn.visible = false;
         noticeWin.visible= false;
    }

    public void ShowAlert1(string msg, string btnName, Action onBtnfunc)
    {
        noticeWin.noticeText.text = msg;
        noticeWin.okBtn.x = 265;
        noticeWin.okBtn.text = btnName;
        onAlertBtn1 = onBtnfunc;
        noticeWin.backBtn.visible = false;
         noticeWin.visible = true;
    }

    public void ShowAlert2(string msg, string btnName1, Action onBtnfunc1, string btnName2, Action onBtnfunc2)
    {
        noticeWin.noticeText.text = msg;
        noticeWin.okBtn.x = 424;
        noticeWin.okBtn.text = btnName1;
        onAlertBtn1 = onBtnfunc1;
        noticeWin.backBtn.text = btnName2;
        onAlertBtn2 = onBtnfunc2;
        noticeWin.backBtn.visible = true;
         noticeWin.visible = true;
    }

    private void HideAlert()
    {
         noticeWin.visible = false;
        onAlertBtn1 = null;
        onAlertBtn2 = null;
    }
    public async Task WaitForResponse()
    {
        await tcs.Task;
    }

}
