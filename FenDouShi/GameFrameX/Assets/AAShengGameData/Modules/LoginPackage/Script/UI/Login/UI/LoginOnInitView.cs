using DataTableFrame;
using FairyGUI;
using FGUIMail;
using login;
using msg.system;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[FGUIViewAttribute(UIViewEnum.LoginOnInitView, typeof(LoginOnInitView))]
public class LoginOnInitView : BaseView
{
    public override string PackageName => G_LoginPage.PACKAGE_NAME;
    public override string ComponentName => G_LoginPage.COMPONENT_NAME;

    protected override Dictionary<EEventType, OnEventLister> EventList => new()
    {
        { EEventType.LoginChangeService, OnChangeService },
        { EEventType.LoadAccountDataComplete, OnLoadAccountDataComplete },

    };

    G_LoginPage view;
    public LoginOnInitView()
    {
        //（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.LoginOnInitView, false);
        Logger.PrintColor("yellow", "LoginOnInitView()"); 
    }

    protected override void OnStartLoad()
    {

    }
    /// <summary>
    /// 界面加载完成,触发函数
    /// </summary>
    /// <param name="uiName"></param>
    /// <param name="gameObject">当前界面的fairyGUI对象</param>
    protected override void OnFinishLoad(GComponent gameObject)
    {
        base.OnFinishLoad(gameObject);
        this.contentPane = gameObject; 
        view = gameObject as G_LoginPage;
        gameObject.SetSize(GRoot.inst.width, GRoot.inst.height);
        gameObject.AddRelation(GRoot.inst, RelationType.Size);
        view.selserverBtn.loginAccount.text = "";
        view.selserverBtn.ServerStatueIcon.c1.SetSelectedIndex(0);
         
        view.loginContent.visible = false;
        view.createPlayerContent.visible = true;
        view.Bg.texture = new NTexture(LoadingBarController.Instance.loadingTexture);
        this.modal = true;
        FGUIOperatHandletManager.Instance.ShowAllPckageToDebug();
    }

