using common;
using FairyGUI;
using login;
using System;
using System.Collections.Generic;

public class MessageBoxVo//传入的信息类
{
    //标题
    public string title = "提示";
    //消息内容
    public string msg = "消息内容";
    //按钮名称
    public string OkBtnName = "确定";
    public string CancelBtnName = "取消";

    //下方不再提醒
    public string TipStr = "本次登录不再提示";

    //右侧按钮上方文本
    public string RightText = "";

    //隐藏取消按钮
    public bool HideCancelBtn = false;
    //今日不在提示key
    public string isCheckNoShowTodayKey = "";
    //点击空白区域关闭
    public bool ClickAreaClose = true;

    //确认按钮点击回调
    public Action OkBtnfunc;
    //取消按钮点击回调
    public Action CancelBtnfunc;

    //不再提醒类型
    public ECheckNoShowState CheckNoShowState = ECheckNoShowState.Login;
}

/// <summary>
/// 不再提醒类型
/// </summary>
public enum ECheckNoShowState { 

    /// <summary>
    /// 登陆不再提醒
    /// </summary>
    Login,

    /// <summary>
    /// 今日不再提醒
    /// </summary>
    Today,
}


[FGUIViewAttribute(UIViewEnum.MessageBoxView, typeof(MessageBoxView))]
public class MessageBoxView : BaseView
{
    public override string PackageName => G_MessageBox.PACKAGE_NAME;
    public override string ComponentName => G_MessageBox.COMPONENT_NAME;
    public override bool IsPreload => true;
    G_MessageBox view;


    private bool isAllowClose = false;
    //弹窗数据
    private MessageBoxVo messageBoxVo = null;

    public MessageBoxView()
    {
        //声明界面{fairyGUI所在包:包里面Componet名字}
        //（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
        setViewAttribute(UIViewLayerType.Pop_view, UIViewEnum.MessageBoxView, false);
        Logger.PrintColor("yellow", "MessageBoxView()");
    }

    /// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="uiName">界面名</param>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
        Logger.PrintColor("yellow", $"onLoadUIEnd complte!! gameObject={gameObject}");
        this.contentPane = gameObject.asCom; ;
        this.Center();
        view = this.contentPane as G_MessageBox;
        this.modal = true;//@@@@待添加，点击黑屏自动取消弹窗功能
        ////调用fairyGUI的Window方法展示界面,加载完直接展示
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == view.OkBtn)
        {
            if (view.todayTips.visible && view.todayCheck.selected)
            {
                //今日不再提示
                CommonViewUtils.NoTipsList.Add(messageBoxVo.isCheckNoShowTodayKey);
                if (messageBoxVo.CheckNoShowState == ECheckNoShowState.Today)
                {
                    PlayerPrefsTools.SetServerTimeStr(messageBoxVo.isCheckNoShowTodayKey);
                }
            }
            OnConfirmFun();
        }
        else if (clickedButton == view.CloseBtn)
        {
            OnCancelFun();
        }
        else if (clickedButton == view.CancelBtn)
        {
            OnCancelFun();
        }
        else if (clickedButton == view.todayCheck)
        {

        }
    }
    protected override void OnShown()
    {
        base.OnShown();
        messageBoxVo = (MessageBoxVo)showArgs;
        //设置消息弹窗内容
        view.Title.text = messageBoxVo.title;
        view.Content.text = messageBoxVo.msg;
        view.rightText.text = messageBoxVo.RightText;
        view.OkBtn.title.SetVar("title", messageBoxVo.OkBtnName).FlushVars();
        view.CancelBtn.title.SetVar("title", messageBoxVo.CancelBtnName).FlushVars();
        view.tipsText.text = messageBoxVo.TipStr;
        //是否隐藏取消按钮
        view.CancelBtn.visible = !messageBoxVo.HideCancelBtn;
        // view.CloseBtn.visible = messageBoxVo.ClickAreaClose;

        //今日不在提示
        if (!string.IsNullOrEmpty(messageBoxVo.isCheckNoShowTodayKey))
        {
            view.todayCheck.selected = false;
            view.todayTips.visible = true;
        }
        else
        {
            //隐藏今日提示
            view.todayTips.visible = false;
        }
    }

    /// <summary>
    /// 确定按钮点击
    /// </summary>
    private void OnConfirmFun()
    {
        UIViewManager.Instance.Hide(UIViewEnum.MessageBoxView);
        if (messageBoxVo != null && messageBoxVo.OkBtnfunc != null)
        {
            messageBoxVo.OkBtnfunc();
        }
    }
    /// <summary>
    /// 取消按钮点击
    /// </summary>
    private void OnCancelFun()
    {
        UIViewManager.Instance.Hide(UIViewEnum.MessageBoxView);
        if (messageBoxVo != null && messageBoxVo.CancelBtnfunc != null)
        {
            messageBoxVo.CancelBtnfunc();
        }
    }

}
