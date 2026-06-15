#if !UNITY_EDITOR && UNITY_WEBGL
// 引入System命名空间
using System;

namespace UnityWebSocket
{
    // WebSocket类，实现了IWebSocket接口
    public class WebSocket : IWebSocket
    {
        // WebSocket连接地址
        public string Address { get; private set; }
        // 子协议数组
        public string[] SubProtocols { get; private set; }
        // WebSocket连接状态，通过WebSocketManager获取当前状态
        public WebSocketState ReadyState { get { return (WebSocketState)WebSocketManager.WebSocketGetState(instanceId); } }

        // 连接打开事件
        public event EventHandler<OpenEventArgs> OnOpen;
        // 连接关闭事件
        public event EventHandler<CloseEventArgs> OnClose;
        // 错误事件
        public event EventHandler<ErrorEventArgs> OnError;
        // 消息接收事件
        public event EventHandler<MessageEventArgs> OnMessage;

        // 内部使用的实例ID
        internal int instanceId = 0;

        // 构造函数，仅指定连接地址
        public WebSocket(string address)
        {
            // 设置连接地址
            this.Address = address;
            // 分配实例
            AllocateInstance();
        }

        // 构造函数，指定连接地址和单个子协议
        public WebSocket(string address, string subProtocol)
        {
            // 设置连接地址
            this.Address = address;
            // 设置子协议数组
            this.SubProtocols = new string[] { subProtocol };
            // 分配实例
            AllocateInstance();
        }

        // 构造函数，指定连接地址和子协议数组
        public WebSocket(string address, string[] subProtocols)
        {
            // 设置连接地址
            this.Address = address;
            // 设置子协议数组
            this.SubProtocols = subProtocols;
            // 分配实例
            AllocateInstance();
        }

        // 分配WebSocket实例的内部方法
        internal void AllocateInstance()
        {
            // 通过WebSocketManager分配实例并获取实例ID
            instanceId = WebSocketManager.AllocateInstance(this.Address);
            // 记录分配实例的日志
            Log($"Allocate socket with instanceId: {instanceId}");
            // 如果子协议数组为空，则直接返回
            if (this.SubProtocols == null) return;
            // 遍历子协议数组
            foreach (var protocol in this.SubProtocols)
            {
                // 如果子协议为空，则跳过本次循环
                if (string.IsNullOrEmpty(protocol)) continue;
                // 记录添加子协议的日志
                Log($"Add Sub Protocol {protocol}, with instanceId: {instanceId}");
                // 通过WebSocketManager添加子协议并获取返回码
                int code = WebSocketManager.WebSocketAddSubProtocol(instanceId, protocol);
                // 如果返回码小于0，表示添加失败
                if (code < 0)
                {
                    // 处理错误事件
                    HandleOnError(GetErrorMessageFromCode(code));
                    // 跳出循环
                    break;
                }
            }
        }

        // 析构函数，释放WebSocket实例
        ~WebSocket()
        {
            // 记录释放实例的日志
            Log($"Free socket with instanceId: {instanceId}");
            // 通过WebSocketManager释放实例
            WebSocketManager.WebSocketFree(instanceId);
        }

        // 异步连接方法
        public void ConnectAsync()
        {
            // 记录连接的日志
            Log($"Connect with instanceId: {instanceId}");
            // 将当前实例添加到WebSocketManager中
            WebSocketManager.Add(this);
            // 通过WebSocketManager发起连接并获取返回码
            int code = WebSocketManager.WebSocketConnect(instanceId);
            // 如果返回码小于0，表示连接失败
            if (code < 0) HandleOnError(GetErrorMessageFromCode(code));
        }

        // 异步关闭连接方法
        public void CloseAsync()
        {
            // 记录关闭连接的日志
            Log($"Close with instanceId: {instanceId}");
            // 通过WebSocketManager关闭连接并获取返回码
            int code = WebSocketManager.WebSocketClose(instanceId, (int)CloseStatusCode.Normal, "Normal Closure");
            // 如果返回码小于0，表示关闭失败
            if (code < 0) HandleOnError(GetErrorMessageFromCode(code));
        }

