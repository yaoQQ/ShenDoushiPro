using FairyGUI;
using FairyGUI.Common;
using msg.onHook;
using OnHookUI;
using ShengBag;
using System.Collections.Generic;

//[FGUIViewAttribute(UIViewEnum.GameMainView, typeof(GameMainView))]
public class GameMainView : BaseView
{
    public override string PackageName => G_GameMainView.PACKAGE_NAME;
    public override string ComponentName => G_GameMainView.COMPONENT_NAME;
    G_GameMainView view;
    public GameMainView()
    {
        ShengBagBinder.BindAll();
        //（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.GameMainView, false);
        Logger.PrintColor("yellow", "GameMainView()");
    }

    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {
        // 挂机
        { EEventType.OnHookGetInfoResp, OnHookResp },
        { EEventType.OnHookSpeedUpResp, OnHookResp },
        { EEventType.OnHookGainRewardResp, OnHookResp },
    };

    /// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="uiName"></param>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
        Logger.PrintColor("yellow", $" onLoadUIEnd complte!! gameObject={gameObject}");
        this.contentPane = gameObject.asCom;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        view = gameObject as G_GameMainView;

        Logger.PrintColor("red", $"GameMainView OnInit()  this.contentPane={this.contentPane}");
        view.BagBtn.onClick.Add(() =>
        {
            //CommonViewUtils.ShowTopTips("点击背包按钮");
            //UIViewManager.Instance.Show(UIViewEnum.LoginSelectServerView);
            CommonViewUtils.ShowTopTips("点击红点测试按钮");

            UIViewManager.Instance.Show(UIViewEnum.RedPointTestView);
        });
        //contentPane.GetChild("BagBtn").asButton.onClick.Add(() =>
        //{
        //    CommonViewUtils.ShowTopTips("点击背包按钮");
        //    UIViewManager.Instance.Show(UIViewEnum.LoginSelectServerView);
        //});
        view.roleButton.onClick.Add(() =>
        {
            CommonViewUtils.ShowTopTips("点击玩家信息");
            UIViewManager.Instance.Show(UIViewEnum.RoleLevelPlayerInfoView);
            UIViewManager.Instance.Hide(UIViewEnum.GameMainView);
        });
        view.fightBtn.onClick.Add(() =>
        {
            CommonViewUtils.ShowTopTips("点击战斗按钮");
            ChangePackageSceneManager.Instance.ToFightScene();
        });
        view.returnBtn.onClick.Add(() =>
        {
            CommonViewUtils.ShowTopTips("点击返回按钮");
            ChangePackageSceneManager.Instance.ReturnToLoginScene();
        });

        view.heroButton.onClick.Add(() =>
        {
            CommonViewUtils.ShowTopTips("点击英雄按钮");
            UIViewManager.Instance.Show(UIViewEnum.ChatView);
        });

        view.bagButton.title = "GM";
        view.bagButton.onClick.Add(() =>
        {
            CommonViewUtils.ShowTopTips("点击背包按钮");
            UIViewManager.Instance.Show(UIViewEnum.GmMainView);
        });
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (view == null) return;

        if (clickedButton == view.HookBtn)
        {
            // TODO
            //if (HookControl.Instance.IsSystemOpen())
            {
                UIViewManager.Instance.Show(UIViewEnum.OnHookView_Main);
            }
        }
    }


    /// <summary>
    ///fairyGUI(Window) 展示界面播放完动画后触发函数 
    /// </summary>
    protected override void OnShown()
    {
        base.OnShown();
        CommonViewUtils.ShowTopTips("这只是一个临时测试主场景！");
        //已经创建过角色了
        //if (UserInfoManager.Instance.userInfo.firstNameState == 1)
        //{

        //}
        //else 
        //{
        //    UIViewManager.Instance.Show(UIViewEnum.CreatePlayerView);
        //}

        RefreshOnHookBtn();
    }
    /// <summary>
    ///fairyGUI(Window) 关闭界面播放完动画后触发函数 
    /// </summary>
    override protected void OnHide()
    {
        base.OnHide();
        this.HideWindowImmediately();
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(G_OnHookView_Main.COMPONENT_NAME);
    }

    protected override void OnDestroy()
    {
        //view.BagBtn.Dispose();
        Logger.PrintColor("red", "@@@@@@@@@@@@@@@@@@@@@@bagGifBtn OnDestroy()");
    }

    void OnHookResp(EventSysArgsBase argsBase) => RefreshOnHookBtn();

    // 挂机
    void RefreshOnHookBtn()
    {
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(G_OnHookView_Main.COMPONENT_NAME);
        if (view == null) return;

        // TODO
        //if (HookControl.Instance.IsSystemOpen())
        //{
        //    view.OpenBtn.visible = false;
        //    return;
        //}
        view.HookBtn.visible = true;

        var btn = view.HookBtn as G_OnHookView_EntranceBtn;
        if (btn == null) return;

        float progress = OnHookControl.Instance.Model.progressBar;
        bool isFull = OnHookControl.Instance.Model.TimeIsFull();
        if (progress >= 1)
        {
            btn.Slider.value = 100;
        }
        else
        {
            btn.Slider.value = progress * 100;

            RefreshOnHookBtn_TimeStr();
            GlobalTimeManager.Instance.timerController.AddTimer(G_OnHookView_Main.COMPONENT_NAME, 1000, int.MaxValue, RefreshOnHookBtn_TimeStr);
        }

        btn.time.visible = !isFull;
        btn.full.visible = !btn.time.visible;

        btn.state_1.visible = progress < 0.5f;
        btn.state_2.visible = 0.5f <= progress && progress < 1;
        btn.state_3.visible = 1 <= progress;
    }

    void RefreshOnHookBtn_TimeStr(int obj = 0)
    {
        if (view == null) return;
        if (view.HookBtn is not G_OnHookView_EntranceBtn btn) return;

        if (OnHookControl.Instance.Model.TimeIsFull())
        {
            RefreshOnHookBtn();
        }
        else
        {
            btn.time.text = OnHookControl.Instance.Model.GetTimeString();
        }
    }
}
