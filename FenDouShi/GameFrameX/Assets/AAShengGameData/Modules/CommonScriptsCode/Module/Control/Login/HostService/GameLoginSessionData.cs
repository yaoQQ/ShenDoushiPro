using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GameLoginSessionData :Singleton<GameLoginSessionData>
{

    private LoginResponse _loginResponse;
    public ServerInfo currSerVerInfo { private set;get;}
    // ==================== 安全访问方法 ====================

    // 顶级字段访问
    public int Status => _loginResponse?.status ?? -1;
    public string Message => _loginResponse?.msg ?? string.Empty;

    // Data字段访问
    public string H5Version => _loginResponse?.data?.h5_version ?? string.Empty;
    public string UserId => _loginResponse?.data?.user_id ?? string.Empty;
    public string JsVersion => _loginResponse?.data?.js_version ?? string.Empty;
    public string ResVersion => _loginResponse?.data?.res_version ?? string.Empty;
    public int IsWhiteIP => _loginResponse?.data?.isWhiteIP ?? 0;
    public string ChannelId => _loginResponse?.data?.channel_id ?? string.Empty;
    public string Account => _loginResponse?.data?.account ?? string.Empty;
    public string Token => _loginResponse?.data?.token ?? string.Empty;
    public string ResCdn => _loginResponse?.data?.res_cdn ?? string.Empty;

    // DefaultServer字段访问
    public int ServerPort => currSerVerInfo.port;
    public string ServerIp => currSerVerInfo?.ip ?? string.Empty;
    public string ServerName => currSerVerInfo?.name ?? string.Empty;
    public int FastLogin => currSerVerInfo?.fast_login ?? 0;
    public int ServerId => currSerVerInfo?.server_id ?? 0;
    public int IsUser => currSerVerInfo?.isUser ?? 0;
    public int ServerStatus => currSerVerInfo?.status ?? 0;
    public void SetGameServerInfo(LoginResponse loginResponse)
    {
        this._loginResponse = loginResponse;
        currSerVerInfo = loginResponse?.data?.default_server;
    }
    //选择服务器后刷新现在的服务器数据
    public void ChangeServiceInfo(ServerInfo serverInfo)
    {
        currSerVerInfo = serverInfo;
    }

    //==================== 请求登入的服务器列表 ====================
    // 新增服务器列表缓存
    public List<ServerInfo> ServerList { private set; get; }
    public int TotalPages { private set; get; }
    public int CurrentPage { private set; get; }
    public int PageSize { private set; get; }

    public ServerListResponse ServerListResponse { private set; get; }
    public void cacheServerList(ServerListResponse serverListResponseData, int page)
    {
        ServerListResponse = serverListResponseData;
        if (page == 0|| page == -1)//只有page == 0和-1的时候才会刷新总页数
        {
            TotalPages = serverListResponseData.data.max_page_num;
            PageSize = serverListResponseData.data.page_size;
        }
        CurrentPage = serverListResponseData.data.page_num;
        ServerList = serverListResponseData.data.servers;
        //page=0---》TotalPages=1 ，PageSize=50 ，CurrentPage=0
        //page=0---》TotalPages=0 ，PageSize=0 ，CurrentPage=1
        // Debug.Log($"page={page} TotalPages={TotalPages} CurrentPage={CurrentPage} PageSize={PageSize} ServerList.Count={ServerList.Count}");
    }
    public void Dispose()
    {
        _loginResponse = null;
        ServerListResponse = null;
        ServerList = null;
        currSerVerInfo = null;
    }
   
}

//==================== 请求主服务器返回数据 ====================

[Serializable]
public class LoginResponse
{
    public ResponseData data;
    public int status;
    public string msg;

    [Serializable]
    public class ResponseData
    {
        public string h5_version;
        public string user_id;
        public object publish; // 由于是null，使用object类型
        public string js_version;
        public string res_version;
        public int isWhiteIP;
        public ServerInfo default_server;
        public string channel_id;
        public string account;
        public string token;
        public string res_cdn;
    }
}
// 服务器信息类

[Serializable]
public class ServerInfo
{
    public RoleInfo role_info;
    public int port;
    public string ip;
    public string name;
    public int fast_login;
    public int server_id;
    public int isUser;
    public int status;
    public string GetStatusText()
    {
        return status switch
        {
            1 => "[color=#333333]停服[/color]",
            2 => "[color=#888888]维护[/color]",
            3 => "[color=#00FF00]良好[/color]",
            4 => "[color=#990000]拥挤[/color]",
            5 => "[color=#FF0000]火爆[/color]",
            _ => "未知状态"
        };
    }
    public serverStatus GetServerStatus()
    {
        return (serverStatus)status;
    }
}
public enum serverStatus
{
    tingfu = 1,
    weihu = 2,
    lianghao = 3,
    yongjue = 4,
    huobao = 5,

    none,
}
// 玩家信息类

[Serializable]
public class RoleInfo
{
    public int level;
    public string name;
    public int head_id;
}
