using msg.login;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityWebSocket;

public class WebLoginModule : BaseModule
{
    public static long roleId;

    public override ModuleEnum ModuleName()
    {
        return ModuleEnum.Login;
    }
    public override void InitRegisterNet()
    {
        RegisterNetMsg((uint)Cmd.LoginResp);
        RegisterNetMsg((uint)Cmd.ForceDisconnectResp);
        RegisterNetMsg((uint)Cmd.HeartbeatResp);
    }
    public override void OnNetMsgLister(uint protoIDInt, byte[] buffer)
    {
     //   Logger.PrintDebug("OnNetMsgLister=" + protoIDInt);
        Cmd protoID = (Cmd)protoIDInt;
        switch (protoID)
        {
            case Cmd.LoginResp: // 登录回调
                OnLoginResp(buffer);
                break;
            case Cmd.ForceDisconnectResp: // 被服务器强制断开回调
                OnForceDisconnectResp(buffer);

                break;
            case Cmd.HeartbeatResp:// 心跳指令回调
                OnHeartbeatResp(buffer);
                break;
        }
    }
    /// <summary>
    /// 申请心跳包
    /// </summary>
    private static void Heartbeat()
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
    private static void HeartbeatStop()
    {
        if (_heartbeatCoroutine != null)
        {
            MainThread.Instance.StopCoroutine(_heartbeatCoroutine);
            _heartbeatCoroutine = null; // 记得置空防止内存泄漏
        }

    }

    public static bool isReconnect = false;
    private static void OnLoginResp(byte[] MsgData)
    {
        CommonViewUtils.ShowTopTips("登录成功!");
        Logger.PrintColor("yellow","=======登录成功!======");

        if (isReconnect)
        {
            UIViewManager.Instance.Hide(UIViewEnum.ReconnectView);
            ModuleManager.Instance.OnReconnect();
            isReconnect=false;
        }
        else
        {
            ModuleManager.Instance.OnLoginSuccess();
            ControlManager.Instance.LoginSuccess();
            LoginInGameMainScene();

        }
        LoginResp loginResp = ProtobufTool.PDeserialize<LoginResp>(MsgData);
        UserInfoManager.Instance.SetUserInfo(loginResp);
        roleId = loginResp.Rid;
        Logger.PrintColor("yellow", $"loginResp.Account={loginResp.Account} loginResp.Token={loginResp.Ms} loginResp={loginResp.Rid} loginResp.Name={loginResp.Name} loginResp.Lv={loginResp.Lv} loginResp.Head={loginResp.Head} loginResp.firstNameState={loginResp.firstNameState} loginResp.serverOpenTime={loginResp.serverOpenTime}");
    }
    public static void LoginInGameMainScene()
    {
        UIViewManager.Instance.Hide(UIViewEnum.LoginOnInitView);
        PreloadManager.Instance.PreLoadPackage(PackageEnum.GameMainPackage);
    }
    private static void OnForceDisconnectResp(byte[] MsgData)
    {
      
        ForceDisconnectResp loginResp = ProtobufTool.PDeserialize<ForceDisconnectResp>(MsgData);
        string result = loginResp.Msg;
        Logger.PrintColor("red", "OnForceDisconnectResp() 被服务器强制断开回调 msg="+ result);
        ModuleManager.Instance.OnLogoutSuccess();
        EventManager.Instance.Dispatch(EEventType.OnLoginError, "服务器断开连接");

        //断线重连
        UIViewManager.Instance.Show(UIViewEnum.ReconnectView);
        CommonViewUtils.ShowAlertMsgTwoBtn("提示", $"服务器断开链接，是否断线重连？", "确定", (po) =>
        {
            isReconnect = true;
            WebLoginModule.SendReloginReq();
        },
      "取消",
      (po) =>
      {
          isReconnect= false;
          ChangePackageSceneManager.Instance.ReturnToLoginScene();
      });
    }
    private static void OnHeartbeatResp(byte[] MsgData)
    {
       // Logger.PrintColor("yellow", "OnHeartbeatResp() 心跳指令回调");
    }

    //========================连接游戏服务器=========
    /// <summary>
    /// 尝试连接游戏服务器的网络套接字
    /// </summary>
    public static void ConnectToGameWebSocket()
    {
        // 从会话数据获取服务器连接信息
        string serverUrl = GameLoginSessionData.Instance.ServerIp;
        string gameToken = GameLoginSessionData.Instance.Token;
        int port = GameLoginSessionData.Instance.ServerPort;

        ;
        if (string.IsNullOrEmpty(serverUrl) || string.IsNullOrEmpty(gameToken))
        {
            Logger.PrintError($"serverUrl={serverUrl} or gameToken={gameToken} is null");
            return;
        }
        // 示例连接地址如ws://192.168.2.230:9090
        Logger.PrintColor("yellow", $"开始连接游戏服务器 {serverUrl}:{port} serviceID={GameLoginSessionData.Instance.ServerId} serverName={GameLoginSessionData.Instance.ServerName}...");
        UnityWebSocketManager.Instance.Connect($"ws://{serverUrl}:{port}");
    }

