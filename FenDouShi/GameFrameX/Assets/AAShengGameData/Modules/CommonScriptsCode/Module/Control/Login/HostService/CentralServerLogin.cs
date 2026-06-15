using System;
using UnityEngine;
using UnityEngine.Networking;

public class CentralServerLogin
{
    //username=ccc143
    /// <summary>
    /// 请求中央服务器登录
    /// </summary>
    /// <param name="username"></param>
    public static void LoginToCentralServer(string username)
    {
        Logger.PrintDebug($"@@@@@@  LoginToCentralServer {username}");
        CentralServerParam.PlayerName = username;
        // 1. 构造表单数据（WWWForm会自动处理Content-Type）
        CentralServerParam.Instance.SendRequest(RequestHostType.CENTRAL_SERVER_URL, "", (request) =>
        {
            Logger.PrintDebug("请求链接主服务器 处理响应...");
            // 5. 处理响应
            if (request.result == UnityWebRequest.Result.Success)
            {
                //{"data":{"h5_version":"1.0.1","user_id":"ccc143","publish":null,"js_version":"1.0.1","res_version":"1.0.1","isWhiteIP":0,"default_server":{"role_info":{"level":1,"name":"初学者","head_id":1},"port":12002,"ip":"192.168.2.166","name":"R2-志斌开发服","fast_login":0,"server_id":2002,"isUser":1,"status":3},"channel_id":"test","account":"test-ccc143","token":"f7a1aa4b-b045-4293-bb3e-5afc3189be5d","res_cdn":""},
                //"status":0,"msg":"success"}
                Logger.PrintColor("yellow", "请求链接主服务器 处理响应成功...");
                Logger.PrintGreen("request.downloadHandler.text=" + request.downloadHandler.text);
                LoginResponse response = DataTableFrame.CongfigUtility.Json.ToObject<LoginResponse>(request.downloadHandler.text);
                Logger.PrintColor("yellow", "===============中央服登录成功！==============");

                Logger.PrintColor("red", $"@@response.data={response.data} @@@response={response.msg}  response.status={response.status}");

                // 保存游戏服连接信息
                PlayerPrefs.SetString(CentralServerParam.PlayerNameKey, username);
                CentralServerParam.PlayerName = username;
                CentralHostDataManager.Instance.SetPlayerName(username);

                GameLoginSessionData.Instance.SetGameServerInfo(response);
                Logger.PrintColor("yellow", $" CentralServerParam.PlayerName={CentralServerParam.PlayerName} ");
                EventManager.Instance.Dispatch(EEventType.LoadAccountDataComplete, response.data.default_server);
                Logger.PrintColor("yellow", $" EEventType.LoadAccountDataComplete ");
            }
            else
            {

                Debug.LogError($"网络错误: {request.error}");
            }

            request.Dispose();
        });

    }
    /// <summary>
    /// 请求请求服务器列表
    /// </summary>
    /// <param serviceType="1">默认请求0页，0=我登陆过的服务器，N=第几页</param>
    public static void ReqServicesList(int page = 0)
    {
        CentralServicesList.RequestServerList(page);
    }

    /// <summary>
    ///  获取http的公告
    /// </summary>
    public static void ReqHostTextByType(RequestHostType hostType, Action<string> callBakc = null)
    {
        // 2. 构造请求数据
        CentralServerParam.Instance.SendRequTextRequest(hostType, "", (request) =>
        {
            // 5. 处理响应
            if (request.result == UnityWebRequest.Result.Success)
            {
                if (callBakc != null)
                {
                    callBakc(request.downloadHandler.text);
                }

            }
            else
            {
                Logger.PrintError($"type={hostType}  请求服务器列表失败: {request.error}");
            }

            request.Dispose();
        });
    }
    ///**获取公告详情 */
    public static void ReqHostTextByType(RequestHostType hostType, string param = "", Action<string> callBakc = null)
    {
        // 2. 构造请求数据
        CentralServerParam.Instance.SendRequTextRequest(hostType, param, (request) =>
        {
            // 5. 处理响应
            if (request.result == UnityWebRequest.Result.Success)
            {
                Logger.PrintDebug($"服务器列表响应: {request.downloadHandler.text}");
                if (callBakc != null)
                {
                    callBakc(request.downloadHandler.text);
                }

            }
            else
            {
                Logger.PrintError($"请求服务器列表失败: {request.error}");
            }

            request.Dispose();
        });
    }
    public static void ShowAllNoticeInfo()
    {
        CentralServerLogin.ReqHostTextByType(RequestHostType.version, (str) =>
        {
            Logger.PrintGreen("RequestHostType.version str=" + str);
        });

        CentralServerLogin.ReqHostTextByType(RequestHostType.user_agreement, (str) =>
        {
            Logger.PrintGreen("RequestHostType.user_agreement str=" + str);
        });
        CentralServerLogin.ReqHostTextByType(RequestHostType.privacy_agreement, (str) =>
        {
            Logger.PrintGreen("RequestHostType.privacy_agreement str=" + str);
        });

        CentralServerLogin.ReqHostTextByType(RequestHostType.age_appropriate_remind, (str) =>
        {
            Logger.PrintGreen("RequestHostType.age_appropriate_remind str=" + str);
        });
        CentralServerLogin.ReqHostTextByType(RequestHostType.maintain_info, GameLoginSessionData.Instance.ServerId.ToString(), (str) =>
        {
            Logger.PrintGreen("RequestHostType.maintain_info str=" + str);
        });

        CentralServerLogin.ReqHostTextByType(RequestHostType.notice, "1", (str) =>
        {
            Logger.PrintGreen("RequestHostType.notice str=" + str);
        });

        CentralServerLogin.ReqHostTextByType(RequestHostType.notice_list, (str) =>
        {
            Logger.PrintGreen("RequestHostType.notice_list str=" + str);
        });
    }
}

