using FairyGUI;
using msg.login;
using System.Collections;
using UnityEngine;
using static UnityEngine.Mesh;

[ControlAttribute]
public class LoginControl : BaseControl<LoginControl>
{
    public  LoginModel Model { get; private set; }

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new LoginModel();
    }


    // 事件监听处理
    protected override void onEventListener()
    {
        // 登录回调
        on<LoginResp>((uint)Cmd.LoginResp, OnLoginResp);
        // 被服务器强制断开回调
        on<ForceDisconnectResp>((uint)Cmd.ForceDisconnectResp, OnForceDisconnectResp);
        // 心跳指令回调
        on<HeartbeatResp>((uint)Cmd.HeartbeatResp, OnHeartbeatResp);
    }
    ///------------------------服务返回----------------



    /// <summary>
    /// 登入成功信息返回
    /// </summary>
    /// <param name="buffer"></param>
    private bool isReconnect = false;
    private void OnLoginResp(LoginResp loginResp)
    {
        CommonViewUtils.ShowTopTips("登录成功!");
        Logger.PrintColor("yellow", $"loginResp.Account={loginResp.Account} loginResp.Token={loginResp.Ms} loginResp={loginResp.Rid} loginResp.Name={loginResp.Name} loginResp.Lv={loginResp.Lv} loginResp.Head={loginResp.Head} loginResp.firstNameState={loginResp.firstNameState} loginResp.serverOpenTime={loginResp.serverOpenTime}");
        Model.LoginResp = loginResp;
        TimeManager.SetHeartbeatServerTime((uint)(loginResp.Ms / 1000));
        UserInfoManager.Instance.SetUserInfo(loginResp);
        if (isReconnect)
        {
            UIViewManager.Instance.Hide(UIViewEnum.ReconnectView);
            ModuleManager.Instance.OnReconnect();
            isReconnect = false;
        }
        else
        {
           // ModuleManager.Instance.OnLoginSuccess();
            ControlManager.Instance.LoginSuccess();
            LoginInGameMainScene();

        }
       
        Logger.PrintColor("yellow", $"loginResp.Account={loginResp.Account} loginResp.Token={loginResp.Ms} loginResp={loginResp.Rid} loginResp.Name={loginResp.Name} loginResp.Lv={loginResp.Lv} loginResp.Head={loginResp.Head} loginResp.firstNameState={loginResp.firstNameState} loginResp.serverOpenTime={loginResp.serverOpenTime}");
    }
    private  void LoginInGameMainScene()
    {

        UIViewManager.Instance.Hide(UIViewEnum.LoginOnInitView);
        PreloadManager.Instance.PreLoadPackage(PackageEnum.GameMainPackage);
    }
    //被服务器强制断开回调
    private void OnForceDisconnectResp(ForceDisconnectResp MsgData)
    {
        Logger.PrintColor("red", "OnForceDisconnectResp() 被服务器强制断开回调");
        string result = MsgData.Msg;
        Logger.PrintColor("red", "OnForceDisconnectResp() 被服务器强制断开回调 msg=" + result);
        ModuleManager.Instance.OnLogoutSuccess();
        EventManager.Instance.Dispatch(EEventType.OnLoginError, "服务器断开连接");
        //断线重连
        UIViewManager.Instance.Show(UIViewEnum.ReconnectView);
        CommonViewUtils.ShowAlertMsgTwoBtn("提示", $"服务器断开链接，是否断线重连？", "确定", (po) =>
        {
            isReconnect = true;
            SendReloginReq();
        },
      "取消",
      (po) =>
      {
          isReconnect = false;
          ChangePackageSceneManager.Instance.ReturnToLoginScene();
      });
    }
    // 心跳指令回调
    private static void OnHeartbeatResp(HeartbeatResp MsgData)
    {
        // Logger.PrintColor("yellow", "OnHeartbeatResp() 心跳指令回调");
        TimeManager.SetHeartbeatServerTime((uint)(MsgData.Ms/1000));

    }



    // -------------------------------发送协议------------
    public void SendMsgGameLogin()
    {
        Logger.PrintColor("yellow", "===================SendMsgGameLogin()发送登录请求....==================");

        LoginReq loginReq = new LoginReq();
        loginReq.Token = GameLoginSessionData.Instance.Token;
        loginReq.Account = GameLoginSessionData.Instance.Account;
        loginReq.ServerId = GameLoginSessionData.Instance.ServerId;
        SendNetMsg((uint)Cmd.LoginReq, loginReq);
    }
    // 新增静态变量保存协程引用
    private  IEnumerator _heartbeatCoroutine;
    // 心跳协程
    private  IEnumerator HeartbeatRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            // 保持原有心跳包发送逻辑
            SendHeartbeatReq();
        }
    }

    // 心跳请求
    public  void SendHeartbeatReq()
    {
        HeartbeatReq heartbeatReq = new HeartbeatReq();
        SendNetMsg((uint)Cmd.HeartbeatReq, heartbeatReq);
    }
    //// 重新登录请求
    public  void SendReloginReq()
    {
        Logger.PrintColor("blue", "SendHeartbeatReq()  ...");
        ReloginReq reloginReq = new ReloginReq();
        reloginReq.Token = GameLoginSessionData.Instance.Token;
        reloginReq.Account = GameLoginSessionData.Instance.Account;
        reloginReq.ServerId = GameLoginSessionData.Instance.ServerId;
        SendNetMsg((uint)Cmd.ReloginReq, reloginReq);
    }

    //-----------------------------方法触发--------------------------
    //登入成功
    public  void LoginComplete()
    {
        CommonViewUtils.ShowTopTips("连接webSocket成功!");
        Heartbeat();
        SendMsgGameLogin();
    }
    /// <summary>
    /// 申请心跳包
    /// </summary>
    private void Heartbeat()
    {
        if (_heartbeatCoroutine != null)
        {
            MainThread.Instance.StopCoroutine(_heartbeatCoroutine);
        }
        _heartbeatCoroutine = HeartbeatRoutine();
        MainThread.Instance.StartCoroutine(_heartbeatCoroutine);

    }
    /// <summary>
    /// 停止心跳包
    /// </summary>
    private void HeartbeatStop()
    {
        if (_heartbeatCoroutine != null)
        {
            MainThread.Instance.StopCoroutine(_heartbeatCoroutine);
            _heartbeatCoroutine = null; // 记得置空防止内存泄漏
        }

    }

    //断开网络连接
    public void OnLoginOut()
    {
        HeartbeatStop();
        Logger.PrintColor("red", "断开服务器连接");
    }




    // 清理数据调用
    protected override void onClear()
    {
    }


}
