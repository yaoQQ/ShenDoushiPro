using msg.login;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class LoginModule : BaseModule
{

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
        Debug.Log("OnNetMsgLister=" + protoIDInt);
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
    private static void OnLoginResp(byte[] MsgData)
    {
        CommonViewUtils.ShowTopTips("登录成功!");
        LoginResp loginResp = ProtobufTool.PDeserialize<LoginResp>(MsgData);
        Logger.PrintColor("yellow", $"loginResp.Account={loginResp.Account} loginResp.Token={loginResp.Ms} loginResp={loginResp.Rid} loginResp.Name={loginResp.Name} loginResp.Lv={loginResp.Lv} loginResp.Head={loginResp.Head} loginResp.firstNameState={loginResp.firstNameState} loginResp.serverOpenTime={loginResp.serverOpenTime}");
        UIViewManager.Instance.Hide(UIViewEnum.LoginOnInitView);
        PreloadManager.Instance.PreLoadPackage(PackageEnum.GameMainPackage);
    }
    private static void OnForceDisconnectResp(byte[] MsgData)
    {
        Logger.PrintColor("red", "OnForceDisconnectResp() 被服务器强制断开回调");
        //LoginResp loginResp = new LoginResp();
    }
    private static void OnHeartbeatResp(byte[] MsgData)
    {
       // Logger.PrintColor("yellow", "OnHeartbeatResp() 心跳指令回调");
    }

    //========================连接游戏服务器=========
    /// <summary>
    /// 尝试连接游戏服务器的网络套接字
    /// </summary>
    public static void ConnectToGameSocket()
    {
        // 从会话数据获取服务器连接信息
        string serverUrl = GameLoginSessionData.Instance.ServerIp;
        string gameToken = GameLoginSessionData.Instance.Token;
        int port = GameLoginSessionData.Instance.ServerPort;

;
        if (string.IsNullOrEmpty(serverUrl)||string.IsNullOrEmpty(gameToken))
        {
            Logger.PrintError($"serverUrl={serverUrl} or gameToken={gameToken} is null");
            return;
        }
        // 示例连接地址如ws://192.168.2.230:9090
        Logger.PrintColor("yellow", $"开始连接游戏服务器 {serverUrl}:{port} serviceID={GameLoginSessionData.Instance.ServerId} serverName={GameLoginSessionData.Instance.ServerName}...");
        NetworkManager.Instance.Connect(GameLoginSessionData.Instance.ServerId.ToString(), serverUrl, port, (NetworkConnect.ConnectError er) =>
        {
            switch (er)
            {
                case NetworkConnect.ConnectError.None:
                    Logger.PrintColor("yellow", $"=============连接游戏服务器Socket成功==================");
                    SendMsgGameLogin();
                    // 启动心跳协程
                    MainThread.Instance.StartCoroutine(HeartbeatRoutine());
                    break;
                case NetworkConnect.ConnectError.Connected:
                    Logger.PrintColor("yellow", $" 已经连接过了");
                    CommonViewUtils.ShowTopTips("已经连接过了");
                    break;
                case NetworkConnect.ConnectError.NotReachable:
                    Logger.PrintColor("yellow", $"无法访问");
                    MessageBoxVo msgVo = new MessageBoxVo();
                    msgVo.title = "提示";
                    msgVo.msg = $"无法访问!";
                    msgVo.OkBtnfunc = () =>
                    {
                    };
                    CommonViewUtils.ShowMessageBox(msgVo);
                    break;
                case NetworkConnect.ConnectError.SocketError:
                    Logger.PrintColor("yellow", $"Socket连接出错");
                    MessageBoxVo msgVo2 = new MessageBoxVo();
                    msgVo2.title = "提示";
                    msgVo2.msg = $"Socket连接出错!";
                    msgVo2.OkBtnfunc = () =>
                    {
                    };
                    CommonViewUtils.ShowMessageBox(msgVo2);
                    break;
                case NetworkConnect.ConnectError.Cancel:
                    Logger.PrintColor("yellow", $"连接被取消");
                    MessageBoxVo msgVo3 = new MessageBoxVo();
                    msgVo3.title = "提示";
                    msgVo3.msg = $"连接被取消!";
                    msgVo3.OkBtnfunc = () =>
                    {
                    };
                    CommonViewUtils.ShowMessageBox(msgVo3);
                    break;

            }
        });
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
        byte[] pDatabuff = ProtobufTool.PSerializer(loginReq);
        NetworkManager.Instance.SendMessage((uint)Cmd.LoginReq, pDatabuff);

    }
    // 心跳协程
    private static IEnumerator HeartbeatRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            if (NetworkManager.Instance.IsConnected())
            {
                SendHeartbeatReq();
            }
        }
    }

    // 心跳请求
    public static void SendHeartbeatReq()
    {
        Logger.PrintColor("blue", "SendHeartbeatReq()  ...");
        HeartbeatReq heartbeatReq = new HeartbeatReq();
        byte[] pDatabuff = ProtobufTool.PSerializer(heartbeatReq);
        NetworkManager.Instance.SendMessage((uint)Cmd.HeartbeatReq, pDatabuff);
    }
    //// 重新登录请求
    public static void SendReloginReq()
    {
        Logger.PrintColor("blue", "SendHeartbeatReq()  ...");
        ReloginReq reloginReq = new ReloginReq();
        reloginReq.Token =GameLoginSessionData.Instance.Token;
        reloginReq.Account = GameLoginSessionData.Instance.Account;
        reloginReq.ServerId = GameLoginSessionData.Instance.ServerId;
        byte[] pDatabuff = ProtobufTool.PSerializer(reloginReq);
        NetworkManager.Instance.SendMessage((uint)Cmd.HeartbeatReq, pDatabuff);
    }

    public override List<int> GetRegisterNotificationList()
    {
        if (notificationList == null)
        {
            notificationList = new List<int>();

            //notificationList.Add(LoginNotice.LoginComplete);

        }
        return notificationList;
    }
    public override void OnNotificationLister(int noticeType, EventSysArgsBase notice)
    {
        switch (noticeType)
        {

            //case LoginNotice.LoginComplete:
            //    break;
        }
    }
}