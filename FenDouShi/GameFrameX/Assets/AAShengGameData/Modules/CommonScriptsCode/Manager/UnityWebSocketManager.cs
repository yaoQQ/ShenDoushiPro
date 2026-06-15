using System;
using System.Collections.Generic;
using UnityWebSocket;
public delegate void WebMessageReceive(uint protoID, byte[] info);
public class UnityWebSocketManager : Singleton<UnityWebSocketManager>
{
    // 定义WebSocket连接地址，默认使用wss://echo.websocket.events
    private string _address = "wss://echo.websocket.events";
    // 定义要发送的文本消息，默认值为"Hello UnityWebSocket!"
    public string sendText = "Hello UnityWebSocket!";
    // 定义一个只读的 WebSocket 列表，用于存储所有的 WebSocket 实例
    //定义一个字典，存储服务器名字和对应的NetworkConnect对象
    private Dictionary<string, WebSocket> m_connectDict = new Dictionary<string, WebSocket>();
    protected Dictionary<uint, WebMessageReceive> dicEventHandler = new Dictionary<uint, WebMessageReceive>();
    // 定义一个WebSocket对象
    private WebSocket socket;
    public void Init()
    {

    }
    public void Connect(string address)
    {
        if (m_connectDict.ContainsKey(address))
        {
            Logger.PrintError($"已经存在连接address：{address}");
            return;
        }
        _address = address;
        // 创建WebSocket对象，并设置连接地址
        // 创建一个新的WebSocket对象
        socket = new WebSocket(address);
        // 注册连接成功事件处理函数
        socket.OnOpen += Socket_OnOpen;
        // 注册接收到消息事件处理函数
        socket.OnMessage += Socket_OnMessage;
        // 注册连接关闭事件处理函数
        socket.OnClose += Socket_OnClose;
        // 注册错误事件处理函数
        socket.OnError += Socket_OnError;
        m_connectDict[address] = socket;
        // 添加连接中的日志信息
        Logger.PrintDebug(string.Format("Connecting..."));
        // 异步发起连接
        socket.ConnectAsync();

    }

    // 从列表中移除一个 WebSocket 实例
    public void Remove(WebSocket socket)
    {
        // 如果列表中包含该 WebSocket 实例，则移除
        if (m_connectDict.ContainsKey(socket.Address))
        {
            m_connectDict.Remove(socket.Address);
        }
    }
    // 每帧更新时调用
    public void OnProcess()
    {
        // 遍历所有 WebSocket 实例，调用其 Update 方法
        foreach (var item in m_connectDict)
        {
            item.Value.Update();
        }
    }
    public void SendAsync(uint protoID, byte[] pDatabuff)
    {
     
        List<byte> all = new List<byte>();
        byte[] header = BitConverter.GetBytes((uint)protoID); // 消息类型
        all.AddRange(header);
        all.AddRange(pDatabuff);
   
        if (protoID != 101005)
        {
            Logger.PrintColor("white", $"发送消息： protoID={protoID} 总长度.length={all.Count}");
        }
        socket.SendAsync(all.ToArray());
    }
    public void RegisterEventHandler(uint protoID, WebMessageReceive handler)
    {
        Logger.PrintLog("注册 协议Id===>>>  ", protoID.ToString());
        if (dicEventHandler.ContainsKey(protoID))
        {
            dicEventHandler[protoID] += handler;
        }
        else
        {
            dicEventHandler[protoID] = handler;
        }
    }
    public void RemoveEventHandler(uint protoID, WebMessageReceive handler)
    {
        if (dicEventHandler.ContainsKey(protoID))
        {
            dicEventHandler[protoID] -= handler;
        }
    }
    public void InvokeCallBack(uint protoID, byte[] ByteArray)
    {
        if (dicEventHandler.ContainsKey(protoID))
        {
            if (dicEventHandler[protoID] != null)
            {
                dicEventHandler[protoID].Invoke(protoID, ByteArray);
            }
            else
            {
                Logger.PrintDebug(protoID + " 无监听");
            }
        }
        else
        {
            Logger.PrintDebug(protoID + " 无监听");
        }
    }

