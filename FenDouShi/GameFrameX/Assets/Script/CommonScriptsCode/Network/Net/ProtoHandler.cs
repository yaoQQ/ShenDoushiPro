using System;
using System.Net.Sockets;
using UnityEngine;
/// <summary>
/// 这段代码是一个网络协议的处理函数。
/// 它通过读取数据流的头和体来解析协议。
/// 当数据流的长度大于等于头的长度时，它会读取头的数据并解析出体的长度。
/// 然后，它会创建一个新的体缓冲区，
/// 并将数据流中的剩余数据读入该缓冲区。
/// 当体缓冲区的长度等于体的长度时，
/// 它会解析出协议ID和协议数据，并调用OnMessageReceived事件。
/// 这段代码还包括一些辅助函数，如GetProtoIDBuff和GetProtoDataBuff，
/// 用于从体缓冲区中提取协议ID和协议数据。
/// </summary>
public class ProtoHandler
{
    private enum ProtoState
    {
        head,
        body,
        close,
    }

    private NetworkConnect m_connect;
    private Socket socket;
    private BufferObj bObj = new BufferObj();

    private ProtoState protoState = ProtoState.head;

    private const int headLength = 4;

    private uint protoID;
    //private int returnCodeID=1;
     
    private byte[] bodyBuffer;
    private int bodyLength;
    private int bodyOffset = 0;


    private byte[] headBuffer = new byte[headLength];
    private int headOffset = 0;



    private bool isSending = false;
    private bool isReceiving = false;
    private SocketAsyncEventArgs socketReceiveAsyncEventArgs;
    private SocketAsyncEventArgs socketSendAsyncEventArgs;
    MessageReceive OnMessageReceived;

    public ProtoHandler(NetworkConnect connect, Socket socket, MessageReceive delegateMessageReceived)
    {
        m_connect = connect;
        this.socket = socket;
        OnMessageReceived = delegateMessageReceived;
    }

    public void Start()
    {
        //SocketAsyncEventArgs同一时间只能进行一个操作，通过Completed来确认当前操作是否完成，如果同步完成是不会触该事件需要自己手动调用处理。
        headBuffer = new byte[headLength];
        socketReceiveAsyncEventArgs = new SocketAsyncEventArgs();
        socketReceiveAsyncEventArgs.SetBuffer(bObj.buffer, 0, bObj.buffer.Length);
        socketReceiveAsyncEventArgs.UserToken = bObj;
        socketReceiveAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(ReceiveCallback);

        socketSendAsyncEventArgs = new SocketAsyncEventArgs();
        socketSendAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(SendCallback);

        Receive();
   
    }

    void Close()
    {
        protoState = ProtoState.close;

        socketReceiveAsyncEventArgs.Dispose();
        socketSendAsyncEventArgs.Dispose();
        headBuffer = null;
    }

    public bool Send(byte[] data)
    {
        try
        {
            isSending = true;
            socketSendAsyncEventArgs.SetBuffer(data, 0, data.Length);
            bool sendSuceed = socket.SendAsync(socketSendAsyncEventArgs);// (data, 0, data.Length, SocketFlags.None, new AsyncCallback (SendCallback), socket);
            if (!sendSuceed)
            {
                Logger.PrintWarning("发送错误");
                return false;
            }
        }
        catch (Exception e)
        {
            Logger.PrintError("发送错误:" + e.Message);
            m_connect.Close(false);
            m_connect.DispatchSocketError();
            return false;
        }
        return true;
    }

    void SendCallback(object sender, SocketAsyncEventArgs e)
    {
        //		int length = socket.EndSend (asyncSend);
        isSending = false;
        if (e.SocketError == SocketError.Success)
            Logger.PrintLog("发送成功");
        else
            Logger.PrintLog("SendCallback:" + e.SocketError);
    }

    void Receive()
    {
        isReceiving = true;
        bool succeed = socket.ReceiveAsync(socketReceiveAsyncEventArgs);// (bObj.buffer, 0, bObj.buffer.Length, SocketFlags.None, new AsyncCallback (ReceiveCallback), bObj);
       // Logger.PrintColor("blue", "Receive() bObj.buffer.Length socketReceiveAsyncEventArgs=" + socketReceiveAsyncEventArgs);
      //  Logger.PrintColor("blue", "Receive() succeed=" + succeed);
        if (!succeed)
        {
            Logger.PrintWarning("接收错误");
        }
    }