    //初始的登入玩家信息
    private void initCreatePlayer()
    {
        view.loginContent.visible = false;
        view.createPlayerContent.visible = true;
        if (!PlayerPrefs.HasKey(CentralServerParam.PlayerNameKey))
        {
            PlayerPrefs.SetString(CentralServerParam.PlayerNameKey, "");
        }
        view.createAccount.asTextField.text = PlayerPrefs.GetString(CentralServerParam.PlayerNameKey);

    }
    protected override void OnButtonClick(GButton clickedButton)
    {
        if (view.cleanCache == clickedButton)
        {
            MessageBoxVo msgVo = new MessageBoxVo();
            msgVo.title = "提示";
            msgVo.msg = $"清理缓存将重新加载更新内容，是否继续？";
            msgVo.OkBtnfunc = () =>
            {
                CommonViewUtils.ShowTopTips("清空缓存完成！");
                MainThread.CleanGameCache();
            };
            CommonViewUtils.ShowMessageBox(msgVo);

          



        }
        else if (view.noticeBtn == clickedButton)
        {
             UIViewManager.Instance.Show(UIViewEnum.ActivityNoticeView);
        }
        else if (view.ageBtn == clickedButton)
        {
            UIViewManager.Instance.Show(UIViewEnum.InfoNoticeView, NoticeEnum.ageType);;
        }
        else if (view.userNotice == clickedButton)
        {
             UIViewManager.Instance.Show(UIViewEnum.InfoNoticeView, NoticeEnum.userNoticeType);
        }
        else if (view.privacyNoticeBtn == clickedButton)
        {
            UIViewManager.Instance.Show(UIViewEnum.InfoNoticeView, NoticeEnum.privacyType);
        }
        else if (view.changeAccountBtn == clickedButton)
        {
            MessageBoxVo msgVo = new MessageBoxVo();
            msgVo.title = "提示";
            msgVo.msg = $"即将切换账号，是否继续？";
            msgVo.OkBtnfunc = () =>
            {
                initCreatePlayer();
            };
            CommonViewUtils.ShowMessageBox(msgVo);


        }

        else if (view.createBtn == clickedButton)
        {
            string playerName = view.createAccount.asTextField.text;
            if (string.IsNullOrEmpty(playerName))
            {
                MessageBoxVo msgVo = new MessageBoxVo();
                msgVo.title = "提示";
                msgVo.msg = $"请输入账号";
                CommonViewUtils.ShowMessageBox(msgVo);
                return;
            }
            //单机模式
            UIViewManager.Instance.Hide(UIViewEnum.LoginOnInitView);
            PreloadManager.Instance.PreLoadPackage(PackageEnum.GameMainPackage);
            //联网模式
            //CentralServerLogin.LoginToCentralServer(playerName);
            Logger.PrintDebug("Click ConnectToGameSocket");
        }
        else if (view.loginBtn == clickedButton)
        {
            if (view.UserKnowBtn.selected)
            {
                MessageBoxVo msgVo = new MessageBoxVo();
                msgVo.title = "提示";
                msgVo.msg = $"尚未同意用户与隐私协议/n是否同意并继续？";
                msgVo.OkBtnfunc = () =>
                {
                    view.UserKnowBtn.selected = true;
        

                    //联网模式
                    // WebLoginModule.ConnectToGameWebSocket();
                };
                CommonViewUtils.ShowMessageBox(msgVo);
               return;
            }
            if (ServerStatu== serverStatus.tingfu|| ServerStatu == serverStatus.weihu)
            {

                MessageBoxVo msgVo = new MessageBoxVo();
                msgVo.title = "提示";
                msgVo.msg = $"当前服务器正在维护中，请留意游戏公告查看维护情况,等待维护结束或选择其它服务器进行游戏。";
                msgVo.OkBtnfunc = () =>
                {
                };
                CommonViewUtils.ShowMessageBox(msgVo);
                return;
            }

            WebLoginModule.ConnectToGameWebSocket();
        }
        else if (view.selserverBtn == clickedButton)
        {
            Logger.PrintDebug("selserverBtn onClick!");
            UIViewManager.Instance.Show(UIViewEnum.LoginSelectServerView);
            CentralServerLogin.ReqHostTextByType(RequestHostType.age_appropriate_remind, (str) =>
            {
                Logger.PrintGreen("RequestHostType.version str=" + str);
            });
        }

    }
    // 开始播放显示动画,不要动画则重写这个接口
    protected override void DoShowAnimation()
    {
        OnShown();
    }
    protected override void DoHideAnimation()
    {
        this.HideWindowImmediately();
    }
    protected override void OnShown()
    {
        base.OnShown();
        initCreatePlayer();


    }

    //主服务器信息消息回调
    private void OnLoadAccountDataComplete(EventSysArgsBase notice)
    {
        Logger.PrintDebug($"NoticeManager OnLoadAccountDataComplete()");
        if (notice is EventSysArgs<ServerInfo> args)
        {

            InitLogin(args.args1);
        }
    }
    private void InitLogin(ServerInfo serverInfo)
    {
        if (serverInfo == null)
        {
            Logger.PrintError("获取主服务器信息 serverInfo is null");
            return;
        }
        view.loginContent.visible = true;
        view.createPlayerContent.visible = false;
        Logger.PrintDebug("=================OnInit()======================");
        view.selserverBtn.loginAccount.text = serverInfo.name;
        SetServerState(serverInfo);

        //测试打印所有通告
        //  CentralServerLogin.ShowAllNoticeInfo();
    }

    //服务器数据改变回调
    private void OnChangeService(EventSysArgsBase argsBase)
    {
        if (argsBase is EventSysArgs<ServerInfo> args)
        {
            ServerInfo info = args.args1;
            SetServerState(info);
        }
    }

    serverStatus ServerStatu= serverStatus.lianghao;
    //设置服务器状态
    private int limitNum = 3;
    private void SetServerState(ServerInfo info)
    {
        if (info == null)
        {
            Logger.PrintError("SetServerState info==null");
            return;
        }
        int statu = info.status - 1;
        if (info.status > limitNum)//@@@0~3没有大于3的选择，只有4个状态，服务器5个状态
        {
            statu = limitNum;
        }

        view.selserverBtn.loginAccount.text = info.name;
        view.selserverBtn.ServerStatueIcon.c1.SetSelectedIndex(statu);
        ServerStatu = info.GetServerStatus();
    }
    protected override void OnDestroy()
    {
       
    }
}