    public bool ContainsKey(uint protoID)
    {
        return dicEventHandler.ContainsKey(protoID);
    }

    public int Count { get { return this.dicEventHandler.Count; } }


    protected void reset()
    {
        this.dicEventHandler.Clear();
    }

    // WebSocket连接成功时调用的事件处理函数
    private void Socket_OnOpen(object sender, OpenEventArgs e)
    {
        Logger.PrintDebug("Socket_OnOpen()");
        EventManager.Instance.Dispatch(EEventType.Login_connect_Success);
        LoginControl.Instance.LoginComplete();
    }


    // 接收到WebSocket消息时调用的事件处理函数
    private  void Socket_OnMessage(object sender, MessageEventArgs e)
    {
       // Logger.PrintDebug("Socket_OnMessage()");
        // 如果接收到的是二进制消息
        if (e.IsBinary)
        {
            // 添加接收二进制消息的日志信息
            uint protoID = BitConverter.ToUInt32(GetProtoIDBuff(e.RawData), 0);
            if (protoID != 101006)
            {
                Logger.PrintColor("yellow", $"收取协议---->protoID={protoID}");
            }
            byte[] protoData = GetProtoDataBuff(e.RawData);
            InvokeCallBack(protoID, protoData);
        }
        // 如果接收到的是文本消息
        else if (e.IsText)
        {
            // 添加接收文本消息的日志信息
            Logger.PrintColor("yellow", string.Format("Receive: {0}", e.Data));
        }
    }
    // 获取协议ID缓存
    byte[] GetProtoIDBuff(byte[] body)
    {
        byte[] target = new byte[4];
        for (int i = 0; i < 4; i++)
        {
            target[i] = body[i];
        }
        return target;
    }
    // 获取协议数据缓存
    byte[] GetProtoDataBuff(byte[] body)
    {
        byte[] target = new byte[body.Length - 4];
        for (int i = 4; i < body.Length; i++)
        {
            target[i - 4] = body[i];
        }
        return target;
    }
    // WebSocket连接关闭时调用的事件处理函数
    private  void Socket_OnClose(object sender, CloseEventArgs e)
    {
        if (e.webSocket != null)
        {
            if (m_connectDict.ContainsKey(e.webSocket.Address))
            {
                m_connectDict.Remove(e.webSocket.Address);
            }
        }
        // 添加连接关闭的日志信息，包含状态码和关闭原因
        Logger.PrintDebug(string.Format("Closed: StatusCode: {0}, Reason: {1}", e.StatusCode, e.Reason));
    }

    // WebSocket发生错误时调用的事件处理函数
    private  void Socket_OnError(object sender, ErrorEventArgs e)
    {
        if(e.webSocket != null)
        {
            if (m_connectDict.ContainsKey(e.webSocket.Address))
            {
                m_connectDict.Remove(e.webSocket.Address);
            }
        }
        // 添加错误日志信息
        Logger.PrintError(string.Format("Error: {0}", e.Message));
    }

    public void CloseWebSocket()
    {
        socket.CloseAsync();
    }

    // 中止所有 WebSocket 连接的方法
    private void SocketAbort()
    {
        // 遍历所有 WebSocket 实例，调用其 Abort 方法
        foreach (var item in m_connectDict)
        {
            item.Value.Abort();
        }
    }
    private void SocketDispose()
    {
        // 遍历所有 WebSocket 实例，调用其 Abort 方法
        List<string> keys = new List<string>(m_connectDict.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            m_connectDict.Remove(keys[i]);
        }
        m_connectDict.Clear();
    }
    /// <summary>
    /// 主动断开连接，并从连接字典中移除
    /// </summary>
    public void Dispose()
    {
        if (socket != null)
        {
            //关闭当前socket。并发送完全部数据
            socket.CloseAsync();
            socket = null;
        }
        SocketAbort();
        SocketDispose();
        EventManager.Instance.Dispatch(EEventType.Login_connect_Out,"断开连接");
        LoginControl.Instance.OnLoginOut();
    }
}