    void ReceiveCallback(object sender, SocketAsyncEventArgs e)
    {
        if (e.SocketError == SocketError.Success)
        {
            if (sender is Socket)
            {
                if (!m_connect.isCurSocket(sender as Socket))
                    return;
            }
            int length = e.BytesTransferred;
            if (length == 0)
            {
                //服务器要求断开
                PrintLog(UtilMethod.ConnectStrs(m_connect.serverName, " 服务器要求断开"));
                m_connect.Close(false);
                if (!NetworkManager.Instance.isLoginServer(m_connect.serverName))
                    m_connect.DispatchSocketError();
                return;
            }
            //Debug.LogError("length   " + length);
            //Debug.LogError("bObj.buffer.Length   " + bObj.buffer.Length);
       //     Logger.PrintColor("blue", " bObj.buffer.Length=" + bObj.buffer.Length);
            if (length > 0 && length <= bObj.buffer.Length)
            {
                BufferObj bObj = e.UserToken as BufferObj;
                HandleData(bObj.buffer, 0, length);
                isReceiving = false;
                if (protoState != ProtoState.close)
                {
                   
                    // Receive();
                 //  JavaNetWorkManager.Instance.accept(bObj.buffer);
                    Receive();
                }
            }
            else
            {
                PrintError(UtilMethod.ConnectStrs(m_connect.serverName, " 接收数据长度有问题"));
            }
        }
        else if (e.SocketError == SocketError.Interrupted ||
            e.SocketError == SocketError.NotSocket ||
            e.SocketError == SocketError.ConnectionAborted ||
            e.SocketError == SocketError.NotConnected)
        {
            //连接已断开
            PrintLog(UtilMethod.ConnectStrs(m_connect.serverName, " 连接已断开"));
            if (m_connect.isInitiativeClose)
                m_connect.isInitiativeClose = false;
            else
            {
                m_connect.Close(false);
                m_connect.DispatchSocketError();
            }
        }
        else
        {
            PrintError(UtilMethod.ConnectStrs(m_connect.serverName, " Disconnect:", e.SocketError.ToString()));
            m_connect.Close(false);
            if (!NetworkManager.Instance.isLoginServer(m_connect.serverName))
                m_connect.DispatchSocketError();
        }
    }
   
    private void PrintLog(string msg)
    {
        ThreadManager.RunMainThread(() =>
        {
            Logger.PrintLog(msg);
        });
    }

    private void PrintError(string msg)
    {
        ThreadManager.RunMainThread(() =>
        {
            Logger.PrintError(msg);
        });
    }
    // 处理接收到的数据
    void HandleData(byte[] data, int offset, int validLen)
    {
        // 如果偏移量大于等于有效长度，直接返回
        if (offset >= validLen) return;
        // 如果当前状态为头部
        if (protoState == ProtoState.head)
        {
            // 读取头部
            ReadHead(data, offset, validLen);
        }
        else if (protoState == ProtoState.body)
        {
            // 如果当前状态为数据体
            ReadBody(data, offset, validLen);
        }
      
    }

    // 读取头部
    void ReadHead(byte[] data, int offset, int validLen)
    {
     //   Logger.PrintColor("blue", "ReadHead() offset=" + offset);
      //  Logger.PrintColor("blue", "ReadHead() validLen=" + validLen);
        //还没读到的数据长度
        int length = validLen - offset;

        //头需要的长度
        int needLen = headLength - headOffset;

        // 读取长度
        int readLen = Mathf.Min(needLen, length);
        for (int i = 0; i < readLen; i++)
        {
            headBuffer[headOffset + i] = data[offset + i];
        }
        headOffset += readLen;
        offset += readLen;

        // 如果头部已经读完
        if (headOffset == headLength)
        {
            // 读取数据体长度
            bodyLength = BitConverter.ToInt32(headBuffer, 0);
            //  int len = getByteLen(headBuffer);
            // int len = BitConverter.ToInt32(headBuffer, 0);
            // bodyLength = len;
            //ulong len3 = BitConverter.ToUInt64(headBuffer, 0);
            //ushort len4 = BitConverter.ToUInt16(headBuffer, 0);
            //   long len5 = BitConverter.ToInt64(headBuffer, 0);
            //头已读完

            // 初始化数据体缓存
            headOffset = 0;
            bodyOffset = 0;
            bodyBuffer = new byte[bodyLength];
            this.protoState = ProtoState.body;

            // 处理数据
            HandleData(data, offset, validLen);
        }
    }


    // 读取数据体
    void ReadBody(byte[] data, int offset, int validLen)
    {
      //  Logger.PrintColor("blue", "ReadBody() offset=" + offset);
     //   Logger.PrintColor("blue", "ReadBody() validLen=" + validLen);
        //还没读到的数据长度
        int length = validLen - offset;

        //body需要的长度
        int needLen = bodyLength - bodyOffset;
        // 读取长度
        int readLen = Mathf.Min(needLen, length);

        for (int i = 0; i < readLen; i++)
        {
            bodyBuffer[bodyOffset + i] = data[offset + i];
        }
        bodyOffset += readLen;
        offset += readLen;

        // 如果数据体已经读完
        if (bodyOffset == bodyLength)
        {
            // 解析协议ID和数据
            protoID = BitConverter.ToUInt16(GetProtoIDBuff(bodyBuffer), 0);
              byte[] protoData = GetProtoDataBuff(bodyBuffer);
            OnMessageReceived.Invoke(protoID, protoData);
            // Logger.PrintColor("blue", "ReadBody() OnMessageReceived.Invoke  protoData.Length=" + data.Length);
            //        JavaNetWorkManager.Instance.accept(bodyBuffer);
            // 初始化头部缓存
            headOffset = 0;
            bodyOffset = 0;
            this.protoState = ProtoState.head;
            // 处理数据
            HandleData(data, offset, validLen);
        }
    }

    // 获取协议ID缓存
    byte[] GetProtoIDBuff(byte[] body)
    {
        byte[] target = new byte[2];
        for (int i = 0; i < 2; i++)
        {
            target[i] = body[i];
        }
        return target;
    }

    // 获取协议数据缓存
    byte[] GetProtoDataBuff(byte[] body)
    {
        byte[] target = new byte[body.Length-2];
        for (int i = 2; i < body.Length; i++)
        {
            target[i-2] = body[i];
        }
        return target;
    }



}
