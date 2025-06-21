using System;
using System.Collections.Generic;
public enum ReturnCode
{
    Success = 1,
    Login_Pwd_Error = 2,
    Regist_Account_Repeat = 3,
    Regist_Account_Format_Error = 4,
    Regist_Pwd_Format_Error = 5,
    Login_Account_not_exist = 6
}

public class NetworkManager
{
    private static NetworkManager m_instance;
    public static NetworkManager Instance
    {
        get
        {
            if (m_instance == null) {
                m_instance = new NetworkManager();
            }
            return m_instance;
        }
    }
    //定义一个字典，存储服务器名字和对应的NetworkConnect对象
    private Dictionary<string, NetworkConnect> m_connectDict = new Dictionary<string, NetworkConnect>();
    //private JavadataConnect m_jsonConnect = new JavadataConnect();
    //定义一个列表，存储所有的服务器名字
    private List<string> m_serverNameList = new List<string>();
    private List<string> m_loginServerNameList = new List<string>();

    //定义一个NetworkConnect对象，用于临时存储
    private NetworkConnect m_tempConnect;

    public NetworkManager() {
       // JavaNetWorkManager.Instance.m_jsonConnect = m_jsonConnect;
    }

    public void RegisterLoginServer(string serverName) {
        m_loginServerNameList.Add(serverName);
    }

   // [BlackList]
    public bool isLoginServer(string serverName) {
        return m_loginServerNameList.Contains(serverName);
    }

    public bool IsConnected(string serverName) {
        if (!m_connectDict.ContainsKey(serverName) || m_connectDict[serverName] == null)
            return false;
        return m_connectDict[serverName].IsConnected;
    }

   // [BlackList]
    public void Connect(string serverName, string ip, int port, Action<NetworkConnect.ConnectError> connectCallback) {
        Logger.PrintDebug("BlackList @@@@Connect存在：Connect" + serverName);
        if (!m_connectDict.ContainsKey(serverName) || m_connectDict[serverName] == null) {
            m_connectDict[serverName] = new NetworkConnect(serverName);
            m_serverNameList.Add(serverName);
        }
        m_connectDict[serverName].Connect(ip, port, connectCallback);
        //m_jsonConnect.socket= m_connectDict[serverName].getCurrSocket();
        //m_jsonConnect.protoHandle = m_connectDict[serverName].getProtoHandler();


    }

    public void Connect(string serverName, string ip, int port, Action<int> connectCallback) {
        Logger.PrintDebug("@@@@Connect存在：Connect" + serverName);
        Action<NetworkConnect.ConnectError> action = (connectError) => {
            if (connectCallback != null)
                connectCallback((int)connectError);
        };
        Connect(serverName, ip, port, action);
        //m_jsonConnect.socket = m_connectDict[serverName].getCurrSocket();
        //m_jsonConnect.protoHandle = m_connectDict[serverName].getProtoHandler();
    }

    //  [BlackList]
    //向指定服务器发送重连请求
    public void Reconnect(string serverName, Action<NetworkConnect.ConnectError> connectCallback) {
        if (!m_connectDict.ContainsKey(serverName) || m_connectDict[serverName] == null) {
            Logger.PrintError("Reconnect的连接不存在：" + serverName);
            return;
        }
        m_connectDict[serverName].Reconnect(connectCallback);
    }

    //向指定服务器发送断开连接请求
    public void Disconnect(string serverName, bool isInitiative = true) {
        if (!m_connectDict.ContainsKey(serverName) || m_connectDict[serverName] == null)
            return;

        if (m_connectDict[serverName].IsConnected) {
            //Logger.PrintLog("主动断开连接");
            m_connectDict[serverName].Close(isInitiative);
        }
    }

    //向指定服务器发送消息
    public void SendMessage(string serverName, uint protoID, byte[] protoBytes) {
        Package package = new Package(protoID, (int)ReturnCode.Success, protoBytes);
        SendMessage(serverName, package);
    }

    //重载SendMessage方法，不需要消息体
    //向指定服务器发送消息
    public void SendMessage(string serverName, uint protoID) {
        Package package = new Package(protoID, (int)ReturnCode.Success, null);
        SendMessage(serverName, package);
    }

    //获取Bowling服务器的NetworkConnect对象
    public NetworkConnect getBowwlingServer() {
        string serverName = "bowling";
        if (!m_connectDict.ContainsKey(serverName) || m_connectDict[serverName] == null) {
            Logger.PrintError("SendMessage的连接不存在：" + serverName);
            return null;
        }
        return m_connectDict[serverName];
    }




    public void SendMessage(string serverName, Package package) {
        if (!m_connectDict.ContainsKey(serverName) || m_connectDict[serverName] == null) {
            Logger.PrintError("SendMessage的连接不存在：" + serverName);
            return;
        }
        m_connectDict[serverName].SendPackage(package);
    }
    //发送消息到Java
    //public void SendMsgToJavaMessage(string protoID, string jsonDaa) {
    //    JsonPackage package = new JsonPackage(protoID, (int)ReturnCode.Success, jsonDaa);
    //    m_jsonConnect.SendPackage(package);
    //}
    ////发送消息到Java
    //public void SendMsgToJavaMessage(JsonPackage package) {
    //    m_jsonConnect.SendPackage(package);
    //}
    //public void RecieveJavaDataPackage(string protoID, string jsonData) {
    //    m_jsonConnect.RecievePackage(protoID, jsonData);
    //}
    //[BlackList]
    //处理所有服务器的网络连接
    public void OnProcess() {
        //protobuf 网络联机
        //遍历所有服务器名字，处理对应的NetworkConnect对象
        int len = m_serverNameList.Count;
        for (int i = 0; i < len; ++i) {
            m_tempConnect = m_connectDict[m_serverNameList[i]];
            if (m_tempConnect != null)
                m_tempConnect.OnProcess();
        }
        //@@@后台java与unity 网络对接
        //if (m_jsonConnect != null) {
        //    m_jsonConnect.OnProcess();
        //}
    }

    //[BlackList]
    //在应用程序退出时关闭所有连接
    public void OnApplicationQuit() {
        //遍历所有服务器名字，关闭对应的NetworkConnect对象
        int len = m_serverNameList.Count;
        for (int i = 0; i < len; ++i) {
            m_tempConnect = m_connectDict[m_serverNameList[i]];
            if (m_tempConnect != null && m_tempConnect.IsConnected)
                m_tempConnect.Close(true);
        }
    }
}