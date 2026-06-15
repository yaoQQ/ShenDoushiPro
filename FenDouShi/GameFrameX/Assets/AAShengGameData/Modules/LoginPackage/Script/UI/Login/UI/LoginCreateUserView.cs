//using DataTableFrame;
//using FairyGUI;
//using login;
//using System.Collections.Generic;
//using UnityEngine;

//public class LoginCreateUserView : BaseView
//{
//    public override string PackageName => G_CreatePlayer.PACKAGE_NAME;
//    public override string ComponentName => G_CreatePlayer.COMPONENT_NAME;

//    protected override Dictionary<EEventType, OnEventLister> EventList => new()
//    {
//        { EEventType.LoadAccountDataComplete, OnLoadAccountDataComplete },
//    };

//    public LoginCreateUserView()
//    {
//        //（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
//        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.LoginCreateUserView, false);
//    }

//    /// <summary>
//    /// 界面加载完成,触发函数
//    /// </summary>
//    /// <param name="uiName"></param>
//    /// <param name="gameObject">当前界面的fairyGUI对象</param>
//    protected override void OnFinishLoad(GComponent gameObject)
//    {
//        Logger.PrintDebug("=================onLoadUIEnd()======================");
//        Logger.PrintColor("yellow", $" onLoadUIEnd complte!! gameObject={gameObject}");
//        this.contentPane = gameObject.asCom;
//        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
//        contentPane.AddRelation(GRoot.inst, RelationType.Size);
//        this.modal = true;

//    }
//    /// <summary>
//    ///  //fairyGUI初始化完成，触发的方法。一般初始化和获取对象在此方法
//    /// </summary>
//    protected override void OnShown()
//    {
//        base.OnShown();
//        if (PlayerPrefs.HasKey(CentralServerParam.PlayerNameKey))
//        {
//            PlayerPrefs.GetString(CentralServerParam.PlayerNameKey);
//        }
//        else
//        {
//            PlayerPrefs.SetString(CentralServerParam.PlayerNameKey, "");
//        }
//        contentPane.GetChild("account").asTextField.text = PlayerPrefs.GetString(CentralServerParam.PlayerNameKey);

//        contentPane.GetChild("createBtn").asButton.onClick.Add(() =>
//        {
//            string playerName = contentPane.GetChild("account").asTextField.text;
//            if (string.IsNullOrEmpty(playerName))
//            {
//                CommonViewUtils.ShowAlertMsg("提示", "请输入账号", "确定", (po) => { });
//                return;
//            }

//            CentralServerLogin.StartLogin(playerName);
//            Logger.PrintDebug("Click ConnectToGameSocket");

//        });
//    }


//    private void OnLoadAccountDataComplete(EventSysArgsBase notice)
//    {
//        Logger.PrintDebug($"NoticeManager OnLoadAccountDataComplete()");
//        UIViewManager.Instance.Hide(UIViewEnum.LoginCreateUserView);
//        UIViewManager.Instance.Show(UIViewEnum.LoginOnInitView);
//    }

//    /// <summary>
//    ///fairyGUI(Window) 关闭界面播放完动画后触发函数 
//    /// </summary>
//    override protected void OnHide()
//    {
//        base.OnHide();
//        Logger.PrintDebug("=================OnHide()======================");
//        HideImmediately();
//    }
//}
