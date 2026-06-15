using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static ServerListResponse;

public class CentralServicesList
{


    /// <summary>
    /// 请求服务器列表
    /// </summary>
    /// <param name="username">用户名</param>
    /// <param name="page">页码：0=登录过的服务器，1=第一页</param>
    /// page=0{"data":{"servers":[{"role_info":{"level":1,"name":"初学者","head_id":1},"port":12002,"ip":"192.168.2.166","name":"R2-志斌开发服","fast_login":0,"server_id":2002,"isUser":1,"status":3}],"page_num":0,"max_page_num":1,"page_size":50},"status":0,"msg":"success"}
    /// page=1{"data":{"servers":[{"role_info":{"level":1,"name":"初学者","head_id":1},"port":12002,"ip":"192.168.2.166","name":"R2-志斌开发服","fast_login":0,"server_id":2002,"isUser":1,"status":3}],"page_num":1},"status":0,"msg":"success"}
    public static void RequestServerList(int page = 0)
    {
        // 2. 构造请求数据
        CentralServerParam.Instance.SendRequest(RequestHostType.FULL_SERVER_LIST_URL, page.ToString(), (request) => {
            // 5. 处理响应
            if (request.result == UnityWebRequest.Result.Success)
            {
                Logger.PrintDebug($"服务器列表响应: {request.downloadHandler.text}");
                ServerListResponse response = DataTableFrame.CongfigUtility.Json.ToObject<ServerListResponse>(request.downloadHandler.text);
                Logger.PrintDebug($"共 response.data={response.data}  response.msg={response.msg} response.status={response.status}页");

                if (response.status == 0)
                {
                    Logger.PrintColor("green", "服务器列表获取成功!");
                    Logger.PrintDebug($"共 {response.data.page_num} 个服务器, {response.data.max_page_num} 页");

                    // 处理服务器列表
                    GameLoginSessionData.Instance.cacheServerList(response, page);
                    EventManager.Instance.Dispatch(EEventType.LoadServiceZoneDataComplete);
                    // 打印服务器信息
                    foreach (ServerInfo server in response.data.servers)
                    {
                        Logger.PrintColor("white", $"[{server.server_id}] {server.name} - {server.ip}:{server.port} (状态: {server.GetStatusText()})");
                    }
                }
                else
                {
                    Logger.PrintError($"获取服务器列表失败: {response.status} - {response.msg}");
                }

            }
            else
            {
                Logger.PrintError($"请求服务器列表失败: {request.error}");
            }

            request.Dispose();
        });
    }


}

// {"data":{"servers":[{"role_info":{"level":1,"name":"初学者","head_id":1},"port":12002,"ip":"192.168.2.166","name":"R2-志斌开发服","fast_login":0,"server_id":2002,"isUser":1,"status":3}],
// "page_num":1},"status":0,"msg":"success"}
[System.Serializable]
public class ServerListResponse
{
    public ServerData data;
    public int status;
    public string msg;
}
[System.Serializable]
public class ServerData
{
    public List<ServerInfo> servers;
    public int page_num;
    public int max_page_num;
    public int page_size;
}