        // 异步发送文本消息方法
        public void SendAsync(string text)
        {
            // 记录发送文本消息的日志
            Log($"Send, type: {Opcode.Text}, size: {text.Length}");
            // 通过WebSocketManager发送文本消息并获取返回码
            int code = WebSocketManager.WebSocketSendStr(instanceId, text);
            // 如果返回码小于0，表示发送失败
            if (code < 0) HandleOnError(GetErrorMessageFromCode(code));
        }

        // 异步发送二进制数据方法
        public void SendAsync(byte[] data)
        {
            // 记录发送二进制数据的日志
            Log($"Send, type: {Opcode.Binary}, size: {data.Length}");
            // 通过WebSocketManager发送二进制数据并获取返回码
            int code = WebSocketManager.WebSocketSend(instanceId, data, data.Length);
            // 如果返回码小于0，表示发送失败
            if (code < 0) HandleOnError(GetErrorMessageFromCode(code));
        }

        // 处理连接打开事件的内部方法
        internal void HandleOnOpen()
        {
            // 记录连接打开的日志
            Log("OnOpen");
            // 触发连接打开事件
            OnOpen?.Invoke(this, new OpenEventArgs());
        }

        // 处理接收到二进制消息的内部方法
        internal void HandleOnMessage(byte[] rawData)
        {
            // 记录接收到二进制消息的日志
            Log($"OnMessage, type: {Opcode.Binary}, size: {rawData.Length}");
            // 触发消息接收事件
            OnMessage?.Invoke(this, new MessageEventArgs(Opcode.Binary, rawData));
        }

        // 处理接收到文本消息的内部方法
        internal void HandleOnMessageStr(string data)
        {
            // 记录接收到文本消息的日志
            Log($"OnMessage, type: {Opcode.Text}, size: {data.Length}");
            // 触发消息接收事件
            OnMessage?.Invoke(this, new MessageEventArgs(Opcode.Text, data));
        }

        // 处理连接关闭事件的内部方法
        internal void HandleOnClose(ushort code, string reason)
        {
            // 记录连接关闭的日志
            Log($"OnClose, code: {code}, reason: {reason}");
            // 触发连接关闭事件
            OnClose?.Invoke(this, new CloseEventArgs(code, reason));
            // 从WebSocketManager中移除当前实例
            WebSocketManager.Remove(instanceId);
        }

        // 处理错误事件的内部方法
        internal void HandleOnError(string msg)
        {
            // 记录错误日志
            Log("OnError, error: " + msg);
            // 触发错误事件
            OnError?.Invoke(this, new ErrorEventArgs(msg));
        }

        // 根据错误码获取错误信息的内部静态方法
        internal static string GetErrorMessageFromCode(int errorCode)
        {
            // 根据错误码返回对应的错误信息
            switch (errorCode)
            {
                case -1: return "WebSocket实例未找到。";
                case -2: return "WebSocket已连接或正在连接中。";
                case -3: return "WebSocket未连接。";
                case -4: return "WebSocket正在关闭中。";
                case -5: return "WebSocket已关闭。";
                case -6: return "WebSocket未处于打开状态。";
                case -7: return "无法关闭WebSocket，指定了无效的代码或原因过长。";
                case -8: return "不支持缓冲区切片。 ";
                default: return $"未知错误码 {errorCode}。";
            }
        }

        // 条件编译的日志方法，仅在定义了UNITY_WEB_SOCKET_LOG时生效
        [System.Diagnostics.Conditional("UNITY_WEB_SOCKET_LOG")]
        static void Log(string msg)
        {
            // 获取当前时间
            var time = DateTime.Now.ToString("HH:mm:ss.fff");
            // 记录日志
            UnityEngine.Debug.Log($"[{time}][UnityWebSocket] {msg}");
        }
    }
}
#endif