    //// 登录请求
    public static void SendMsgGameLogin()
    {
        Logger.PrintColor("yellow", "===================SendMsgGameLogin()发送登录请求....==================");

        LoginReq loginReq = new LoginReq();
        loginReq.Token = GameLoginSessionData.Instance.Token;
        loginReq.Account = GameLoginSessionData.Instance.Account;
        loginReq.ServerId = GameLoginSessionData.Instance.ServerId;

        Logger.PrintColor("white", $"发送协议数据 token={loginReq.Token} account={loginReq.Account} serverId={loginReq.ServerId}");
        Logger.PrintColor("white", $"发送协议数据 typeof(ProtobufTool)={typeof(ProtobufTool)} ");
        byte[] pDatabuff = ProtobufTool.PSerializer(loginReq);
        Logger.PrintColor("white", $"发送协议数据 pDatabuff={pDatabuff.Length}");
        Logger.PrintColor("white", $"发送协议数据 UnityWebSocketManager.Instance={UnityWebSocketManager.Instance}");
        UnityWebSocketManager.Instance.SendAsync((uint)Cmd.LoginReq, pDatabuff);
    }
    // 新增静态变量保存协程引用
    private static IEnumerator _heartbeatCoroutine;
    // 心跳协程
    private static IEnumerator HeartbeatRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
                // 保持原有心跳包发送逻辑
                SendHeartbeatReq();
        }
    }

    // 心跳请求
    public static void SendHeartbeatReq()
    {
        HeartbeatReq heartbeatReq = new HeartbeatReq();
        byte[] pDatabuff = ProtobufTool.PSerializer(heartbeatReq);
        UnityWebSocketManager.Instance.SendAsync((uint)Cmd.HeartbeatReq, pDatabuff);
    }
    //// 重新登录请求
    public static void SendReloginReq()
    {
        Logger.PrintColor("blue", "SendHeartbeatReq()  ...");
        ReloginReq reloginReq = new ReloginReq();
        reloginReq.Token = GameLoginSessionData.Instance.Token;
        reloginReq.Account = GameLoginSessionData.Instance.Account;
        reloginReq.ServerId = GameLoginSessionData.Instance.ServerId;

        byte[] pDatabuff = ProtobufTool.PSerializer(reloginReq);
        UnityWebSocketManager.Instance.SendAsync((uint)Cmd.ReloginReq, pDatabuff);
    }

    public override List<int> GetRegisterNotificationList()
    {
        if (notificationList == null)
        {
            notificationList = new List<int>();

            notificationList.Add((int)EEventType.Login_connect_Success);
            notificationList.Add((int)EEventType.Login_connect_Out);
            notificationList.Add((int)EEventType.OnLoginError);
        }
        return notificationList;
    }

    public override void OnNotificationLister(int noticeType, EventSysArgsBase notice)
    {
        Logger.PrintColor("blue", "$$$$$$$$$$$$$$$$$noticeType=" + noticeType);
        Logger.PrintColor("blue", "$$$$$$$$$$$$$$$$$EventSysArgsBase=" + notice);
        switch (noticeType)
        {

            case (int)EEventType.Login_connect_Success:
                LoginComplete(notice);
                break;
            case (int)EEventType.Login_connect_Out:
                OnLoginOut(notice);
                break;
            case (int)EEventType.OnLoginError:
                OnLoginError(notice);
                break;
        }
    }
    //登入成功
    private static void LoginComplete(EventSysArgsBase rep)
    {
        CommonViewUtils.ShowTopTips("连接webSocket成功!");
        Heartbeat();
        SendMsgGameLogin();
    }
    //断开网络连接
    private static void OnLoginOut(EventSysArgsBase rep)
    {
        HeartbeatStop();
        Logger.PrintColor("red", "断开服务器连接");
    }

    private void OnLoginError(EventSysArgsBase notice)
    {
        if (notice == null)
        {
            return;
        }
        var msg = notice as EventSysArgs<string>;
        MessageBoxVo msgVo3 = new MessageBoxVo();
        msgVo3.title = "提示";
        msgVo3.msg = $"连接错误!";
        msgVo3.OkBtnfunc = () =>
        {
        };
        CommonViewUtils.ShowMessageBox(msgVo3);
    }



}