#if !NET_LEGACY && (UNITY_EDITOR || !UNITY_WEBGL)
using System;
using System.Collections.Concurrent;// 引入 System.Collections.Concurrent 命名空间，用于处理线程安全的集合类
using System.IO;// 引入 System.IO 命名空间，用于文件和流的输入输出操作
using System.Net.WebSockets;// 引入 System.Net.WebSockets 命名空间，用于处理 WebSocket 相关操作
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnityWebSocket
{
    public class WebSocket : IWebSocket
    {
        // 获取 WebSocket 的地址，只读属性
        public string Address { get; private set; }
        // 获取 WebSocket 使用的子协议数组，只读属性
        public string[] SubProtocols { get; private set; }

        // 获取 WebSocket 的当前状态
        public WebSocketState ReadyState
        {
            get
            {
                // 如果 socket 为空，返回已关闭状态
                if (socket == null)
                    return WebSocketState.Closed;
                // 根据 socket 的状态返回对应的 WebSocketState
                switch (socket.State)
                {
                    case System.Net.WebSockets.WebSocketState.Closed:
                    case System.Net.WebSockets.WebSocketState.None:
                        return WebSocketState.Closed;
                    case System.Net.WebSockets.WebSocketState.CloseReceived:
                    case System.Net.WebSockets.WebSocketState.CloseSent:
                        return WebSocketState.Closing;
                    case System.Net.WebSockets.WebSocketState.Connecting:
                        return WebSocketState.Connecting;
                    case System.Net.WebSockets.WebSocketState.Open:
                        return WebSocketState.Open;
                }
                // 默认返回已关闭状态
                return WebSocketState.Closed;
            }
        }

        // 定义打开连接事件
        public event EventHandler<OpenEventArgs> OnOpen;
        // 定义关闭连接事件
        public event EventHandler<CloseEventArgs> OnClose;
        // 定义错误事件
        public event EventHandler<ErrorEventArgs> OnError;
        // 定义消息接收事件
        public event EventHandler<MessageEventArgs> OnMessage;

        // 客户端 WebSocket 实例
        private ClientWebSocket socket;
        // 判断 WebSocket 是否处于打开状态
        private bool isOpening => socket != null && socket.State == System.Net.WebSockets.WebSocketState.Open;
        // 发送队列，用于存储待发送的数据缓冲区
        private ConcurrentQueue<SendBuffer> sendQueue = new ConcurrentQueue<SendBuffer>();
        // 事件队列，用于存储各种事件参数
        private ConcurrentQueue<EventArgs> eventQueue = new ConcurrentQueue<EventArgs>();
        // 标记是否正在处理关闭操作
        private bool closeProcessing;
        // 取消令牌源，用于取消异步操作
        private CancellationTokenSource cts = null;

        #region APIs 
        // 构造函数，初始化 WebSocket 地址
        public WebSocket(string address)
        {
            this.Address = address;
        }

        // 构造函数，初始化 WebSocket 地址和单个子协议
        public WebSocket(string address, string subProtocol)
        {
            this.Address = address;
            this.SubProtocols = new string[] { subProtocol };
        }

        // 构造函数，初始化 WebSocket 地址和子协议数组
        public WebSocket(string address, string[] subProtocols)
        {
            this.Address = address;
            this.SubProtocols = subProtocols;
        }

        // 异步连接到 WebSocket 服务器
        public void ConnectAsync()
        {
            // 如果 socket 已存在，抛出错误并返回
            if (socket != null)
            {
                HandleError(new Exception("Socket is busy."));
                return;
            }

            // 创建新的客户端 WebSocket 实例
            socket = new ClientWebSocket();
            // 创建取消令牌源
            cts = new CancellationTokenSource();

            // 支持子协议
            if (this.SubProtocols != null)
            {
                // 遍历子协议数组
                foreach (var protocol in this.SubProtocols)
                {
                    // 如果子协议为空，跳过
                    if (string.IsNullOrEmpty(protocol)) continue;
                    // 记录添加子协议的日志
                    Log($"Add Sub Protocol {protocol}");
                    // 将子协议添加到 WebSocket 选项中
                    socket.Options.AddSubProtocol(protocol);
                }
            }

            // 启动连接任务
            Task.Run(ConnectTask);
        }

        // 异步关闭 WebSocket 连接
        public void CloseAsync()
        {
            // 如果 WebSocket 未打开，直接返回
            if (!isOpening) return;
            // 标记开始处理关闭操作
            closeProcessing = true;
        }

        // 异步发送二进制数据
        public void SendAsync(byte[] data)
        {
            // 如果 WebSocket 未打开，直接返回
            if (!isOpening)
            {
                Logger.PrintError($"WebSocket 未打开，直接返回socket.State={socket.State}");
                return;
            }
            // 创建发送缓冲区
            var buffer = new SendBuffer(data, WebSocketMessageType.Binary);
            // 将缓冲区添加到发送队列中
            sendQueue.Enqueue(buffer);
        }

        // 异步发送文本数据
        public void SendAsync(string text)
        {
            // 如果 WebSocket 未打开，直接返回
            if (!isOpening)
            {
                Logger.PrintError("WebSocket 未打开，直接返回");
                return;
            }
            // 将文本转换为 UTF-8 编码的字节数组
            var data = Encoding.UTF8.GetBytes(text);
            // 创建发送缓冲区
            var buffer = new SendBuffer(data, WebSocketMessageType.Text);
            // 将缓冲区添加到发送队列中
            sendQueue.Enqueue(buffer);
        }
        #endregion

        // 定义发送缓冲区类
        class SendBuffer
        {
            // 待发送的数据
            public byte[] data;
            // 数据类型
            public WebSocketMessageType type;
            // 构造函数，初始化数据和类型
            public SendBuffer(byte[] data, WebSocketMessageType type)
            {
                this.data = data;
                this.type = type;
            }
        }

        // 清空发送队列
        private void CleanSendQueue()
        {
            // 循环从队列中取出元素，直到队列为空
            while (sendQueue.TryDequeue(out var _)) ;
        }

        // 清空事件队列
        private void CleanEventQueue()
        {
            // 循环从队列中取出元素，直到队列为空
            while (eventQueue.TryDequeue(out var _)) ;
        }

        // 连接任务，用于异步连接到 WebSocket 服务器
        private async Task ConnectTask()
        {
            // 记录连接任务开始日志
            Log("Connect Task Begin ...");

            try
            {
                // 创建 WebSocket 地址的 Uri 实例
                var uri = new Uri(Address);
                // 异步连接到 WebSocket 服务器
                await socket.ConnectAsync(uri, cts.Token);
            }
            catch (Exception e)
            {
                // 处理连接错误
                HandleError(e);
                // 处理连接关闭
                HandleClose((ushort)CloseStatusCode.Abnormal, e.Message);
                return;
            }

            // 处理连接打开事件
            HandleOpen();

            // 记录连接任务成功日志
            Log("Connect Task Success !");

            // 启动接收任务
            StartReceiveTask();
            // 启动发送任务
            StartSendTask();
        }

        // 启动发送任务，用于异步发送数据
        private async void StartSendTask()
        {
            // 记录发送任务开始日志
            Log("Send Task Begin ...");

            try
            {
                // 当未处理关闭操作且 socket 和取消令牌源有效时，持续发送数据
                Logger.PrintDebug($"closeProcessing={closeProcessing} socket={socket} cts={cts}");
                while (!closeProcessing && socket != null && cts != null && !cts.IsCancellationRequested)
                {
                   // Logger.PrintDebug($"sendQueue.Count={sendQueue.Count} closeProcessing={closeProcessing}");
                    // 当未处理关闭操作且发送队列中有数据时，从队列中取出数据并发送
                    while (!closeProcessing && sendQueue.Count > 0 && sendQueue.TryDequeue(out var buffer))
                    {
                        // 记录发送数据的日志
                    //    Logger.PrintColor("red",$"Send, type: {buffer.type}, size: {buffer.data.Length}, queue left: {sendQueue.Count}");
                        // 异步发送数据
                        await socket.SendAsync(new ArraySegment<byte>(buffer.data), buffer.type, true, cts.Token);
                    }
                    // 线程休眠 3 毫秒
                    Thread.Sleep(3);
                }
                // 如果正在处理关闭操作且 socket 和取消令牌源有效，关闭发送输出
                if (closeProcessing && socket != null && cts != null && !cts.IsCancellationRequested)
                {
                    // 清空发送队列
                    CleanSendQueue();
                    // 记录开始关闭发送的日志
                    Log($"Close Send Begin ...");
                    // 异步关闭发送输出
                    await socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Normal Closure", cts.Token);
                    // 记录关闭发送成功的日志
                    Log($"Close Send Success !");
                }
            }
            catch (Exception e)
            {
                // 处理发送错误
                HandleError(e);
            }
            finally
            {
                // 标记关闭操作处理完成
                closeProcessing = false;
            }

            // 记录发送任务结束日志
            Log("Send Task End !");
        }

        // 启动接收任务，用于异步接收数据
        private async void StartReceiveTask()
        {
            // 记录接收任务开始日志
            Logger.PrintDebug("Receive Task Begin ...");

            // 关闭原因
            string closeReason = "";
            // 关闭代码
            ushort closeCode = 0;
            // 标记是否已关闭
            bool isClosed = false;
            // 接收缓冲区
            var segment = new ArraySegment<byte>(new byte[8192]);
            // 内存流，用于存储接收到的数据
            var ms = new MemoryStream();

            try
            {
                Logger.PrintDebug($"Receive Task Begin ...isClosed={isClosed} cts.IsCancellationRequested={cts.IsCancellationRequested}");
                // 当未关闭且取消令牌未请求取消时，持续接收数据
                while (!isClosed && !cts.IsCancellationRequested)
                {
                   // Logger.PrintDebug($"StartReceiveTask() closeProcessing={isClosed} cts.IsCancellationRequested={cts.IsCancellationRequested} cts.Token={cts.Token}");
                    // 异步接收数据
                    var result = await socket.ReceiveAsync(segment, cts.Token);
                  //  Logger.PrintColor("yellow", $"socket.ReceiveAsync result={result.Count} result.EndOfMessage={result.EndOfMessage}");
                    // 将接收到的数据写入内存流
                    ms.Write(segment.Array, 0, result.Count);
                    // 如果不是消息结束，继续接收
                    if (!result.EndOfMessage) continue;
                    // 获取内存流中的数据
                    var data = ms.ToArray();
                    // 重置内存流长度
                    ms.SetLength(0);
                    // 根据消息类型处理数据
                    switch (result.MessageType)
                    {
                        case WebSocketMessageType.Binary:
                            // 处理二进制消息
                            HandleMessage(Opcode.Binary, data);
                            break;
                        case WebSocketMessageType.Text:
                            // 处理文本消息
                            HandleMessage(Opcode.Text, data);
                            break;
                        case WebSocketMessageType.Close:
                            // 标记已关闭
                            isClosed = true;
                            // 获取关闭代码
                            closeCode = (ushort)result.CloseStatus;
                            // 获取关闭原因
                            closeReason = result.CloseStatusDescription;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                // 处理接收错误
                HandleError(e);
                // 设置异常关闭代码
                closeCode = (ushort)CloseStatusCode.Abnormal;
                // 设置关闭原因
                closeReason = e.Message;
            }
            finally
            {
                // 关闭内存流
                ms.Close();
            }

            // 处理关闭事件
            HandleClose(closeCode, closeReason);

            // 记录接收任务结束日志
            Log("Receive Task End !");
        }

        // 释放 WebSocket 资源
        public void SocketDispose()
        {
            // 记录释放资源日志
            Log("Dispose");
            // 从管理器中移除当前 WebSocket 实例
            UnityWebSocketManager.Instance.Remove(this);
            // 清空发送队列
            CleanSendQueue();
            // 清空事件队列
            CleanEventQueue();
            // 释放 WebSocket 资源
            socket.Dispose();
            // 将 socket 置为 null
            socket = null;
            // 释放取消令牌源
            cts.Dispose();
            // 将取消令牌源置为 null
            cts = null;
        }

        // 处理连接打开事件
        private void HandleOpen()
        {
            // 记录连接打开日志
            Log("OnOpen");
            // 将打开事件参数添加到事件队列中
            eventQueue.Enqueue(new OpenEventArgs());
        }

        // 处理接收到的消息
        private void HandleMessage(Opcode opcode, byte[] rawData)
        {
            // 记录接收到消息的日志
            //Logger.PrintColor("yellow",$"@@@@@@HandleMessage(), type: {opcode}, size: {rawData.Length}");
            // 将消息事件参数添加到事件队列中
            eventQueue.Enqueue(new MessageEventArgs(opcode, rawData));
        }

        // 处理连接关闭事件
        private void HandleClose(ushort code, string reason)
        {
            // 记录连接关闭日志
            Log($"OnClose, code: {code}, reason: {reason}");
            // 将关闭事件参数添加到事件队列中
            eventQueue.Enqueue(new CloseEventArgs(code, reason, this));
        }

        // 处理错误事件
        private void HandleError(Exception exception)
        {
            // 记录错误日志
            Log("OnError, error: " + exception.Message);
            // 将错误事件参数添加到事件队列中
            eventQueue.Enqueue(new ErrorEventArgs(exception.Message,this));
        }

        // 更新方法，用于处理事件队列中的事件
        internal void Update()
        {
           // Logger.PrintDebug($"webSocket Update() eventQueue.Count={eventQueue.Count}");
            // 当事件队列中有事件时，取出事件并处理
            while (eventQueue.Count > 0 && eventQueue.TryDequeue(out var e))
            {
            //    Logger.PrintDebug($"webSocket Update() eventQueue.Count={eventQueue.Count} e={e}");
                if (e is CloseEventArgs)
                {
                    // 触发关闭事件
                    OnClose?.Invoke(this, e as CloseEventArgs);
                    // 释放 WebSocket 资源
                    SocketDispose();
                    break;
                }
                else if (e is OpenEventArgs)
                {
                    // 触发打开事件
                    OnOpen?.Invoke(this, e as OpenEventArgs);
                }
                else if (e is MessageEventArgs)
                {
                 //   Logger.PrintColor("blue", "WebSocket Update() 触发消息事件");
                    // 触发消息事件
                    OnMessage?.Invoke(this, e as MessageEventArgs);
                }
                else if (e is ErrorEventArgs)
                {
                    // 触发错误事件
                    OnError?.Invoke(this, e as ErrorEventArgs);
                }
            }
        }

        // 中止 WebSocket 操作
        internal void Abort()
        {
            // 记录中止操作日志
            Log("Abort");
            // 如果取消令牌源存在，请求取消操作
            if (cts != null)
            {
                cts.Cancel();
            }
        }

        // 条件编译指令，仅在定义了 UNITY_WEB_SOCKET_LOG 时编译该方法
        [System.Diagnostics.Conditional("UNITY_WEB_SOCKET_LOG")]
        // 日志记录方法
        static void Log(string msg)
        {
            // 获取当前时间
            var time = DateTime.Now.ToString("HH:mm:ss.fff");
            // 获取当前线程 ID
            var thread = Thread.CurrentThread.ManagedThreadId;
            // 输出日志信息
            Logger.PrintDebug($"[{time}][UnityWebSocket][T-{thread:D3}] {msg}");
        }
    }
}
#endif
